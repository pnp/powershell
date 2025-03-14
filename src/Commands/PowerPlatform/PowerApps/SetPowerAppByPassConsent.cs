using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerApps
{
    [Cmdlet(VerbsCommon.Set, "PnPPowerAppByPassConsent")]
    [RequiredApiApplicationPermissions("https://management.azure.com/user_impersonation", "https://service.powerapps.com/user")]
    public class PnPPowerAppByPassConsent : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public PowerAppPipeBind Identity;

        [Parameter(Mandatory = false)]
        public bool Enabled = true;

        protected override void ExecuteCmdlet()
        {
            var environmentName = ParameterSpecified(nameof(Environment)) ? Environment.GetName() : PowerPlatformUtility.GetDefaultEnvironment(ArmRequestHelper, Connection.AzureEnvironment)?.Name;
            var powerAppsUrl = PowerPlatformUtility.GetPowerAppsEndpoint(Connection.AzureEnvironment);
            var appName = Identity.GetName();

            LogDebug($"Setting specific PowerApp with the provided name '{appName}' consent within the environment '{environmentName}'");

            var postData = new
            {
                bypassconsent = Enabled.ToString()
            };
            var response = RestHelper.Post(Connection.HttpClient, $"{powerAppsUrl}/providers/Microsoft.PowerApps/scopes/admin/environments/{environmentName}/apps/{appName}/setPowerAppConnectionDirectConsentBypass?api-version=2021-02-01", PowerAppsServiceAccessToken, payload: postData);
            WriteObject(response, false);
        }
    }
}
