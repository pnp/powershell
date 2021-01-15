using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Properties;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.SharePoint.Client;


namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Base class for all the PnP Microsoft Office Management API related cmdlets
    /// </summary>
    public abstract class PnPOfficeManagementApiCmdlet : PnPConnectedCmdlet
    {
        [Parameter(Mandatory = false, DontShow = true)]
        public SwitchParameter ByPassPermissionCheck;

        /// <summary>
        /// Returns an Access Token for the Microsoft Office Management API, if available, otherwise NULL
        /// </summary>
        public OfficeManagementApiToken Token
        {
            get
            {
                if (PnPConnection.CurrentConnection?.Context != null)
                {
                    var token = TokenHandler.GetAccessToken(GetType(), "https://manage.office.com/.default");
                    return new OfficeManagementApiToken(token);
                }
                return null;
            }
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            if (PnPConnection.CurrentConnection?.Context != null)
            {
                if (PnPConnection.CurrentConnection.Context.GetContextSettings().Type == Framework.Utilities.Context.ClientContextType.Cookie)
                {
                    throw new PSInvalidOperationException("This cmdlet not work with a WebLogin/Cookie based connection towards SharePoint.");
                }
            }
        }

        /// <summary>
        /// Returns an Access Token for the Microsoft Office Management API, if available, otherwise NULL
        /// </summary>
        public string AccessToken => Token?.AccessToken;

        /// <summary>
        /// Root URL to the Office 365 Management API
        /// </summary>
        protected string ApiRootUrl => $"https://manage.office.com/api/v1.0/{Token.TenantId}/";

    }
}