namespace TradeHorizon.Domain.Interfaces.Websockets
{
    public interface IGateTickerClient : IWebSocketClient { }
    public interface IGateTradesClient : IWebSocketClient { }
    public interface IGateCandlestickClient : IWebSocketClient { }
    public interface IGatePublicLiquidatesClient : IWebSocketClient { }
    public interface IGateContractStatsClient : IWebSocketClient { }
    public interface IGateOrderBookUpdateClient : IWebSocketClient { }
}