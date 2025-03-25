using TradeHorizon.Business.Interfaces;
using TradeHorizon.Domain;
using TradeHorizon.DataAccess.Interfaces;
using System.Text.Json;

namespace TradeHorizon.Business.Services
{
    public class CoinalyzeService : ICoinalyzeService
    {
        private readonly ICoinalyzeRepository _coinalyzeRepository;

        public CoinalyzeService(ICoinalyzeRepository coinalyzeRepository)
        {
            _coinalyzeRepository = coinalyzeRepository;
        }

        // Get Current Funding Rate
        public async Task<CurrentFundingRate> GetCurrentFundingRateAsync(string symbols, bool isPredicted)
        {
            try
            {
                string currentFundingRate = await _coinalyzeRepository.GetCurrentFundingRateAsync(symbols, isPredicted);
                if (string.IsNullOrEmpty(currentFundingRate))
                    return new CurrentFundingRate();
                var rawData = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(currentFundingRate, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (rawData?.Count == 0 || rawData == null)
                    return new CurrentFundingRate();

                var fundingRateData =  rawData.FirstOrDefault() ?? new Dictionary<string, JsonElement>();

                return new CurrentFundingRate
                {
                    CurrentFR = new CurrentOI
                    {
                        Symbol = fundingRateData.GetValueOrDefault("symbol").GetString() ?? string.Empty,
                        CurrentOIValue = fundingRateData.GetValueOrDefault("value").GetDecimal(),
                        UnixTimeStamp = fundingRateData.GetValueOrDefault("update").GetInt64()
                    }
                } ?? new CurrentFundingRate();
            }
            catch(Exception)
            {
                return new CurrentFundingRate();
            }
        }

        // Get Historical Funding Rate
        public async Task<List<OHLCVData>> GetHistoricalFundingRateAsync(string symbols, string interval, long from, long to, bool isPredicted)
        {
            try
            {
                string historicalFundingRateText = await _coinalyzeRepository.GetHistoricalFundingRateAsync(symbols, interval, from, to, isPredicted);
                if (string.IsNullOrEmpty(historicalFundingRateText))
                    return new List<OHLCVData>();
                var historicalFRList = JsonSerializer.Deserialize<List<OHLCVData>>(historicalFundingRateText, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return historicalFRList ?? new List<OHLCVData>();
            }
            catch(Exception)
            {
                return new List<OHLCVData>();
            }
        }
        // Get Current Open Interest (OI)
        public async Task<CurrentOI> GetCurrentOpenInterestAsync(string symbols)
        {
            try
            {
            string currentOIText = await _coinalyzeRepository.GetCurrentOpenInterestAsync(symbols); 
            if (string.IsNullOrEmpty(currentOIText))
                return new CurrentOI();
            List<CurrentOI>? currentOIList = JsonSerializer.Deserialize<List<CurrentOI>>(currentOIText, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            return currentOIList?.FirstOrDefault() ?? new CurrentOI();
            }
            catch(Exception)
            {
                return new CurrentOI();
            }
        }

        // Get Historical Open Interest (OI) -> List
        public async Task<List<OHLCVData>> GetHistoricalOpenInterestAsync(string symbols, string interval, long from, long to, string convert_to_usd)
        {
            try
            {
            string historicalOIText = await _coinalyzeRepository.GetHistoricalOpenInterestAsync(symbols, interval, from, to, convert_to_usd);
            if (string.IsNullOrEmpty(historicalOIText))
                return new List<OHLCVData>();
            var historicalOIList = JsonSerializer.Deserialize<List<OHLCVData>>(historicalOIText, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true 
            });

            return historicalOIList ?? new List<OHLCVData>(); 
            }
            catch(Exception)
            {
                return new List<OHLCVData>();
            }
        }

        // Get Liquidation History
        public async Task<List<LiquidationHistory>> GetLiquidationHistoryAsync(string symbols, string interval, Int64 from, Int64 to, string convert_to_usd)
        {
            try
            {
                var liquidationHistoryText = await _coinalyzeRepository.GetLiquidationHistoryAsync(symbols, interval, from, to, convert_to_usd);
                if (string.IsNullOrEmpty(liquidationHistoryText))
                    return new List<LiquidationHistory>();
                var liquidationHistoryList = JsonSerializer.Deserialize<List<LiquidationHistory>>(liquidationHistoryText, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return liquidationHistoryList ?? new List<LiquidationHistory>();
            }
            catch(Exception)
            {
                return new List<LiquidationHistory>();
            }
        }

        // Get Long-Short Ration
        public async Task<List<LiquidationHistory>> GetLongShortRatioHistoryAsync(string symbols, string interval, Int64 from, Int64 to)
        {
            try
            {
                var longShortRatioText = await _coinalyzeRepository.GetLongShortRatioHistoryAsync(symbols, interval, from, to);
                if (string.IsNullOrEmpty(longShortRatioText))
                    return new List<LiquidationHistory>();
                var longShortRatioList = JsonSerializer.Deserialize<List<LiquidationHistory>>(longShortRatioText, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return longShortRatioList ?? new List<LiquidationHistory>();
            }
            catch(Exception)
            {
                return new List<LiquidationHistory>();
            }
        }

    }
}
