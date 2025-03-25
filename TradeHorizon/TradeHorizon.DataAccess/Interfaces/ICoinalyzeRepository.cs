using TradeHorizon.Domain;


namespace TradeHorizon.DataAccess.Interfaces
{
    public interface ICoinalyzeRepository
    {
        Task<string> GetCurrentOpenInterestAsync(string symbols);
        Task<string> GetHistoricalOpenInterestAsync(string symbols, string interval, Int64 from, Int64 to, string convert_to_usd);
        Task<string> GetCurrentFundingRateAsync(string symbols, bool isPredicted);
        Task<string> GetHistoricalFundingRateAsync(string symbols, string interval, Int64 from, Int64 to, bool isPredicted);
        Task<string> GetResponseTextAsync(string url);
        Task<string> GetLiquidationHistoryAsync(string symbols, string interval, Int64 from, Int64 to, string convert_to_usd);
        Task<string> GetLongShortRatioHistoryAsync(string symbols, string interval, Int64 from, Int64 to);
    }
}
