using Microsoft.AspNetCore.SignalR;
using TradeHorizon.Domain.Interfaces.Websockets;

namespace TradeHorizon.API.Hubs
{
    public abstract class BaseWebSocketHub<TClient> : Hub where TClient : IWebSocketClient
    {
        private readonly TClient _webSocketClient;

        protected BaseWebSocketHub(TClient webSocketClient)
        {
            _webSocketClient = webSocketClient;
        }

        public async Task Subscribe(string? rawMessage, string groupName)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                var token = Context.ConnectionAborted;

                if (!string.IsNullOrEmpty(rawMessage))
                    _ = _webSocketClient.StartClientAsync(rawMessage, _webSocketClient, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

        public async Task Unsubscribe(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}