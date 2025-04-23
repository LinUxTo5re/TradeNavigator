using System.Text.Json;
using TradeHorizon.Domain.Constants;
using TradeHorizon.DataAccess.Interfaces.RestAPI;
using TradeHorizon.Business.Interfaces.RestAPI;

namespace TradeHorizon.Business.Services.BackgroundService
{
    public class FilterContractService: IFilterContractService
    {
        private readonly string coingeckoMarketURL;
        private readonly IFilterContractRepository _filterContractRepository;


        public FilterContractService(IFilterContractRepository filterContractRepository)
        {
            coingeckoMarketURL = ApiConstants.CoingeckoMarketsList;
            _filterContractRepository = filterContractRepository;
        }

        public async Task<List<FilteredContractModel>> GetFilteredContractsList()
        {
            List<CoinGeckoModel>? cgJson = [];
            List<GateTickerModel>? tickerJson  = [];
            List<GateContractModel>? contractJson = [];
            List<FilteredContractModel> filteredData = [];

            try
            {
                // Fetch coingecko markets list
                for (int page = 1; page <= 5; page++)
                {
                    string url = $"{coingeckoMarketURL}?vs_currency=usd&per_page=250&page={page}";
                    var json = await _filterContractRepository.GetCoingeckoMarketsList(url);
                    var items = JsonSerializer.Deserialize<List<CoinGeckoModel>>(json);
                    if (items != null)
                        cgJson.AddRange(items);
                }
                
                // Fetch Gate.io tickers list
                string? tickerResponse = await _filterContractRepository.GetGateioTickersList(ApiConstants.GateIoTickersListUrl) ?? string.Empty;
                tickerJson = JsonSerializer.Deserialize<List<GateTickerModel>>(tickerResponse);

                // Fetch Gate.io contracts list
                string? contractResponse = await _filterContractRepository.GetGateioContractsList(ApiConstants.GateIoContractsListUrl) ?? string.Empty;
                contractJson = JsonSerializer.Deserialize<List<GateContractModel>>(contractResponse);

                 // Applied filtering criteria on data
                var filteredTickerNames = tickerJson?
                    .Where(t => t.GateVolume24HUsdt >= VarConstants.GateVolume24HUsdt) // 1M
                    .Select(t => t.Contract)
                    .ToHashSet() ?? [];

                var filteredContracts = contractJson?
                    .Where(c => filteredTickerNames.Contains(c.Name))
                    .Where(c => c.IsPreMarket  == false)
                    .Where(c => c.InDelisting == false)
                    .Where(c => c.LeverageMax >= VarConstants.LeverageMax) // 20X
                    .Select(c => c.Name?.Split('_')[0].ToLower())
                    .ToHashSet() ?? [];

                var filteredCg = cgJson
                    .Where(cg => cg.IndexMarketCap >= VarConstants.IndexMarketCap) // 50M
                    .Where(cg => cg.IndexVolume24HUsdt >= VarConstants.IndexVolume24HUsdt) // 50M
                    .Where(cg => filteredContracts.Contains(cg.Symbol))
                    .Select(cg => cg.Symbol)
                    .ToList();



                foreach (var symbol in filteredCg)
                {
                    var cgItem = cgJson.First(c => c.Symbol == symbol);
                    var contractItem = contractJson?.First(c => c.Name?.Split('_')[0].ToLower() == symbol);
                    var tickerItem = tickerJson?.First(t => t.Contract?.Split('_')[0].ToLower() == symbol);

                    var dto = new FilteredContractModel
                    {
                        IndexMarketCap = cgItem.IndexMarketCap,
                        IndexVolume24hUsdt = cgItem.IndexVolume24HUsdt,
                        Price = contractItem?.MarkPrice,
                        Contract = contractItem?.Name,
                        FundingRate = contractItem?.FundingRate,
                        ShortUsers = contractItem?.ShortUsers,
                        LongUsers = contractItem?.LongUsers,
                        GateVolume24hUsdt = tickerItem?.GateVolume24HUsdt
                    };

                    filteredData.Add(dto);
                }

                return filteredData;

            }
            catch(Exception)
            {
                return [];
            }
        }
    }
}