using TradeHorizon.Domain.Interfaces.Strategies;
using Microsoft.AspNetCore.SignalR;
using TradeHorizon.API.Hubs.Strategies;
using TradeHorizon.Domain.Constants;

public class StrategiesBroadcaster : IStrategiesBroadcaster
{
    private readonly IHubContext<BreakoutStrategyHub> _breakoutStrategyHubContext;
    private readonly IHubContext<MomentumStrategyHub> _momentumStrategyHubContext;

    public StrategiesBroadcaster(IHubContext<BreakoutStrategyHub> BreakoutStrategyHubContext, IHubContext<MomentumStrategyHub> MomentumStrategyHubContext)
    {
        _breakoutStrategyHubContext = BreakoutStrategyHubContext;
        _momentumStrategyHubContext = MomentumStrategyHubContext;
    }
    public async Task BroadcastBreakoutStrategyAsync(string contract, BreakoutDirection direction, decimal? price, long timestamp)
    {
        var data = new Dictionary<string, object>
        {
            {"Contract", contract.ToString()},
            { "Direction", direction.ToString() },
            { "Price", price ?? 0 },
            { "Timestamp", timestamp }
        };
        await _breakoutStrategyHubContext.Clients.Group(SignalRConstants.BreakoutStrategyGroup).SendAsync(SignalRConstants.ReceiveBreakoutStrategy, data);
    }
    public async Task BroadcastMomentumStrategyAsync(string contract, MomentumDirection direction, decimal? price, long timestamp)
    {
        var data = new Dictionary<string, object>
        {
            {"Contract", contract.ToString()},
            { "Direction", direction.ToString() },
            { "Price", price ?? 0 },
            { "Timestamp", timestamp }
        };
        await _momentumStrategyHubContext.Clients.Group(SignalRConstants.MomentumStrategyGroup).SendAsync(SignalRConstants.ReceiveMomentumStrategy, data);
    }
}