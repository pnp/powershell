using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.EntraID
{
    [Cmdlet(VerbsCommon.Remove, "PnPEntraIDApp")]
    [RequiredMinimalApiPermissions("Application.ReadWrite.All")]
    [Alias("Remove-PnPAzureADApp")]
    public class RemoveEntraIDApp : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public EntraIDAppPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var app = Identity.GetApp(this, Connection, AccessToken);

            if (Force || ShouldContinue($"Remove app '{app.DisplayName}' with id '{app.Id}'", string.Empty))
            {
                Utilities.REST.GraphHelper.DeleteAsync(Connection, $"/v1.0/applications/{app.Id}", AccessToken).GetAwaiter().GetResult();
            }
        }
    }
}