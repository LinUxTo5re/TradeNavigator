
namespace TradeHorizon.Business.Interfaces.RestAPI
{
    public interface IFilterContractService
    {
        Task<List<FilteredContractModel>> GetFilteredContractsList();
    }
}