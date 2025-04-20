namespace TradeHorizon.Domain.Interfaces.Websockets
{
    public interface IGateTickerProcessor : IWebSocketProcessor { }
    public interface IGateTradesProcessor : IWebSocketProcessor { }
    public interface IGateCandlesticksProcessor : IWebSocketProcessor { }
    public interface IGatePublicLiquidatesProcessor : IWebSocketProcessor { }
    public interface IGateContractStatsProcessor : IWebSocketProcessor { }
    public interface IGateOrderBookUpdateProcessor : IWebSocketProcessor { }
}
