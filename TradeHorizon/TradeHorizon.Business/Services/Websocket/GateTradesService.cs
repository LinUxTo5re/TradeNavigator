using TradeHorizon.Domain.Websockets.Interfaces;
using System.Text.Json;

namespace TradeHorizon.Business.Services.Websocket
{
    public class GateTradesService : IGateTradesProcessor
    {
        private readonly IGateTradesBroadcaster _broadcaster;
        public WebSocketMessage? webSocketMessage = null;

        public GateTradesService(IGateTradesBroadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public async Task ProcessAsync(string rawMessage)
        {
            if(!string.IsNullOrEmpty(rawMessage))
            {
                webSocketMessage = WebSocketMessageDeserializer.DeserializeWithResultData<TradesModel>(rawMessage);
                //string json = JsonSerializer.Serialize(webSocketMessage);
                await _broadcaster.BroadcastToGroupAsync("ProcessedGateTradesGroup", "ProcessedGateTradesData", webSocketMessage);
            }
        }
    }
}
