using Microsoft.AspNetCore.SignalR;
using TradeHorizon.Domain.Websockets.Interfaces;
using TradeHorizon.API.Hubs;
public class GateTradeBroadcaster : IGateTradesBroadcaster
{
    private readonly IHubContext<GateTradesHub> _hubContext;

    public GateTradeBroadcaster(IHubContext<GateTradesHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task BroadcastToGroupAsync(string groupName, string eventName, string message)
    {
        return _hubContext.Clients.Group(groupName).SendAsync(eventName, message);
    }
}
