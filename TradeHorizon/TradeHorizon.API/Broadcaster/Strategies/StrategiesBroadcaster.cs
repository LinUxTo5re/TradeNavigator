using TradeHorizon.Domain.Interfaces.Strategies;
using Microsoft.AspNetCore.SignalR;
using TradeHorizon.API.Hubs.Strategies;
using TradeHorizon.Domain.Constants;

public class StrategiesBroadcaster : IStrategiesBroadcaster
{
    private readonly IHubContext<BreakoutStrategyHub> _breakoutStrategyHubContext;

    public StrategiesBroadcaster(IHubContext<BreakoutStrategyHub> BreakoutStrategyHubContext)
    {
        _breakoutStrategyHubContext = BreakoutStrategyHubContext;
    }
    public async Task BroadcastBreakoutStrategyAsync(BreakoutDirection direction, decimal? price, long timestamp)
    {
        var data = new Dictionary<string, object>
        {
            { "Direction", direction.ToString() },
            { "Price", price ?? 0 },
            { "Timestamp", timestamp }
        };
        await _breakoutStrategyHubContext.Clients.Group(SignalRConstants.BreakoutStrategyGroup).SendAsync(SignalRConstants.ReceiveBreakoutStrategy, data);
    }
}