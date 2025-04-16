using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.DataAccess.Repositories.Websocket
{
    public class GateOrderBookUpdateClient : IGateOrderBookUpdateClient
    {
        private readonly IGateOrderBookUpdateProcessor _processor;
        private readonly string _uri;
        private readonly IWebSocketClient _webClient;

        public GateOrderBookUpdateClient(IGateOrderBookUpdateProcessor processor, IWebSocketClient webClient, string uri)
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