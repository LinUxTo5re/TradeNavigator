using TradeHorizon.Domain.Interfaces.Websockets;

namespace TradeHorizon.API.Hubs
{
    public class GateContractStatsHub : BaseWebSocketHub<IGateContractStatsClient>
    {
        public GateContractStatsHub(IGateContractStatsClient client) : base(client) { }
    }
}