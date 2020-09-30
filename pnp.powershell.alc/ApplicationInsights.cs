using System;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using System.Collections.Generic;

namespace PnP.PowerShell.ALC
{
    public class ApplicationInsights
    {
        private TelemetryClient telemetryClient;
        private Dictionary<string, string> telemetryProperties;

        public void Initialize(string serverLibraryVersion, string serverVersion, string initializationType, string assemblyVersion)
        {
            TelemetryConfiguration config = TelemetryConfiguration.CreateDefault();
            telemetryClient = new TelemetryClient(config);
            config.InstrumentationKey = "a301024a-9e21-4273-aca5-18d0ef5d80fb";
            //config..Context.Session.Id = Guid.NewGuid().ToString();
            telemetryClient.Context.Cloud.RoleInstance = "PnPPowerShell";
            telemetryClient.Context.Device.OperatingSystem = Environment.OSVersion.ToString();

            telemetryProperties = new Dictionary<string, string>(10);
            telemetryProperties.Add("ServerLibraryVersion", serverLibraryVersion);
            telemetryProperties.Add("ServerVersion", serverVersion);
            telemetryProperties.Add("ConnectionMethod", initializationType.ToString());
            telemetryProperties.Add("Version", assemblyVersion);
            telemetryProperties.Add("Platform", "SPO");
        }
        public void TrackEvent(string cmdletName)
        {
            telemetryClient.TrackEvent(cmdletName, telemetryProperties);
        }
    }
}
