using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPPowerShellTelemetryEnabled")]
    [OutputType(typeof(bool))]
    public class GetPowerShellTelemetryEnabled : PnPSharePointCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(Connection.ApplicationInsights != null);
        }
    }
}