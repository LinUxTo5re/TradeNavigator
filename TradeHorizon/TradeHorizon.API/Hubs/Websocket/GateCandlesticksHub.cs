using TradeHorizon.Domain.Interfaces.Websockets;

namespace TradeHorizon.API.Hubs
{
    public class GateCandlesticksHub : BaseWebSocketHub<IGateCandlestickClient>
    {
        public GateCandlesticksHub(IGateCandlestickClient client) : base(client) { }
    }
}