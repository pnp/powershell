using PnP.Framework.Graph.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public class GraphAdditionalHeadersPipeBind
    {
        private Dictionary<string, string> Headers;

        public GraphAdditionalHeadersPipeBind(Hashtable hashtable)
        {
            Headers = hashtable.Cast<DictionaryEntry>().ToDictionary(kvp => (string)kvp.Key, kvp => (string)kvp.Value);
        }

        public GraphAdditionalHeadersPipeBind(Dictionary<string, string> dictionary)
        {
            Headers = dictionary;
        }

        internal Dictionary<string, string> GetHeaders(bool consistencyLevelEventualPresent)
        {
            if (consistencyLevelEventualPresent)
            {
                if (Headers == null)
                {
                    Headers = new Dictionary<string, string>();
                }
                Headers.Remove("ConsistencyLevel");
                Headers.Add("ConsistencyLevel", "eventual");
            }
            return Headers;
        }
    }
}
