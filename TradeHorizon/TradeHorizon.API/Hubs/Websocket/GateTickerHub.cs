using TradeHorizon.Domain.Interfaces.Websockets;

namespace TradeHorizon.API.Hubs
{
    public class GateTickerHub : BaseWebSocketHub<IGateTickerClient>
    {
        public GateTickerHub(IGateTickerClient client) : base(client) { }
    }
}