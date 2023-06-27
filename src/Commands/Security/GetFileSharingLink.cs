using PnP.Framework.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Security
{
    [Cmdlet(VerbsCommon.Get, "PnPFileSharingLink")]
    public class GetFileSharingLink : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string FileUrl;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeUrl = string.Empty;
            var ctx = Connection.PnPContext;

            ctx.Web.EnsureProperties(w => w.ServerRelativeUrl);

            if (!FileUrl.ToLower().StartsWith(ctx.Web.ServerRelativeUrl.ToLower()))
            {
                serverRelativeUrl = UrlUtility.Combine(ctx.Web.ServerRelativeUrl, FileUrl);
            }
            else
            {
                serverRelativeUrl = FileUrl;
            }

            var file = ctx.Web.GetFileByServerRelativeUrl(serverRelativeUrl);

            var sharingLinks = file.GetShareLinks();

            WriteObject(sharingLinks?.RequestedItems, true);
        }
    }
}
