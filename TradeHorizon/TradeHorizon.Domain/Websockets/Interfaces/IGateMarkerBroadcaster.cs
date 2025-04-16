
namespace TradeHorizon.Domain.Websockets.Interfaces
{
    public interface IGateTickerBroadcaster : ISignalRBroadcaster { }
    public interface IGateTradesBroadcaster : ISignalRBroadcaster { }
    public interface IGateCandlesticksBroadcaster : ISignalRBroadcaster { }
    public interface IGatePublicLiquidatesBroadcaster : ISignalRBroadcaster { }
    public interface IGateContractStatsBroadcaster : ISignalRBroadcaster { }
    public interface IGateOrderBookUpdateBroadcaster : ISignalRBroadcaster { }

}