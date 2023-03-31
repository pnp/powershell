using Microsoft.ApplicationInsights;
using System.Collections.Generic;

namespace PnP.PowerShell.ALC
{
    /// <summary>
    /// Azure Application Insights Telemetry Client to allow for usage tracking
    /// </summary>
    public class ApplicationInsights
    {
        /// <summary>
        /// Instance of the TelemetryClient to use to report back statistical data
        /// </summary>
        private TelemetryClient _telemetryClient;

        /// <summary>
        /// Information to provide with each telemetry log entry
        /// </summary>
        private static Dictionary<string, string> telemetryProperties;
        
        /// <summary>
        /// Initializes a new instance of the ApplicationInsights telemetry
        /// </summary>
        /// <param name="serverLibraryVersion">Version of SharePoint Online to which a connection has been established</param>
        /// <param name="serverVersion">Version of the server to which a connection has been established</param>
        /// <param name="initializationType">Information on what method has been used to establish a connection</param>
        /// <param name="assemblyVersion">The PnP PowerShell version in use</param>
        /// <param name="operatingSystem">The operating system on which PnP PowerShell is being used</param>
        public void Initialize(string serverLibraryVersion, string serverVersion, string initializationType, string assemblyVersion, string operatingSystem, string psVersion = "")
        {
            // Retrieve an instance of the telemetry client to use
            _telemetryClient = TelemetryClientFactory.GetTelemetryClient();

            // Define the base set of information to log with each trackable event in Azure Application Insights
            if (telemetryProperties == null)
            {
                telemetryProperties = new Dictionary<string, string>
                {
                    { "ServerLibraryVersion", serverLibraryVersion },           // Version of SharePoint Online to which a connection has been established
                    { "ServerVersion", serverVersion },                         // Version of the server to which a connection has been established
                    { "ConnectionMethod", initializationType.ToString() },      // Information on what method has been used to establish a connection
                    { "Version", assemblyVersion },                             // The PnP PowerShell version in use
                    { "Platform", "SPO" },                                      // Platform to which the connection has been made
                    { "OperatingSystem", operatingSystem},                       // The operating system on which PnP PowerShell is being used
                    { "PSVersion", psVersion}
                };
            }
        }

        /// <summary>
        /// Sends information to Azure Application Insights to track an event
        /// </summary>
        /// <param name="cmdletName">Name of the PnP PowerShell cmdlet that is being executed</param>
        /// <param name="properties">Additional information on the cmdlet being executed. Optional.</param>
        public void TrackEvent(string cmdletName, Dictionary<string, string> properties = null)
        {
            // Take the base set of information to log
            var localProps = telemetryProperties;

            // If additional properties have been provided to log, add them to the base set of information to log
            if (properties != null)
            {
                foreach (var prop in properties)
                {
                    localProps.Add(prop.Key, prop.Value);
                }
            }

            // Create the logging entry
            _telemetryClient.TrackEvent(cmdletName, localProps);
        }
    }
}