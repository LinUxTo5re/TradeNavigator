using Microsoft.AspNetCore.SignalR;
using TradeHorizon.Domain.Interfaces.Websockets;
using TradeHorizon.API.Hubs;
public class GateCandlesticksBroadcaster : IGateCandlesticksBroadcaster
{
    private readonly IHubContext<GateCandlesticksHub> _hubContext;

    public GateCandlesticksBroadcaster(IHubContext<GateCandlesticksHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task BroadcastToGroupAsync<T>(string groupName, string eventName, T message)
    {
        return _hubContext.Clients.Group(groupName).SendAsync(eventName, message);
    }
}
