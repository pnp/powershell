using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Set, "PnPContext")]
    [Attributes.ApiPermissionsNotRequired(Remarks = "This cmdlet replaces the client context held in memory and performs no request.")]
    public class SetContext : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1)]
        public ClientContext Context;

        protected override void ProcessRecord()
        {
            Connection.Context = Context;
        }
    }
}