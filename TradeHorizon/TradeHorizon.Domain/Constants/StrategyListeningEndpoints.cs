namespace TradeHorizon.Domain.Constants
{
    public static class StrategyListeningEndpoints
    {
        public const string BaseLocalhostUrl = "https://localhost:7001";

        #region Rest api hub
        public const string RestOHLCVHubUrl = $"{BaseLocalhostUrl}/hub/rest/ohlcv";
        #endregion

        #region ws hub
        public const string WsCandlestickHubUrl = $"{BaseLocalhostUrl}/hub/ws/gate-candlesticks";
        #endregion
    }
}