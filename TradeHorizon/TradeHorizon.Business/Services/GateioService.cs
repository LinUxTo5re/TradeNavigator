using TradeHorizon.Business.Interfaces;
using TradeHorizon.Domain;
using TradeHorizon.DataAccess.Interfaces;
using System.Text.Json;
using TradeHorizon.Domain.Constants;

namespace TradeHorizon.Business.Services
{
    public class GateioService : IGateioService
    {
        private readonly IGateioRepository _gateioRepository;
        public GateioService(IGateioRepository gateioRepository)
        {
            _gateioRepository = gateioRepository;
        }

        public async Task<List<OHLCVModel>> GetOHLCVHistAsync(string contract, long? from, long? to, int? limit, string interval)
        {
            try
            {
                if((from > 0 && to > 0 && limit > 0) || (from > to) || limit > ApiConstants.GateIoOHLCVHistLimit)
                    return new List<OHLCVModel>();
                if(from ==0 && to == 0 && limit == 0)
                    return new List<OHLCVModel>();

                string historicalOHLCVText = await _gateioRepository.GetOHLCVHistAsync(contract, from, to, limit, interval);
                if(string.IsNullOrEmpty(historicalOHLCVText))
                    return new List<OHLCVModel>();
                var historicalOHLCVList = JsonSerializer.Deserialize<List<OHLCVModel>>(historicalOHLCVText, new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true 
                });

                return historicalOHLCVList ?? new List<OHLCVModel>();
            }
            catch(Exception)
            {
                return new List<OHLCVModel>();
            }
        }

        public async Task<List<FundingRateModel>> GetFundingRateHistAsync(string contract, Int64? from, Int64? to, int? limit)
        {
            try
            {
                if((from > 0 && to > 0 && limit > 0) || (from > to) || limit > ApiConstants.GateIoFundingRateHistLimit)
                    return new List<FundingRateModel>();
                if(from ==0 && to == 0 && limit == 0)
                    return new List<FundingRateModel>(); 
                
                string historicalFRText = await _gateioRepository.GetFundingRateHistAsync(contract,from, to, limit);
                if(string.IsNullOrEmpty(historicalFRText))
                    return new List<FundingRateModel>();
                var historicalFRList = JsonSerializer.Deserialize<List<FundingRateModel>>(historicalFRText, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return historicalFRList ?? new List<FundingRateModel>();
            }
            catch(Exception)
            {
                return new List<FundingRateModel>();
            }
        }

        public async Task<List<ContractStatsModel>> GetContractStatsAsync(string contract, long? from, string? interval, int? limit)
        {
            try
            {
                if((limit > ApiConstants.GateIoContractStatsLimit) || (from ==0 && limit == 0))
                    return new List<ContractStatsModel>();

                string historicalContractStatsText = await _gateioRepository.GetContractStatsAsync(contract, from, interval, limit);
                if(string.IsNullOrEmpty(historicalContractStatsText))
                    return new List<ContractStatsModel>();
                var historicalContractStatsList = JsonSerializer.Deserialize<List<ContractStatsModel>>(historicalContractStatsText, new JsonSerializerOptions{
                    PropertyNameCaseInsensitive = true
                });
                return historicalContractStatsList ?? new List<ContractStatsModel>();
            }
            catch(Exception)
            {
                return new List<ContractStatsModel>();
            }
        }

        public async Task<OrderBookModel> GetOrderBookAsync(string contract, string? interval, int? limit, bool? with_id)
        {
            try
            {
                if(limit > ApiConstants.GateIoOrderBookLimit)
                    limit = ApiConstants.GateIoOrderBookLimit;

                string orderBookText = await _gateioRepository.GetOrderBookAsync(contract, interval, limit, with_id);
                if(string.IsNullOrEmpty(orderBookText))
                return new OrderBookModel();
                var orderBook = JsonSerializer.Deserialize<OrderBookModel>(orderBookText, new JsonSerializerOptions{
                    PropertyNameCaseInsensitive = true
                });
                return orderBook ?? new OrderBookModel();
            }
            catch(Exception)
            {
                return new OrderBookModel();
            }
        }

        public async Task<List<LiquidationOrderModel>> GetLiqOrdersAsync(string? contract, long? from, long? to, int? limit)
        {
            try
            {
                if(limit > ApiConstants.GateIoLiqOrdersLimit)
                    limit = ApiConstants.GateIoLiqOrdersLimit;
                string liquidationOrdersText = await _gateioRepository.GetLiqOrdersAsync(contract, from, to, limit);
                if(string.IsNullOrEmpty(liquidationOrdersText))
                    return new List<LiquidationOrderModel>();
                
                var liquidationOrdersList = JsonSerializer.Deserialize<List<LiquidationOrderModel>>(liquidationOrdersText, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return liquidationOrdersList ?? new List<LiquidationOrderModel>();
            }
            catch(Exception)
            {
                return new List<LiquidationOrderModel>();
            }
        }
    }
}