namespace TradeHorizon.Domain.Interfaces.Websockets
{
    public interface IWebSocketProcessor
    {
        Task ProcessAsync(string rawMessage);
    }
}