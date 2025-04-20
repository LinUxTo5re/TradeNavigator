using System.Text.Json.Serialization;

public class TradesModel
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("create_time")]
    public long CreateTime { get; set; }

    [JsonPropertyName("create_time_ms")]
    public long CreateTimeMs { get; set; }

    [JsonPropertyName("price")]
    public string? Price { get; set; }

    [JsonPropertyName("contract")]
    public string? Contract { get; set; }
}
