using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using TradeHorizon.Domain.Interfaces.Websockets;
using TradeHorizon.Domain.Constants;
using System.Text.Json;

public class GateWebSocketClient : IWebSocketClient
{
    private readonly IWebSocketProcessor _processor;
    private readonly string _uri;
    private readonly ConcurrentDictionary<string, ClientWebSocket> _connections;

    public GateWebSocketClient(IWebSocketProcessor processor)
    {
        _processor = processor;
        _uri = WsConstants.GateIoBaseWsUrl;
        _connections = new ConcurrentDictionary<string, ClientWebSocket>();
    }

    public async Task StartClientAsync<T>(string userInput, T processor, CancellationToken cancellationToken)
    {
        List<string> keys = [];
        var cleanedUserInput = GenerateKeysFromUserInput(userInput, out keys);

        if (keys.Any(k => _connections.ContainsKey(k)) || cleanedUserInput is null)
            return;

        var socket = new ClientWebSocket();
        await socket.ConnectAsync(new Uri(_uri), cancellationToken);

        var allAdded = true;
        foreach (var k in keys)
        {
            if (!_connections.TryAdd(k, socket))
                allAdded = false;
        }

        if (!allAdded)
        {
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Duplicate connection detected", cancellationToken);
            return;
        }

        var messageBytes = Encoding.UTF8.GetBytes(cleanedUserInput);
        await socket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, cancellationToken);

        _ = Task.Run(async () =>
        {
            var buffer = new byte[4096];
            try
            {
                while (!cancellationToken.IsCancellationRequested && socket.State == WebSocketState.Open)
                {
                    var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
                    if (result.MessageType == WebSocketMessageType.Close)
                        break;

                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    await _processor.ProcessAsync(message);
                }
            }
            finally
            {
                foreach (var k in keys)
                    _connections.TryRemove(k, out _);

                if (socket.State == WebSocketState.Open || socket.State == WebSocketState.CloseReceived)
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed", CancellationToken.None);
                socket.Dispose();
            }
        }, cancellationToken);
    }

    private string? GenerateKeysFromUserInput(string userInput, out List<string> keys)
    {
        keys = [];
        try
        {
            string contract = string.Empty;
            string timeframe = string.Empty;
            string frequency = string.Empty;
            var modifiedJson = new Dictionary<string, JsonElement>();

            using var doc = JsonDocument.Parse(userInput);
            var channel = doc.RootElement.GetProperty("channel").GetString();

            if (doc.RootElement.TryGetProperty("contract", out var contractProp))// contains single contract
            {
                contract = contractProp.GetString() ?? string.Empty;

                timeframe = doc.RootElement.TryGetProperty("timeframe", out var tf) ? tf.GetString() ?? string.Empty : string.Empty;
                frequency = doc.RootElement.TryGetProperty("frequency", out var fr) ? fr.GetString() ?? string.Empty : string.Empty;

                string key = timeframe == string.Empty ? frequency : timeframe;

                keys.Add($"{channel}-{contract}:{key}");

                modifiedJson = doc.RootElement
                    .EnumerateObject()
                    .Where(p => p.Name != "contract" && p.Name != "timeframe")
                    .ToDictionary(p => p.Name, p => p.Value);
            }
            else if (doc.RootElement.TryGetProperty("payload", out var payloadProp) && payloadProp.ValueKind == JsonValueKind.Array) // contains N number of contracts/timeframes
            {
                foreach (var contractItem in payloadProp.EnumerateArray())
                {
                    var contractName = contractItem.GetString();
                    if (!string.IsNullOrEmpty(contractName))
                    {
                        keys.Add($"{channel}-{contractName}");
                    }
                }
            }

            return modifiedJson.Count > 0 ? JsonSerializer.Serialize(modifiedJson) : userInput;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"got error {ex.ToString()}");
            return null;
        }
    }
}
