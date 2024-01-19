using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsLifecycle.Enable, "PnPFlow")]
    public class EnableFlow : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public PowerAutomateFlowPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter AsAdmin;

        protected override void ExecuteCmdlet()
        {
            var environmentName = Environment.GetName();
            var flowName = Identity.GetName();
            RestHelper.PostAsync(Connection.HttpClient, $"https://api.flow.microsoft.com/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/flows/{flowName}/start?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
        }
    }
}