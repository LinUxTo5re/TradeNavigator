using System.Text.Json.Serialization;

public class WebSocketMessage
{
    [JsonPropertyName("time")]
    public long? Time { get; set; }

    [JsonPropertyName("time_ms")]
    public long? TimeMs { get; set; }

    [JsonPropertyName("conn_id")]
    public string? ConnectionId { get; set; }

    [JsonPropertyName("trace_id")]
    public string? TraceId { get; set; }

    [JsonPropertyName("channel")]
    public string? Channel { get; set; }

    [JsonPropertyName("event")]
    public string? Event { get; set; }

    [JsonPropertyName("payload")]
    public List<string>? Payload { get; set; }

    [JsonPropertyName("result")]
    public object? Result { get; set; } // Non-generic, just a placeholder for result data
    
    [JsonPropertyName("error")]
    public ErrorOutput? Error { get; set; }
}

public class ResultData<T>
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    public List<T>? Data {get; set;} = null; // List of obj
    public T? DataArray {get; set; }  // Array inside obj
}

public class ErrorOutput
{
    [JsonPropertyName("code")]
    public int ErrorCode { get; set; } = 0;

    [JsonPropertyName("message")]
    public string? ErrorMessage { get; set; }
}
