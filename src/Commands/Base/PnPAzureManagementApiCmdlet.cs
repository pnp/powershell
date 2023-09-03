using System;
using System.Management.Automation;
using System.Net.Http;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Model;

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
                if (Connection != null)
                {
                    if (Connection.ConnectionMethod == ConnectionMethod.ManagedIdentity)
                    {
                        return TokenHandler.GetManagedIdentityTokenAsync(this, Connection.HttpClient, "https://management.azure.com", Connection.UserAssignedManagedIdentityObjectId, Connection.UserAssignedManagedIdentityClientId, Connection.UserAssignedManagedIdentityAzureResourceId).GetAwaiter().GetResult();
                    }
                    else if (Connection.ConnectionMethod == ConnectionMethod.AzureADWorkloadIdentity)
                    {
                        return TokenHandler.GetAzureADWorkloadIdentityTokenAsync(this, "https://management.azure.com/.default").GetAwaiter().GetResult();
                    }
                    else
                    {
                        if (Connection.Context != null)
                        {
                            return TokenHandler.GetAccessToken(this, "https://management.azure.com/.default", Connection);
                        }
                    }
                }
                WriteVerbose("Unable to acquire token for resource: https://management.azure.com/");
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