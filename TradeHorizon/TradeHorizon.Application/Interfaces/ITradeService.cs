using TradeHorizon.Domain;

namespace TradeHorizon.Application.Interfaces
{
    public interface ITradeService
    {
        Task<List<OHLCVData>> GetOHLCVDataAsync(string symbol, string interval, long from, long to);
    }
}
