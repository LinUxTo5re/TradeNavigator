using TradeHorizon.Domain.Interfaces.Websockets;
using TradeHorizon.Domain.Constants;

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
                await _broadcaster.BroadcastToGroupAsync(SignalRConstants.OrderBookUpdateGroupWS, SignalRConstants.ReceiveOrderBookUpdateWS, webSocketMessage);
            }
        }
    }
}
