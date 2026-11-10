using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Disable, "PnPPowerShellTelemetry")]
    [Obsolete("Disable-PnPPowerShellTelemetry is deprecated and will be removed in the next release.")]
    [Attributes.ApiPermissionsNotRequired(Remarks = "This cmdlet performs no request.")]
    public class DisablePowerShellTelemetry : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ProcessRecord()
        {
            WriteObject("Telemetry disabled");
        }
    }
}
