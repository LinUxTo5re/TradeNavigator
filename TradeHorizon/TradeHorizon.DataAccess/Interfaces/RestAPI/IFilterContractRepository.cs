
namespace TradeHorizon.DataAccess.Interfaces.RestAPI
{
    public interface IFilterContractRepository
    {
        Task<string> GetResponseTextAsync(string url);
        Task<string> GetGateioContractsList(string url);
        Task<string> GetGateioTickersList(string url);
        Task<string> GetCoingeckoMarketsList(string url);
    }
}