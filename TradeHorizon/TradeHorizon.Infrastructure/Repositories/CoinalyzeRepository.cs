using System.Text.Json;
using TradeHorizon.Application.Interfaces;
using TradeHorizon.Domain;

namespace TradeHorizon.Infrastructure.Repositories
{
    public class CoinalyzeRepository : ICoinalyzeRepository
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "249f56f8-e4b1-4bf4-84d9-45e2843b1194"; // Move this to config later

        public CoinalyzeRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OHLCVData>> GetOHLCVDataAsync(string symbol, string interval, long from, long to)
        {
            string url = $"https://api.coinalyze.net/v1/ohlcv-history?symbols={symbol}&interval={interval}&from={from}&to={to}";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("api-key", ApiKey);  // Add API Key to Header

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<OHLCVData>>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<OHLCVData>();
        }
    }
}
