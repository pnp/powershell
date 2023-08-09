using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;
using System.Net;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Add, "PnPFlowOwner")]
    public class AddFlowOwner : PnPAzureManagementApiCmdlet
    {
        private const string ParameterSet_BYID = "Return by specific ID/Username";

        [Parameter(Mandatory = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public PowerAutomateFlowPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYID)]
        public string User;

        [Parameter(Mandatory = true)]
        public FlowUserRoleName RoleName = FlowUserRoleName.CanView;

        [Parameter(Mandatory = false)]
        public SwitchParameter AsAdmin;

        protected override void ExecuteCmdlet()
        {
            string type = "User";
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

            Guid idToAdd = Guid.Empty;
            if (ParameterSpecified(nameof(User)))
            {
                var accessToken = TokenHandler.GetAccessToken(this, $"https://{Connection.GraphEndPoint}/.default", Connection);
                PnP.PowerShell.Commands.Model.AzureAD.User user;
                if (Guid.TryParse(User, out Guid identityGuid))
                {
                    user = PnP.PowerShell.Commands.Utilities.AzureAdUtility.GetUser(accessToken, identityGuid);
                }
                else
                {
                    user = PnP.PowerShell.Commands.Utilities.AzureAdUtility.GetUser(accessToken, WebUtility.UrlEncode(User));
                }

                if (user == null)
                {
                    throw new PSArgumentException("User not found.");
                }
                else
                {
                    idToAdd = (Guid)user.Id;
                    //WriteObject($"Added {user.UserPrincipalName} to flow {flowName} with access level {RoleName}");
                }
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
                                id = idToAdd,
                                type = type
                            },
                            roleName = RoleName
                        }
                    }
                }
            };

            RestHelper.PostAsync(Connection.HttpClient, $"https://management.azure.com/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/flows/{flowName}/modifyPermissions?api-version=2016-11-01", AccessToken, payload).GetAwaiter().GetResult();
        }
    }
}
