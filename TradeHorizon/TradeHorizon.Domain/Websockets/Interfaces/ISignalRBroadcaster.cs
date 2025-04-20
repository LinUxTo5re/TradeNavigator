using Microsoft.AspNetCore.SignalR;

namespace TradeHorizon.Domain.Websockets.Interfaces
{
    public interface ISignalRBroadcaster
    {
        Task BroadcastToGroupAsync<T>(string groupName, string eventName, T message);
    }
}