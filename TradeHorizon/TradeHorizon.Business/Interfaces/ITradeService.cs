using TradeHorizon.Domain;

namespace TradeHorizon.Business.Interfaces
{
    public interface ITradeService
    {
        Task<List<OHLCVData>> GetOHLCVDataAsync(string symbol, string interval, long from, long to);
    }
}
