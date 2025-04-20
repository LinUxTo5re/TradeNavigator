using TradeHorizon.Domain.Interfaces.Websockets;

namespace TradeHorizon.DataAccess.Repositories.Websocket
{
    public class GateTradeClient : IGateTradesClient
    {
        private readonly IGateTradesProcessor _processor;
        private readonly IWebSocketClient _webClient;

        public GateTradeClient(IGateTradesProcessor processor, IWebSocketClient webClient)
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