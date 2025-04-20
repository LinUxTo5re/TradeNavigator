
namespace TradeHorizon.Business.Interfaces.RestAPI
{
    public interface IGateioService
    {
        Task<List<OHLCVModel>> GetOHLCVHistAsync(string contract, Int64? from, Int64? to, int? limit, string interval);
        Task<List<FundingRateModel>> GetFundingRateHistAsync(string contract, Int64? from, Int64? to, int? limit);
        Task<List<ContractStatsModel>> GetContractStatsAsync(string contract, Int64? from, string? interval, int? limit);
        Task<OrderBookModel> GetOrderBookAsync(string contract, string? interval, int? limit, bool? with_id);
        Task<List<LiquidationOrderModel>> GetLiqOrdersAsync(string? contract, Int64? from, Int64? to, int? limit);

    }
}