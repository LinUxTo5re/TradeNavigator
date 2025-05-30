using Microsoft.AspNetCore.SignalR;
using TradeHorizon.Domain.Websockets.Interfaces;
using TradeHorizon.API.Hubs;
public class GatePublicLiquidatesBroadcaster : IGatePublicLiquidatesBroadcaster
{
    private readonly IHubContext<GatePublicLiquidatesHub> _hubContext;

    public GatePublicLiquidatesBroadcaster(IHubContext<GatePublicLiquidatesHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task BroadcastToGroupAsync<T>(string groupName, string eventName, T message)
    {
        return _hubContext.Clients.Group(groupName).SendAsync(eventName, message);
    }
}
