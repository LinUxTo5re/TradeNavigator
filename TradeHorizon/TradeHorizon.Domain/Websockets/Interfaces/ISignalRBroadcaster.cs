

namespace TradeHorizon.Domain.Websockets.Interfaces
{
    public interface ISignalRBroadcaster
    {
        Task BroadcastToGroupAsync(string groupName, string eventName, string message);
    }
}