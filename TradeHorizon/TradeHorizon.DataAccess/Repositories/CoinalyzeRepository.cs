using System.Text.Json;
using TradeHorizon.Domain;
using TradeHorizon.DataAccess.Interfaces;
using TradeHorizon.Domain.Constants;

namespace TradeHorizon.DataAccess.Repositories
{
    public class CoinalyzeRepository : ICoinalyzeRepository
    {
        private readonly HttpClient _httpClient;

        public CoinalyzeRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetch get response from coinalyze
        public async Task<string> GetResponseTextAsync(string url)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("api-key", ApiConstants.CoinalyzeAPIKEY);

                HttpResponseMessage  httpResponseMessage = await _httpClient.SendAsync(request);
                httpResponseMessage.EnsureSuccessStatusCode();
                return await httpResponseMessage.Content.ReadAsStringAsync() ?? string.Empty;
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }

        // Get current funding rate
        public async Task<string> GetCurrentFundingRateAsync(string symbols, bool isPredicted)
        {
            string currentFRUrl = string.Empty;
            try
            {
                if (isPredicted)
                    currentFRUrl = $"{ApiConstants.CoinalyzeBaseUrl}{ApiConstants.CurrentPredictedFundingRateUrl}?symbols={symbols}";
                else
                    currentFRUrl = $"{ApiConstants.CoinalyzeBaseUrl}{ApiConstants.CurrentFundingRateUrl}?symbols={symbols}";
                return await GetResponseTextAsync(currentFRUrl);
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }

        public async Task<string> GetHistoricalFundingRateAsync(string symbols, string interval, Int64 from, Int64 to, bool isPredicted)
        {
            string historicalFR = string.Empty;
            try
            {
                if(isPredicted)
                    historicalFR =  $"{ApiConstants.CoinalyzeBaseUrl}{ApiConstants.HistoricalPredictedFundingRateUrl}?symbols={symbols}&interval={interval}&from={from}&to={to}";
                else
                    historicalFR = $"{ApiConstants.CoinalyzeBaseUrl}{ApiConstants.HistoricalFundingRateUrl}?symbols={symbols}&interval={interval}&from={from}&to={to}";
                return await GetResponseTextAsync(historicalFR);
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }
        // Get current open interest
        public async Task<string> GetCurrentOpenInterestAsync(string symbols)
        {
            try
            {
            string currentOIUrl = $"{ApiConstants.CoinalyzeBaseUrl}{ApiConstants.CurrentOIUrl}?symbols={symbols}";
            return await GetResponseTextAsync(currentOIUrl);
            }
            catch(Exception)
            {
                return string.Empty;
            }

        }

        // Get current open interest history
        public async Task<string> GetHistoricalOpenInterestAsync(string symbols, string interval, long from, long to, string convert_to_usd)
        {
            try
            {
                string historicalOIUrl = $"{ApiConstants.CoinalyzeBaseUrl}{ApiConstants.HistoricalOIUrl}?symbols={symbols}&interval={interval}&from={from}&to={to}&convert_to_usd={convert_to_usd}";
                return await GetResponseTextAsync(historicalOIUrl);
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }

        // Get Liquidation history
        public async Task<string> GetLiquidationHistoryAsync(string symbols, string interval, Int64 from, Int64 to, string convert_to_usd)
        {
            try
            {
                string liquidationHistoryUrl = $"{ApiConstants.CoinalyzeBaseUrl}{ApiConstants.LiquidationHistoryUrl}?symbols={symbols}&interval={interval}&from={from}&to={to}&convert_to_usd={convert_to_usd}";
                return await GetResponseTextAsync(liquidationHistoryUrl);            }
            catch(Exception)
            {
                return string.Empty;
            }
        }

        // Get Long-Short ratio
        public async Task<string> GetLongShortRatioHistoryAsync(string symbols, string interval, Int64 from, Int64 to)
        {
            try
            {
                string longShortRatioUrl = $"{ApiConstants.CoinalyzeBaseUrl}{ApiConstants.LongShortRationUrl}?symbols={symbols}&interval={interval}&from={from}&to={to}";
                return await GetResponseTextAsync(longShortRatioUrl);
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }
    }
}
