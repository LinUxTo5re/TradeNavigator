using TradeHorizon.Domain.Websockets.Interfaces;

public class GateTickerService : IGateTickerProcessor
{
    private readonly ISignalRBroadcaster _broadcaster;

    public GateTickerService(ISignalRBroadcaster broadcaster)
    {
        _broadcaster = broadcaster;
    }

    public async Task ProcessAsync(string rawMessage)
    {
        var processed = $"[PROCESSED] {rawMessage}";
        // Broadcast to API group
        await _broadcaster.BroadcastToGroupAsync("ProcessedGateTickerGroup","ProcessedGateTickerData", processed);
    }
}
