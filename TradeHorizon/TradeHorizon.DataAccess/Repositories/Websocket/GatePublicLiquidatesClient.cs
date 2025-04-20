using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.DataAccess.Repositories.Websocket
{
    public class GatePublicLiquidatesClient : IGatePublicLiquidatesClient
    {
        private readonly IGatePublicLiquidatesProcessor _processor;
        private readonly string _uri;
        private readonly IWebSocketClient _webClient;

        public GatePublicLiquidatesClient(IGatePublicLiquidatesProcessor processor, IWebSocketClient webClient, string uri)
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