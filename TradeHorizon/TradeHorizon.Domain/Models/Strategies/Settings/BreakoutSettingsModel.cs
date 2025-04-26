namespace TradeHorizon.Domain.Models.Strategies
{
    public class BreakoutSettingsModel
    {
        public string Contract { get; set; } = "BTC_USDT";
        public decimal ThresholdPercent { get; set; } = 1;
        public int SlidingWindow { get; set; } = 20;
        public PriceSource PriceSourceToCheck { get; set; } = PriceSource.Close;
        public bool VolumeConfirmationRequired { get; set; } = false;
        public decimal VolumeMultiplier { get; set; } = 0.5m;
    }

}