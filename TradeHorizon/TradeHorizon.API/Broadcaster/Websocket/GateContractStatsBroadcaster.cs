using Microsoft.AspNetCore.SignalR;
using TradeHorizon.Domain.Interfaces.Websockets;
using TradeHorizon.API.Hubs;
public class GateContractStatsBroadcaster : IGateContractStatsBroadcaster
{
    private readonly IHubContext<GateContractStatsHub> _hubContext;

    public GateContractStatsBroadcaster(IHubContext<GateContractStatsHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task BroadcastToGroupAsync<T>(string groupName, string eventName, T message)
    {
        return _hubContext.Clients.Group(groupName).SendAsync(eventName, message);
    }
}
