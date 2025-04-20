using System.Text.Json.Serialization;

public class OHLCVData
{
    [JsonPropertyName("history")]
    public List<OHLCVTData>? oHLCVDatas { get; set; }
    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }
}

public class OHLCVTData
{
    [JsonPropertyName("o")]
    public decimal? OpenPrice { get; set; }
    [JsonPropertyName("h")]
    public decimal? HighPrice { get; set; }
    [JsonPropertyName("l")]
    public decimal? LowPrice { get; set; }
    [JsonPropertyName("c")]
    public decimal? ClosePrice { get; set; }
    [JsonPropertyName("v")]
    public decimal? Volume { get; set; }
    [JsonPropertyName("t")]
    public long? UnixTimeStamp { get; set; }
}

