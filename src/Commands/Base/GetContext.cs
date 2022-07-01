using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPContext")]
    [OutputType(typeof(Microsoft.SharePoint.Client.ClientContext))]
    public class GetSPOContext : PnPSharePointCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(Connection.Context);
        }
    }
}