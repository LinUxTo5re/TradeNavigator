using TradeHorizon.Domain.Interfaces.Websockets;
using TradeHorizon.Domain.Constants;

namespace TradeHorizon.Business.Services.Websocket
{
    public class GateTickerService : IGateTickerProcessor
    {
        private readonly IGateTickerBroadcaster _broadcaster;
        public WebSocketMessage? webSocketMessage = null;
        public GateTickerService(IGateTickerBroadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public async Task ProcessAsync(string rawMessage)
        {
            if(!string.IsNullOrEmpty(rawMessage))
            {
                webSocketMessage = WebSocketMessageDeserializer.DeserializeWithResultData<TickerModel>(rawMessage);
                await _broadcaster.BroadcastToGroupAsync(SignalRConstants.TickerGroupWS, SignalRConstants.ReceiveTickerWS, webSocketMessage);
            }
        }
    }
}
