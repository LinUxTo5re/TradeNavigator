namespace TradeHorizon.Domain.Interfaces.RestAPI
{
    public interface IGateioBroadcaster
    {
        Task BroadcastOHLCVAsync(object? data);
        Task BroadcastFundingRateAsync(object? data);
        Task BroadcastContractStatsAsync(object? data);
        Task BroadcastOrderBookAsync(object? data);
        Task BroadcastLiqOrdersAsync(object? data);
    }
}