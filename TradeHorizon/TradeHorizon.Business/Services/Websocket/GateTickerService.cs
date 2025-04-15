using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.Business.Services.Websocket
{
    public class GateTickerService : IGateTickerProcessor
    {
        private readonly IGateTickerBroadcaster _broadcaster;

        public GateTickerService(IGateTickerBroadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public async Task ProcessAsync(string rawMessage)
        {
            var processed = $"[PROCESSED] {rawMessage}";
            await _broadcaster.BroadcastToGroupAsync("ProcessedGateTickerGroup", "ProcessedGateTickerData", processed);
        }
    }
}
