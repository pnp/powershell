using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Utilities.JSON
{
    public static class Convert
    {
        public static PSObject ConvertToPSObject(string jsonString)
        {
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(jsonString);
            return ConvertToPSObject(jsonElement);
        }

        public static PSObject ConvertToPSObject(JsonElement element) => ConvertToPSObject(element, null);

        public static PSObject ConvertToPSObject(JsonElement element, string jsonPropertyName)
        {
            var value = ConvertToObject(element);
            if (!string.IsNullOrEmpty(jsonPropertyName))
            {
                var pso = new PSObject();
                pso.Properties.Add(new PSNoteProperty(jsonPropertyName, value));
                return pso;
            }
            if (value is PSObject)
            {
                return (PSObject)value;
            }
            if(value is List<object>)
            {
                var pso = new PSObject();
                pso.Properties.Add(new PSNoteProperty("Values", value));
                return pso;
            }
            throw new FormatException($"primitive type[{element.ValueKind}] can not be converted to PSObject");
        }

        public static object ConvertToObject(string jsonString)
        {
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(jsonString);
            return ConvertToObject(jsonElement);
        }

        public static object ConvertToObject(JsonElement element)
        {
            object value = null;
            switch (element.ValueKind)
            {
                case JsonValueKind.Array:
                    {
                        value = ConvertToPSObjectArray(element);
                        break;
                    }
                case JsonValueKind.True:
                case JsonValueKind.False:
                    {
                        value = element.GetBoolean();
                        break;
                    }
                case JsonValueKind.String:
                    {
                        value = element.GetString();
                        break;
                    }
                case JsonValueKind.Object:
                    {
                        var nestedPso = new PSObject();
                        foreach (var prop in element.EnumerateObject())
                        {
                            var propValue = ConvertToObject(prop.Value);
                            nestedPso.Properties.Add(new PSNoteProperty(prop.Name, propValue));
                        }
                        value = nestedPso;
                        break;
                    }
                case JsonValueKind.Number:
                    {
                        if (element.TryGetInt64(out long valLong))
                        {
                            value = valLong;
                        }
                        else if (element.TryGetDouble(out double valDouble))
                        {
                            value = valDouble;
                        }
                        break;
                    }
            }

            return value;
        }

        private static List<object> ConvertToPSObjectArray(JsonElement element)
        {
            var list = new List<object>();

            foreach (var subelement in element.EnumerateArray())
            {
                var value = ConvertToObject(subelement);
                list.Add(value);
            }
            return list;
        }
    }
}