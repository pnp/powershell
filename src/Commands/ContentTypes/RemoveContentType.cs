using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Remove, "PnPContentType")]
    public class RemoveContentType : PnPWebCmdlet
    {

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public ContentTypePipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var ct = Identity?.GetContentTypeOrThrow(nameof(Identity), CurrentWeb);
            if (Force || ShouldContinue($"Remove Content Type '{ct.EnsureProperty(c => c.Name)}'?", Resources.Confirm))
            {
                ct.DeleteObject();
                ClientContext.ExecuteQueryRetry();
            }
        }
    }
}
