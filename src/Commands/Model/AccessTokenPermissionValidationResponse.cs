using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using PnP.PowerShell.Commands.Utilities;
using System.Data;
using PnP.Framework.Diagnostics;

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
        /// <param name="tokenType">The type of token that is being validated (delegate or app-only)</param>
        /// <returns><see cref="AccessTokenPermissionValidationResponse[]"/> instance containing the results of the evaluation of each of the permission attributes on the cmdlet or NULL if the permission validation failed</returns>
        internal static AccessTokenPermissionValidationResponse[] EvaluatePermissions(Type cmdletType, string accessToken, Enums.ResourceTypeName audience, Enums.IdType tokenType)
        {
            Log.Debug("AccessTokenPermissionValidationResponse",$"Evaluating {tokenType.GetDescription()} permissions in access token for audience {audience.GetDescription()}");
            //cmdlet.LogDebug($"Evaluating {tokenType.GetDescription()} permissions in access token for audience {audience.GetDescription()}");

            // Retrieve the scopes we have in our AccessToken
            var scopes = TokenHandler.ReturnScopes(accessToken);

            if (scopes.Length == 0)
            {
                Log.Debug("AccessTokenPermissionValidationResponse",$"Access token does not contain any specific {tokenType.GetDescription()} permission scopes for resource {audience.GetDescription()}");
            }
            else
            {
                Log.Debug("AccessTokenPermissionValidationResponse",$"Access token contains the following {(scopes.Length != 1 ? $"{scopes.Length} " : "")}{tokenType.GetDescription()} permission scope{(scopes.Length != 1 ? "s" : "")} for resource {audience.GetDescription()}: {string.Join(", ", scopes.Select(s => s.Scope))}");
            }

            // Check if an attribute is present on the cmdlet that indicates that the cmdlet is not available under the current token type
            if((Attribute.IsDefined(cmdletType, typeof(ApiNotAvailableUnderDelegatedPermissions)) && tokenType == Enums.IdType.Delegate) ||
               (Attribute.IsDefined(cmdletType, typeof(ApiNotAvailableUnderApplicationPermissions)) && tokenType == Enums.IdType.Application))
            {
                Log.Debug("AccessTokenPermissionValidationResponse",$"This cmdlet is not available under {tokenType.GetDescription()} permissions");
                return null;
            }

            // Examine the permission attributes on the cmdlet class to determine the required permissions
            RequiredApiPermission[] requiredScopes = null;

            
            var requiredScopesAttributes = ((RequiredApiPermissionsBase[])Attribute.GetCustomAttributes(cmdletType, tokenType == Enums.IdType.Application ? typeof(RequiredApiApplicationPermissions) : typeof(RequiredApiDelegatedPermissions))).Concat((RequiredApiPermissionsBase[])Attribute.GetCustomAttributes(cmdletType, typeof(RequiredApiDelegatedOrApplicationPermissions))).ToArray();

            // No permissions have been defined, so we assume that no permissions are required and thus the validation succeeds
            if (requiredScopesAttributes == null || requiredScopesAttributes.Length == 0)
            {
                Log.Debug("AccessTokenPermissionValidationResponse","No required permissions have been defined on this cmdlet");

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

                Log.Debug("AccessTokenPermissionValidationResponse",$"Validating {tokenType.GetDescription()} permission{(requiredScopes.Length != 1 ? "s" : "")} on {audience.GetDescription()}: {string.Join(" and ", requiredScopes.Select(s => s.Scope))} - {(responses.Last().RequiredPermissionsPresent ? "Present" : "Not present")}");
            }

            if(responses.Any(r => r.RequiredPermissionsPresent))
            {
                Log.Debug("AccessTokenPermissionValidationResponse","Permission validation succeeded");
            }
            else
            {
                Log.Debug("AccessTokenPermissionValidationResponse","Permission validation failed");
            }

            return responses.ToArray();            
        }

        #endregion
    }
}