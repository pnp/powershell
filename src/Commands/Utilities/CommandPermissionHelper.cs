using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Builds and caches the permission metadata of all cmdlets in this module. Used by Get-PnPCommandPermission and the cmdlet name argument completer.
    /// </summary>
    internal static class CommandPermissionHelper
    {
        #region Inference rules

        /// <summary>
        /// Verbs which only read data. Any other verb is assumed to modify data.
        /// </summary>
        private static readonly HashSet<string> ReadVerbs = new(StringComparer.OrdinalIgnoreCase)
        {
            VerbsCommon.Get,
            VerbsCommon.Find,
            VerbsCommon.Search,
            VerbsCommon.Show,
            VerbsData.Export,
            VerbsDiagnostic.Test,
            VerbsDiagnostic.Measure,
            VerbsCommunications.Receive
        };

        /// <summary>
        /// Nouns on which a modifying operation affects the security or the structure of a site, for which Contribute rights are not sufficient.
        /// The noun is evaluated without its PnP prefix.
        /// </summary>
        private static readonly HashSet<string> ElevatedNouns = new(StringComparer.OrdinalIgnoreCase)
        {
            "Site", "Web", "SubWeb", "Feature", "Group", "SiteGroup", "GroupMember", "GroupOwner",
            "User", "RoleDefinition", "RoleAssignment", "CustomAction", "EventReceiver",
            "SiteDesign", "SiteScript", "AppCatalog", "App", "StorageEntity", "HubSite",
            "Theme", "AuditSetting", "PropertyBagValue", "IndexedPropertyBagKey", "WebHook"
        };

        /// <summary>
        /// Fragments in a noun which indicate that a modifying operation affects security, for which Contribute rights are not sufficient.
        /// </summary>
        private static readonly string[] ElevatedNounFragments =
        [
            "Permission", "RoleDefinition", "RoleAssignment", "SiteCollectionAdmin", "Sharing", "ExternalUser", "Auditing"
        ];

        private const string GuidanceInferred = "These permissions have been derived from the type of cmdlet and the operation it performs. They are a least privilege estimate and may need to be raised for specific operations.";

        #endregion

        #region Command index

        private static readonly Lazy<IReadOnlyList<CommandPermission>> _permissions = new(BuildPermissions, isThreadSafe: true);

        private static readonly Lazy<IReadOnlyDictionary<string, CommandPermission>> _permissionsByName = new(() =>
        {
            var index = new Dictionary<string, CommandPermission>(StringComparer.OrdinalIgnoreCase);
            foreach (var permission in _permissions.Value)
            {
                index.TryAdd(permission.CommandName, permission);

                foreach (var alias in permission.Aliases)
                {
                    index.TryAdd(alias, permission);
                }
            }
            return index;
        }, isThreadSafe: true);

        /// <summary>
        /// All cmdlets in this module with their permission information, ordered by cmdlet name
        /// </summary>
        internal static IReadOnlyList<CommandPermission> GetAll() => _permissions.Value;

        /// <summary>
        /// The names of all cmdlets in this module, excluding their aliases, ordered alphabetically
        /// </summary>
        internal static IEnumerable<string> GetCommandNames() => _permissions.Value.Select(permission => permission.CommandName);

        /// <summary>
        /// Looks up the permission information of one cmdlet by its name or by one of its aliases
        /// </summary>
        /// <param name="commandName">Name or alias of the cmdlet to look up</param>
        /// <returns>The permission information of the cmdlet or NULL if no cmdlet with the provided name exists in this module</returns>
        internal static CommandPermission Get(string commandName)
        {
            if (string.IsNullOrWhiteSpace(commandName))
            {
                return null;
            }

            return _permissionsByName.Value.TryGetValue(commandName.Trim(), out var permission) ? permission : null;
        }

        private static IReadOnlyList<CommandPermission> BuildPermissions()
        {
            return GetCmdletTypes()
                .Select(Build)
                .Where(permission => permission != null)
                .OrderBy(permission => permission.CommandName, StringComparer.OrdinalIgnoreCase)
                .ToArray();
        }

        private static IEnumerable<Type> GetCmdletTypes()
        {
            Type[] types;

            try
            {
                types = typeof(BasePSCmdlet).Assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                // Some types could not be loaded. Continue with the ones that could be, so a single unloadable type does not take down the entire lookup.
                types = e.Types.Where(type => type != null).ToArray();
            }

            return types.Where(type => type.GetCustomAttribute<CmdletAttribute>(false) != null);
        }

        #endregion

        #region Permission resolution

        private static CommandPermission Build(Type cmdletType)
        {
            var cmdletAttribute = cmdletType.GetCustomAttribute<CmdletAttribute>(false);
            if (cmdletAttribute == null)
            {
                return null;
            }

            var delegatedAttributes = GetPermissionAttributes<RequiredApiDelegatedPermissions>(cmdletType)
                .Concat(GetPermissionAttributes<RequiredApiDelegatedOrApplicationPermissions>(cmdletType))
                .ToArray();
            var applicationAttributes = GetPermissionAttributes<RequiredApiApplicationPermissions>(cmdletType)
                .Concat(GetPermissionAttributes<RequiredApiDelegatedOrApplicationPermissions>(cmdletType))
                .ToArray();

            var permission = new CommandPermission
            {
                CommandName = $"{cmdletAttribute.VerbName}-{cmdletAttribute.NounName}",
                Aliases = cmdletType.GetCustomAttributes<AliasAttribute>(false).SelectMany(alias => alias.AliasNames).Distinct(StringComparer.OrdinalIgnoreCase).ToArray(),
                DelegatedAvailable = !Attribute.IsDefined(cmdletType, typeof(ApiNotAvailableUnderDelegatedPermissions)),
                ApplicationAvailable = !Attribute.IsDefined(cmdletType, typeof(ApiNotAvailableUnderApplicationPermissions)),
                DelegatedPermissions = ToPermissionSets(delegatedAttributes),
                ApplicationPermissions = ToPermissionSets(applicationAttributes)
            };

            var resourceDependent = cmdletType.GetCustomAttribute<ApiPermissionsDependOnResource>(false);

            if (delegatedAttributes.Length > 0 || applicationAttributes.Length > 0)
            {
                permission.PermissionSource = CommandPermissionSource.Declared;
                permission.MinimumSharePointRole = InferSharePointRole(cmdletType, cmdletAttribute);
            }
            else if (resourceDependent != null)
            {
                permission.PermissionSource = CommandPermissionSource.ResourceDependent;
                permission.MinimumSharePointRole = SharePointMinimumRole.NotApplicable;
                permission.Guidance = BuildResourceDependentGuidance(resourceDependent);
            }
            else
            {
                ApplyInferredPermissions(cmdletType, cmdletAttribute, permission);
            }

            ApplyAdditionalRoles(cmdletType, permission);

            return permission;
        }

        /// <summary>
        /// Composes the guidance for a cmdlet of which the permissions follow from the resource it is pointed at at runtime
        /// </summary>
        private static string BuildResourceDependentGuidance(ApiPermissionsDependOnResource attribute)
        {
            var guidance = string.IsNullOrWhiteSpace(attribute.ParameterName)
                ? "The permissions required by this cmdlet follow from the resource it acts on and can therefore not be stated up front."
                : $"The permissions required by this cmdlet follow from the value provided to -{attribute.ParameterName} and can therefore not be stated up front.";

            if (!string.IsNullOrWhiteSpace(attribute.Remarks))
            {
                guidance += $" {attribute.Remarks}";
            }

            if (!string.IsNullOrWhiteSpace(attribute.DocumentationUrl))
            {
                guidance += $" See {attribute.DocumentationUrl} for the permissions per resource.";
            }

            return guidance;
        }

        private static IEnumerable<RequiredApiPermissionsBase> GetPermissionAttributes<T>(Type cmdletType) where T : RequiredApiPermissionsBase
        {
            return Attribute.GetCustomAttributes(cmdletType, typeof(T)).Cast<RequiredApiPermissionsBase>();
        }

        private static CommandPermissionSet[] ToPermissionSets(IEnumerable<RequiredApiPermissionsBase> attributes)
        {
            return attributes
                .Select(attribute => new CommandPermissionSet
                {
                    Permissions = attribute.PermissionScopes?.Where(permission => permission != null).ToArray() ?? []
                })
                .Where(set => set.Permissions.Length > 0)
                .ToArray();
        }

        /// <summary>
        /// Derives the API permissions for a cmdlet which does not declare them through its permission attributes
        /// </summary>
        private static void ApplyInferredPermissions(Type cmdletType, CmdletAttribute cmdletAttribute, CommandPermission permission)
        {
            // Cmdlets which do not connect to an API at all, i.e. Get-PnPChangeLog or Connect-PnPOnline
            if (!typeof(PnPConnectedCmdlet).IsAssignableFrom(cmdletType))
            {
                permission.PermissionSource = CommandPermissionSource.NotApplicable;
                permission.MinimumSharePointRole = SharePointMinimumRole.NotApplicable;
                permission.Guidance = "This cmdlet does not call into an API which requires permissions to be granted.";
                return;
            }

            // Cmdlets which use SharePoint CSOM. The permission needed follows from the site the cmdlet acts on and the operation it performs.
            if (typeof(PnPSharePointCmdlet).IsAssignableFrom(cmdletType))
            {
                var role = InferSharePointRole(cmdletType, cmdletAttribute);
                var (delegatedScope, applicationScope) = GetSharePointScopes(role);

                permission.PermissionSource = CommandPermissionSource.Inferred;
                permission.MinimumSharePointRole = role;
                permission.DelegatedPermissions = [CreatePermissionSet(ResourceTypeName.SharePoint, delegatedScope)];
                permission.ApplicationPermissions = [CreatePermissionSet(ResourceTypeName.SharePoint, applicationScope)];
                permission.Guidance = GuidanceInferred;
                return;
            }

            // Cmdlets which call into an API for which no permission attributes have been declared and for which the permission cannot be derived, i.e. Microsoft Graph
            permission.PermissionSource = CommandPermissionSource.Unknown;
            permission.MinimumSharePointRole = SharePointMinimumRole.NotApplicable;
            permission.Guidance = "No permission metadata has been declared on this cmdlet and the permissions required could not be derived. Consult the documentation of this cmdlet for the permissions it needs.";
        }

        /// <summary>
        /// Determines the minimum SharePoint role needed to run a cmdlet based on the type of cmdlet, the verb it uses and the noun it acts on
        /// </summary>
        private static SharePointMinimumRole InferSharePointRole(Type cmdletType, CmdletAttribute cmdletAttribute)
        {
            if (!typeof(PnPSharePointCmdlet).IsAssignableFrom(cmdletType))
            {
                return SharePointMinimumRole.NotApplicable;
            }

            // Cmdlets which run against the SharePoint Online admin site always need the tenant administrator role
            if (typeof(PnPSharePointOnlineAdminCmdlet).IsAssignableFrom(cmdletType))
            {
                return SharePointMinimumRole.SharePointAdministrator;
            }

            if (ReadVerbs.Contains(cmdletAttribute.VerbName))
            {
                return SharePointMinimumRole.SiteVisitor;
            }

            return IsElevatedNoun(cmdletAttribute.NounName) ? SharePointMinimumRole.SiteOwner : SharePointMinimumRole.SiteMember;
        }

        /// <summary>
        /// Returns the least privileged SharePoint delegated and application scope which covers the provided role
        /// </summary>
        private static (string Delegated, string Application) GetSharePointScopes(SharePointMinimumRole role) => role switch
        {
            SharePointMinimumRole.SiteVisitor => ("AllSites.Read", "Sites.Read.All"),
            SharePointMinimumRole.SiteMember => ("AllSites.Write", "Sites.ReadWrite.All"),
            _ => ("AllSites.FullControl", "Sites.FullControl.All")
        };

        private static bool IsElevatedNoun(string nounName)
        {
            var noun = nounName.StartsWith("PnP", StringComparison.OrdinalIgnoreCase) ? nounName[3..] : nounName;

            return ElevatedNouns.Contains(noun) || ElevatedNounFragments.Any(fragment => noun.Contains(fragment, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Adds the roles which need to be held next to the API permissions for specific groups of cmdlets
        /// </summary>
        private static void ApplyAdditionalRoles(Type cmdletType, CommandPermission permission)
        {
            var roles = new List<string>();

            switch (permission.MinimumSharePointRole)
            {
                case SharePointMinimumRole.SharePointAdministrator:
                    roles.Add("SharePoint Administrator or Global Administrator");
                    break;
                case SharePointMinimumRole.SiteCollectionAdministrator:
                    roles.Add("Site collection administrator on the target site collection");
                    break;
                case SharePointMinimumRole.SiteOwner:
                    roles.Add("Full Control on the target site, i.e. through the Owners group");
                    break;
                case SharePointMinimumRole.SiteMember:
                    roles.Add("Contribute on the target site, i.e. through the Members group");
                    break;
                case SharePointMinimumRole.SiteVisitor:
                    roles.Add("Read on the target site, i.e. through the Visitors group");
                    break;
            }

            if (cmdletType.Namespace?.Contains(".Taxonomy", StringComparison.OrdinalIgnoreCase) == true)
            {
                roles.Add("Term Store Administrator, Group Manager or Contributor in the term store for write operations");

                if (permission.PermissionSource == CommandPermissionSource.Inferred)
                {
                    permission.Guidance += " These cmdlets use SharePoint CSOM, so Microsoft Graph TermStore permissions do not apply.";
                }
            }

            if (cmdletType.Namespace?.Contains(".UserProfiles", StringComparison.OrdinalIgnoreCase) == true)
            {
                if (permission.MinimumSharePointRole != SharePointMinimumRole.SharePointAdministrator)
                {
                    roles.Add("SharePoint Administrator for tenant wide user profile operations");
                }

                if (permission.PermissionSource == CommandPermissionSource.Inferred)
                {
                    permission.Guidance += " Support for application permissions varies per user profile API.";
                }
            }

            permission.AdditionalRoles = roles.Distinct().ToArray();
        }

        private static CommandPermissionSet CreatePermissionSet(ResourceTypeName resourceType, string scope)
        {
            return new CommandPermissionSet
            {
                Permissions = [new RequiredApiPermission(resourceType, scope)]
            };
        }

        #endregion
    }
}
