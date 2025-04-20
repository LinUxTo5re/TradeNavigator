using TradeHorizon.Domain.Websockets.Interfaces;
using System.Text.Json;

namespace TradeHorizon.Business.Services.Websocket
{
    public class GatePublicLiquidatesService : IGatePublicLiquidatesProcessor
    {
        private readonly IGatePublicLiquidatesBroadcaster _broadcaster;
        public WebSocketMessage? webSocketMessage = null;
        public GatePublicLiquidatesService(IGatePublicLiquidatesBroadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public async Task ProcessAsync(string rawMessage)
        {
            if(!string.IsNullOrEmpty(rawMessage))
            {
                webSocketMessage = WebSocketMessageDeserializer.DeserializeWithResultData<PublicLiqOrdersModel>(rawMessage);
                // string json = JsonSerializer.Serialize(webSocketMessage);
                await _broadcaster.BroadcastToGroupAsync("ProcessedGatePublicLiquidatesGroup", "ProcessedGatePublicLiquidatesData", webSocketMessage);
            }
        }
    }
}
