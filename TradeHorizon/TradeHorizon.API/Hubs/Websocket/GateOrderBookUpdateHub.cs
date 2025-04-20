using TradeHorizon.Domain.Interfaces.Websockets;

namespace TradeHorizon.API.Hubs
{
    public class GateOrderBookUpdateHub : BaseWebSocketHub<IGateOrderBookUpdateClient>
    {
        public GateOrderBookUpdateHub(IGateOrderBookUpdateClient client) : base(client) { }
    }
}