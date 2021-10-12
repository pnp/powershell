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
        /// <summary>
        /// Context information detailing the type of message returned
        /// </summary>
        [JsonPropertyName("@odata.context")]
        public string Context { get; set; }

        /// <summary>
        /// NextLink detailing the link to query to fetch the next batch of results
        /// </summary>
        [JsonPropertyName("nextLink")]
        public string NextLink { get; set; }

        /// <summary>
        /// OData NextLink detailing the link to query to fetch the next batch of results
        /// </summary>
        [JsonPropertyName("@odata.nextLink")]
        public string ODataNextLink // { get; set; }
        {
             get { return NextLink; }
             set { NextLink = value; }
        }

        /// <summary>
        /// The items contained in the results
        /// </summary>
        [JsonPropertyName("value")]
        public IEnumerable<T> Items { get; set; }
    }
}
