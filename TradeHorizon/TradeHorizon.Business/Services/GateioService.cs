using TradeHorizon.Business.Interfaces;
using TradeHorizon.Domain;
using TradeHorizon.DataAccess.Interfaces;
using System.Text.Json;

namespace TradeHorizon.Business.Services
{
    public class GateioService : IGateioService
    {
        private readonly IGateioRepository _gateioRepository;
        public GateioService(IGateioRepository gateioRepository)
        {
            _gateioRepository = gateioRepository;
        }

        public async Task<List<OHLCVTgateIoData>> GetOHLCVAsync(string contract, long? from, long? to, int? limit, string interval)
        {
            try
            {
                if((from > 0 && to > 0 && limit > 0) || (from > to) || limit > 2000)
                    return new List<OHLCVTgateIoData>();
                if(from ==0 && to == 0 && limit == 0)
                    return new List<OHLCVTgateIoData>();
                    
                string historicalOHLCVText = await _gateioRepository.GetOHLCVAsync(contract, from, to, limit, interval);
                if(string.IsNullOrEmpty(historicalOHLCVText))
                    return new List<OHLCVTgateIoData>();
                var historicalOHLCVList = JsonSerializer.Deserialize<List<OHLCVTgateIoData>>(historicalOHLCVText, new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true 
                });

                return historicalOHLCVList ?? new List<OHLCVTgateIoData>();
            }
            catch(Exception)
            {
                return new List<OHLCVTgateIoData>();
            }
        }
    }
}