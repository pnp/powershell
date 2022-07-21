using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPAccessToken", DefaultParameterSetName = DefaultParam)]
    [RequiredMinimalApiPermissions("https://graph.microsoft.com/.default")]
    [OutputType(typeof(System.IdentityModel.Tokens.Jwt.JwtSecurityToken), ParameterSetName = new[] { DefaultParam_Decoded, ResourceTypeParam_Decoded, ResourceUrlParam_Decoded })]
    [OutputType(typeof(string), ParameterSetName = new[] { DefaultParam, ResourceTypeParam, ResourceUrlParam })]
    public class GetPnPAccessToken : PnPGraphCmdlet
    {
        private const string DefaultParam = "Default";
        private const string ResourceTypeParam = "Resource Type Name";
        private const string ResourceUrlParam = "Resource Url";
        private const string DefaultParam_Decoded = "Default (decoded)";
        private const string ResourceTypeParam_Decoded = "Resource Type Name (decoded)";
        private const string ResourceUrlParam_Decoded = "Resource Url (decoded)";

        [Parameter(Mandatory = true, ParameterSetName = ResourceTypeParam)]
        [Parameter(Mandatory = true, ParameterSetName = ResourceTypeParam_Decoded)]
        public ResourceTypeName ResourceTypeName = ResourceTypeName.Graph;

        [Parameter(Mandatory = true, ParameterSetName = ResourceUrlParam)]
        [Parameter(Mandatory = true, ParameterSetName = ResourceUrlParam_Decoded)]
        public string ResourceUrl;

        [Parameter(Mandatory = true, ParameterSetName = DefaultParam_Decoded)]
        [Parameter(Mandatory = true, ParameterSetName = ResourceTypeParam_Decoded)]
        [Parameter(Mandatory = true, ParameterSetName = ResourceUrlParam_Decoded)]
        public SwitchParameter Decoded;
        protected override void ExecuteCmdlet()
        {
            var accessTokenValue = AccessToken;

            if (ParameterSetName == ResourceTypeParam)
            {
                accessTokenValue = null;

                switch (ResourceTypeName)
                {
                    case ResourceTypeName.Graph:
                        accessTokenValue = AccessToken;
                        break;
                    case ResourceTypeName.SharePoint:
                        accessTokenValue = TokenHandler.GetAccessToken(null, Connection?.Context?.Url?.TrimEnd('/') + "/.default", Connection);
                        break;
                    case ResourceTypeName.ARM:
                        accessTokenValue = TokenHandler.GetAccessToken(null, "https://management.azure.com/.default", Connection);
                        break;
                }
            }
            else if (ParameterSetName == ResourceUrlParam)
            {
                accessTokenValue = TokenHandler.GetAccessToken(null, ResourceUrl, Connection);
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