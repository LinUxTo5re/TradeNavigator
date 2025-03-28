using System.Text.Json.Serialization;

public class FundingRateModel
{
    [JsonPropertyName("r")]
    public string? FundingRate { get; set; }
    [JsonPropertyName("t")]
    public Double? UnixTimeStamp { get; set; }}