using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Enable, "PnPPowerShellTelemetry")]
    public class EnablePowerShellTelemetry : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ProcessRecord()
        {
            var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var telemetryFile = System.IO.Path.Combine(userFolder, ".pnppowershelltelemetry");
            if (Force || ShouldContinue("Do you want to enable telemetry for PnP PowerShell?", Properties.Resources.Confirm))
            {
                System.IO.File.WriteAllText(telemetryFile, "allow");
                Environment.SetEnvironmentVariable("PNPPOWERSHELL_DISABLETELEMETRY", "false");                
                Connection?.InitializeTelemetry(Connection.Context, Connection.InitializationType);                
                WriteObject("Telemetry enabled");
            }
            else
            {
                var enabled = false;
                if (System.IO.File.Exists(telemetryFile))
                {
                    enabled = System.IO.File.ReadAllText(telemetryFile).ToLower() == "allow" || Environment.GetEnvironmentVariable("PNPPOWERSHELL_DISABLETELEMETRY").Equals("false", StringComparison.InvariantCultureIgnoreCase);
                }
                WriteObject($"Telemetry setting unchanged: currently {(enabled ? "enabled" : "disabled")}");
            }
        }
    }
}
