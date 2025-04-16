using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.DataAccess.Repositories.Websocket
{
    public class GateCandlesticksClient : IGateCandlestickClient
    {
        private readonly IGateCandlesticksProcessor _processor;
        private readonly string _uri;
        private readonly IWebSocketClient _webClient;

        public GateCandlesticksClient(IGateCandlesticksProcessor processor, IWebSocketClient webClient, string uri)
        {
            _processor = processor;
            _webClient = webClient;
            _uri = uri;
        }
        public async Task StartClientAsync<T>(string userInput, T processor, CancellationToken cancellationToken)
        {
           await _webClient.StartClientAsync(userInput, _processor, cancellationToken);
        }
    }
}