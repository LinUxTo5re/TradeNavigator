namespace TradeHorizon.Domain.Constants
{
    public static class WsConstants
    {
        public const string WsContractSettle = "usdt";
        public const string GateIoBaseWsUrl = $"wss://fx-ws.gateio.ws/v4/ws/{WsContractSettle}";
    }
}