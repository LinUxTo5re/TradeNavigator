using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.API.Hubs
{
    public class GateCandlesticksHub : BaseWebSocketHub<IGateCandlestickClient>
    {
        public GateCandlesticksHub(IGateCandlestickClient client) : base(client) { }
    }
}