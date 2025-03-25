using TradeHorizon.Domain;

namespace TradeHorizon.Business.Interfaces
{
    public interface ICoinalyzeService
    {
        Task<CurrentOI> GetCurrentOpenInterestAsync(string symbols);
        Task<List<OHLCVData>> GetHistoricalOpenInterestAsync(string symbols, string interval, Int64 from, Int64 to, string convert_to_usd);
        Task<CurrentFundingRate> GetCurrentFundingRateAsync(string symbols, bool isPredicted);
        Task<List<OHLCVData>> GetHistoricalFundingRateAsync(string symbols, string interval, Int64 from, Int64 to, bool isPredicted);
    }
}
