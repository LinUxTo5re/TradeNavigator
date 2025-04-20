using TradeHorizon.Domain.Interfaces.Websockets;

namespace TradeHorizon.DataAccess.Repositories.Websocket
{
    public class GateCandlesticksClient : IGateCandlestickClient
    {
        private readonly IGateCandlesticksProcessor _processor;
        private readonly IWebSocketClient _webClient;

        public GateCandlesticksClient(IGateCandlesticksProcessor processor, IWebSocketClient webClient)
        {
            _processor = processor;
            _webClient = webClient;
        }
        public async Task StartClientAsync<T>(string userInput, T processor, CancellationToken cancellationToken)
        {
           await _webClient.StartClientAsync(userInput, _processor, cancellationToken);
        }
    }
}