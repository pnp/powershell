using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Base class for all the PnP Azure Management API related cmdlets
    /// </summary>
    public abstract class PnPAzureManagementApiCmdlet : PnPConnectedCmdlet
    {
        /// <summary>
        /// Returns an Access Token for the Azure Management API, if available, otherwise NULL
        /// </summary>
        public string AccessToken
        {
            get
            {
                if (Connection?.Context != null)
                {
                    return TokenHandler.GetAccessToken(this, "https://management.azure.com/.default", Connection);
                }
                return null;
            }
        }

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
        }
    }
}