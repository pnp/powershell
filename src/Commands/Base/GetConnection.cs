using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPConnection")]
    [OutputType(typeof(PnPConnection))]
    [Attributes.ApiPermissionsNotRequired(Remarks = "This cmdlet returns the connection currently held in memory and performs no request.")]
    public class GetPnPConnection : PnPSharePointCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(Connection);
        }
    }
}