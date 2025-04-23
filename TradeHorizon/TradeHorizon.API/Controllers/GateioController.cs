using Microsoft.AspNetCore.Mvc;
using TradeHorizon.Business.Interfaces.RestAPI;
using TradeHorizon.Domain.Constants;
using TradeHorizon.Domain.Interfaces.RestAPI;

namespace TradeHorizon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GateioController : ControllerBase
    {
        private readonly IGateioService _gateioService;
        private readonly IGateioBroadcaster _broadcaster;

        public GateioController(IGateioService gateioService, IGateioBroadcaster broadcaster)
        {
            _gateioService = gateioService;
            _broadcaster = broadcaster;
        }

        [HttpGet("ohlcv")]
        public async Task<IActionResult> GetOHLCVHistAsync([FromQuery] string contract, [FromQuery] Int64? from, [FromQuery] Int64? to, [FromQuery] int? limit, [FromQuery] string interval)
        {
            try
            {
                List<OHLCVModel> historicalOHLCV = await _gateioService.GetOHLCVHistAsync(contract, from, to, limit, interval);
                if (historicalOHLCV == null || historicalOHLCV.Count == 0)
                    return NotFound(new { message = VarConstants.DataNotAvailMsg });

                var errorMessage = historicalOHLCV.FirstOrDefault(x => x.ApiErrors != null && !string.IsNullOrEmpty(x.ApiErrors.Error))?.ApiErrors?.Error;

                if (!string.IsNullOrEmpty(errorMessage))
                    return StatusCode(422, new { message = errorMessage });

                await _broadcaster.BroadcastOHLCVAsync(historicalOHLCV ?? []);
                return Ok(historicalOHLCV);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [HttpGet("funding-rate-history")]
        public async Task<IActionResult> GetFundingRateHistAsync([FromQuery] string contract, [FromQuery] Int64? from, [FromQuery] Int64? to, [FromQuery] int? limit)
        {
            try
            {
                List<FundingRateModel> historicalFundingRate = await _gateioService.GetFundingRateHistAsync(contract, from, to, limit);
                if (historicalFundingRate == null || historicalFundingRate.Count == 0)
                    return NotFound(new { message = VarConstants.DataNotAvailMsg });

                var errorMessage = historicalFundingRate.FirstOrDefault(x => x.ApiErrors != null && !string.IsNullOrEmpty(x.ApiErrors.Error))?.ApiErrors?.Error;

                if (!string.IsNullOrEmpty(errorMessage))
                    return StatusCode(422, new { message = errorMessage });

                await _broadcaster.BroadcastFundingRateAsync(historicalFundingRate ?? []);
                return Ok(historicalFundingRate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [HttpGet("contract-stats")]
        public async Task<IActionResult> GetContractStatsAsync([FromQuery] string contract, [FromQuery] Int64? from, [FromQuery] string? interval, [FromQuery] int? limit)
        {
            try
            {
                List<ContractStatsModel> historicalContractStats = await _gateioService.GetContractStatsAsync(contract, from, interval, limit);
                if (historicalContractStats == null || historicalContractStats.Count == 0)
                    return NotFound(new { message = VarConstants.DataNotAvailMsg });

                var errorMessage = historicalContractStats.FirstOrDefault(x => x.ApiErrors != null && !string.IsNullOrEmpty(x.ApiErrors.Error))?.ApiErrors?.Error;

                if (!string.IsNullOrEmpty(errorMessage))
                    return StatusCode(422, new { message = errorMessage });

                await _broadcaster.BroadcastContractStatsAsync(historicalContractStats ?? []);
                return Ok(historicalContractStats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [HttpGet("order-book")]
        public async Task<IActionResult> GetOrderBookAsync([FromQuery] string contract, [FromQuery] string? interval, [FromQuery] int? limit, [FromQuery] bool? with_id)
        {
            try
            {
                OrderBookModel orderBookList = await _gateioService.GetOrderBookAsync(contract, interval, limit, with_id);
                if (orderBookList == null || (orderBookList.Asks?.Count == 0 && orderBookList.Bids?.Count == 0))
                    return NotFound(new { message = VarConstants.DataNotAvailMsg });

                var errorMessage = orderBookList?.ApiErrors?.Error;

                if (!string.IsNullOrEmpty(errorMessage))
                    return StatusCode(422, new { message = errorMessage });

                await _broadcaster.BroadcastOrderBookAsync(orderBookList ?? new());
                return Ok(orderBookList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        [HttpGet("liq-orders")]
        public async Task<IActionResult> GetLiqOrdersAsync([FromQuery] string? contract, [FromQuery] Int64? from, [FromQuery] Int64? to, [FromQuery] int? limit)
        {
            try
            {
                List<LiquidationOrderModel> liqOrdersList = await _gateioService.GetLiqOrdersAsync(contract, from, to, limit);
                if (liqOrdersList == null || liqOrdersList.Count == 0)
                    return NotFound(new { message = VarConstants.DataNotAvailMsg });

                var errorMessage = liqOrdersList.FirstOrDefault(x => x.ApiErrors != null && !string.IsNullOrEmpty(x.ApiErrors.Error))?.ApiErrors?.Error;

                if (!string.IsNullOrEmpty(errorMessage))
                    return StatusCode(422, new { message = errorMessage });

                await _broadcaster.BroadcastLiqOrdersAsync(liqOrdersList ?? []);
                return Ok(liqOrdersList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }
    }
}