namespace TradeHorizon.Domain.Models.Strategies
{
    public class BreakoutStrategySettingsDto
    {
        public string GroupName { get; set; } = string.Empty;
        public decimal ThresholdPercent { get; set; }
        public int SlidingWindow { get; set; }
        public string PriceSource { get; set; } = string.Empty;
        public bool VolumeConfirmationRequired { get; set; }
        public decimal VolumeMultiplier { get; set; }
    }
}