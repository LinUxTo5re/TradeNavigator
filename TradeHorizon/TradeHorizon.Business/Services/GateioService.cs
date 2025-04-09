using TradeHorizon.Business.Interfaces;
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
                    return new List<OHLCVModel>{
                        new OHLCVModel {
                            ApiErrors = new ApiError{
                                Error = string.Concat(VarConstants.FromToLimitError, " Allowed Limit: ", ApiConstants.GateIoOHLCVHistLimit)
                            }
                        }
                    };

                if ((from == null || from == 0) && (to == null || to == 0) && (limit == null || limit == 0))
                    return new List<OHLCVModel>{
                        new OHLCVModel{
                            ApiErrors = new ApiError{
                                Error = VarConstants.FromToLimitMissingError
                            }
                        }
                    };

                string historicalOHLCVText = await _gateioRepository.GetOHLCVHistAsync(contract, from, to, limit, interval);
                if(string.IsNullOrEmpty(historicalOHLCVText))
                    return new List<OHLCVModel>();

                var historicalOHLCVList = JsonSerializer.Deserialize<List<OHLCVModel>>(historicalOHLCVText, new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true 
                });

                return historicalOHLCVList ?? new List<OHLCVModel>();
            }
            catch(Exception ex)
            {
                return new List<OHLCVModel>{
                        new OHLCVModel{
                            ApiErrors = new ApiError{
                                Error = ex.ToString()
                            }
                        }
                    };
            }
        }

        public async Task<List<FundingRateModel>> GetFundingRateHistAsync(string contract, Int64? from, Int64? to, int? limit)
        {
            try
            {
                if((from > 0 && to > 0 && limit > 0) || (from > to) || limit > ApiConstants.GateIoFundingRateHistLimit)
                    return new List<FundingRateModel>
                    {
                        new FundingRateModel{
                            ApiErrors = new ApiError{
                                Error = string.Concat(VarConstants.FromToLimitError, " Allowd Limit: ", ApiConstants.GateIoFundingRateHistLimit)
                            }
                        }
                    };

                if(from ==0 && to == 0 && limit == 0)
                    return new List<FundingRateModel>{
                        new FundingRateModel{
                            ApiErrors = new ApiError{
                                Error = VarConstants.FromToLimitMissingError
                            }
                        }
                    }; 
                
                string historicalFRText = await _gateioRepository.GetFundingRateHistAsync(contract,from, to, limit);
                if(string.IsNullOrEmpty(historicalFRText))
                    return new List<FundingRateModel>();

                var historicalFRList = JsonSerializer.Deserialize<List<FundingRateModel>>(historicalFRText, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return historicalFRList ?? new List<FundingRateModel>();
            }
            catch(Exception ex)
            {
                return new List<FundingRateModel>{
                    new FundingRateModel{
                        ApiErrors = new ApiError{
                            Error = ex.ToString()
                        }
                    }
                };
            }
        }

        public async Task<List<ContractStatsModel>> GetContractStatsAsync(string contract, long? from, string? interval, int? limit)
        {
            try
            {
                if((limit > ApiConstants.GateIoContractStatsLimit) || (from ==0 && limit == 0))
                    return new List<ContractStatsModel>{
                        new ContractStatsModel{ 
                            ApiErrors = new ApiError{
                                Error = string.Concat(VarConstants.FromLimitError, " Allowed Limit: ", ApiConstants.GateIoContractStatsLimit)
                            }
                        }
                    };

                string historicalContractStatsText = await _gateioRepository.GetContractStatsAsync(contract, from, interval, limit);
                if(string.IsNullOrEmpty(historicalContractStatsText))
                    return new List<ContractStatsModel>();

                var historicalContractStatsList = JsonSerializer.Deserialize<List<ContractStatsModel>>(historicalContractStatsText, new JsonSerializerOptions{
                    PropertyNameCaseInsensitive = true
                });
                return historicalContractStatsList ?? new List<ContractStatsModel>();
            }
            catch(Exception ex)
            {
                return new List<ContractStatsModel>{
                    new ContractStatsModel{
                        ApiErrors = new ApiError{
                            Error = ex.ToString()
                        }
                    }
                };
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
            catch(Exception ex)
            {
                return new OrderBookModel{
                   ApiErrors = new ApiError{
                    Error = ex.ToString()
                   }
                };
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
            catch(Exception ex)
            {
                return new List<LiquidationOrderModel>{
                    new LiquidationOrderModel {
                        ApiErrors = new ApiError{
                            Error = ex.ToString()
                        }
                    }
                };
            }
        }
    }
}