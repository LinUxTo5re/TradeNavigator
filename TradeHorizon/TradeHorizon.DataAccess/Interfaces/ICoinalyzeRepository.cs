using TradeHorizon.Domain;


namespace TradeHorizon.DataAccess.Interfaces
{
    public interface ICoinalyzeRepository
    {
        Task<List<OHLCVData>> GetOHLCVDataAsync(string symbol, string interval, long from, long to);
    }
}
