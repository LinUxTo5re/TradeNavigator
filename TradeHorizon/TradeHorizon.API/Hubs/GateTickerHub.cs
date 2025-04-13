using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using TradeHorizon.Domain.Websockets.Interfaces;

namespace TradeHorizon.API.Hubs
{
    public class GateTickerHub : Hub
    {
        private readonly IGateTickerClient _gateTickerClient;

        public GateTickerHub(IGateTickerClient gateTickerClient)
        {
            _gateTickerClient = gateTickerClient;
        }

        public async Task Subscribe(string rawMessage)
        {
            var json = JsonDocument.Parse(rawMessage);
            var eventType = json.RootElement.GetProperty("event").GetString();

            if (eventType == "subscribe")
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "ProcessedGateTickerGroup");
                _ = Task.Run(() => _gateTickerClient.StartClientAsync(rawMessage, Context.ConnectionAborted));
            }
        }

        public async Task Unsubscribe(string symbol)
        {
            var groupName ="ProcessedGateTickerGroup";

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
