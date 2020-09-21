using PnP.PowerShell.CmdletHelpAttributes;
using System;
#if PNPPSCORE
using System.IdentityModel.Tokens.Jwt;
#else
using System.IdentityModel.Tokens;
#endif
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Disable, "PnPPowerShellTelemetry")]
    public class DisablePowerShellTelemetry : PSCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ProcessRecord()
        {
            var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var telemetryFile = System.IO.Path.Combine(userProfile, ".pnppowershelltelemetry");
            if (Force || ShouldContinue("Do you want to disable telemetry for PnP PowerShell?", "Confirm"))
            {
                System.IO.File.WriteAllText(telemetryFile, "disallow");
                if (PnPConnection.CurrentConnection != null)
                {
                    PnPConnection.CurrentConnection.TelemetryClient = null;
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