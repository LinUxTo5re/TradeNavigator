using TradeHorizon.Domain.Interfaces.Websockets;

namespace TradeHorizon.API.Hubs
{
    public class GatePublicLiquidatesHub : BaseWebSocketHub<IGatePublicLiquidatesClient>
    {
        public GatePublicLiquidatesHub(IGatePublicLiquidatesClient client) : base(client) { }
    }
}