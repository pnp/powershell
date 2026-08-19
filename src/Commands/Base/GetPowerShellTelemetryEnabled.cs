using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPPowerShellTelemetryEnabled")]
    [OutputType(typeof(bool))]
    [Attributes.ApiPermissionsNotRequired(Remarks = "This cmdlet reports a local setting of the current connection and performs no request.")]
    public class GetPowerShellTelemetryEnabled : PnPSharePointCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(Connection.ApplicationInsights != null);
        }
    }
}