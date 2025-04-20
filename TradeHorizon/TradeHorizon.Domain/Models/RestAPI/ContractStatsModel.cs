using System.Text.Json.Serialization;

public class ContractStatsModel
{
    [JsonPropertyName("time")]
    public Int64 UnixTimeStamp { get; set; }
    [JsonPropertyName("mark_price")]
    public decimal? MarketPrice { get; set; }
    [JsonPropertyName("top_long_size")]
    public decimal? TopLongSize { get; set; }
    [JsonPropertyName("top_short_size")]
    public decimal? TopShortSize { get; set; }
    [JsonPropertyName("top_long_account")]
    public int? TopLongAccount { get; set; }
    [JsonPropertyName("top_short_account")]
    public int? TopShortAccount { get; set; }
    [JsonPropertyName("long_taker_size")]
    public decimal? LongTakerSize { get; set; }
    [JsonPropertyName("short_taker_size")]
    public decimal? ShortTakerSize { get; set; }
    [JsonPropertyName("lsr_taker")]
    public decimal? AccountLongShortRatio { get; set; }
    [JsonPropertyName("lsr_account")]
    public decimal? TakerLongShortRatio { get; set; }
    [JsonPropertyName("long_liq_size")]
    public decimal? LongLiquidationSize { get; set; }
    [JsonPropertyName("long_liq_amount")]
    public decimal? LongLiquidationAmountBaseCurrency { get; set; }
    [JsonPropertyName("long_liq_usd")]
    public decimal? LongLiquidationVolumeQuoteCurrency { get; set; }
    [JsonPropertyName("short_liq_size")]
    public decimal? ShortLiquidationSize { get; set; }
    [JsonPropertyName("short_liq_amount")]
    public decimal? ShortLiquidationAmountBaseCurrency { get; set; }
    [JsonPropertyName("short_liq_usd")]
    public decimal? ShortLiquidationVolumeQuoteCurrency { get; set; }
    [JsonPropertyName("open_interest")]
    public decimal? OpenInterestBase { get; set; }
    [JsonPropertyName("open_interest_usd")]
    public decimal? OpenInterestQuote { get; set; }
    [JsonPropertyName("top_lsr_account")]
    public decimal? TopLongShortRatioAccount { get; set; }
    [JsonPropertyName("top_lsr_size")]
    public decimal? TopLongShortRatioPosition { get; set; }
    [JsonPropertyName("long_users")]
    public int? LongUsersNumber { get; set; }
    [JsonPropertyName("short_users")]
    public int? ShortUsersNumber { get; set; }
    public ApiError? ApiErrors { get; set; }
}