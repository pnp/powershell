using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Utilities.Auth;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Base class for all the PnP Azure Management API related cmdlets
    /// </summary>
    public abstract class PnPAzureManagementApiCmdlet : PnPConnectedCmdlet
    {
        /// <summary>
        /// Returns an Access Token for the Microsoft Office Management API, if available, otherwise NULL
        /// </summary>
        public string AccessToken => TokenHandler.GetAccessToken(this, $"{Endpoints.GetArmEndpoint(Connection)}/.default", Connection);

        public string ArmAudience => $"{Endpoints.GetArmEndpoint(Connection)}/.default";
        /// <summary>
        /// Returns an Access Token for the Microsoft PowerApps Services, if available, otherwise NULL
        /// </summary>
        public string PowerAppsServiceAccessToken => TokenHandler.GetAccessToken(this, "https://service.powerapps.com/.default", Connection);

        public GraphHelper ArmRequestHelper { get; private set; }
        public GraphHelper PowerAppsServerRequestHelper { get; private set; }

        public string PowerappServicesAudience => "https://service.powerapps.com/.default";
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
            this.ArmRequestHelper = new GraphHelper(this, Connection, ArmAudience);
            this.PowerAppsServerRequestHelper = new GraphHelper(this, Connection, PowerappServicesAudience);
        }
    }
}