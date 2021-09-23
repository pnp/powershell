using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Utilities.REST
{
    /// <summary>
    /// Contains a collection of results
    /// </summary>
    /// <typeparam name="T">Model type to map the contents to</typeparam>
    public class RestResultCollection<T>
    {
        [JsonPropertyName("nextLink")]
        public string NextLink { get; set; }

        [JsonPropertyName("@odata.nextLink")]
        private string ODataNextLink
        {
            get { return NextLink; }
            set { NextLink = value; }
        }

        [JsonPropertyName("value")]
        public IEnumerable<T> Items { get; set; }
    }
}
