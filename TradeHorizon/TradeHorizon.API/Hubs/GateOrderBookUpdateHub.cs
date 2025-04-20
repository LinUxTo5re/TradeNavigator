using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.API.Hubs
{
    public class GateOrderBookUpdateHub : BaseWebSocketHub<IGateOrderBookUpdateClient>
    {
        public GateOrderBookUpdateHub(IGateOrderBookUpdateClient client) : base(client) { }
    }
}