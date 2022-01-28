using System;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using System.Collections.Generic;
using System.Linq;

namespace PnP.PowerShell.ALC
{
    // implement telemtryclient as a singleton to avoid memory leaks
    public static class TelemetryClientFactory
  {
    private static TelemetryClient _telemetryClient;
 
    public static TelemetryClient GetTelemetryClient()
    {
            
      if (_telemetryClient == null)
      {
        
            TelemetryConfiguration config = TelemetryConfiguration.CreateDefault();
            _telemetryClient = new TelemetryClient(config);
            config.InstrumentationKey = "a301024a-9e21-4273-aca5-18d0ef5d80fb";
            //config..Context.Session.Id = Guid.NewGuid().ToString();
            _telemetryClient.Context.Cloud.RoleInstance = "PnPPowerShell";
            _telemetryClient.Context.Device.OperatingSystem = Environment.OSVersion.ToString();

            

      }
 
      return _telemetryClient;
    }
  }

    public class ApplicationInsights
    {
        private TelemetryClient telemetryClient;
        private static  Dictionary<string, string> telemetryProperties;
        


        public void Initialize(string serverLibraryVersion, string serverVersion, string initializationType, string assemblyVersion, string operatingSystem)
        {
            telemetryClient = TelemetryClientFactory.GetTelemetryClient();
            if (telemetryProperties == null ) {
                telemetryProperties = new Dictionary<string, string>
            {
                { "ServerLibraryVersion", serverLibraryVersion },
                { "ServerVersion", serverVersion },
                { "ConnectionMethod", initializationType.ToString() },
                { "Version", assemblyVersion },
                { "Platform", "SPO" },
                { "OperatingSystem", operatingSystem}
            };
            }
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
