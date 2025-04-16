using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.Business.Services.Websocket
{
    public class GatePublicLiquidatesService : IGatePublicLiquidatesProcessor
    {
        private readonly IGatePublicLiquidatesBroadcaster _broadcaster;

        public GatePublicLiquidatesService(IGatePublicLiquidatesBroadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public async Task ProcessAsync(string rawMessage)
        {
            var processed = $"[P-Liq-Orders] {rawMessage}";
            await _broadcaster.BroadcastToGroupAsync("ProcessedGatePublicLiquidatesGroup", "ProcessedGatePublicLiquidatesData", processed);
        }
    }
}
