using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.Business.Services.Websocket
{
    public class GateTradesService : IGateTradesProcessor
    {
        private readonly IGateTradesBroadcaster _broadcaster;

        public GateTradesService(IGateTradesBroadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public async Task ProcessAsync(string rawMessage)
        {
            var processed = $"[TRADES] {rawMessage}";
            await _broadcaster.BroadcastToGroupAsync("ProcessedGateTradesGroup", "ProcessedGateTradesData", processed);
        }
    }
}
