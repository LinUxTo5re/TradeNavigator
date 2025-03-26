using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TradeHorizon.Business.Interfaces;
using TradeHorizon.Domain;

namespace TradeHorizon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GateioController : ControllerBase
    {
        private readonly IGateioService _gateioService;

        public GateioController(IGateioService gateioService)
        {
            _gateioService = gateioService;
        }

        [HttpGet("ohlcv")]
        public async Task<IActionResult> GetOHLCVAsync([FromQuery] string contract, [FromQuery] Int64? from, [FromQuery] Int64? to, [FromQuery] int? limit, [FromQuery] string interval)
        {
            try
            {
                List<OHLCVTgateIoData> historicalOHLCV = await _gateioService.GetOHLCVAsync(contract, from, to, limit, interval);
                if(historicalOHLCV == null || historicalOHLCV.Count == 0)
                    return NotFound(new { message = "No data available for the symbols." });
                return Ok(historicalOHLCV);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, new { message = "Failed to fetch data from external service.", error = ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }
    }
}