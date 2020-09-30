using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Features
{
    [Cmdlet(VerbsLifecycle.Disable, "PnPFeature")]
    public class DisableFeature : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public GuidPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public FeatureScope Scope = FeatureScope.Web;

        protected override void ExecuteCmdlet()
        {
            Guid featureId = Identity.Id;

            if (Scope == FeatureScope.Web)
            {
                SelectedWeb.DeactivateFeature(featureId);
            }
            else
            {
                ClientContext.Site.DeactivateFeature(featureId);
            }
        }
    }
}
