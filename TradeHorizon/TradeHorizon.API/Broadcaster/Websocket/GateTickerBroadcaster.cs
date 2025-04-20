using Microsoft.AspNetCore.SignalR;
using TradeHorizon.Domain.Interfaces.Websockets;
using TradeHorizon.API.Hubs;
public class GateTickerBroadcaster : IGateTickerBroadcaster
{
    private readonly IHubContext<GateTickerHub> _hubContext;

    public GateTickerBroadcaster(IHubContext<GateTickerHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task BroadcastToGroupAsync<T>(string groupName, string eventName, T message)
    {
        return _hubContext.Clients.Group(groupName).SendAsync(eventName, message);
    }
}
