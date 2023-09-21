using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.EntraID;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Utility class to work with Entra ID Service Principals
    /// </summary>
    internal static class ServicePrincipalUtility
    {
        /// <summary>
        /// Returns all service principals
        /// </summary>
        public static List<ServicePrincipal> GetServicePrincipals(PnPConnection connection, string accesstoken, string filter = null)
        {
            string requestUrl = $"https://{connection.GraphEndPoint}/v1.0/servicePrincipals";
            Dictionary<string, string> additionalHeaders = null;
            if (!string.IsNullOrEmpty(filter))
            {
                requestUrl = $"{requestUrl}?$filter=({filter})";
                additionalHeaders = new Dictionary<string, string>
                {
                    { "ConsistencyLevel", "eventual" }
                };
            }
            var result = REST.GraphHelper.GetResultCollectionAsync<ServicePrincipal>(connection, requestUrl, accesstoken, additionalHeaders: additionalHeaders).GetAwaiter().GetResult();
            return result.ToList();
        }

        /// <summary>
        /// Returns the service principal of the provided built in type
        /// </summary>
        public static ServicePrincipal GetServicePrincipalByBuiltInType(PnPConnection connection, string accesstoken, ServicePrincipalBuiltInType builtInType)
        {
            ServicePrincipal result = null;
            switch (builtInType)
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
        public static ServicePrincipal GetServicePrincipalByObjectId(PnPConnection connection, string accesstoken, Guid objectId)
        {
            IEnumerable<ServicePrincipal> result;
            try
            {
                result = Utilities.REST.GraphHelper.GetResultCollectionAsync<ServicePrincipal>(connection, $"https://{connection.GraphEndPoint}/v1.0/servicePrincipals?$filter=id eq '{objectId}'", accesstoken).GetAwaiter().GetResult();

                var servicePrincipal = result.FirstOrDefault();
                servicePrincipal.AppRoles.ForEach(ar => ar.ServicePrincipal = servicePrincipal);
                return servicePrincipal;
            }
            catch (Exception) { }
            return null;
        }

        /// <summary>
        /// Returns the service principal of the provided app id
        /// </summary>
        public static ServicePrincipal GetServicePrincipalByAppId(PnPConnection connection, string accesstoken, Guid appId)
        {
            IEnumerable<ServicePrincipal> result;
            try
            {
                result = Utilities.REST.GraphHelper.GetResultCollectionAsync<ServicePrincipal>(connection, $"https://{connection.GraphEndPoint}/v1.0/servicePrincipals?$filter=appid eq '{appId}'", accesstoken).GetAwaiter().GetResult();

                var servicePrincipal = result.FirstOrDefault();
                servicePrincipal.AppRoles.ForEach(ar => ar.ServicePrincipal = servicePrincipal);
                return servicePrincipal;
            }
            catch (Exception) { }
            return null;
        }

        /// <summary>
        /// Returns the service principal with the provided name
        /// </summary>
        public static ServicePrincipal GetServicePrincipalByAppName(PnPConnection connection, string accesstoken, string appName)
        {
            IEnumerable<ServicePrincipal> result;
            try
            {
                result = Utilities.REST.GraphHelper.GetResultCollectionAsync<ServicePrincipal>(connection, $"https://{connection.GraphEndPoint}/v1.0/servicePrincipals?$filter=displayName eq '{appName}'", accesstoken).GetAwaiter().GetResult();

                var servicePrincipal = result.FirstOrDefault();
                servicePrincipal.AppRoles.ForEach(ar => ar.ServicePrincipal = servicePrincipal);
                return servicePrincipal;
            }
            catch (Exception) { }
            return null;
        }

        /// <summary>
        /// Returns the service principal app role assignments of the service principal with the provided object id
        /// </summary>
        public static List<ServicePrincipalAppRoleAssignment> GetServicePrincipalAppRoleAssignmentsByServicePrincipalObjectId(PnPConnection connection, string accesstoken, string servicePrincipalObjectId, string appRoleAssignmentId)
        {
            try
            {
                // Retrieve the specific app role assigned to the service principal
                var results = Utilities.REST.GraphHelper.GetAsync<ServicePrincipalAppRoleAssignment>(connection, $"https://{connection.GraphEndPoint}/v1.0/servicePrincipals/{servicePrincipalObjectId}/appRoleAssignments/{appRoleAssignmentId}", accesstoken).GetAwaiter().GetResult();
                var enrichedResults = ServicePrincipalUtility.EnrichEntraIDServicePrincipalAppRoleAssignments(connection, accesstoken, new List<ServicePrincipalAppRoleAssignment> { results });
                return enrichedResults.ToList();
            }
            catch (Exception) { }
            return null;
        }

        /// <summary>
        /// Returns the service principal app role assignments of the service principal with the provided object id
        /// </summary>
        public static List<ServicePrincipalAppRoleAssignment> GetServicePrincipalAppRoleAssignmentsByServicePrincipalObjectId(PnPConnection connection, string accesstoken, string servicePrincipalObjectId)
        {
            try
            {
                // Retrieve all the app role assigned to the service principal
                var results = Utilities.REST.GraphHelper.GetResultCollectionAsync<ServicePrincipalAppRoleAssignment>(connection, $"https://{connection.GraphEndPoint}/v1.0/servicePrincipals/{servicePrincipalObjectId}/appRoleAssignments", accesstoken).GetAwaiter().GetResult();
                var enrichedResults = ServicePrincipalUtility.EnrichEntraIDServicePrincipalAppRoleAssignments(connection, accesstoken, results);
                return enrichedResults.ToList();
            }
            catch (Exception) { }
            return null;
        }

        /// <summary>
        /// Enrich the service principal app role assignments with the friendly claim value
        /// </summary>
        /// <param name="connection">Connection to use to communicate with Microsoft Graph</param>
        /// <param name="accesstoken">Access Token to use to authenticate to Microsoft Graph</param>
        /// <param name="entraIDServicePrincipalAppRoleAssignments">Enumerable with app role assignments to enrich</param>
        /// <returns>Enriched app role assignements</returns>
        private static IEnumerable<ServicePrincipalAppRoleAssignment> EnrichEntraIDServicePrincipalAppRoleAssignments(PnPConnection connection, string accesstoken, IEnumerable<ServicePrincipalAppRoleAssignment> entraIDServicePrincipalAppRoleAssignments)
        {
            // Since the result does not contain the friendly claim value (i.e. Group.ReadWrite.All) but just identifiers for it, we will enrich the result with the friendly name oursevles
            var servicePrincipalCache = new Dictionary<Guid, ServicePrincipal>();
            foreach (var entraIDServicePrincipalAppRoleAssignment in entraIDServicePrincipalAppRoleAssignments)
            {
                // Ensure we have the ResourceId and the AppRoleId which will be needed to define the friendly claim value for the app role assignment
                if (!entraIDServicePrincipalAppRoleAssignment.ResourceId.HasValue || !entraIDServicePrincipalAppRoleAssignment.AppRoleId.HasValue) continue;

                // Keep a cache for the service principals to avoid unnecessary calls to the Graph API
                ServicePrincipal servicePrincipal;

                // Check if we have the service principal in the cache
                if (servicePrincipalCache.ContainsKey(entraIDServicePrincipalAppRoleAssignment.ResourceId.Value))
                {
                    // Service principal is in cache, so use it
                    servicePrincipal = servicePrincipalCache[entraIDServicePrincipalAppRoleAssignment.ResourceId.Value];
                }
                else
                {
                    // Service principal is not in cache yet, retrieve it from the Graph API
                    servicePrincipal = GetServicePrincipalByObjectId(connection, accesstoken, entraIDServicePrincipalAppRoleAssignment.ResourceId.Value);

                    if (servicePrincipal != null)
                    {
                        servicePrincipalCache.Add(entraIDServicePrincipalAppRoleAssignment.ResourceId.Value, servicePrincipal);
                    }
                }

                if (servicePrincipal != null)
                {
                    // If we have a service principal, look for the app role and match its claim value to the app role assignment name
                    entraIDServicePrincipalAppRoleAssignment.AppRoleName = servicePrincipal.AppRoles.FirstOrDefault(ar => ar.Id == entraIDServicePrincipalAppRoleAssignment.AppRoleId.Value)?.Value;
                }
            }

            return entraIDServicePrincipalAppRoleAssignments;
        }

        /// <summary>
        /// Adds a role assignment to the provided service principal
        /// </summary>
        /// <param name="connection">Connection to use to communicate with Microsoft Graph</param>
        /// <param name="accesstoken">Access Token to use to authenticate to Microsoft Graph</param>
        /// <param name="principalToAddRoleTo">The service principal to add the role to</param>
        /// <param name="appRoleToAdd">The app role to add to the service principal</param>
        /// <returns>The service principal app role assignment</returns>
        public static ServicePrincipalAppRoleAssignment AddServicePrincipalRoleAssignment(PnPConnection connection, string accesstoken, ServicePrincipal principalToAddRoleTo, ServicePrincipalAppRole appRoleToAdd)
        {
            var content = new StringContent($"{{'principalId':'{principalToAddRoleTo.Id}','resourceId':'{appRoleToAdd.ServicePrincipal.Id}','appRoleId':'{appRoleToAdd.Id}'}}");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = Utilities.REST.GraphHelper.PostAsync<ServicePrincipalAppRoleAssignment>(connection, $"https://{connection.GraphEndPoint}/v1.0/servicePrincipals/{principalToAddRoleTo.Id}/appRoleAssignments", content, accesstoken).GetAwaiter().GetResult();
            return result;
        }

        /// <summary>
        /// Removes all role assignments from the provided service principal
        /// </summary>
        /// <param name="connection">Connection to use to communicate with Microsoft Graph</param>
        /// <param name="accesstoken">Access Token to use to authenticate to Microsoft Graph</param>
        /// <param name="principalToRemoveRoleFrom">The service principal to remove the role assignments from</param>
        public static void RemoveServicePrincipalRoleAssignment(PnPConnection connection, string accesstoken, ServicePrincipal principalToRemoveRoleFrom)
        {
            var assignments = GetServicePrincipalAppRoleAssignmentsByServicePrincipalObjectId(connection, accesstoken, principalToRemoveRoleFrom.Id);
            foreach (var assignment in assignments)
            {
                Utilities.REST.GraphHelper.DeleteAsync(connection, $"https://{connection.GraphEndPoint}/v1.0/servicePrincipals/{principalToRemoveRoleFrom.Id}/appRoleAssignments/{assignment.Id}", accesstoken).GetAwaiter().GetResult();
            }
        }

        /// <summary>
        /// Removes the provided role from the role assignments of the provided service principal
        /// </summary>
        /// <param name="connection">Connection to use to communicate with Microsoft Graph</param>
        /// <param name="accesstoken">Access Token to use to authenticate to Microsoft Graph</param>
        /// <param name="principalToRemoveRoleFrom">The service principal to remove the role assignment from</param>
        /// <param name="appRoleToRemove">The app role to remove from the role assignments</param>
        public static void RemoveServicePrincipalRoleAssignment(PnPConnection connection, string accesstoken, ServicePrincipal principalToRemoveRoleFrom, ServicePrincipalAppRole appRoleToRemove)
        {
            var assignments = GetServicePrincipalAppRoleAssignmentsByServicePrincipalObjectId(connection, accesstoken, principalToRemoveRoleFrom.Id);
            foreach (var assignment in assignments)
            {
                if (assignment.AppRoleId == appRoleToRemove.Id)
                {
                    Utilities.REST.GraphHelper.DeleteAsync(connection, $"https://{connection.GraphEndPoint}/v1.0/servicePrincipals/{principalToRemoveRoleFrom.Id}/appRoleAssignments/{assignment.Id}", accesstoken).GetAwaiter().GetResult();
                }
            }
        }

        /// <summary>
        /// Removes role with the provided name from the role assignments of the provided service principal
        /// </summary>
        /// <param name="connection">Connection to use to communicate with Microsoft Graph</param>
        /// <param name="accesstoken">Access Token to use to authenticate to Microsoft Graph</param>
        /// <param name="principalToRemoveRoleFrom">The service principal to remove the role assignment from</param>
        /// <param name="roleName">The name of the app role to remove from the role assignments</param>
        public static void RemoveServicePrincipalRoleAssignment(PnPConnection connection, string accesstoken, ServicePrincipal principalToRemoveRoleFrom, string roleName)
        {
            var assignments = GetServicePrincipalAppRoleAssignmentsByServicePrincipalObjectId(connection, accesstoken, principalToRemoveRoleFrom.Id);
            foreach (var assignment in assignments)
            {
                if (assignment.AppRoleName == roleName)
                {
                    Utilities.REST.GraphHelper.DeleteAsync(connection, $"https://{connection.GraphEndPoint}/v1.0/servicePrincipals/{principalToRemoveRoleFrom.Id}/appRoleAssignments/{assignment.Id}", accesstoken).GetAwaiter().GetResult();
                }
            }
        }

        /// <summary>
        /// Removes a role assignment from the provided service principal
        /// </summary>
        /// <param name="connection">Connection to use to communicate with Microsoft Graph</param>
        /// <param name="accesstoken">Access Token to use to authenticate to Microsoft Graph</param>
        /// <param name="principalToRemoveRoleFrom">The service principal to remove the role assignment from</param>
        /// <param name="appRoleAssignmentoRemove">The app role assignment to remove from the service principal</param>
        public static void RemoveServicePrincipalRoleAssignment(PnPConnection connection, string accesstoken, ServicePrincipalAppRoleAssignment appRoleAssignmenToRemove)
        {
            Utilities.REST.GraphHelper.DeleteAsync(connection, $"https://{connection.GraphEndPoint}/v1.0/servicePrincipals/{appRoleAssignmenToRemove.PrincipalId}/appRoleAssignments/{appRoleAssignmenToRemove.Id}", accesstoken).GetAwaiter().GetResult();
        }
    }
}