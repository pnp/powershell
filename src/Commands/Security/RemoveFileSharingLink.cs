using Microsoft.Office.SharePoint.Tools;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Properties;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Security
{
    [Cmdlet(VerbsCommon.Remove, "PnPFileSharingLink")]
    [OutputType(typeof(void))]
    public class RemoveFileSharingLink : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string FileUrl;

        [Parameter(Mandatory = false)]
        public string Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

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

            if (sharingLinks?.RequestedItems != null && sharingLinks.Length > 0)
            {
                if (ParameterSpecified(nameof(Identity)) && !string.IsNullOrEmpty(Identity))
                {
                    var link = sharingLinks.Where(s => s.Id == Identity).FirstOrDefault();
                    if (link != null)
                    {
                        if (Force || ShouldContinue($"Remove Sharing Link with ID {Identity} ?", Resources.Confirm))
                        {
                            link.DeletePermission();
                        }
                    }
                    else
                    {
                        throw new PSArgumentException($"Sharing link with ID {Identity} not found");
                    }
                }
                else
                {
                    if (Force || ShouldContinue($"Remove all sharing links associated with the file ?", Resources.Confirm))
                    {
                        file.DeleteShareLinks();
                    }
                }
            }
            else
            {
                throw new PSArgumentException("No sharing links were found for the specified file");
            }
        }
    }
}
