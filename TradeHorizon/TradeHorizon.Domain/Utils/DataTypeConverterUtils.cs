using System.Text.Json;
using System.Text.Json.Serialization;

public class StringToIntConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String && int.TryParse(reader.GetString(), out var value))
        {
            return value;
        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetInt32();
        }
        throw new JsonException($"Unexpected token parsing int. Token: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options) =>
        writer.WriteNumberValue(value);
}

public class StringToDecimalConverter : JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String && decimal.TryParse(reader.GetString(), out var value))
        {
            return value;
        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetDecimal();
        }
        throw new JsonException($"Unexpected token parsing decimal. Token: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options) =>
        writer.WriteNumberValue(value);
}

public class StringToLongConverter : JsonConverter<long>
{
    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String && long.TryParse(reader.GetString(), out var value))
        {
            return value;
        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetInt64();
        }
        throw new JsonException($"Unexpected token parsing long. Token: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options) =>
        writer.WriteNumberValue(value);
}

public class StringToDoubleConverter : JsonConverter<double>
{
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String && double.TryParse(reader.GetString(), out var value))
        {
            return value;
        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetDouble();
        }
        throw new JsonException($"Unexpected token parsing Double. Token: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options) =>
        writer.WriteNumberValue(value);
}

public class DecimalToNullableLongConverter : JsonConverter<long?>
{
    public override long? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        if (reader.TokenType == JsonTokenType.Number)
        {
            if (reader.TryGetDecimal(out decimal decimalValue))
            {
                return (long)decimalValue;
            }
        }

        if (reader.TokenType == JsonTokenType.String)
        {
            var str = reader.GetString();
            if (decimal.TryParse(str, out decimal decimalValue))
            {
                return (long)decimalValue;
            }
        }

        throw new JsonException($"Unexpected token parsing long. Token: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, long? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteNumberValue(value.Value);
        else
            writer.WriteNullValue();
    }
}