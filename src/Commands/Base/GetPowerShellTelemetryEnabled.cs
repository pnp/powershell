using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPPowerShellTelemetryEnabled")]
    [Obsolete("Get-PnPPowerShellTelemetryEnabled is deprecated and will be removed in the next release.")]
    [OutputType(typeof(bool))]
    [Attributes.ApiPermissionsNotRequired(Remarks = "This cmdlet performs no request.")]
    public class GetPowerShellTelemetryEnabled : PnPSharePointCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(false);
        }
    }
}