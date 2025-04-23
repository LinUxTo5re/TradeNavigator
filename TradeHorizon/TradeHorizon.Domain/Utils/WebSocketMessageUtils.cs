using System.Text.Json;

public static class WebSocketMessageDeserializer
{
    public static WebSocketMessage? DeserializeWithResultData<T>(string json) // json to c# object conversion
    {
        ResultData<T>? resultData = new();
        try
        {
            var webSocketMessage = JsonSerializer.Deserialize<WebSocketMessage>(json);
            JsonElement root = JsonSerializer.Deserialize<JsonElement>(json);

            if (root.TryGetProperty("result", out JsonElement resultElement))
            {
                if (resultElement.ValueKind == JsonValueKind.Object) // for status 
                {
                    resultData = JsonSerializer.Deserialize<JsonElement>(resultElement.GetRawText()).TryGetProperty("status", out JsonElement checkStatusElement) ?
                        JsonSerializer.Deserialize<ResultData<T>>(resultElement.GetRawText()) // normal object
                        :
                        resultData = new ResultData<T>
                        {
                            DataArray = JsonSerializer.Deserialize<T>(resultElement.GetRawText()),
                            Data = null,
                            Status = null
                        }; // array inside object 

                }
                else if (resultElement.ValueKind == JsonValueKind.Array)
                {
                    resultData = new ResultData<T>// List of obj
                    {
                        Status = null,
                        Data = JsonSerializer.Deserialize<List<T>>(resultElement.GetRawText())
                    };
                }
                if (webSocketMessage is not null)
                    webSocketMessage.Result = resultData;
            }
            return webSocketMessage;
        }
        catch (Exception ex)
        {
            return new WebSocketMessage
            {
                Result = new ResultData<T>
                {
                    Status = $"EXCEPTION: {ex}"
                }
            };
        }
    }
}
