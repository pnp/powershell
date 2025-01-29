using Microsoft.SharePoint.Client;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Base class for all the PnP Microsoft Graph related cmdlets
    /// </summary>
    public abstract class PnPGraphCmdlet : PnPConnectedCmdlet
    {
        /// <summary>
        /// The default audience to target Microsoft Graph APIs
        /// </summary>
        public string MicrosoftGraphDefaultAudience => $"https://{Connection.GraphEndPoint}/.default";

        /// <summary>
        /// Reference the the SharePoint context on the current connection. If NULL it means there is no SharePoint context available on the current connection.
        /// </summary>
        public ClientContext ClientContext => Connection?.Context;

        /// <summary>
        /// Reference the the SharePoint context on the current connection encapsulated as a PnPContext. If NULL it means there is no SharePoint context available on the current connection.
        /// </summary>
        public PnPContext PnPContext => Connection?.PnPContext;

        /// <summary>
        /// An instance of the <see cref="ApiRequestHelper"/> class to help with making requests to the Microsoft Graph services
        /// </summary>
        public ApiRequestHelper GraphRequestHelper { get; private set; }

        /// <summary>
        /// Returns an Access Token for the Microsoft Graph API, if available, otherwise NULL
        /// </summary>
        public string AccessToken => TokenHandler.GetAccessToken(MicrosoftGraphDefaultAudience, Connection);        

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
            GraphRequestHelper = new ApiRequestHelper(GetType(), Connection, MicrosoftGraphDefaultAudience);
        }
    }
}