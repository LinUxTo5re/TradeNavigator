namespace TradeHorizon.Domain.Constants
{
    public static class VarConstants
    {
        #region Api-errors
        public const string FromToLimitError = "Provide either both 'from' and 'to' values, or only the 'limit' value — not both. Also check if 'from' is greater than 'to'";
        public const string FromToLimitMissingError = "Please provide either both 'from' and 'to' parameters or the 'limit' parameter.";
        public const string DataNotAvailMsg = "No data available for the symbols.";
        public const string FromLimitError = "Provide either 'from' or the 'limit' value — not both.";
        #endregion

        #region Filter contract default values
        public const long GateVolume24HUsdt = 1000000; // 1M
        public const int LeverageMax = 20; // 20X 
        public const long IndexMarketCap = 50000000; // 50M (from coingecko)
        public const long IndexVolume24HUsdt = 50000000; // 50M (from coingecko)
        #endregion
    }
}