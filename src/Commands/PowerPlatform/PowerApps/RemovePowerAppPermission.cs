using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerApps
{
    [Cmdlet(VerbsCommon.Remove, "PnPPowerAppPermission")]
    public class RemovePowerAppPermission : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = false)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public PowerAppPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string User;

        [Parameter(Mandatory = false)]
        public SwitchParameter AsAdmin;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var environmentName = ParameterSpecified(nameof(Environment)) ? Environment.GetName() : PowerPlatformUtility.GetDefaultEnvironment(ArmRequestHelper, Connection.AzureEnvironment)?.Name;
            if (string.IsNullOrEmpty(environmentName))
            {
                throw new PSArgumentException("Environment not found.", nameof(Environment));
            }

            var appName = Identity.GetName();
            if (string.IsNullOrEmpty(appName))
            {
                throw new PSArgumentException("PowerApp not found.", nameof(Identity));
            }

            LogDebug("Acquiring access token for Microsoft Graph to look up user");
            var graphAccessToken = TokenHandler.GetAccessToken($"https://{Connection.GraphEndPoint}/.default", Connection);
            LogDebug("Microsoft Graph access token acquired");

            Model.AzureAD.User graphUser;
            if (Guid.TryParse(User, out Guid identityGuid))
            {
                LogDebug($"Looking up user through Microsoft Graph by user id {identityGuid}");
                graphUser = Utilities.AzureAdUtility.GetUser(graphAccessToken, identityGuid, azureEnvironment: Connection.AzureEnvironment);
            }
            else
            {
                LogDebug($"Looking up user through Microsoft Graph by user principal name {User}");
                graphUser = Utilities.AzureAdUtility.GetUser(graphAccessToken, User, azureEnvironment: Connection.AzureEnvironment);
            }

            if (graphUser == null)
            {
                throw new PSArgumentException("User not found.", nameof(User));
            }

            var payload = new
            {
                delete = new[]
                {
                    new
                    {
                        id = graphUser.Id.Value,
                    }
                }
            };
            if (Force || ShouldContinue($"Remove PowerApp permission for user with id '{graphUser.Id.Value}' from app '{appName}'?", Properties.Resources.Confirm))
            {
                string baseUrl = PowerPlatformUtility.GetPowerAppsEndpoint(Connection.AzureEnvironment);
                LogDebug($"Removing user {graphUser.Id.Value} permissions from PowerApp {appName} in environment {environmentName}");
                PowerAppsRequestHelper.Post($"{baseUrl}/providers/Microsoft.PowerApps{(AsAdmin ? "/scopes/admin/environments/" + environmentName : "")}/apps/{appName}/modifyPermissions?api-version=2022-11-01", payload);
            }
        }
    }
}
