using PnP.Core.Model.Security;
using PnP.Core.Model.SharePoint;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Security
{
    [Cmdlet(VerbsCommon.Get, "PnPFileSharingLink")]
    [OutputType(typeof(IGraphPermissionCollection))]
    public class GetFileSharingLink : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public FilePipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            IFile file = Identity.GetCoreFile(Connection.PnPContext, this);

            LogDebug("Retrieving file sharing details from Microsoft Graph");
            var sharingLinks = file?.GetShareLinks();

            WriteObject(sharingLinks?.RequestedItems, true);
        }
    }
}
