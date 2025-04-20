using TradeHorizon.DataAccess.Interfaces.RestAPI;
using TradeHorizon.Domain.Constants;

namespace TradeHorizon.DataAccess.Repositories.RestAPI
{
    public class GateioRepository: IGateioRepository
    {
        private readonly HttpClient _httpClient;

        public GateioRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetch get response from coinalyze
        public async Task<string> GetResponseTextAsync(string url)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, url);

                HttpResponseMessage  httpResponseMessage = await _httpClient.SendAsync(request);
                httpResponseMessage.EnsureSuccessStatusCode();
                return await httpResponseMessage.Content.ReadAsStringAsync() ?? string.Empty;
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }
    
        public async Task<string> GetOHLCVHistAsync(string contract, long? from, long? to, int? limit, string interval)
        {
            string ohlcvUrl = string.Empty;
            try
            {
                if(limit > 0)       
                    ohlcvUrl = $"{ApiConstants.GateIoBaseUrl}{ApiConstants.GateIoFuturesCandlesticksUrl}?contract={contract}&interval={interval}&limit={limit}";
                else
                    ohlcvUrl = $"{ApiConstants.GateIoBaseUrl}{ApiConstants.GateIoFuturesCandlesticksUrl}?contract={contract}&interval={interval}&from={from}&to={to}";

                return await GetResponseTextAsync(ohlcvUrl);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<string> GetFundingRateHistAsync(string contract, long? from, long? to, int? limit)
        {
            string fundingRateUrl = string.Empty;
            try
            {
                if(limit > 0)
                    fundingRateUrl = $"{ApiConstants.GateIoBaseUrl}{ApiConstants.GateIoFuturesFundingRateUrl}?contract={contract}&limit={limit}";
                else
                    fundingRateUrl = $"{ApiConstants.GateIoBaseUrl}{ApiConstants.GateIoFuturesFundingRateUrl}?contract={contract}&from={from}&to={to}";
                return await GetResponseTextAsync(fundingRateUrl);
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }

        public async Task<string> GetContractStatsAsync(string contract, long? from, string? interval, int? limit)
        {
            try
            {
                string contractStatsUrl = $"{ApiConstants.GateIoBaseUrl}{ApiConstants.GateIoFuturesContractStatsUrl}?contract={contract}&limit={limit}&interval={interval}&from={from}";
                return await GetResponseTextAsync(contractStatsUrl);
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }

        public async Task<string> GetOrderBookAsync(string contract, string? interval, int? limit, bool? with_id)
        { 
            try
            {
                string orderBookUrl = $"{ApiConstants.GateIoBaseUrl}{ApiConstants.GateIoFuturesOrderBookUrl}?contract={contract}&interval={interval}&limit={limit}&with_id={with_id?.ToString().ToLower()}";
                return await GetResponseTextAsync(orderBookUrl);
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }

        public async Task<string> GetLiqOrdersAsync(string? contract, long? from, long? to, int? limit)
        {
            try
            {
                string liqOrdersUrl = $"{ApiConstants.GateIoBaseUrl}{ApiConstants.GateIoFuturesLiqOrdersUrl}?contract={contract}&from={from}&to={to}&limit={limit}";
                return await GetResponseTextAsync(liqOrdersUrl);
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }
    }
}