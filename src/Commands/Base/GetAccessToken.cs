using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPAccessToken", DefaultParameterSetName = DefaultParam)]
    [RequiredMinimalApiPermissions("https://graph.microsoft.com/.default")]
    public class GetPnPAccessToken : PnPGraphCmdlet
    {
        private const string DefaultParam = "Default";
        private const string ResourceTypeParam = "Resource Type Name";
        private const string ResourceUrlParam = "Resource Url";

        [Parameter(Mandatory = false, ParameterSetName = ResourceTypeParam)]
        public ResourceTypeName ResourceTypeName = ResourceTypeName.Graph;

        [Parameter(Mandatory = false, ParameterSetName = ResourceUrlParam)]
        public string ResourceUrl;

        [Parameter(ParameterSetName = DefaultParam)]
        [Parameter(ParameterSetName = ResourceTypeParam)]
        [Parameter(ParameterSetName = ResourceUrlParam)]
        [Parameter(Mandatory = false)]
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
                        accessTokenValue = TokenHandler.GetAccessToken(null, PnPConnection.Current?.Context?.Url?.TrimEnd('/') + "/.default");
                        break;
                    case ResourceTypeName.ARM:
                        accessTokenValue = TokenHandler.GetAccessToken(null, "https://management.azure.com/.default");
                        break;
                }
            }
            else if (ParameterSetName == ResourceUrlParam)
            {
                accessTokenValue = TokenHandler.GetAccessToken(null, ResourceUrl);
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