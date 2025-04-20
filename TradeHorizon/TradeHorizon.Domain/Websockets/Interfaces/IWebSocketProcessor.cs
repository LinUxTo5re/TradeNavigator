namespace TradeHorizon.Domain.Websockets.Interfaces
{
    public interface IWebSocketProcessor
    {
        Task ProcessAsync(string rawMessage);
    }
}