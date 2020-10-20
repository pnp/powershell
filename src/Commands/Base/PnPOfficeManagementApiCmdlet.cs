using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Properties;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net.Http;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Base class for all the PnP Microsoft Office Management API related cmdlets
    /// </summary>
    public abstract class PnPOfficeManagementApiCmdlet : PnPConnectedCmdlet
    {
        [Parameter(Mandatory = false, DontShow = true)]
        public SwitchParameter ByPassPermissionCheck;

        /// <summary>
        /// Returns an Access Token for the Microsoft Office Management API, if available, otherwise NULL
        /// </summary>
        public OfficeManagementApiToken Token
        {
            get
            {
                var tokenType = TokenType.All;

                // Collect, if present, the token type attribute
                var tokenTypeAttribute = (TokenTypeAttribute)Attribute.GetCustomAttribute(GetType(), typeof(TokenTypeAttribute));
                if (tokenTypeAttribute != null)
                {
                    tokenType = tokenTypeAttribute.TokenType;
                }

                // Collect the permission attributes to discover required roles
                var requiredRoleAttributes = (OfficeManagementApiPermissionCheckAttribute[])Attribute.GetCustomAttributes(GetType(), typeof(OfficeManagementApiPermissionCheckAttribute));
                var orRequiredRoles = new List<string>(requiredRoleAttributes.Length);
                var andRequiredRoles = new List<string>(requiredRoleAttributes.Length);

                foreach (var requiredRoleAttribute in requiredRoleAttributes)
                {
                    foreach (OfficeManagementApiPermission role in Enum.GetValues(typeof(OfficeManagementApiPermission)))
                    {
                        if (role != OfficeManagementApiPermission.None)
                        {
                            if (requiredRoleAttribute.OrApiPermissions.HasFlag(role))
                            {
                                orRequiredRoles.Add(role.ToString().Replace("_", "."));
                            }
                            if (requiredRoleAttribute.AndApiPermissions.HasFlag(role))
                            {
                                andRequiredRoles.Add(role.ToString().Replace("_", "."));
                            }
                        }
                    }
                }

                // Ensure we have an active connection
                if (PnPConnection.CurrentConnection != null)
                {
                    string[] managementShellScopes = null;
                    if (PnPConnection.CurrentConnection.ClientId == PnPConnection.PnPManagementShellClientId)
                    {
                        var managementShellScopesAttribute = (PnPManagementShellScopesAttribute)Attribute.GetCustomAttribute(GetType(), typeof(PnPManagementShellScopesAttribute));
                        if (managementShellScopesAttribute != null)
                        {
                            managementShellScopes = managementShellScopesAttribute.PermissionScopes;
                        }
                    }
                    // There is an active connection, try to get a Microsoft Office Management API Token on the active connection
                    if (PnPConnection.CurrentConnection.TryGetToken(Enums.TokenAudience.OfficeManagementApi, PnPConnection.CurrentConnection.AzureEnvironment, ByPassPermissionCheck.ToBool() ? null : orRequiredRoles.ToArray(), ByPassPermissionCheck.ToBool() ? null : andRequiredRoles.ToArray(), tokenType, managementShellScopes) is OfficeManagementApiToken token)
                    {
                        // Microsoft Office Management API Access Token available, return it
                        return token;
                    }
                }

                // No valid Microsoft Office Management API Access Token available, throw an error
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(string.Format(Resources.NoApiAccessToken, Enums.TokenAudience.OfficeManagementApi)), "NO_OAUTH_TOKEN", ErrorCategory.ConnectionError, null));
                return null;
            }
        }

        /// <summary>
        /// Returns an Access Token for the Microsoft Office Management API, if available, otherwise NULL
        /// </summary>
        public string AccessToken => Token?.AccessToken;

        /// <summary>
        /// Root URL to the Office 365 Management API
        /// </summary>
        protected string ApiRootUrl => $"https://manage.office.com/api/v1.0/{Token.TenantId}/";

    }
}