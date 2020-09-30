
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ClientSidePages
{
    [Cmdlet(VerbsCommon.Get, "PnPClientSidePage")]
    public class GetClientSidePage : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public ClientSidePagePipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var clientSidePage = Identity.GetPage((Microsoft.SharePoint.Client.ClientContext)SelectedWeb.Context);

            if (clientSidePage == null)
                throw new Exception($"Page '{Identity?.Name}' does not exist");

            WriteObject(clientSidePage);
        }
    }
}