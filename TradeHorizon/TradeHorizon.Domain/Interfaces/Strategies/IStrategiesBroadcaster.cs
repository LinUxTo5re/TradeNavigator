namespace TradeHorizon.Domain.Interfaces.Strategies
{
    public interface IStrategiesBroadcaster
    {
        Task BroadcastBreakoutStrategyAsync(BreakoutDirection direction, decimal? price, long timestamp);
    }
}