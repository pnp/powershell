using Microsoft.SharePoint.Client;
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

        [Parameter(Mandatory = false)]
        public string User;

        [Parameter(Mandatory = false)]
        public string Group;

        [Parameter(Mandatory = false)]
        public SwitchParameter Tenant;

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

            if (string.IsNullOrEmpty(User) && string.IsNullOrEmpty(Group) && !Tenant.IsPresent)
            {
                throw new PSArgumentException("Either User, Group, or Tenant must be specified.");
            }

            if ((Tenant.IsPresent && (!string.IsNullOrEmpty(User) || !string.IsNullOrEmpty(Group))) ||
                (!string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Group)))
            {
                throw new PSArgumentException("Specify only one of User, Group, or Tenant.");
            }

            string graphAccessToken = TokenHandler.GetAccessToken($"https://{Connection.GraphEndPoint}/.default", Connection);
            LogDebug("Microsoft Graph access token acquired");

            var graphRequestHelper = new ApiRequestHelper(GetType(), Connection, $"https://{Connection.GraphEndPoint}/.default");

            string entityId = null ;

            if (!string.IsNullOrEmpty(User))
            {
                LogDebug("Processing User parameter");
                Model.AzureAD.User graphUser;
                if (Guid.TryParse(User, out Guid userGuid))
                {
                    LogDebug($"Looking up user through Microsoft Graph by user id {userGuid}");
                    graphUser = Utilities.EntraIdUtility.GetUser(graphAccessToken, userGuid, azureEnvironment: Connection.AzureEnvironment);
                }
                else
                {
                    LogDebug($"Looking up user through Microsoft Graph by user principal name {User}");
                    graphUser = Utilities.EntraIdUtility.GetUser(graphAccessToken, User, azureEnvironment: Connection.AzureEnvironment);
                }

                if (graphUser == null)
                {
                    throw new PSArgumentException("User not found.", nameof(User));
                }

                entityId = graphUser.Id.ToString();
            }
            else if (!string.IsNullOrEmpty(Group))
            {
                LogDebug("Processing Group parameter");

                var graphGroup = Guid.TryParse(Group, out Guid groupGuid)
                    ? Utilities.AzureADGroupsUtility.GetGroup(graphRequestHelper, groupGuid)
                    : Utilities.AzureADGroupsUtility.GetGroup(graphRequestHelper, Group);

                if (graphGroup == null)
                {
                    throw new PSArgumentException("Group not found.", nameof(Group));
                }

                entityId = graphGroup.Id.ToString();
            }
            else if (Tenant.IsPresent)
            {
                LogDebug("Processing Tenant parameter");

                string TenantGUID = TenantExtensions.GetTenantIdByUrl(Connection.Url, Connection.AzureEnvironment);
                entityId = $"tenant-{TenantGUID}";
                LogDebug($"Tenant ID resolved: {entityId}");
            }

            var payload = new
            {
                delete = new[]
                {
                    new
                    {
                        id = entityId,
                    }
                }
            };

            if (Force || ShouldContinue($"Remove PowerApp permission for entity with id '{entityId}' from app '{appName}'?", Properties.Resources.Confirm))
            {
                string baseUrl = PowerPlatformUtility.GetPowerAppsEndpoint(Connection.AzureEnvironment);
                LogDebug($"Removing entity {entityId} permissions from PowerApp {appName} in environment {environmentName}");
                PowerAppsRequestHelper.Post($"{baseUrl}/providers/Microsoft.PowerApps{(AsAdmin ? "/scopes/admin/environments/" + environmentName : "")}/apps/{appName}/modifyPermissions?api-version=2022-11-01", payload);
            }
        }
    }
}