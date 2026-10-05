using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using System.Linq;
using PnP.PowerShell.Commands.Utilities.Auth;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Base class for all the PnP Microsoft Office Management API related cmdlets
    /// </summary>
    public abstract class PnPOfficeManagementApiCmdlet : PnPConnectedCmdlet
    {
        /// <summary>
        /// Returns an Access Token for the Microsoft Office Management API, if available, otherwise NULL
        /// </summary>
        public string AccessToken => TokenHandler.GetAccessToken($"{Endpoints.GetOfficeManagementApiEndpoint(Connection)}/.default", Connection);
        public ApiRequestHelper RequestHelper { get; set; }
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            if (Connection?.Context != null)
            {
                if (Connection?.Context.GetContextSettings().Type == Framework.Utilities.Context.ClientContextType.Cookie)
                {
                    throw new PSInvalidOperationException("This cmdlet not work with a WebLogin/Cookie based connection towards SharePoint.");
                }
            }
            RequestHelper = new ApiRequestHelper(GetType(), Connection, $"{Endpoints.GetOfficeManagementApiEndpoint(Connection)}/.default");
        }

        protected Guid? TenantId
        {
            get
            {
                var parsedToken = new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(AccessToken);
                return Guid.TryParse(parsedToken.Claims.FirstOrDefault(c => c.Type == "tid").Value, out Guid tenandIdGuid) ? (Guid?)tenandIdGuid : null;
            }
        }
        /// <summary>
        /// Root URL to the Office 365 Management API
        /// </summary>
        protected string ApiRootUrl => $"{Endpoints.GetOfficeManagementApiEndpoint(Connection)}/api/v1.0/{TenantId}/";
    }
}