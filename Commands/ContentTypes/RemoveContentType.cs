using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Remove, "PnPContentType")]
    public class RemoveContentType : PnPWebCmdlet
    {

        [Parameter(Mandatory = true, Position=0, ValueFromPipeline=true)]
        public ContentTypePipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue(Resources.RemoveContentType, Resources.Confirm))
            {
                ContentType ct = null;
                if (Identity.ContentType != null)
                {
                    ct = Identity.ContentType;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Identity.Id))
                    {
                        ct = SelectedWeb.GetContentTypeById(Identity.Id);
                    }
                    else if (!string.IsNullOrEmpty(Identity.Name))
                    {
                        ct = SelectedWeb.GetContentTypeByName(Identity.Name);
                    }
                }
                if(ct != null)
                {
                    ct.DeleteObject();
                    ClientContext.ExecuteQueryRetry();
                }

            }
        }
    }
}
