namespace TradeHorizon.Domain.Models.Strategies
{
    public class BreakoutStrategySettingsDto
    {
        public string Contract {get; set; } = "BTC_USDT";
        public string Timeframe { get; set; } = "5m";
        public string GroupName { get; set; } = string.Empty;
        public decimal ThresholdPercent { get; set; } = 1;
        public int SlidingWindow { get; set; } = 20;
        public string PriceSource { get; set; } = "Close";
        public bool VolumeConfirmationRequired { get; set; } = false;
        public decimal VolumeMultiplier { get; set; } = 0.5m;
    }
}