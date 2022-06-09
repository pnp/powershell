using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPConnection")]
    [OutputType(typeof(PnPConnection))]
    public class GetPnPConnection : PnPSharePointCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(Connection);
        }
    }
}