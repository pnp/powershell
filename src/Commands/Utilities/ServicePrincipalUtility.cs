using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.AzureAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Utility class to work with Azure AD Service Principals
    /// </summary>
    internal static class ServicePrincipalUtility
    {
        /// <summary>
        /// Returns all service principals
        /// </summary>
        public static List<AzureADServicePrincipal> GetServicePrincipals(PnPConnection connection, string accesstoken)
        {
            var result = Utilities.REST.GraphHelper.GetResultCollectionAsync<AzureADServicePrincipal>(connection, $"https://{connection.GraphEndPoint}/v1.0/servicePrincipals", accesstoken).GetAwaiter().GetResult();
            return result.ToList();
        }

        /// <summary>
        /// Returns the service principal of the provided built in type
        /// </summary>
        public static AzureADServicePrincipal GetServicePrincipalByBuiltInType(PnPConnection connection, string accesstoken, ServicePrincipalBuiltInType builtInType)
        {
            AzureADServicePrincipal result = null;
            switch(builtInType)
            {
                case ServicePrincipalBuiltInType.MicrosoftGraph:
                    result = ServicePrincipalUtility.GetServicePrincipalByAppId(connection, accesstoken, new Guid("00000003-0000-0000-c000-000000000000"));
                    break;

                case ServicePrincipalBuiltInType.SharePointOnline:
                    result = ServicePrincipalUtility.GetServicePrincipalByAppId(connection, accesstoken, new Guid("00000003-0000-0ff1-ce00-000000000000"));
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(builtInType), builtInType, null);
            };
            return result;
        }

        /// <summary>
        /// Returns the service principal of the provided object id
        /// </summary>
        public static AzureADServicePrincipal GetServicePrincipalByObjectId(PnPConnection connection, string accesstoken, Guid objectId)
        {
            IEnumerable<AzureADServicePrincipal> result;
            try
            {
                result = Utilities.REST.GraphHelper.GetResultCollectionAsync<AzureADServicePrincipal>(connection, $"https://{connection.GraphEndPoint}/v1.0/servicePrincipals?$filter=id eq '{objectId}'", accesstoken).GetAwaiter().GetResult();
                return result.FirstOrDefault();
            }
            catch (Exception) { }
            return null;            
        }

        /// <summary>
        /// Returns the service principal of the provided app id
        /// </summary>
        public static AzureADServicePrincipal GetServicePrincipalByAppId(PnPConnection connection, string accesstoken, Guid appId)
        {
            IEnumerable<AzureADServicePrincipal> result;
            try
            {
                result = Utilities.REST.GraphHelper.GetResultCollectionAsync<AzureADServicePrincipal>(connection, $"https://{connection.GraphEndPoint}/v1.0/servicePrincipals?$filter=appid eq '{appId}'", accesstoken).GetAwaiter().GetResult();
                return result.FirstOrDefault();
            }
            catch (Exception) { }
            return null;
        }

        /// <summary>
        /// Returns the service principal with the provided name
        /// </summary>
        public static AzureADServicePrincipal GetServicePrincipalByAppName(PnPConnection connection, string accesstoken, string appName)
        {
            IEnumerable<AzureADServicePrincipal> result;
            try
            {
                result = Utilities.REST.GraphHelper.GetResultCollectionAsync<AzureADServicePrincipal>(connection, $"https://{connection.GraphEndPoint}/v1.0/servicePrincipals?$filter=displayName eq '{appName}'", accesstoken).GetAwaiter().GetResult();
                return result.FirstOrDefault();
            }
            catch (Exception) { }
            return null;
        }

        public static AzureADServicePrincipalAppRoleCreatedConfirmation AddServicePrincipalRoleAssignment(PnPConnection connection, string accesstoken, AzureADServicePrincipal principalToAddRoleTo, AzureADServicePrincipal resourceContainingRole, AzureADServicePrincipalAppRole appRoleToAdd)
        {
            var content = new StringContent($"{{'principalId':'{principalToAddRoleTo.Id}','resourceId':'{resourceContainingRole.Id}','appRoleId':'{appRoleToAdd.Id}'}}");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = Utilities.REST.GraphHelper.PostAsync<AzureADServicePrincipalAppRoleCreatedConfirmation>(connection, $"https://{connection.GraphEndPoint}/v1.0/servicePrincipals/{principalToAddRoleTo.Id}/appRoleAssignments'", content, accesstoken).GetAwaiter().GetResult();
            return result;
        }
    }
}