
namespace TradeHorizon.Domain.Websockets.Interfaces
{
    public interface IGateTickerClient
    {
        Task StartClientAsync(string userInputJson, CancellationToken cancellationToken);
    }
}