using System.Text.Json.Serialization;

public class CurrentOI
{
    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }

    [JsonPropertyName("value")]
    public decimal CurrentOIValue { get; set; }

    [JsonPropertyName("update")]
    public long UnixTimeStamp { get; set; }
}
