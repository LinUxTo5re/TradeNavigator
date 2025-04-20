using TradeHorizon.Domain.Websockets.Interfaces;
using System.Text.Json;

namespace TradeHorizon.Business.Services.Websocket
{
    public class GateOrderBookUpdateService : IGateOrderBookUpdateProcessor
    {
        private readonly IGateOrderBookUpdateBroadcaster _broadcaster;
        public WebSocketMessage? webSocketMessage = null;

        public GateOrderBookUpdateService(IGateOrderBookUpdateBroadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public async Task ProcessAsync(string rawMessage)
        {
            if(!string.IsNullOrEmpty(rawMessage))
            {
                webSocketMessage = WebSocketMessageDeserializer.DeserializeWithResultData<OrderBookUpdateModel>(rawMessage);
                // string json = JsonSerializer.Serialize(webSocketMessage);
                await _broadcaster.BroadcastToGroupAsync("ProcessedOrderBookUpdateGroup", "ProcessedOrderBookUpdateData", webSocketMessage);
            }
        }
    }
}
