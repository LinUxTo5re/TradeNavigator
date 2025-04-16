using Microsoft.AspNetCore.SignalR;
using TradeHorizon.Domain.Websockets.Interfaces;
using TradeHorizon.API.Hubs;
public class GateContractStatsBroadcaster : IGateContractStatsBroadcaster
{
    private readonly IHubContext<GateContractStatsHub> _hubContext;

    public GateContractStatsBroadcaster(IHubContext<GateContractStatsHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task BroadcastToGroupAsync(string groupName, string eventName, string message)
    {
        return _hubContext.Clients.Group(groupName).SendAsync(eventName, message);
    }
}
