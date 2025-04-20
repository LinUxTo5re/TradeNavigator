namespace TradeHorizon.Domain.Constants
{
    public class VarConstants
    {
        #region Api-errors
        public const string FromToLimitError = "Provide either both 'from' and 'to' values, or only the 'limit' value — not both. Also check if 'from' is greater than 'to'";
        public const string FromToLimitMissingError = "Please provide either both 'from' and 'to' parameters or the 'limit' parameter.";
        public const string DataNotAvailMsg = "No data available for the symbols.";
        public const string FromLimitError = "Provide either 'from' or the 'limit' value — not both.";
        #endregion
    }
}