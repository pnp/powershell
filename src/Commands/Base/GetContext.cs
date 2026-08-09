using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPContext")]
    [OutputType(typeof(Microsoft.SharePoint.Client.ClientContext))]
    [Attributes.ApiPermissionsNotRequired(Remarks = "This cmdlet returns the client context currently held in memory and performs no request.")]
    public class GetSPOContext : PnPSharePointCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(Connection.Context);
        }
    }
}