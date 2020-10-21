using System;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Utilities.JSON
{
    public class DateTimeISO8601Converter : System.Text.Json.Serialization.JsonConverter<Nullable<DateTime>>
    {
        public override Nullable<DateTime> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value != null)
            {
                return DateTime.Parse(reader.GetString());
            }
            else
            {
                return null;
            }
        }

        public override void Write(Utf8JsonWriter writer, Nullable<DateTime> value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value.Value.ToString("yyyy-MM-ddTHH:mmZ"));
            }
        }
    }
}