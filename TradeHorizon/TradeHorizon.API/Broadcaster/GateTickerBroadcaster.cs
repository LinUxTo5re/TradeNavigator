using Microsoft.AspNetCore.SignalR;
using TradeHorizon.Domain.Websockets.Interfaces;
using TradeHorizon.API.Hubs;
public class GateTickerBroadcaster : IGateTickerBroadcaster
{
    private readonly IHubContext<GateTickerHub> _hubContext;

    public GateTickerBroadcaster(IHubContext<GateTickerHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task BroadcastToGroupAsync(string groupName, string eventName, string message)
    {
        return _hubContext.Clients.Group(groupName).SendAsync(eventName, message);
    }
}
