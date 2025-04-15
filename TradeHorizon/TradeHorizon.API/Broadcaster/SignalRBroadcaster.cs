using Microsoft.AspNetCore.SignalR;
using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.API.Hubs.Broadcaster
{
    public class SignalRBroadcaster<THub> : ISignalRBroadcaster where THub : Hub
    {
        private readonly IHubContext<THub> _hubContext;

        public SignalRBroadcaster(IHubContext<THub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task BroadcastToGroupAsync(string groupName, string eventName, string message)
        {
            return _hubContext.Clients.Group(groupName).SendAsync(eventName, message);
        }
    }

}