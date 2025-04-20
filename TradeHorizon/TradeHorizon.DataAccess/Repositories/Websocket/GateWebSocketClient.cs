using System.Net.WebSockets;
using System.Text;
using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.DataAccess.Repositories.Websocket
{
    public class GateWebSocketClient : IWebSocketClient
    {
        private readonly IWebSocketProcessor _processor;
        private readonly string _uri;

        public GateWebSocketClient(IWebSocketProcessor processor, string uri)
        {
            _processor = processor;
            _uri = uri;
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