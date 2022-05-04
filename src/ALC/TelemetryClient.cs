using System;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;

namespace PnP.PowerShell.ALC
{
    /// <summary>
    /// Telemetry client instance implemented as a singleton to avoid memory leaks
    /// </summary>
    public static class TelemetryClientFactory
    {
        /// <summary>
        /// Singleton instance of the TelemetruClient
        /// </summary>
        private static TelemetryClient _telemetryClient;

        /// <summary>
        /// Gets an instance of the telemetry client to use. Creates a new instance if not already present in memory or otherwise re-uses the one available in memory.
        /// </summary>
        /// <returns>TelemetryClient instance</returns>
        public static TelemetryClient GetTelemetryClient()
        {
            // If we already have an instance, return it
            if (_telemetryClient != null) return _telemetryClient;

            // Create a new telemetry instance
            TelemetryConfiguration config = TelemetryConfiguration.CreateDefault();
            
            _telemetryClient = new TelemetryClient(config);
            config.InstrumentationKey = "a301024a-9e21-4273-aca5-18d0ef5d80fb";
            _telemetryClient.Context.Cloud.RoleInstance = "PnPPowerShell";
            _telemetryClient.Context.Device.OperatingSystem = Environment.OSVersion.ToString();

            return _telemetryClient;
        }
    }
}