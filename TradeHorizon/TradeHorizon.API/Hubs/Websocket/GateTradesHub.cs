using TradeHorizon.Domain.Interfaces.Websockets;

namespace TradeHorizon.API.Hubs
{
    public class GateTradesHub : BaseWebSocketHub<IGateTradesClient>
    {
        public GateTradesHub(IGateTradesClient client) : base(client) { }
    }
}
