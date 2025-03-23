using Microsoft.AspNetCore.Mvc;
using TradeHorizon.Application.Interfaces;
using TradeHorizon.Domain;

namespace TradeHorizon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet("ohlcv")]
        public async Task<IActionResult> GetOHLCVData(
            [FromQuery] string symbol = "BTCUSDT_PERP.A",
            [FromQuery] string interval = "5min",
            [FromQuery] long from = 1711134713,
            [FromQuery] long to = 1742670713)
        {
            var data = await _tradeService.GetOHLCVDataAsync(symbol, interval, from, to);
            return Ok(data);
        }
    }
}
