using System.Text.Json.Serialization;

public class OHLCVModel
{
    [JsonPropertyName("o")]
    public string? OpenPrice { get; set; }
    [JsonPropertyName("h")]
    public string? HighPrice { get; set; }
    [JsonPropertyName("l")]
    public string? LowPrice { get; set; }
    [JsonPropertyName("c")]
    public string? ClosePrice { get; set; }
    [JsonPropertyName("v")]
    public Int64? Volume { get; set; }
    [JsonPropertyName("t")]
    public Double? UnixTimeStamp { get; set; }
    [JsonPropertyName("sum")]
    public string? Sum { get; set; }
}