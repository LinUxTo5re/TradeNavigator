using System.Net.WebSockets;
using System.Text;
using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.DataAccess.Repositories.Websocket
{
public class GateioTickerClient : IGateTickerClient
{
    private readonly IGateTickerProcessor _processor;

    public GateioTickerClient(IGateTickerProcessor processor)
    {
        _processor = processor;
    }

    public async Task StartClientAsync(string userInput, CancellationToken cancellationToken)
    {
        var socket = new ClientWebSocket();
        await socket.ConnectAsync(new Uri("wss://fx-ws-testnet.gateio.ws/v4/ws/usdt"), cancellationToken);

        var subscribeMessage = userInput;
        var messageBytes = Encoding.UTF8.GetBytes(subscribeMessage);
        await socket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, cancellationToken);

        var buffer = new byte[4096];
        while (!cancellationToken.IsCancellationRequested)
        {
            var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
            if (result.MessageType == WebSocketMessageType.Close) break;

            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            await _processor.ProcessAsync(message);
        }
    }
}

}
