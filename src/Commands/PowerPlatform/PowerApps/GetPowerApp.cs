using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerApps
{
    [Cmdlet(VerbsCommon.Get, "PnPPowerApp")]
    [RequiredApiApplicationPermissions("https://management.azure.com/user_impersonation", "https://service.powerapps.com/user")]
    [OutputType(typeof(Model.PowerPlatform.PowerApp.PowerApp))]
    public class GetPowerApp : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = false)]
        public SwitchParameter AsAdmin;

        [Parameter(Mandatory = false)]
        public PowerAppPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var environmentName = ParameterSpecified(nameof(Environment)) ? Environment.GetName() : PowerPlatformUtility.GetDefaultEnvironment(ArmRequestHelper, Connection.AzureEnvironment)?.Name;
            var powerAppsUrl = PowerPlatformUtility.GetPowerAppsEndpoint(Connection.AzureEnvironment);


            if (ParameterSpecified(nameof(Identity)))
            {
                var appName = Identity.GetName();

                LogDebug($"Retrieving specific PowerApp with the provided name '{appName}' within the environment '{environmentName}'");

                var result = PowerAppsRequestHelper.Get<Model.PowerPlatform.PowerApp.PowerApp>($"{powerAppsUrl}/providers/Microsoft.PowerApps{(AsAdmin ? "/scopes/admin/environments/" + environmentName : "")}/apps/{appName}?api-version=2016-11-01");
                 
                WriteObject(result, false);
            }
            else
            {
                LogDebug($"Retrieving all PowerApps within environment '{environmentName}'");

                var apps = PowerAppsRequestHelper.GetResultCollection<Model.PowerPlatform.PowerApp.PowerApp>($"{powerAppsUrl}/providers/Microsoft.PowerApps/apps?api-version=2016-11-01&$filter=environment eq '{environmentName}'");
                WriteObject(apps, true);
            }
        }
    }
}