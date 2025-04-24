namespace TradeHorizon.Domain.Interfaces.Strategies
{
    public interface IStrategiesBroadcaster
    {
        Task BroadcastBreakoutStrategyAsync(string contract, BreakoutDirection direction, decimal? price, long timestamp);
    }
}