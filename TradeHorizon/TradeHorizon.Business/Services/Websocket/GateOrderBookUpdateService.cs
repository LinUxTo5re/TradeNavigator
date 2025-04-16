using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.Business.Services.Websocket
{
    public class GateOrderBookUpdateService : IGateOrderBookUpdateProcessor
    {
        private readonly IGateOrderBookUpdateBroadcaster _broadcaster;

        public GateOrderBookUpdateService(IGateOrderBookUpdateBroadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public async Task ProcessAsync(string rawMessage)
        {
            var processed = $"[P-Liq-Orders] {rawMessage}";
            await _broadcaster.BroadcastToGroupAsync("ProcessedOrderBookUpdateGroup", "ProcessedOrderBookUpdateData", processed);
        }
    }
}
