﻿using PnP.PowerShell.Commands.Base;
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
        [Parameter(Mandatory = false)]
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
            var environmentName = ParameterSpecified(nameof(Environment)) ? Environment.GetName() : PowerPlatformUtility.GetDefaultEnvironment(ArmRequestHelper, Connection.AzureEnvironment)?.Name;
            if (string.IsNullOrEmpty(environmentName))
            {
                throw new PSArgumentException("Environment not found.", nameof(Environment));
            }

            var flowName = Identity.GetName();
            if (string.IsNullOrEmpty(flowName))
            {
                throw new PSArgumentException("Flow not found.", nameof(Identity));
            }

            LogDebug("Acquiring access token for Microsoft Graph to look up user");
            
            var graphAccessToken = TokenHandler.GetAccessToken($"https://{Connection.GraphEndPoint}/.default", Connection);

            LogDebug("Microsoft Graph access token acquired");

            Model.AzureAD.User user;
            if (Guid.TryParse(User, out Guid identityGuid))
            {
                LogDebug("Looking up user through Microsoft Graph by user id {identityGuid}");
                user = Utilities.AzureAdUtility.GetUser(graphAccessToken, identityGuid, azureEnvironment: Connection.AzureEnvironment);
            }
            else
            {
                LogDebug($"Looking up user through Microsoft Graph by user principal name {User}");
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

            LogDebug($"Assigning user {Role} permissions to flow {flowName} in environment {environmentName}");
            string baseUrl = PowerPlatformUtility.GetPowerAutomateEndpoint(Connection.AzureEnvironment);
            RestHelper.Post(Connection.HttpClient, $"{baseUrl}/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/flows/{flowName}/modifyPermissions?api-version=2016-11-01", AccessToken, payload);
        }
    }
}
