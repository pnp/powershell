using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsLifecycle.Enable, "PnPPowerShellTelemetry")]
    [Obsolete("Enable-PnPPowerShellTelemetry is deprecated and will be removed in the next release.")]
    [Attributes.ApiPermissionsNotRequired(Remarks = "This cmdlet performs no request.")]
    public class EnablePowerShellTelemetry : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ProcessRecord()
        {
            WriteObject("Telemetry no longer available");
        }
    }
}
