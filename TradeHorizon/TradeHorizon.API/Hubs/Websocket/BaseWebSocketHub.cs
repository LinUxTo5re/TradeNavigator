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

        public async Task Subscribe(string rawMessage, string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var token = Context.ConnectionAborted;
            _ = Task.Run(() => _webSocketClient.StartClientAsync(rawMessage, _webSocketClient, token));
        }

        public async Task Unsubscribe(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}