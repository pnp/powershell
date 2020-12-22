using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PropertyBagPipeBind
    {
        private readonly Hashtable _hashtable;
        private string _jsonString;
        private JsonElement _jsonObject;

        public PropertyBagPipeBind(Hashtable hashtable)
        {
            _hashtable = hashtable;
            _jsonString = null;
            _jsonObject = default;
        }

        public PropertyBagPipeBind(string json)
        {
            _hashtable = null;
            _jsonString = json;
            _jsonObject = JsonDocument.Parse(json).RootElement;
        }

        public string Json => _jsonString;


        public Hashtable Properties => _hashtable;

        public override string ToString() => Json ?? HashtableToJsonString(_hashtable);

        private string HashtableToJsonString(Hashtable hashtable)
        {
            var container = new Dictionary<string, object>();

            foreach (var key in hashtable.Keys)
            {
                var rawValue = hashtable[key];

                // To ensure the value is not serialized as PSObject
                object value = rawValue is PSObject
                    ? ((PSObject)rawValue).BaseObject
                    : rawValue;

                container.Add(key.ToString(), value);
            }
            return JsonSerializer.Serialize(container, new JsonSerializerOptions() { WriteIndented = false });
        }

    }
}