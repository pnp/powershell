﻿using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities.Auth;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPAccessToken", DefaultParameterSetName = ResourceTypeParam)]
    [OutputType(typeof(Microsoft.IdentityModel.JsonWebTokens.JsonWebToken), ParameterSetName = [ResourceTypeParam_Decoded, ResourceUrlParam_Decoded])]
    [OutputType(typeof(string), ParameterSetName = [ResourceTypeParam, ResourceUrlParam])]
    public class GetPnPAccessToken : PnPGraphCmdlet
    {
        private const string ResourceTypeParam = "Resource Type Name";
        private const string ResourceUrlParam = "Resource Url";
        private const string ResourceTypeParam_Decoded = "Resource Type Name (decoded)";
        private const string ResourceUrlParam_Decoded = "Resource Url (decoded)";

        [Parameter(Mandatory = false, ParameterSetName = ResourceTypeParam)]
        [Parameter(Mandatory = false, ParameterSetName = ResourceTypeParam_Decoded)]
        public ResourceTypeName ResourceTypeName = ResourceTypeName.Graph;

        [Parameter(Mandatory = true, ParameterSetName = ResourceUrlParam)]
        [Parameter(Mandatory = true, ParameterSetName = ResourceUrlParam_Decoded)]
        [ValidateNotNullOrEmpty]
        public string ResourceUrl;

        [Parameter(Mandatory = true, ParameterSetName = ResourceTypeParam_Decoded)]
        [Parameter(Mandatory = true, ParameterSetName = ResourceUrlParam_Decoded)]
        public SwitchParameter Decoded;

        [Parameter(Mandatory = false, ParameterSetName = ResourceTypeParam)]
        [Parameter(Mandatory = false, ParameterSetName = ResourceTypeParam_Decoded)]
        [Parameter(Mandatory = false, ParameterSetName = ResourceUrlParam)]
        [Parameter(Mandatory = false, ParameterSetName = ResourceUrlParam_Decoded)]
        public string[] Scopes = ["AllSites.FullControl"];
        protected override void ExecuteCmdlet()
        {
            string accessTokenValue = null;

            if (ParameterSetName == ResourceTypeParam || ParameterSetName == ResourceTypeParam_Decoded)
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
            else if (ParameterSetName == ResourceUrlParam || ParameterSetName == ResourceUrlParam_Decoded)
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
                WriteError(new PSArgumentException("Unable to retrieve access token"), ErrorCategory.InvalidResult);
            }

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