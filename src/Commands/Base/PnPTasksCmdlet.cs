﻿using Microsoft.SharePoint.Client;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    public abstract class PnPTasksCmdlet : PnPConnectedCmdlet
    {
        /// <summary>
        /// Reference the the SharePoint context on the current connection. If NULL it means there is no SharePoint context available on the current connection.
        /// </summary>
        public ClientContext ClientContext => Connection?.Context;

        public PnPContext PnPContext => Connection?.PnPContext;

        public ApiRequestHelper RequestHelper { get; private set; }
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (Connection?.Context != null)
            {
                var contextSettings = Connection.Context.GetContextSettings();
                if (contextSettings?.Type == Framework.Utilities.Context.ClientContextType.Cookie || contextSettings?.Type == Framework.Utilities.Context.ClientContextType.SharePointACSAppOnly)
                {
                    var typeString = contextSettings?.Type == Framework.Utilities.Context.ClientContextType.Cookie ? "WebLogin/Cookie" : "ACS";
                    throw new PSInvalidOperationException($"This cmdlet does not work with a {typeString} based connection towards SharePoint.");
                }
            }
            RequestHelper = new ApiRequestHelper(this.GetType(), Connection, $"https://tasks.office.com/.default");
        }

        /// <summary>
        /// Returns an Access Token for the Microsoft Graph API, if available, otherwise NULL
        /// </summary>
        public string AccessToken => TokenHandler.GetAccessToken($"https://tasks.office.com/.default", Connection);
    }
}
