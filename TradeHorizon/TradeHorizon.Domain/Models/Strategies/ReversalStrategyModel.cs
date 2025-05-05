using TradeHorizon.Domain.Models.Strategies;

public class ReversalStrategyModel
{
    public LiqOrders? LiquidatedOrders { get; set; }
    public Candlestick? WickCandlestick { get; set; }
}