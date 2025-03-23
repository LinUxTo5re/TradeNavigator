using TradeHorizon.Application.Interfaces;
using TradeHorizon.Domain;

namespace TradeHorizon.Application.Services
{
    public class TradeService : ITradeService
    {
        private readonly ICoinalyzeRepository _coinalyzeRepository;

        public TradeService(ICoinalyzeRepository coinalyzeRepository)
        {
            _coinalyzeRepository = coinalyzeRepository;
        }

        public async Task<List<OHLCVData>> GetOHLCVDataAsync(string symbol, string interval, long from, long to)
        {
            return await _coinalyzeRepository.GetOHLCVDataAsync(symbol, interval, from, to);
        }
    }
}
