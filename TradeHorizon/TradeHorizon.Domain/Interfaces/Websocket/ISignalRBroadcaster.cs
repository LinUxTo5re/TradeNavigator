using Microsoft.AspNetCore.SignalR;

namespace TradeHorizon.Domain.Interfaces.Websockets
{
    public interface ISignalRBroadcaster
    {
        Task BroadcastToGroupAsync<T>(string groupName, string eventName, T message);
    }
}