using Microsoft.AspNetCore.SignalR;
using TradeHorizon.Domain.Websockets.Interfaces;
using TradeHorizon.API.Hubs;
public class GateCandlesticksBroadcaster : IGateCandlesticksBroadcaster
{
    private readonly IHubContext<GateCandlesticksHub> _hubContext;

    public GateCandlesticksBroadcaster(IHubContext<GateCandlesticksHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task BroadcastToGroupAsync(string groupName, string eventName, string message)
    {
        return _hubContext.Clients.Group(groupName).SendAsync(eventName, message);
    }
}
