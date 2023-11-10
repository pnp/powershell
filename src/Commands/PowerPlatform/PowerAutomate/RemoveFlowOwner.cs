using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;
using System.Net;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Remove, "PnPFlowOwner")]
    public class RemoveFlowOwner : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public PowerAutomateFlowPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string User;

        [Parameter(Mandatory = false)]
        public SwitchParameter AsAdmin;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

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
                user = Utilities.AzureAdUtility.GetUser(graphAccessToken, WebUtility.UrlEncode(User), azureEnvironment: Connection.AzureEnvironment);
            }

            if (user == null)
            {
                throw new PSArgumentException("User not found.", nameof(User));
            }

            var payload = new
            {
                delete = new[]
                {
                    new
                    {
                        id = user.Id.Value,
                    }
                }
            };

            if(Force || ShouldContinue($"Remove flow owner with id '{user.Id.Value}' from flow '{flowName}'?", "Remove flow owner"))
            {
                WriteVerbose($"Removing user {user.Id.Value} permissions from flow {flowName} in environment {environmentName}");
                RestHelper.PostAsync(Connection.HttpClient, $"https://management.azure.com/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/flows/{flowName}/modifyPermissions?api-version=2016-11-01", AccessToken, payload).GetAwaiter().GetResult();
            }
        }
    }
}
