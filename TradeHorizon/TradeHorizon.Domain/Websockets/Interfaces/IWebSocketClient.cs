namespace TradeHorizon.Domain.Websockets.Interfaces
{
    public interface IWebSocketClient
    {
        Task StartClientAsync<T>(string userInput, T processor, CancellationToken cancellationToken);
    }
}