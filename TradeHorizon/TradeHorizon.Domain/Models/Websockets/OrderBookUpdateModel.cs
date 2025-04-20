using System.Text.Json.Serialization;

public class OrderBookUpdateModel
{
    [JsonPropertyName("t")]
    public long Time { get; set; }

    [JsonPropertyName("s")]
    public string? Symbol { get; set; }

    [JsonPropertyName("U")]
    public long FirstUpdateId { get; set; }

    [JsonPropertyName("u")]
    public long LastUpdateId { get; set; }

    [JsonPropertyName("b")]
    public List<OrderBookPriceLevel>? Bids { get; set; }

    [JsonPropertyName("a")]
    public List<OrderBookPriceLevel>? Asks { get; set; }
}

public class OrderBookPriceLevel
{
    [JsonPropertyName("p")]
    public string? Price { get; set; }

    [JsonPropertyName("s")]
    public long Size { get; set; }
}
