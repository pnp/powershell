using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Properties;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using System.Linq;

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
        public string AccessToken
        {
            get
            {
                if (PnPConnection.Current?.Context != null)
                {
                    return TokenHandler.GetAccessToken(GetType(), "https://manage.office.com/.default");
                }
                return null;
            }
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            if (PnPConnection.Current?.Context != null)
            {
                if (PnPConnection.Current.Context.GetContextSettings().Type == Framework.Utilities.Context.ClientContextType.Cookie)
                {
                    throw new PSInvalidOperationException("This cmdlet not work with a WebLogin/Cookie based connection towards SharePoint.");
                }
            }
        }

        protected Guid? TenantId
        {
            get
            {
                var parsedToken = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(AccessToken);
                return Guid.TryParse(parsedToken.Claims.FirstOrDefault(c => c.Type == "tid").Value, out Guid tenandIdGuid) ? (Guid?)tenandIdGuid : null;
            }
        }
        /// <summary>
        /// Root URL to the Office 365 Management API
        /// </summary>
        protected string ApiRootUrl => $"https://manage.office.com/api/v1.0/{TenantId}/";
        
    }
}