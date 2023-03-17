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
        public string Name { get; set; }
        /// <summary>
        /// Unique identifier of this app. Use <see cref="Properties.displayName" /> instead to see the friendly name of the app as shown through apps.powerapps.com.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Type of object, typically Microsoft.PowerApps/apps
        /// </summary>
        public string Type { get; set; }
        public PowerAppTags Tags { get; set; }
        /// <summary>
        /// Additional information on the App
        /// </summary>
        [JsonPropertyName("properties")]
        public PowerAppProperties Properties { get; set; }
        /// <summary>
        /// Location of the app
        /// </summary>
        public string AppLocation { get; set; }
        /// <summary>
        /// Flag if the app is component library or normal app
        /// </summary>
        public bool IsAppComponentLibrary { get; set; }
        /// <summary>
        /// Type of App - CanvasClassicApp/AppComponentLibrary
        /// </summary>
        public string AppType { get; set; }
    }
}