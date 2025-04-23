using Microsoft.AspNetCore.Mvc;
using TradeHorizon.Business.Interfaces.RestAPI;

namespace TradeHorizon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinalyzeController : ControllerBase
    {
        private readonly ICoinalyzeService _coinalyzeService;

        public CoinalyzeController(ICoinalyzeService coinalyzeService)
        {
            _coinalyzeService = coinalyzeService;
        }

        [HttpGet("oi-rate/current")] //Open Interest
        public async Task<IActionResult> GetCurrentOpenInterestAsync([FromQuery] string symbols)
        {
            try
            {
                CurrentOI currentOI = await _coinalyzeService.GetCurrentOpenInterestAsync(symbols);
                if (currentOI == null || string.IsNullOrEmpty(currentOI.Symbol))
                    return NotFound(new { message = "No data available for the symbols." });
                return Ok(currentOI);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, new { message = "Failed to fetch data from external service.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [HttpGet("oi-rate/historical")] //Open Interest
        public async Task<IActionResult> GetHistoricalOpenInterestAsync([FromQuery] string symbols, [FromQuery] string interval, [FromQuery] Int64 from, [FromQuery] Int64 to, [FromQuery] string convert_to_usd = "false")
        {
            try
            {
                List<OHLCVData> historicalOI = await _coinalyzeService.GetHistoricalOpenInterestAsync(symbols, interval, from, to, convert_to_usd);
                if (historicalOI == null || historicalOI.Count == 0)
                    return NotFound(new { message = "No data available for the symbols." });
                return Ok(historicalOI);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, new { message = "Failed to fetch data from external service.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [HttpGet("f-rate/current")] // Funding Rate (actual/predicted)
        public async Task<IActionResult> GetCurrentFundingRateAsync([FromQuery] string symbols, [FromQuery] bool ispredicted = false)
        {
            try
            {
                var currentFundingRate = await _coinalyzeService.GetCurrentFundingRateAsync(symbols, ispredicted);
                if (currentFundingRate == null || string.IsNullOrEmpty(currentFundingRate?.CurrentFR?.Symbol))
                    return NotFound(new { message = "No data available for the symbols." });
                return Ok(currentFundingRate);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, new { message = "Failed to fetch data from external service.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [HttpGet("f-rate/historical")] // Funding Rate (actual/predicted)
        public async Task<IActionResult> GetHistoricalFundingRateAsync([FromQuery] string symbols, [FromQuery] string interval, [FromQuery] Int64 from, [FromQuery] Int64 to, [FromQuery] bool ispredicted = false)
        {
            try
            {
                var historicalFundingRate = await _coinalyzeService.GetHistoricalFundingRateAsync(symbols, interval, from, to, ispredicted);
                if (historicalFundingRate == null || historicalFundingRate.Count == 0)
                    return NotFound(new { message = "No data available for the symbols." });
                return Ok(historicalFundingRate);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, new { message = "Failed to fetch data from external service.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [HttpGet("liquidation-history")]
        public async Task<IActionResult> GetLiquidationHistoryAsync([FromQuery] string symbols, [FromQuery] string interval, [FromQuery] Int64 from, [FromQuery] Int64 to, [FromQuery] string convert_to_usd = "false")
        {
            try
            {
                var liquidationHistory = await _coinalyzeService.GetLiquidationHistoryAsync(symbols, interval, from, to, convert_to_usd);
                if (liquidationHistory == null || liquidationHistory.Count == 0)
                    return NotFound(new { message = "No data available for the symbols." });
                return Ok(liquidationHistory);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, new { message = "Failed to fetch data from external service.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [HttpGet("long-short-ratio-history")]
        public async Task<IActionResult> GetLongShortRatioHistoryAsync([FromQuery] string symbols, [FromQuery] string interval, [FromQuery] Int64 from, [FromQuery] Int64 to)
        {
            try
            {
                var longShortRatio = await _coinalyzeService.GetLongShortRatioHistoryAsync(symbols, interval, from, to);
                if (longShortRatio == null || longShortRatio.Count == 0)
                    return NotFound(new { message = "No data available for the symbols." });
                return Ok(longShortRatio);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, new { message = "Failed to fetch data from external service.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }
    }
}
