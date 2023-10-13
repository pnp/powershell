using System;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Utilities.Json
{
    internal sealed class MicrosoftSearchExternalItemPropertyConverter: JsonConverter<Hashtable>
    {
        public override Hashtable Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var converter = (JsonConverter<Hashtable>)options.GetConverter(typeof(Hashtable));
            return converter.Read(ref reader, typeToConvert, options);
        }

        public override void Write(Utf8JsonWriter writer, Hashtable hashtable, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (DictionaryEntry value in hashtable)
            {
                switch (value.Value)
                {
                    case string:
                        writer.WritePropertyName($"{value.Key}@odata.type");
                        writer.WriteStringValue("String");

                        writer.WritePropertyName(value.Key.ToString());
                        writer.WriteStringValue(value.Value.ToString());
                        break;

                    case DateTime dateTime:
                        writer.WritePropertyName($"{value.Key}@odata.type");
                        writer.WriteStringValue("DateTimeOffset");

                        writer.WritePropertyName(value.Key.ToString());
                        writer.WriteRawValue($"\"{dateTime:o}\"");
                        break;

                    case IEnumerable ieNumerable:
                        writer.WritePropertyName($"{value.Key}@odata.type");
                        writer.WriteStringValue("Collection(String)");

                        writer.WritePropertyName(value.Key.ToString());
                        writer.WriteStartArray();
                        foreach (object item in ieNumerable)
                        {
                            writer.WriteStringValue(item.ToString());
                        }
                        writer.WriteEndArray();
                        break;

                    default:
                        writer.WritePropertyName(value.Key.ToString());
                        writer.WriteStringValue(value.Value.ToString());
                        break;
                }
            }
            writer.WriteEndObject();
        }
    }
}
