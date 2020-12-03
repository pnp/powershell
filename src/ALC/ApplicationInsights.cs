using System;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using System.Collections.Generic;
using System.Linq;

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

            telemetryProperties = new Dictionary<string, string>(10)
            {
                { "ServerLibraryVersion", serverLibraryVersion },
                { "ServerVersion", serverVersion },
                { "ConnectionMethod", initializationType.ToString() },
                { "Version", assemblyVersion },
                { "Platform", "SPO" }
            };
        }
        public void TrackEvent(string cmdletName, Dictionary<string, string> properties = null)
        {
            var localProps = telemetryProperties;
            if (properties != null)
            {
                foreach (var prop in properties)
                {
                    localProps.Add(prop.Key, prop.Value);
                }
            }
            telemetryClient.TrackEvent(cmdletName, localProps);
        }
    }
}
