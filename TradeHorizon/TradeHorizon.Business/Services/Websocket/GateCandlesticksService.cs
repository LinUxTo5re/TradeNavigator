using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.Business.Services.Websocket
{
    public class GateCandlesticksService : IGateCandlesticksProcessor
    {
        private readonly IGateCandlesticksBroadcaster _broadcaster;

        public GateCandlesticksService(IGateCandlesticksBroadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public async Task ProcessAsync(string rawMessage)
        {
            var processed = $"[CANDLESTICKS] {rawMessage}";
            await _broadcaster.BroadcastToGroupAsync("ProcessedGateCandlestickGroup", "ProcessedGateCandlestickData", processed);
        }
    }
}
