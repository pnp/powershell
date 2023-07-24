using Microsoft.Office.SharePoint.Tools;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Properties;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Security
{
    [Cmdlet(VerbsCommon.Remove, "PnPFolderSharingLink")]
    [OutputType(typeof(void))]
    public class RemoveFolderSharingLink : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public FolderPipeBind Folder;

        [Parameter(Mandatory = false)]
        public string Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeUrl = string.Empty;
            var ctx = Connection.PnPContext;

            ctx.Web.EnsureProperties(w => w.ServerRelativeUrl);

            var folder = Folder.GetFolder(ctx);

            var sharingLinks = folder.GetShareLinks();

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
                    if (Force || ShouldContinue($"Remove all sharing links associated with the folder ?", Resources.Confirm))
                    {
                        folder.DeleteShareLinks();
                    }
                }
            }
            else
            {
                throw new PSArgumentException("No sharing links were found for the specified folder");
            }
        }
    }
}
