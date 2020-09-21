using PnP.PowerShell.CmdletHelpAttributes;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPPowerShellTelemetryEnabled")]
    public class GetPowerShellTelemetryEnabled : PnPSharePointCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(PnPConnection.CurrentConnection.TelemetryClient != null);
        }
    }
}