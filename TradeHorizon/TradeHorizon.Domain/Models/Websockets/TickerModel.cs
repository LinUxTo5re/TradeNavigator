using System.Text.Json.Serialization;

public class TickerModel
{
    [JsonPropertyName("contract")]
    public string? Contract { get; set; }

    [JsonPropertyName("last")]
    public string? Last { get; set; }

    [JsonPropertyName("change_percentage")]
    public string? ChangePercentage { get; set; }

    [JsonPropertyName("total_size")]
    public string? TotalSize { get; set; }

    [JsonPropertyName("volume_24h")]
    public string? Volume24h { get; set; }

    [JsonPropertyName("volume_24h_base")]
    public string? Volume24hBase { get; set; }

    [JsonPropertyName("volume_24h_quote")]
    public string? Volume24hQuote { get; set; }

    [JsonPropertyName("volume_24h_settle")]
    public string? Volume24hSettle { get; set; }

    [JsonPropertyName("mark_price")]
    public string? MarkPrice { get; set; }

    [JsonPropertyName("funding_rate")]
    public string? FundingRate { get; set; }

    [JsonPropertyName("funding_rate_indicative")]
    public string? FundingRateIndicative { get; set; }

    [JsonPropertyName("index_price")]
    public string? IndexPrice { get; set; }

    [JsonPropertyName("quanto_base_rate")]
    public string? QuantoBaseRate { get; set; }

    [JsonPropertyName("low_24h")]
    public string? Low24h { get; set; }

    [JsonPropertyName("high_24h")]
    public string? High24h { get; set; }

    [JsonPropertyName("price_type")]
    public string? PriceType { get; set; }

    [JsonPropertyName("change_from")]
    public string? ChangeFrom { get; set; }
}
