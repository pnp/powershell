using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    /// <summary>
    /// Contains information on one Microsoft Power Apps App
    /// </summary>
    public class PowerApp
    {
        /// <summary>
        /// Name of the app as its GUID
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Unique identifier of this app. Use <see cref="Properties.displayName" /> instead to see the friendly name of the app as shown through apps.powerapps.com.
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// Type of object, typically Microsoft.PowerApps/apps
        /// </summary>
        public string type { get; set; }
        public Tags tags { get; set; }
        /// <summary>
        /// Additional information on the App
        /// </summary>
        [JsonPropertyName("properties")]
        public PowerAppProperties properties { get; set; }
        /// <summary>
        /// Location of the app
        /// </summary>
        public string appLocation { get; set; }
        /// <summary>
        /// Flag if the app is component library or normal app
        /// </summary>
        public bool isAppComponentLibrary { get; set; }
        /// <summary>
        /// Type of App - CanvasClassicApp/AppComponentLibrary
        /// </summary>
        public string appType { get; set; }
    }
}