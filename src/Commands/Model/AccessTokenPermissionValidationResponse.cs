using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Model to evaluate and reply to an access token permission validation when cross checking it with the required permissions for a cmdlet
    /// </summary>
    public class AccessTokenPermissionValidationResponse
    {
        #region Properties

        /// <summary>
        /// Indicates if the permissions required for the Cmdlet are present in the access token
        /// </summary>
        public bool RequiredPermissionsPresent { get; set; }

        /// <summary>
        /// List of permissions that are missing in the access token
        /// </summary>
        public RequiredApiPermission[] MissingPermissions { get; set; }

        /// <summary>
        /// List of permissions that are required for the Cmdlet to run
        /// </summary>
        public RequiredApiPermission[] RequiredPermissions { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Extracts the oAuth JWT token to compare the permissions in it (roles) with the required permissions for the cmdlet provided through an attribute
        /// </summary>
        /// <param name="cmdlet">The cmdlet that will be executed. Used to check for the permissions attribute.</param>
        /// <param name="accessToken">The oAuth JWT token that needs to be validated for its roles</param>
        /// <param name="audience">The audience for which the permissions should be validated, i.e. Microsoft Graph</param>
        /// <returns><see cref="AccessTokenPermissionValidationResponse[]"/> instance containing the results of the evaluation of each of the permission attributes on the cmdlet</returns>
        internal static AccessTokenPermissionValidationResponse[] EvaluatePermissions(Cmdlet cmdlet, string accessToken, Enums.ResourceTypeName audience)
        {
            cmdlet.WriteVerbose($"Evaluating permissions in access token for audience {audience.GetDescription()}");

            RequiredApiPermission[] requiredScopes = null;
            var requiredScopesAttributes = (RequiredMinimalApiPermissions[])Attribute.GetCustomAttributes(cmdlet.GetType(), typeof(RequiredMinimalApiPermissions));

            // No permissions have been defined, so we assume that no permissions are required and thus the validation succeeds
            if (requiredScopesAttributes == null || requiredScopesAttributes.Length == 0)
            {
                cmdlet.WriteVerbose("No required permissions have been defined on this cmdlet");

                return new[] {
                    new AccessTokenPermissionValidationResponse
                    {
                        RequiredPermissionsPresent = true,
                        MissingPermissions = Array.Empty<RequiredApiPermission>(),
                        RequiredPermissions = Array.Empty<RequiredApiPermission>()
                    }
                };
            }

            // Create a list to hold the evaluation of the permissions in each attribute
            var responses = new List<AccessTokenPermissionValidationResponse>(requiredScopesAttributes.Length);

            // Retrieve the scopes we have in our AccessToken
            var scopes = TokenHandler.ReturnScopes(accessToken);

            if (scopes.Length == 0)
            {
                cmdlet.WriteVerbose($"Access token does not contain any scopes for resource {audience.GetDescription()}");
            }
            else
            {
                cmdlet.WriteVerbose($"Access token contains the following {scopes.Length} scope{(scopes.Length != 1 ? "s" : "")} for resource {audience.GetDescription()}: {string.Join(", ", scopes.Select(s => s.Scope))}");
            }

            // Each attribute specifies one or more required scopes which are considered as ANDs towards eachother. The attributes towards eachother are considered as ORs. So at least all of the scopes in one of the attributes should be present in the access token.
            foreach (var requiredScopesAttribute in requiredScopesAttributes)
            {
                if (requiredScopesAttribute != null)
                {
                    requiredScopes = requiredScopesAttribute.PermissionScopes.Where(ps => ps.ResourceType == audience).ToArray();
                }

                // Ensure there are permission attributes present on the cmdlet, otherwise we have nothing to compare it against
                if (requiredScopes == null || requiredScopes.Length == 0)
                {
                    // No permission attributes on the cmdlet, so we assume that no specific permissions are required and thus the validation succeeds
                    responses.Add(new AccessTokenPermissionValidationResponse
                    {
                        RequiredPermissionsPresent = true,
                        MissingPermissions = Array.Empty<RequiredApiPermission>(),
                        RequiredPermissions = Array.Empty<RequiredApiPermission>()
                    });
                }
                else
                {
                    // Permissions have been defined, so we need to check if the access token contains these permissions
                    var missingScopes = requiredScopes.Where(requiredScope => !scopes.Any(scope => scope.Scope.Equals(requiredScope.Scope, StringComparison.InvariantCultureIgnoreCase))).ToArray();

                    responses.Add(new AccessTokenPermissionValidationResponse
                    {
                        RequiredPermissionsPresent = missingScopes.Length == 0,
                        RequiredPermissions = requiredScopes,
                        MissingPermissions = missingScopes
                    });
                }

                cmdlet.WriteVerbose($"Required permissions on {audience.GetDescription()}: {string.Join(" and ", requiredScopes.Select(s => s.Scope))} - {(responses.Last().RequiredPermissionsPresent ? "Present" : "Not present")}");
            }

            if(responses.Any(r => r.RequiredPermissionsPresent))
            {
                cmdlet.WriteVerbose("Permission validation succeeded");
            }
            else
            {
                cmdlet.WriteVerbose("Permission validation failed");
            }

            return responses.ToArray();            
        }

        #endregion
    }
}