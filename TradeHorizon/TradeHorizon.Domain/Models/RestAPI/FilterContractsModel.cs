using System.Text.Json.Serialization;

public class CoinGeckoModel
{
    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }

    [JsonPropertyName("market_cap")]
    [JsonConverter(typeof(DecimalToNullableLongConverter))]
    public long? IndexMarketCap { get; set; }

    [JsonPropertyName("total_volume")]
    [JsonConverter(typeof(DecimalToNullableLongConverter))]
    public long? IndexVolume24HUsdt { get; set; }
}

public class GateTickerModel
{
    [JsonPropertyName("contract")]
    public string? Contract { get; set; }

    [JsonPropertyName("volume_24h_quote")]
    [JsonConverter(typeof(StringToLongConverter))]
    public long? GateVolume24HUsdt { get; set; }
}

public class GateContractModel
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("is_pre_market")]
    public bool? IsPreMarket { get; set; }
    [JsonPropertyName("in_delisting")]
    public bool? InDelisting { get; set; }

    [JsonPropertyName("leverage_max")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int? LeverageMax { get; set; }

    [JsonPropertyName("mark_price")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? MarkPrice { get; set; }

    [JsonPropertyName("funding_rate")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? FundingRate { get; set; }

    [JsonPropertyName("short_users")]
    public int? ShortUsers { get; set; }
    [JsonPropertyName("long_users")]
    public int? LongUsers { get; set; }
}

public class FilteredContractModel
{
    public long? IndexMarketCap { get; set; }
    public long? IndexVolume24hUsdt { get; set; }
    public decimal? Price { get; set; }
    public string? Contract { get; set; }
    public decimal? FundingRate { get; set; }
    public int? ShortUsers { get; set; }
    public int? LongUsers { get; set; }
    public long? GateVolume24hUsdt { get; set; }
}