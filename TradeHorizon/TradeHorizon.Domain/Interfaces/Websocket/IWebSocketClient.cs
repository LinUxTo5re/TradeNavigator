namespace TradeHorizon.Domain.Interfaces.Websockets
{
    public interface IWebSocketClient
    {
        Task StartClientAsync<T>(string userInput, T processor, CancellationToken cancellationToken);
    }
}