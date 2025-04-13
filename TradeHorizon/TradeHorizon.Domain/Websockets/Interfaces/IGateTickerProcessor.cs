// Domain/Websockets/Interfaces/IGateTickerProcessor.cs
namespace TradeHorizon.Domain.Websockets.Interfaces
{
    public interface IGateTickerProcessor
    {
        Task ProcessAsync(string rawMessage);
    }
}
