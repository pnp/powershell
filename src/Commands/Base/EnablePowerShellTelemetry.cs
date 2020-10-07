
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Enable, "PowerShellTelemetry")]
    public class EnablePowerShellTelemetry : PSCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ProcessRecord()
        {
            var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var telemetryFile = System.IO.Path.Combine(userFolder, ".pnppowershelltelemetry");
            if (Force || ShouldContinue("Do you want to enable telemetry for PnP PowerShell?", "Confirm"))
            {
                PnPConnection.CurrentConnection?.InitializeTelemetry(PnPConnection.CurrentConnection.Context, PnPConnection.CurrentConnection.InitializationType);
                System.IO.File.WriteAllText(telemetryFile, "allow");
                WriteObject("Telemetry enabled");
            }
            else
            {
                var enabled = false;
                if (System.IO.File.Exists(telemetryFile))
                {
                    enabled = System.IO.File.ReadAllText(telemetryFile).ToLower() == "allow";
                }
                WriteObject($"Telemetry setting unchanged: currently {(enabled ? "enabled" : "disabled")}");
            }
        }
    }
}