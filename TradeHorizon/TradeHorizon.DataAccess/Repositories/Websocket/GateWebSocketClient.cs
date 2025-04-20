using System.Net.WebSockets;
using System.Text;
using TradeHorizon.Domain.Interfaces.Websockets;
using TradeHorizon.Domain.Constants;

namespace TradeHorizon.DataAccess.Repositories.Websocket
{
    public class GateWebSocketClient : IWebSocketClient
    {
        private readonly IWebSocketProcessor _processor;
        private readonly string _uri;

        public GateWebSocketClient(IWebSocketProcessor processor)
        {
            _processor = processor;
            _uri = WsConstants.GateIoBaseWsUrl;
        }

        public async Task StartClientAsync<T>(string userInput, T processor, CancellationToken cancellationToken)
        {
            var socket = new ClientWebSocket();
            await socket.ConnectAsync(new Uri(_uri), cancellationToken);

            var messageBytes = Encoding.UTF8.GetBytes(userInput);
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