using System.Text.Json;
using TradeHorizon.Domain;
using TradeHorizon.DataAccess.Interfaces;
using TradeHorizon.Domain.Constants;

namespace TradeHorizon.DataAccess.Repositories
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
    
        public async Task<string> GetOHLCVAsync(string contract, long? from, long? to, int? limit, string interval)
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
    }
}