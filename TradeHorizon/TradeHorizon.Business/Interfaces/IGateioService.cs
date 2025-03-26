using TradeHorizon.Domain;

namespace TradeHorizon.Business.Interfaces
{
    public interface IGateioService
    {
        Task<List<OHLCVTgateIoData>> GetOHLCVAsync(string contract, Int64? from, Int64? to, int? limit, string interval);
    }
}