using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Features
{
    [Cmdlet(VerbsLifecycle.Enable, "PnPFeature")]
    
    
    
    
    public class EnableFeature : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position=0, ValueFromPipeline=true)]
        public GuidPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public FeatureScope Scope = FeatureScope.Web;

        [Parameter(Mandatory = false)]
        public SwitchParameter Sandboxed;


        protected override void ExecuteCmdlet()
        {
            var featureId = Identity.Id;
            if(Scope == FeatureScope.Web)
            {
                SelectedWeb.ActivateFeature(featureId, Sandboxed);
            }
            else
            {
                ClientContext.Site.ActivateFeature(featureId, Sandboxed);
            }
        }

    }
}
