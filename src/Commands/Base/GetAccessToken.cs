using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities.Auth;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPAccessToken", DefaultParameterSetName = ResourceTypeParam)]
    [OutputType(typeof(System.IdentityModel.Tokens.Jwt.JwtSecurityToken), ParameterSetName = new[] { ResourceTypeParam_Decoded, ResourceUrlParam_Decoded })]
    [OutputType(typeof(string), ParameterSetName = new[] { ResourceTypeParam, ResourceUrlParam })]
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
                        accessTokenValue = TokenHandler.GetAccessToken(this, rootUrl + "/.default", Connection);
                        break;
                    case ResourceTypeName.ARM:
                        accessTokenValue = TokenHandler.GetAccessToken(this, ARMEndpoint.GetARMEndpoint(Connection), Connection);
                        break;
                }
            }
            else if (ParameterSetName == ResourceUrlParam || ParameterSetName == ResourceUrlParam_Decoded)
            {
                accessTokenValue = TokenHandler.GetAccessToken(this, ResourceUrl, Connection);
            }

            if (accessTokenValue == null)
            {
                WriteError(new PSArgumentException("Unable to retrieve access token"), ErrorCategory.InvalidResult);
            }

            if (Decoded.IsPresent)
            {
                WriteObject(new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(accessTokenValue));
            }
            else
            {
                WriteObject(accessTokenValue);
            }
        }
    }
}