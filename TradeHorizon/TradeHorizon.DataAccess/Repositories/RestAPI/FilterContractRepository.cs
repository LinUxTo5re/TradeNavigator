using TradeHorizon.Domain.Constants;
using TradeHorizon.DataAccess.Interfaces.RestAPI;
using System.Text.Json;

namespace TradeHorizon.DataAccess.Repositories.RestAPI
{
    public class FilterContractRepository : IFilterContractRepository
    {
        private readonly HttpClient _httpClient;
        public FilterContractRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetResponseTextAsync(string url)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, url);

                HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(request);
                httpResponseMessage.EnsureSuccessStatusCode();
                return await httpResponseMessage.Content.ReadAsStringAsync() ?? string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public async Task<string> GetCoingeckoMarketsList(string url)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("accept", "application/json");
                request.Headers.Add("x-cg-demo-api-key", ApiConstants.CoingeckoAPIKEY);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<string> GetGateioContractsList(string url)
        {
            try
            {
                return await GetResponseTextAsync($"{ApiConstants.GateIoBaseUrl}{url}");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<string> GetGateioTickersList(string url)
        {
            try
            {
                return await GetResponseTextAsync($"{ApiConstants.GateIoBaseUrl}{url}");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}