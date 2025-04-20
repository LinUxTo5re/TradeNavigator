using TradeHorizon.Domain.Websockets.Interfaces;
using System.Text.Json;

namespace TradeHorizon.Business.Services.Websocket
{
    public class GateContractStatsService : IGateContractStatsProcessor
    {
        private readonly IGateContractStatsBroadcaster _broadcaster;
        public WebSocketMessage? webSocketMessage = null;
        public GateContractStatsService(IGateContractStatsBroadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public async Task ProcessAsync(string rawMessage)
        {
            if(!string.IsNullOrEmpty(rawMessage))
            {
                webSocketMessage = WebSocketMessageDeserializer.DeserializeWithResultData<ContractStatModel>(rawMessage);
                // string json = JsonSerializer.Serialize(webSocketMessage);
                await _broadcaster.BroadcastToGroupAsync("ProcessedGateContractStatsGroup", "ProcessedGateContractStatsData", webSocketMessage);
            }
        }
    }
}
