using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.AzureAD;
using PnP.PowerShell.Commands.Utilities.REST;
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
        public static List<AzureADServicePrincipal> GetServicePrincipals(GraphHelper requestHelper, string filter = null)
        {
            string requestUrl = $"v1.0/servicePrincipals";
            Dictionary<string, string> additionalHeaders = null;
            if (!string.IsNullOrEmpty(filter))
            {
                requestUrl = $"{requestUrl}?$filter=({filter})";
                additionalHeaders = new Dictionary<string, string>
                {
                    { "ConsistencyLevel", "eventual" }
                };
            }
            var result = requestHelper.GetResultCollection<AzureADServicePrincipal>(requestUrl, additionalHeaders: additionalHeaders);
            return result.ToList();
        }

        /// <summary>
        /// Returns the service principal of the provided built in type
        /// </summary>
        public static AzureADServicePrincipal GetServicePrincipalByBuiltInType(GraphHelper requestHelper, ServicePrincipalBuiltInType builtInType)
        {
            AzureADServicePrincipal result = null;
            switch (builtInType)
            {
                case ServicePrincipalBuiltInType.MicrosoftGraph:
                    result = ServicePrincipalUtility.GetServicePrincipalByAppId(requestHelper, new Guid("00000003-0000-0000-c000-000000000000"));
                    break;

                case ServicePrincipalBuiltInType.SharePointOnline:
                    result = ServicePrincipalUtility.GetServicePrincipalByAppId(requestHelper, new Guid("00000003-0000-0ff1-ce00-000000000000"));
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(builtInType), builtInType, null);
            };
            return result;
        }

        /// <summary>
        /// Returns the service principal of the provided object id
        /// </summary>
        public static AzureADServicePrincipal GetServicePrincipalByObjectId(GraphHelper requestHelper, Guid objectId)
        {
            IEnumerable<AzureADServicePrincipal> result;
            try
            {
                result = requestHelper.GetResultCollection<AzureADServicePrincipal>($"v1.0/servicePrincipals?$filter=id eq '{objectId}'");

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
        public static AzureADServicePrincipal GetServicePrincipalByAppId(GraphHelper requestHelper, Guid appId)
        {
            IEnumerable<AzureADServicePrincipal> result;
            try
            {
                result = requestHelper.GetResultCollection<AzureADServicePrincipal>($"v1.0/servicePrincipals?$filter=appid eq '{appId}'");

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
        public static AzureADServicePrincipal GetServicePrincipalByAppName(GraphHelper requestHelper, string appName)
        {
            IEnumerable<AzureADServicePrincipal> result;
            try
            {
                result = requestHelper.GetResultCollection<AzureADServicePrincipal>($"v1.0/servicePrincipals?$filter=displayName eq '{appName}'");

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
        public static List<AzureADServicePrincipalAppRoleAssignment> GetServicePrincipalAppRoleAssignmentsByServicePrincipalObjectId(GraphHelper requestHelper, string servicePrincipalObjectId, string appRoleAssignmentId)
        {
            try
            {
                // Retrieve the specific app role assigned to the service principal
                var results = requestHelper.Get<AzureADServicePrincipalAppRoleAssignment>($"v1.0/servicePrincipals/{servicePrincipalObjectId}/appRoleAssignments/{appRoleAssignmentId}");
                var enrichedResults = EnrichAzureADServicePrincipalAppRoleAssignments(requestHelper, new List<AzureADServicePrincipalAppRoleAssignment> { results });
                return enrichedResults.ToList();
            }
            catch (Exception) { }
            return null;
        }

        /// <summary>
        /// Returns the service principal app role assignments of the service principal with the provided object id
        /// </summary>
        public static List<AzureADServicePrincipalAppRoleAssignment> GetServicePrincipalAppRoleAssignmentsByServicePrincipalObjectId(GraphHelper requestHelper, string servicePrincipalObjectId)
        {
            try
            {
                // Retrieve all the app role assigned to the service principal
                var results = requestHelper.GetResultCollection<AzureADServicePrincipalAppRoleAssignment>($"v1.0/servicePrincipals/{servicePrincipalObjectId}/appRoleAssignments");
                var enrichedResults = EnrichAzureADServicePrincipalAppRoleAssignments(requestHelper, results);
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
        /// <param name="azureADServicePrincipalAppRoleAssignments">Enumerable with app role assignments to enrich</param>
        /// <returns>Enriched app role assignements</returns>
        private static IEnumerable<AzureADServicePrincipalAppRoleAssignment> EnrichAzureADServicePrincipalAppRoleAssignments(GraphHelper requestHelper, IEnumerable<AzureADServicePrincipalAppRoleAssignment> azureADServicePrincipalAppRoleAssignments)
        {
            // Since the result does not contain the friendly claim value (i.e. Group.ReadWrite.All) but just identifiers for it, we will enrich the result with the friendly name oursevles
            var servicePrincipalCache = new Dictionary<Guid, AzureADServicePrincipal>();
            foreach (var azureADServicePrincipalAppRoleAssignment in azureADServicePrincipalAppRoleAssignments)
            {
                // Ensure we have the ResourceId and the AppRoleId which will be needed to define the friendly claim value for the app role assignment
                if (!azureADServicePrincipalAppRoleAssignment.ResourceId.HasValue || !azureADServicePrincipalAppRoleAssignment.AppRoleId.HasValue) continue;

                // Keep a cache for the service principals to avoid unnecessary calls to the Graph API
                AzureADServicePrincipal servicePrincipal;

                // Check if we have the service principal in the cache
                if (servicePrincipalCache.ContainsKey(azureADServicePrincipalAppRoleAssignment.ResourceId.Value))
                {
                    // Service principal is in cache, so use it
                    servicePrincipal = servicePrincipalCache[azureADServicePrincipalAppRoleAssignment.ResourceId.Value];
                }
                else
                {
                    // Service principal is not in cache yet, retrieve it from the Graph API
                    servicePrincipal = GetServicePrincipalByObjectId(requestHelper, azureADServicePrincipalAppRoleAssignment.ResourceId.Value);

                    if (servicePrincipal != null)
                    {
                        servicePrincipalCache.Add(azureADServicePrincipalAppRoleAssignment.ResourceId.Value, servicePrincipal);
                    }
                }

                if (servicePrincipal != null)
                {
                    // If we have a service principal, look for the app role and match its claim value to the app role assignment name
                    azureADServicePrincipalAppRoleAssignment.AppRoleName = servicePrincipal.AppRoles.FirstOrDefault(ar => ar.Id == azureADServicePrincipalAppRoleAssignment.AppRoleId.Value)?.Value;
                }
            }

            return azureADServicePrincipalAppRoleAssignments;
        }

        /// <summary>
        /// Adds a role assignment to the provided service principal
        /// </summary>
        /// <param name="connection">Connection to use to communicate with Microsoft Graph</param>
        /// <param name="accesstoken">Access Token to use to authenticate to Microsoft Graph</param>
        /// <param name="principalToAddRoleTo">The service principal to add the role to</param>
        /// <param name="appRoleToAdd">The app role to add to the service principal</param>
        /// <returns>The service principal app role assignment</returns>
        public static AzureADServicePrincipalAppRoleAssignment AddServicePrincipalRoleAssignment(GraphHelper requestHelper, AzureADServicePrincipal principalToAddRoleTo, AzureADServicePrincipalAppRole appRoleToAdd)
        {
            var content = new StringContent($"{{'principalId':'{principalToAddRoleTo.Id}','resourceId':'{appRoleToAdd.ServicePrincipal.Id}','appRoleId':'{appRoleToAdd.Id}'}}");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = requestHelper.Post<AzureADServicePrincipalAppRoleAssignment>($"v1.0/servicePrincipals/{principalToAddRoleTo.Id}/appRoleAssignments", content);
            return result;
        }

        /// <summary>
        /// Removes all role assignments from the provided service principal
        /// </summary>
        /// <param name="connection">Connection to use to communicate with Microsoft Graph</param>
        /// <param name="accesstoken">Access Token to use to authenticate to Microsoft Graph</param>
        /// <param name="principalToRemoveRoleFrom">The service principal to remove the role assignments from</param>
        public static void RemoveServicePrincipalRoleAssignment(GraphHelper requestHelper, AzureADServicePrincipal principalToRemoveRoleFrom)
        {
            var assignments = GetServicePrincipalAppRoleAssignmentsByServicePrincipalObjectId(requestHelper, principalToRemoveRoleFrom.Id);
            foreach (var assignment in assignments)
            {
                requestHelper.Delete($"v1.0/servicePrincipals/{principalToRemoveRoleFrom.Id}/appRoleAssignments/{assignment.Id}");
            }
        }

        /// <summary>
        /// Removes the provided role from the role assignments of the provided service principal
        /// </summary>
        /// <param name="connection">Connection to use to communicate with Microsoft Graph</param>
        /// <param name="accesstoken">Access Token to use to authenticate to Microsoft Graph</param>
        /// <param name="principalToRemoveRoleFrom">The service principal to remove the role assignment from</param>
        /// <param name="appRoleToRemove">The app role to remove from the role assignments</param>
        public static void RemoveServicePrincipalRoleAssignment(GraphHelper requestHelper, AzureADServicePrincipal principalToRemoveRoleFrom, AzureADServicePrincipalAppRole appRoleToRemove)
        {
            var assignments = GetServicePrincipalAppRoleAssignmentsByServicePrincipalObjectId(requestHelper, principalToRemoveRoleFrom.Id);
            foreach (var assignment in assignments)
            {
                if (assignment.AppRoleId == appRoleToRemove.Id)
                {
                    requestHelper.Delete($"v1.0/servicePrincipals/{principalToRemoveRoleFrom.Id}/appRoleAssignments/{assignment.Id}");
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
        public static void RemoveServicePrincipalRoleAssignment(GraphHelper requestHelper, AzureADServicePrincipal principalToRemoveRoleFrom, string roleName)
        {
            var assignments = GetServicePrincipalAppRoleAssignmentsByServicePrincipalObjectId(requestHelper, principalToRemoveRoleFrom.Id);
            foreach (var assignment in assignments)
            {
                if (assignment.AppRoleName == roleName)
                {
                    requestHelper.Delete($"v1.0/servicePrincipals/{principalToRemoveRoleFrom.Id}/appRoleAssignments/{assignment.Id}");
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
        public static void RemoveServicePrincipalRoleAssignment(GraphHelper requestHelper, AzureADServicePrincipalAppRoleAssignment appRoleAssignmenToRemove)
        {
            requestHelper.Delete( $"v1.0/servicePrincipals/{appRoleAssignmenToRemove.PrincipalId}/appRoleAssignments/{appRoleAssignmenToRemove.Id}");
        }
    }
}