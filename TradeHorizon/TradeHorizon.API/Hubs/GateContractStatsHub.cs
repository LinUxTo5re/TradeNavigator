using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.API.Hubs
{
    public class GateContractStatsHub : BaseWebSocketHub<IGateContractStatsClient>
    {
        public GateContractStatsHub(IGateContractStatsClient client) : base(client) { }
    }
}