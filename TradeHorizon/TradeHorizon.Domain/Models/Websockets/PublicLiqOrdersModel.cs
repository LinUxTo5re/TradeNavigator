using System.Text.Json.Serialization;

public class PublicLiqOrdersModel
{
    [JsonPropertyName("price")]
    public string? Price { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("time")]
    public long TimeMs { get; set; }

    [JsonPropertyName("contract")]
    public string? Contract { get; set; }
}
