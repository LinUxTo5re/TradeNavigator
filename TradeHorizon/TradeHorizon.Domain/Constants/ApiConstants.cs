namespace TradeHorizon.Domain.Constants
{
    public static class ApiConstants
    {
        public const string CoinalyzeAPIKEY = "249f56f8-e4b1-4bf4-84d9-45e2843b1194";
        public const string CoingeckoAPIKEY = "CG-So7529GipNnGVpAgVoWbeYj9";
        public const string ContractSettle = "usdt";

        #region coinalyze
        public const string CoinalyzeBaseUrl = "https://api.coinalyze.net/v1/";
        public const string CurrentOIUrl = "open-interest";
        public const string HistoricalOIUrl = "open-interest-history";
        public const string CurrentFundingRateUrl = "funding-rate";
        public const string CurrentPredictedFundingRateUrl = "predicted-funding-rate";
        public const string HistoricalFundingRateUrl = "funding-rate-history";
        public const string HistoricalPredictedFundingRateUrl = "predicted-funding-rate-history";
        public const string LiquidationHistoryUrl = "liquidation-history";
        public const string LongShortRationUrl = "long-short-ratio-history";
        #endregion

        #region gate.io
        public const string GateIoPrefixUrl = "/api/v4";
        public const string GateIoBaseUrl = $"https://api.gateio.ws{GateIoPrefixUrl}";
        public const string GateIoFuturesCandlesticksUrl = $"/futures/{ContractSettle}/candlesticks";
        public const string GateIoFuturesFundingRateUrl = $"/futures/{ContractSettle}/funding_rate";
        public const string GateIoFuturesContractStatsUrl = $"/futures/{ContractSettle}/contract_stats"; // LSR/OI
        public const string GateIoFuturesLiqOrdersUrl = $"/futures/{ContractSettle}/liq_orders";
        public const string GateIoFuturesTradeBookUrl = $"/futures/{ContractSettle}/trades";
        public const string GateIoFuturesOrderBookUrl = $"/futures/{ContractSettle}/order_book";
        public const string GateIoTickersListUrl = $"/futures/{ContractSettle}/tickers";
        public const string GateIoContractsListUrl = $"/futures/{ContractSettle}/contracts";
        #endregion

        #region  gate.io limit
        public const int GateIoOHLCVHistLimit = 2000;
        public const int GateIoFundingRateHistLimit = 1000;
        public const int GateIoContractStatsLimit = 100;
        public const int GateIoOrderBookLimit = 300;
        public const int GateIoLiqOrdersLimit = 1000;
        #endregion

        #region coingecko
        public const string CoingeckoMarketsList = "https://api.coingecko.com/api/v3/coins/markets";
        #endregion
    }
}
