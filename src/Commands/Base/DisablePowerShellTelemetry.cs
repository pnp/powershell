using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Disable, "PnPPowerShellTelemetry")]
    public class DisablePowerShellTelemetry : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ProcessRecord()
        {
            var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var telemetryFile = System.IO.Path.Combine(userFolder, ".pnppowershelltelemetry");
            if (Force || ShouldContinue("Do you want to disable telemetry for PnP PowerShell?", Properties.Resources.Confirm))
            {
                System.IO.File.WriteAllText(telemetryFile, "disallow");
                Environment.SetEnvironmentVariable("PNPPOWERSHELL_DISABLETELEMETRY", "true");
                if (Connection != null)
                {
                    Connection.ApplicationInsights = null;
                }
                WriteObject("Telemetry disabled");
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
