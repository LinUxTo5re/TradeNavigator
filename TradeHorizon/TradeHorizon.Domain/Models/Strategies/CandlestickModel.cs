namespace TradeHorizon.Domain.Models.Strategies
{
    public class Candlestick
    {
        public long Timestamp { get; set; }
        public decimal Open { get; set; } 
        public decimal High { get; set; } 
        public decimal Low { get; set; } 
        public decimal Close { get; set; } 
        public decimal Volume { get; set; }
        public string? Symbol { get; set; }
    }
}
