using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.AzureAD
{
    [Cmdlet(VerbsCommon.Remove, "PnPAzureADApp")]
    [RequiredMinimalApiPermissions("Application.ReadWrite.All")]
    public class RemoveAzureADApp : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public AzureADAppPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var app = Identity.GetApp(this, HttpClient, AccessToken);

            if (Force || ShouldContinue($"Remove app '{app.DisplayName}' with id '{app.Id}'", string.Empty))
            {
                Utilities.REST.GraphHelper.DeleteAsync(HttpClient, $"/v1.0/applications/{app.Id}", AccessToken).GetAwaiter().GetResult();
            }
        }
    }
}