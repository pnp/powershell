using System;
using System.Collections.Generic;
using System.Linq;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Determines whether the permission scopes present in an access token satisfy a required permission. A required scope is satisfied by the
    /// very same scope, but also by any scope which is strictly more privileged, i.e. Sites.FullControl.All covers Sites.Read.All. Without this
    /// a connection holding the highest scope would be reported as lacking the lowest one, as the permission metadata states the least privileged
    /// scope which suffices rather than the exact scope to hold.
    /// </summary>
    internal static class ApiPermissionEvaluator
    {
        /// <summary>
        /// The SharePoint site scopes ordered by privilege. The delegated notation (AllSites.*) and the application notation (Sites.*.All) describe
        /// the same four levels, so they share one ladder.
        /// Sites.Selected is deliberately absent: it grants access only to the sites the application has explicitly been granted access to, so it
        /// neither covers nor is covered by any of the tenant wide scopes.
        /// </summary>
        private static readonly Dictionary<string, int> SharePointSiteScopeLevels = new(StringComparer.OrdinalIgnoreCase)
        {
            ["AllSites.Read"] = 1,
            ["Sites.Read.All"] = 1,
            ["AllSites.Write"] = 2,
            ["Sites.ReadWrite.All"] = 2,
            ["AllSites.Manage"] = 3,
            ["Sites.Manage.All"] = 3,
            ["AllSites.FullControl"] = 4,
            ["Sites.FullControl.All"] = 4
        };

        /// <summary>
        /// The access levels which can appear as the second segment of a scope, ordered by privilege. Anything not listed here describes an
        /// operation rather than a level and is therefore only ever satisfied by an exact match.
        /// Write is deliberately absent: on Microsoft Graph it denotes an operation which does not include reading, i.e. AuditActivity.Write
        /// uploads audit logs while AuditActivity.Read reads them, so ranking it above Read would report a permission as held which it is not.
        /// The SharePoint scopes which do use Write as a level, such as AllSites.Write, are covered by the ladder above instead.
        /// </summary>
        private static readonly Dictionary<string, int> AccessLevels = new(StringComparer.OrdinalIgnoreCase)
        {
            ["ReadBasic"] = 1,
            ["Read"] = 2,
            ["ReadWrite"] = 3,
            ["Manage"] = 4,
            ["FullControl"] = 5
        };

        /// <summary>
        /// Determines whether any of the scopes present in an access token satisfies the required permission
        /// </summary>
        /// <param name="required">The permission the cmdlet requires</param>
        /// <param name="granted">The permissions present in the access token</param>
        internal static bool IsSatisfiedBy(RequiredApiPermission required, IEnumerable<RequiredApiPermission> granted)
        {
            return required != null && granted != null &&
                   granted.Any(grantedPermission => grantedPermission != null &&
                                                    grantedPermission.ResourceType == required.ResourceType &&
                                                    Covers(grantedPermission.Scope, required.Scope, required.ResourceType));
        }

        private static bool Covers(string grantedScope, string requiredScope, ResourceTypeName resourceType)
        {
            if (string.IsNullOrWhiteSpace(grantedScope) || string.IsNullOrWhiteSpace(requiredScope))
            {
                return false;
            }

            if (grantedScope.Equals(requiredScope, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // SharePoint expresses the same privilege levels in two notations, so those are compared through one ladder rather than by convention
            if (resourceType == ResourceTypeName.SharePoint &&
                SharePointSiteScopeLevels.TryGetValue(grantedScope, out var grantedLevel) &&
                SharePointSiteScopeLevels.TryGetValue(requiredScope, out var requiredLevel))
            {
                return grantedLevel >= requiredLevel;
            }

            return CoversByConvention(grantedScope, requiredScope);
        }

        /// <summary>
        /// Compares two scopes which follow the Resource.Access[.Target] convention, i.e. Group.ReadWrite.All covers Group.Read.All and
        /// Files.Read.All covers Files.Read. Scopes which do not follow the convention, such as Sites.Selected, user_impersonation and the
        /// PowerApps user scope, are only ever satisfied by an exact match.
        /// </summary>
        private static bool CoversByConvention(string grantedScope, string requiredScope)
        {
            var grantedSegments = grantedScope.Split('.');
            var requiredSegments = requiredScope.Split('.');

            if (grantedSegments.Length is < 2 or > 3 || requiredSegments.Length is < 2 or > 3)
            {
                return false;
            }

            // Both scopes must be about the same resource, i.e. Group.ReadWrite.All says nothing about Sites.Read.All
            if (!grantedSegments[0].Equals(requiredSegments[0], StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (!AccessLevels.TryGetValue(grantedSegments[1], out var grantedAccess) ||
                !AccessLevels.TryGetValue(requiredSegments[1], out var requiredAccess) ||
                grantedAccess < requiredAccess)
            {
                return false;
            }

            var grantedTarget = grantedSegments.Length == 3 ? grantedSegments[2] : null;
            var requiredTarget = requiredSegments.Length == 3 ? requiredSegments[2] : null;

            if (string.Equals(grantedTarget, requiredTarget, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // A tenant wide scope covers the narrower variant of the same resource, i.e. Files.Read.All covers Files.Read and
            // ExternalConnection.ReadWrite.All covers ExternalConnection.ReadWrite.OwnedBy. Selected is excluded, as that grants
            // access only to the resources the application has explicitly been granted access to.
            return "All".Equals(grantedTarget, StringComparison.OrdinalIgnoreCase) &&
                   !"Selected".Equals(requiredTarget, StringComparison.OrdinalIgnoreCase);
        }
    }
}
