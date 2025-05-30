using Microsoft.AspNetCore.SignalR;
using TradeHorizon.Domain.Websockets.Interfaces;
using TradeHorizon.API.Hubs;
public class GateOrderBookUpdateBroadcaster : IGateOrderBookUpdateBroadcaster
{
    private readonly IHubContext<GateOrderBookUpdateHub> _hubContext;

    public GateOrderBookUpdateBroadcaster(IHubContext<GateOrderBookUpdateHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task BroadcastToGroupAsync<T>(string groupName, string eventName, T message)
    {
        return _hubContext.Clients.Group(groupName).SendAsync(eventName, message);
    }
}
