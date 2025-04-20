using System.Text.Json.Serialization;

public class LiquidationHistory
{
    [JsonPropertyName("history")]
    public List<LongShortData>? longShortData { get; set; }
    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }
}

public class LongShortData 
{
    [JsonPropertyName("l")]
    public decimal? Long { get; set; }
    [JsonPropertyName("s")]
    public decimal? Short { get; set; }
    [JsonPropertyName("r")]
    public decimal? LongShortRatio { get; set; }
    [JsonPropertyName("t")]
    public long? UnixTimeStamp { get; set; }
}