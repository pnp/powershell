using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Utilities.Auth;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Base class for all the PnP Azure Resource Management API related cmdlets
    /// </summary>
    public abstract class PnPAzureManagementApiCmdlet : PnPConnectedCmdlet
    {
        /// <summary>
        /// The default audience for targeting Azure Resource Manager APIs
        /// </summary>
        public string ArmDefaultAudience => $"{Endpoints.GetArmEndpoint(Connection)}/.default";

        /// <summary>
        /// The default audience to target PowerApps APIs
        /// </summary>
        public string PowerAppDefaultAudience => "https://service.powerapps.com/.default";

        /// <summary>
        /// Returns an Access Token for the Microsoft Office Management API, if available, otherwise NULL
        /// </summary>
        public string AccessToken => TokenHandler.GetAccessToken(ArmDefaultAudience, Connection);

        /// <summary>
        /// Returns an Access Token for the Microsoft PowerApps Services, if available, otherwise NULL
        /// </summary>
        public string PowerAppsServiceAccessToken => TokenHandler.GetAccessToken(PowerAppDefaultAudience, Connection);

        /// <summary>
        /// An instance of the <see cref="ApiRequestHelper"/> class to help with making requests to the Azure Resource Manager services
        /// </summary>
        public ApiRequestHelper ArmRequestHelper { get; private set; }

        /// <summary>
        /// An instance of the <see cref="ApiRequestHelper"/> class to help with making requests to the PowerApps services
        /// </summary>
        public ApiRequestHelper PowerAppsRequestHelper { get; private set; }

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

            ArmRequestHelper = new ApiRequestHelper(GetType(), Connection, ArmDefaultAudience);
            PowerAppsRequestHelper = new ApiRequestHelper(GetType(), Connection, PowerAppDefaultAudience);
        }
    }
}