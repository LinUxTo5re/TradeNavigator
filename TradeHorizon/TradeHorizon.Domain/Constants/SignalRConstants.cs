namespace TradeHorizon.Domain.Constants
{
    public static class SignalRConstants
    {
        #region RestAPI group name
        public const string OHLCVGroup = "OHLCVGroup";
        public const string FundingRateGroup = "FundingRateGroup";
        public const string ContractStatsGroup = "ContractStatsGroup";
        public const string OrderBookGroup = "OrderBookGroup";
        public const string LiqOrdersGroup = "LiqOrdersGroup";
        #endregion

        #region RestAPI event name
        public const string ReceiveOHLCV = "ReceiveOHLCV";
        public const string ReceiveFundingRate = "ReceiveFundingRate";
        public const string ReceiveContractStats = "ReceiveContractStats";
        public const string ReceiveOrderBook = "ReceiveOrderBook";
        public const string ReceiveLiqOrders = "ReceiveLiqOrders";

        #endregion 

        #region WS group name
        public const string CandlestickGroupWS = "CandlestickGroupWS";
        public const string ContractStatsGroupWS = "ContractStatsGroupWS";
        public const string OrderBookUpdateGroupWS = "OrderBookUpdateGroupWS";
        public const string PLiqOrdersGroupWS = "PLiqOrdersGroupWS";
        public const string TickerGroupWS = "TickerGroupWS";
        public const string TradesGroupWS = "TradesGroupWS";

        #endregion

        #region WS event name
        public const string ReceiveCandlestickData = "ReceiveCandlestickDataWS";
        public const string ReceiveContractStatsWS = "ReceiveContractStatsWS";
        public const string ReceiveOrderBookUpdateWS = "ReceiveOrderBookUpdateWS";
        public const string ReceivePLiqOrdersWS = "ReceivePLiqOrdersWS";
        public const string ReceiveTickerWS = "ReceiveTickerWS";
        public const string ReceiveTradesWS = "ReceiveTradesWS";
        #endregion
    }
}