using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.Business.Services.Websocket
{
    public class GateContractStatsService : IGateContractStatsProcessor
    {
        private readonly IGateContractStatsBroadcaster _broadcaster;

        public GateContractStatsService(IGateContractStatsBroadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public async Task ProcessAsync(string rawMessage)
        {
            var processed = $"[ContractStats] {rawMessage}";
            await _broadcaster.BroadcastToGroupAsync("ProcessedGateContractStatsGroup", "ProcessedGateContractStatsData", processed);
        }
    }
}
