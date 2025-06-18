using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Utilities.REST
{
    /// <summary>
    /// Contains a collection inside a Rows response
    /// </summary>
    /// <typeparam name="T">Model type to map the contents to</typeparam>
    public class RestRowCollection<T>
    {
        /// <summary>
        /// The items contained in the results
        /// </summary>
        [JsonPropertyName("Row")]
        public IEnumerable<T> Items { get; set; }
    }
}
