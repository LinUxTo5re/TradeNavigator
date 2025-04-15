using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.API.Hubs
{
    public class GateTickerHub : BaseWebSocketHub<IGateTickerClient>
    {
        public GateTickerHub(IGateTickerClient client) : base(client) { }
    }
}