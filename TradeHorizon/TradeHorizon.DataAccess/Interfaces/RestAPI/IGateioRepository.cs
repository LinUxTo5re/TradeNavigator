
namespace TradeHorizon.DataAccess.Interfaces.RestAPI
{
    public interface IGateioRepository
    {
        Task<string> GetResponseTextAsync(string url);
        Task<string> GetOHLCVHistAsync(string contract, Int64? from, Int64? to, int? limit, string interval);
        Task<string> GetFundingRateHistAsync(string contract, Int64? from, Int64? to, int? limit);
        Task<string> GetContractStatsAsync(string contract, Int64? from, string? interval, int? limit);
        Task<string> GetOrderBookAsync(string contract, string? interval, int? limit, bool? with_id);
        Task<string> GetLiqOrdersAsync(string? contract, long? from, long? to, int? limit);

    }
}