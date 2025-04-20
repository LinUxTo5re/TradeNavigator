using TradeHorizon.Domain.Interfaces.Websockets;
using TradeHorizon.Domain.Constants;

namespace TradeHorizon.Business.Services.Websocket
{
    public class GateCandlesticksService : IGateCandlesticksProcessor
    {
        private readonly IGateCandlesticksBroadcaster _broadcaster;
        public WebSocketMessage? webSocketMessage = null;
        public GateCandlesticksService(IGateCandlesticksBroadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public async Task ProcessAsync(string rawMessage)
        {
            if(!string.IsNullOrEmpty(rawMessage))
            {
                webSocketMessage = WebSocketMessageDeserializer.DeserializeWithResultData<CandlestickModel>(rawMessage);
                // string json = JsonSerializer.Serialize(webSocketMessage);
                await _broadcaster.BroadcastToGroupAsync(SignalRConstants.CandlestickGroupWS, SignalRConstants.ReceiveCandlestickData, webSocketMessage);
            }
        }
    }
}
