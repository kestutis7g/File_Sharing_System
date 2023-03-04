using System.Text.Json;
using System.Text.Json.Serialization;

namespace Forum.Core.Converters;

public class DateOnlyConverter : JsonConverter<DateOnly?>
{
    private readonly string serializationFormat;

    public DateOnlyConverter() : this(null)
    {
    }

    public DateOnlyConverter(string? serializationFormat)
    {
        this.serializationFormat = serializationFormat ?? "yyyy-MM-dd";
    }

    public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value != null ? DateOnly.Parse(value!) : null;
    }

    public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
    {
        if (value != null)
        {
            writer.WriteStringValue(value.Value.ToString(serializationFormat));
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
