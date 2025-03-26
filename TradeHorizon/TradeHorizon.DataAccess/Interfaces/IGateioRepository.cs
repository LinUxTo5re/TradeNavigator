using TradeHorizon.Domain;

namespace TradeHorizon.DataAccess.Interfaces
{
    public interface IGateioRepository
    {
        Task<string> GetResponseTextAsync(string url);
        Task<string> GetOHLCVAsync(string contract, Int64? from, Int64? to, int? limit, string interval);
    }
}