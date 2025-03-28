using System.Text.Json.Serialization;

public class OrderBookModel
{
    [JsonPropertyName("id")]
    public Int64? OrderBookID { get; set; }
    [JsonPropertyName("current")]
    public double? CurrentUnixTimestamp { get; set; }
    [JsonPropertyName("update")]
    public double? UpdateUnixTimestamp { get; set; }
    [JsonPropertyName("bids")]
    public List<AsksBidsList>? Bids { get; set; }
    [JsonPropertyName("asks")]
    public List<AsksBidsList>? Asks { get; set; }
}

public class AsksBidsList
{
    [JsonPropertyName("p")]
    public string? AskBidPrice { get; set; }
    [JsonPropertyName("s")]
    public Int64? AskBidSize { get; set; }
}