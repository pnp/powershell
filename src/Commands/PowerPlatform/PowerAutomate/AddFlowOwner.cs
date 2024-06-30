using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Add, "PnPFlowOwner")]
    public class AddFlowOwner : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public PowerAutomateFlowPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string User;

        [Parameter(Mandatory = true)]
        public Enums.FlowAccessRole Role = Enums.FlowAccessRole.CanView;

        [Parameter(Mandatory = false)]
        public SwitchParameter AsAdmin;

        protected override void ExecuteCmdlet()
        {
            var environmentName = Environment.GetName();
            if (string.IsNullOrEmpty(environmentName))
            {
                throw new PSArgumentException("Environment not found.", nameof(Environment));
            }

            var flowName = Identity.GetName();
            if (string.IsNullOrEmpty(flowName))
            {
                throw new PSArgumentException("Flow not found.", nameof(Identity));
            }

            WriteVerbose("Acquiring access token for Microsoft Graph to look up user");
            
            var graphAccessToken = TokenHandler.GetAccessToken(this, $"https://{Connection.GraphEndPoint}/.default", Connection);

            WriteVerbose("Microsoft Graph access token acquired");

            Model.AzureAD.User user;
            if (Guid.TryParse(User, out Guid identityGuid))
            {
                WriteVerbose("Looking up user through Microsoft Graph by user id {identityGuid}");
                user = Utilities.AzureAdUtility.GetUser(graphAccessToken, identityGuid, azureEnvironment: Connection.AzureEnvironment);
            }
            else
            {
                WriteVerbose($"Looking up user through Microsoft Graph by user principal name {User}");
                user = Utilities.AzureAdUtility.GetUser(graphAccessToken, User, azureEnvironment: Connection.AzureEnvironment);
            }

            if (user == null)
            {
                throw new PSArgumentException("User not found.", nameof(User));
            }

            var payload = new
            {
                put = new[]
                {
                    new
                    {
                        properties = new
                        {
                            principal = new
                            {
                                id = user.Id.Value,
                                type = "User"
                            },
                            roleName = Role.ToString()
                        }
                    }
                }
            };

            WriteVerbose($"Assigning user {Role} permissions to flow {flowName} in environment {environmentName}");
            string baseUrl = PowerPlatformUtility.GetPowerAutomateEndpoint(Connection.AzureEnvironment);
            RestHelper.Post(Connection.HttpClient, $"{baseUrl}/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/flows/{flowName}/modifyPermissions?api-version=2016-11-01", AccessToken, payload);
        }
    }
}
