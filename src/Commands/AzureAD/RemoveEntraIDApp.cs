using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.AzureAD
{
    [Cmdlet(VerbsCommon.Remove, "PnPAzureADApp")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Application.ReadWrite.All")]
    [Alias("Remove-PnPEntraIDApp")]
    public class RemoveAzureADApp : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public AzureADAppPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var app = Identity.GetApp(GraphRequestHelper);

            if (Force || ShouldContinue($"Remove app '{app.DisplayName}' with id '{app.Id}'", Properties.Resources.Confirm))
            {
                GraphRequestHelper.Delete($"/v1.0/applications/{app.Id}");
            }
        }
    }
}