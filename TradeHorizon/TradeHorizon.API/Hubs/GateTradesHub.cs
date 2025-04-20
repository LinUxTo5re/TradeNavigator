using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.API.Hubs
{
    public class GateTradesHub : BaseWebSocketHub<IGateTradesClient>
    {
        public GateTradesHub(IGateTradesClient client) : base(client) { }
    }
}
