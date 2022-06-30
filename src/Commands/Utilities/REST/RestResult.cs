using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Utilities.REST
{
    /// <summary>
    /// Contains a single result
    /// </summary>
    /// <typeparam name="T">Model type to map the content to</typeparam>
    public class RestResult<T>
    {
        /// <summary>
        /// The content contained in the results
        /// </summary>
        [JsonPropertyName("value")]
        public T Content { get; set; }
    }
}
