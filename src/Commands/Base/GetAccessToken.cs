using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities.Auth;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPAccessToken", DefaultParameterSetName = ParameterSet_ResourceTypeName)]
    [OutputType(typeof(Microsoft.IdentityModel.JsonWebTokens.JsonWebToken), ParameterSetName = [ParameterSet_TypeNameDecoded, ParameterSet_ResourceUrlDecoded])]
    [OutputType(typeof(string), ParameterSetName = [ParameterSet_ResourceTypeName, ParameterSet_ResourceUrl])]
    public class GetPnPAccessToken : PnPGraphCmdlet
    {
        private const string ParameterSet_ResourceTypeName = "Resource Type Name";
        private const string ParameterSet_ResourceUrl = "Resource Url";
        private const string ParameterSet_TypeNameDecoded = "Resource Type Name (decoded)";
        private const string ParameterSet_ResourceUrlDecoded = "Resource Url (decoded)";
        private const string ParameterSet_ListScopes = "List Permission Scopes";


        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ResourceTypeName)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TypeNameDecoded)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ListScopes)]

        public ResourceTypeName ResourceTypeName = ResourceTypeName.Graph;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ResourceUrl)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ResourceUrlDecoded)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ListScopes)]

        [ValidateNotNullOrEmpty]
        public string ResourceUrl;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_TypeNameDecoded)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ResourceUrlDecoded)]
        public SwitchParameter Decoded;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ResourceTypeName)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TypeNameDecoded)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ResourceUrl)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ResourceUrlDecoded)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ListScopes)]

        public string[] Scopes = ["AllSites.FullControl"];

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ListScopes)]
        public SwitchParameter ListPermissionScopes;

        protected override void ExecuteCmdlet()
        {
            string accessTokenValue = null;

            if (ParameterSetName == ParameterSet_ResourceTypeName || ParameterSetName == ParameterSet_TypeNameDecoded)
            {
                switch (ResourceTypeName)
                {
                    case ResourceTypeName.Graph:
                        accessTokenValue = AccessToken;
                        break;
                    case ResourceTypeName.SharePoint:
                        var currentUrl = Connection?.Context?.Url?.TrimEnd('/');
                        if (string.IsNullOrEmpty(currentUrl))
                        {
                            throw new PSArgumentException("No connection found, please login first.");
                        }
                        var rootUrl = new Uri(currentUrl).GetLeftPart(UriPartial.Authority);
                        accessTokenValue = TokenHandler.GetAccessToken(rootUrl + "/.default", Connection);
                        break;
                    case ResourceTypeName.AzureManagementApi:
                        accessTokenValue = TokenHandler.GetAccessToken($"{Endpoints.GetArmEndpoint(Connection)}/.default", Connection);
                        break;
                }
            }
            else if (ParameterSetName == ParameterSet_ResourceUrl || ParameterSetName == ParameterSet_ResourceUrlDecoded)
            {
                accessTokenValue = TokenHandler.GetAccessToken(ResourceUrl, Connection);
            }

            if (ParameterSpecified(nameof(Scopes)))
            {
                var authManager = Connection.Context.GetContextSettings().AuthenticationManager;
                accessTokenValue = authManager.GetAccessTokenAsync(Scopes).GetAwaiter().GetResult();
            }

            if (accessTokenValue == null)
            {
                WriteError(new PSArgumentException("Unable to retrieve access token"));
            }
            if (ListPermissionScopes.IsPresent)
            {
                WriteObject(TokenHandler.ReturnScopes(accessTokenValue));
            }
            else
            {
                if (Decoded.IsPresent)
                {
                    WriteObject(new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(accessTokenValue));
                }
                else
                {
                    WriteObject(accessTokenValue);
                }
            }
        }
    }
}