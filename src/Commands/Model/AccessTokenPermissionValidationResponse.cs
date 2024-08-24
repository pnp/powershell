using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public string[] MissingPermissions { get; set; }

        /// <summary>
        /// List of permissions that are required for the Cmdlet to run
        /// </summary>
        public string[] RequiredPermissions { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Extracts the oAuth JWT token to compare the permissions in it (roles) with the required permissions for the cmdlet provided through an attribute
        /// </summary>
        /// <param name="cmdletType">The cmdlet that will be executed. Used to check for the permissions attribute.</param>
        /// <param name="accessToken">The oAuth JWT token that needs to be validated for its roles</param>
        /// <returns><see cref="AccessTokenPermissionValidationResponse"/> instance containing the results of the evaluation</returns>
        internal static List<AccessTokenPermissionValidationResponse> EvaluatePermissions(Type cmdletType, string accessToken)
        {
            var responses = new List<AccessTokenPermissionValidationResponse>();

            string[] requiredScopes = null;
            var attributes = (RequiredMinimalApiPermissions[])Attribute.GetCustomAttributes(cmdletType, typeof(RequiredMinimalApiPermissions));

            foreach (var requiredScopesAttribute in attributes)
            {

                if (requiredScopesAttribute != null)
                {
                    requiredScopes = requiredScopesAttribute.PermissionScopes;
                }

                // Ensure there are permission attributes present on the cmdlet, otherwise we have nothing to compare it against
                if (requiredScopes == null || requiredScopes.Length == 0)
                {
                    responses.Add(new AccessTokenPermissionValidationResponse
                    {
                        RequiredPermissionsPresent = true,
                        MissingPermissions = Array.Empty<string>(),
                        RequiredPermissions = Array.Empty<string>()
                    });
                }
                else
                {
                    // Retrieve the scopes we have in our AccessToken
                    var scopes = TokenHandler.ReturnScopes(accessToken);

                    var missingScopes = requiredScopes.Where(rs => !scopes.Contains(rs)).ToArray();

                    responses.Add(new AccessTokenPermissionValidationResponse
                    {
                        RequiredPermissionsPresent = missingScopes.Length == 0,
                        RequiredPermissions = requiredScopes,
                        MissingPermissions = missingScopes
                    });

                }

            }
            return responses;
            #endregion
        }
    }
}