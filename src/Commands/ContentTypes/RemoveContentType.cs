using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.ContentTypes
{
    [Cmdlet(VerbsCommon.Remove, "PnPContentType")]
    public class RemoveContentType : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ArgumentCompleter(typeof(ContentTypeCompleter))]
        public ContentTypePipeBind Identity;


        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var ct = Identity?.GetContentTypeOrThrow(nameof(Identity), Connection.PnPContext);
            if (Force || ShouldContinue($"Remove Content Type '{ct.Name}'?", Resources.Confirm))
            {
                ct.Delete();
            }
        }
    }
}
