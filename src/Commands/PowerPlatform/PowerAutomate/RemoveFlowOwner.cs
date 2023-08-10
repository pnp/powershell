using AngleSharp.Css;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;
using System.Net;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Remove, "PnPFlowOwner")]
    public class RemoveFlowOwner : PnPAzureManagementApiCmdlet
    {
        private const string ParameterSet_BYID = "Return by specific ID/Username";

        [Parameter(Mandatory = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public PowerAutomateFlowPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYID)]
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
                throw new PSArgumentException("Environment not found.");
            }

            var flowName = Identity.GetName();
            if (string.IsNullOrEmpty(flowName))
            {
                throw new PSArgumentException("Flow not found.");
            }

            Guid idToRemove = Guid.Empty;
            if (ParameterSpecified(nameof(User)))
            {
                /// <summary>
                /// graphAccessToken => The OAuth 2.0 access token to use for invoking Microsoft Graph. 
                /// This is used to get the user information from AzureAD
                /// </summary>
                var graphAccessToken = TokenHandler.GetAccessToken(this, $"https://{Connection.GraphEndPoint}/.default", Connection);
                PnP.PowerShell.Commands.Model.AzureAD.User user;
                if (Guid.TryParse(User, out Guid identityGuid))
                {
                    user = PnP.PowerShell.Commands.Utilities.AzureAdUtility.GetUser(graphAccessToken, identityGuid);
                }
                else
                {
                    user = PnP.PowerShell.Commands.Utilities.AzureAdUtility.GetUser(graphAccessToken, WebUtility.UrlEncode(User));
                }

                if (user == null)
                {
                    throw new PSArgumentException("User not found.");
                }
                else
                {
                    idToRemove = (Guid)user.Id;
                }
            }

            var payload = new
            {
                delete = new[]
                {
                    new
                    {
                        id = idToRemove,
                    }
                }
            };

            if(Force || ShouldContinue($"Remove flow owner with id '{idToRemove}' from flow '{flowName}'?", "Remove flow owner"))
            {
                RestHelper.PostAsync(Connection.HttpClient, $"https://management.azure.com/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/flows/{flowName}/modifyPermissions?api-version=2016-11-01", AccessToken, payload).GetAwaiter().GetResult();
            }
        }
    }
}
