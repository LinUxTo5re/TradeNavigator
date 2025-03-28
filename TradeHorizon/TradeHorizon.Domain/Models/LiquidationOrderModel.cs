using System.Text.Json.Serialization;

public class LiquidationOrderModel
{
    [JsonPropertyName("time")]
    public double? UnixTimestamp { get; set; }
    [JsonPropertyName("contract")]
    public string? FuturesContract { get; set; }
    [JsonPropertyName("size")]
    public Int64? PositionSize { get; set; }
    [JsonPropertyName("order_size")]
    public Int64? LiquidationOrderSize { get; set; }
    [JsonPropertyName("order_price")]
    public string? LiquidationOrderPrice { get; set; }
    [JsonPropertyName("fill_price")]
    public string? LiquidationFillPrice { get; set; }
    [JsonPropertyName("left")]
    public Int64? LeftLiquidationSize { get; set; }
}