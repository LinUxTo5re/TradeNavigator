namespace TradeHorizon.Domain;

public class OHLCVData
{
        public long Timestamp { get; set; }
        public decimal Open { get; set; } 
        public decimal High { get; set; } 
        public decimal Low { get; set; } 
        public decimal Close { get; set; } 
        public decimal Volume { get; set; }
}
