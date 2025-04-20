using System.Text.Json.Serialization;

public class CandlestickModel
{
    [JsonPropertyName("t")]
    public long Time { get; set; }

    [JsonPropertyName("v")]
    public long Volume { get; set; }

    [JsonPropertyName("c")]
    public string? Close { get; set; }

    [JsonPropertyName("h")]
    public string? High { get; set; }

    [JsonPropertyName("l")]
    public string? Low { get; set; }

    [JsonPropertyName("o")]
    public string? Open { get; set; }

    [JsonPropertyName("a")]
    public string? Amount { get; set; }

    [JsonPropertyName("n")]
    public string? Name { get; set; }
}
