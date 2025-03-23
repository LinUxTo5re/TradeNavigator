using TradeHorizon.Domain;

namespace TradeHorizon.Application.Interfaces
{
    public interface ICoinalyzeRepository
    {
        Task<List<OHLCVData>> GetOHLCVDataAsync(string symbol, string interval, long from, long to);
    }
}
