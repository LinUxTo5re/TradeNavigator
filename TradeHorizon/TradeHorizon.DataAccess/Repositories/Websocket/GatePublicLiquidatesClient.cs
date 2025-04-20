using TradeHorizon.Domain.Interfaces.Websockets;

namespace TradeHorizon.DataAccess.Repositories.Websocket
{
    public class GatePublicLiquidatesClient : IGatePublicLiquidatesClient
    {
        private readonly IGatePublicLiquidatesProcessor _processor;
        private readonly IWebSocketClient _webClient;

        public GatePublicLiquidatesClient(IGatePublicLiquidatesProcessor processor, IWebSocketClient webClient)
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