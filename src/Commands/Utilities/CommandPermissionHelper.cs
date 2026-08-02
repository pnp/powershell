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
            VerbsCommunications.Read,
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

        /// <summary>
        /// Nouns on which a modifying operation changes the structure of a list or library. That needs the Manage Lists right, which the Contribute permission level does not grant,
        /// so Edit is the lowest permission level that suffices. The API scope needed stays the same as for any other write operation.
        /// </summary>
        private static readonly HashSet<string> ListManagementNouns = new(StringComparer.OrdinalIgnoreCase)
        {
            // Deliberately not including content operations such as Folder, DocumentSet and ListItemVersion: adding, moving or deleting items, folders and versions
            // is covered by Contribute and does not need the Manage Lists right.
            "List", "View", "Field", "ContentType", "ContentTypeToList", "FieldFromList", "ListDesign",
            "DefaultColumnValues", "ListRecordDeclaration", "ListWebhook", "DocumentSetField"
        };

        private const string GuidanceInferred = "These permissions have been derived from the type of cmdlet and the operation it performs. They are a least privilege estimate and may need to be raised for specific operations. The estimate covers the SharePoint API only, so a cmdlet which also calls another API needs the permissions of that API on top of these.";

        private const string GuidanceNotApplicable = "This cmdlet does not require API permissions on the Entra ID application registration used to connect with PnP PowerShell.";

        #endregion

        #region Command index

        // Only the reflection over the assembly is cached. The permission instances themselves are composed per request, so a caller that modifies a returned
        // instance cannot affect what a later call returns.
        private static readonly Lazy<IReadOnlyList<Type>> _cmdletTypes = new(
            () => GetCmdletTypes().OrderBy(GetCommandName, StringComparer.OrdinalIgnoreCase).ToArray(), isThreadSafe: true);

        private static readonly Lazy<IReadOnlyDictionary<string, Type>> _cmdletTypesByName = new(() =>
        {
            var index = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
            foreach (var cmdletType in _cmdletTypes.Value)
            {
                index.TryAdd(GetCommandName(cmdletType), cmdletType);

                foreach (var alias in GetAliases(cmdletType))
                {
                    index.TryAdd(alias, cmdletType);
                }
            }
            return index;
        }, isThreadSafe: true);

        /// <summary>
        /// All cmdlets in this module with their permission information, ordered by cmdlet name
        /// </summary>
        internal static IEnumerable<CommandPermission> GetAll() => _cmdletTypes.Value.Select(Build).Where(permission => permission != null);

        /// <summary>
        /// The names of all cmdlets in this module, excluding their aliases, ordered alphabetically
        /// </summary>
        internal static IEnumerable<string> GetCommandNames() => _cmdletTypes.Value.Select(GetCommandName);

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

            return _cmdletTypesByName.Value.TryGetValue(commandName.Trim(), out var cmdletType) ? Build(cmdletType) : null;
        }

        private static string GetCommandName(Type cmdletType)
        {
            var cmdletAttribute = cmdletType.GetCustomAttribute<CmdletAttribute>(false);
            return $"{cmdletAttribute.VerbName}-{cmdletAttribute.NounName}";
        }

        private static string[] GetAliases(Type cmdletType) => cmdletType.GetCustomAttributes<AliasAttribute>(false)
            .SelectMany(alias => alias.AliasNames)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

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

            // Read all permission attributes in one go so they stay in the order they are declared on the cmdlet. Reading them per type and concatenating would group
            // them by attribute type instead, which breaks the convention that the least privileged alternative is declared first.
            var permissionAttributes = Attribute.GetCustomAttributes(cmdletType, typeof(RequiredApiPermissionsBase)).Cast<RequiredApiPermissionsBase>().ToArray();

            var delegatedAttributes = permissionAttributes
                .Where(attribute => attribute is RequiredApiDelegatedPermissions or RequiredApiDelegatedOrApplicationPermissions)
                .ToArray();
            var applicationAttributes = permissionAttributes
                .Where(attribute => attribute is RequiredApiApplicationPermissions or RequiredApiDelegatedOrApplicationPermissions)
                .ToArray();

            // Where a cmdlet declares permissions, the token types it declares them for are the ones it supports. Declaring a scope for one token type only, such as the
            // delegated only ServiceMessageViewpoint.Write on Set-PnPMessageCenterAnnouncementAsArchived, means the other token type is not supported rather than that it
            // needs no permissions. Where nothing is declared, only the explicit ApiNotAvailableUnder markers say anything about availability.
            var hasDeclaredPermissions = permissionAttributes.Length > 0;

            var permission = new CommandPermission
            {
                CommandName = $"{cmdletAttribute.VerbName}-{cmdletAttribute.NounName}",
                Aliases = GetAliases(cmdletType),
                DelegatedAvailable = !Attribute.IsDefined(cmdletType, typeof(ApiNotAvailableUnderDelegatedPermissions)) && (!hasDeclaredPermissions || delegatedAttributes.Length > 0),
                ApplicationAvailable = !Attribute.IsDefined(cmdletType, typeof(ApiNotAvailableUnderApplicationPermissions)) && (!hasDeclaredPermissions || applicationAttributes.Length > 0),
                DelegatedPermissions = ToPermissionSets(delegatedAttributes),
                ApplicationPermissions = ToPermissionSets(applicationAttributes)
            };

            // A RequiredApiDelegatedOrApplicationPermissions attribute declares a scope for both token types, which contradicts an ApiNotAvailableUnder* attribute
            // on the same cmdlet. The token type the cmdlet cannot be used with wins, so no permissions are reported for it.
            if (permission.DelegatedAvailable == false)
            {
                permission.DelegatedPermissions = [];
            }

            if (permission.ApplicationAvailable == false)
            {
                permission.ApplicationPermissions = [];
            }

            var resourceDependent = cmdletType.GetCustomAttribute<ApiPermissionsDependOnResource>(false);
            var permissionsNotRequired = cmdletType.GetCustomAttribute<ApiPermissionsNotRequired>(false);

            if (permissionsNotRequired != null)
            {
                permission.PermissionSource = CommandPermissionSource.NotApplicable;
                permission.MinimumSharePointRole = SharePointMinimumRole.NotApplicable;
                permission.Guidance = string.IsNullOrWhiteSpace(permissionsNotRequired.Remarks) ? GuidanceNotApplicable : $"{GuidanceNotApplicable} {permissionsNotRequired.Remarks}";
                permission.ResourceTypes = [];
                permission.DelegatedAvailable = null;
                permission.ApplicationAvailable = null;

                return permission;
            }

            if (delegatedAttributes.Length > 0 || applicationAttributes.Length > 0)
            {
                permission.PermissionSource = CommandPermissionSource.Declared;

                // Where SharePoint scopes are declared they say more about the rights needed than the verb does, i.e. Sync-PnPSharePointUserProfilesFromAzureActiveDirectory
                // declares Sites.FullControl.All while its verb would suggest Contribute is enough
                permission.MinimumSharePointRole = GetRoleFromDeclaredSharePointScopes(cmdletType, permission) ?? InferSharePointRole(cmdletType, cmdletAttribute);

                AddSharePointRequirementToDeclaredPermissions(cmdletType, permission);

                // A cmdlet can declare the permissions it always needs and additionally call another API depending on how it is invoked, i.e.
                // Sync-PnPSharePointUserProfilesFromAzureActiveDirectory only calls Microsoft Graph when -Users is not provided. The declared permissions stay
                // authoritative, the conditional requirement is described in the guidance so the reported set is not mistaken for the complete picture.
                if (resourceDependent != null)
                {
                    permission.Guidance = string.IsNullOrWhiteSpace(permission.Guidance)
                        ? BuildResourceDependentGuidance(resourceDependent, isAdditional: true)
                        : $"{permission.Guidance} {BuildResourceDependentGuidance(resourceDependent, isAdditional: true)}";
                }
            }
            else if (resourceDependent != null)
            {
                permission.PermissionSource = CommandPermissionSource.ResourceDependent;
                permission.MinimumSharePointRole = SharePointMinimumRole.NotApplicable;
                permission.Guidance = BuildResourceDependentGuidance(resourceDependent);
                permission.DelegatedAvailable = null;
                permission.ApplicationAvailable = null;
            }
            else
            {
                ApplyInferredPermissions(cmdletType, cmdletAttribute, permission);
            }

            ApplyAdditionalRoles(cmdletType, cmdletAttribute, permission);
            ApplyResourceTypes(permission, resourceDependent);

            return permission;
        }

        /// <summary>
        /// Adds the SharePoint permission needed by a cmdlet which uses SharePoint CSOM but only declares permissions on another API, i.e. Set-PnPList which declares
        /// Microsoft Graph information protection permissions while also performing SharePoint CSOM operations. The SharePoint permission is required next to the
        /// declared permissions, so it is added to each of the declared alternatives to keep the AND within a set and the OR between the sets intact.
        /// </summary>
        private static void AddSharePointRequirementToDeclaredPermissions(Type cmdletType, CommandPermission permission)
        {
            if (!typeof(PnPSharePointCmdlet).IsAssignableFrom(cmdletType))
            {
                return;
            }

            // If a SharePoint permission is already declared, the declared metadata is complete and takes precedence
            if (permission.DelegatedPermissions.Concat(permission.ApplicationPermissions)
                .SelectMany(set => set.Permissions)
                .Any(scope => scope.ResourceType == ResourceTypeName.SharePoint))
            {
                return;
            }

            var (delegatedScope, applicationScope) = GetSharePointScopes(permission.MinimumSharePointRole);

            if (permission.DelegatedAvailable == true)
            {
                permission.DelegatedPermissions = AddScopeToEachSet(permission.DelegatedPermissions, delegatedScope);
            }

            if (permission.ApplicationAvailable == true)
            {
                permission.ApplicationPermissions = AddScopeToEachSet(permission.ApplicationPermissions, applicationScope);
            }

            permission.PermissionSource = CommandPermissionSource.DeclaredAndInferred;
            permission.Guidance = "This cmdlet uses SharePoint CSOM next to the API for which permissions are declared on it. The SharePoint permission listed has been derived from the operation the cmdlet performs and is required in addition to the declared permissions.";
        }

        /// <summary>
        /// Adds a scope to every alternative in the provided sets, or returns a single set holding just that scope if there are no alternatives yet
        /// </summary>
        private static CommandPermissionSet[] AddScopeToEachSet(CommandPermissionSet[] sets, string scope)
        {
            var additionalScope = new RequiredApiPermission(ResourceTypeName.SharePoint, scope);

            if (sets.Length == 0)
            {
                return [new CommandPermissionSet { Permissions = [additionalScope] }];
            }

            return sets.Select(set => new CommandPermissionSet
            {
                Permissions = [.. set.Permissions, additionalScope]
            }).ToArray();
        }

        /// <summary>
        /// Records the APIs this cmdlet needs permissions on, so cmdlets of which the exact scopes depend on the resource can still be found when filtering on a resource type
        /// </summary>
        private static void ApplyResourceTypes(CommandPermission permission, ApiPermissionsDependOnResource resourceDependent)
        {
            var resourceTypes = permission.DelegatedPermissions.Concat(permission.ApplicationPermissions)
                .SelectMany(set => set.Permissions)
                .Select(scope => scope.ResourceType)
                .ToList();

            if (resourceDependent != null)
            {
                resourceTypes.Add(resourceDependent.ResourceType);
            }

            permission.ResourceTypes = resourceTypes.Distinct().OrderBy(resourceType => resourceType).ToArray();
        }

        /// <summary>
        /// Composes the guidance for a cmdlet of which the permissions follow from the resource it is pointed at at runtime
        /// </summary>
        private static string BuildResourceDependentGuidance(ApiPermissionsDependOnResource attribute, bool isAdditional = false)
        {
            var subject = isAdditional
                ? "This cmdlet can require additional permissions which"
                : "The permissions required by this cmdlet";

            var guidance = string.IsNullOrWhiteSpace(attribute.ParameterName)
                ? $"{subject} follow from the resource it acts on and can therefore not be stated up front."
                : $"{subject} follow from the value provided to -{attribute.ParameterName} and can therefore not be stated up front.";

            if (!string.IsNullOrWhiteSpace(attribute.Remarks))
            {
                guidance += $" {attribute.Remarks}";
            }

            if (!string.IsNullOrWhiteSpace(attribute.DocumentationUrl))
            {
                guidance += isAdditional ? $" See {attribute.DocumentationUrl}." : $" See {attribute.DocumentationUrl} for the permissions per resource.";
            }

            return guidance;
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
            // Cmdlets which do not use the PnP connection, i.e. Get-PnPChangeLog or Connect-PnPOnline. Note that a cmdlet in this category can still call an API using
            // a token it acquires itself, such as Register-PnPEntraIDApp, which is why the guidance is scoped to the application registration PnP PowerShell connects with.
            if (!typeof(PnPConnectedCmdlet).IsAssignableFrom(cmdletType))
            {
                permission.PermissionSource = CommandPermissionSource.NotApplicable;
                permission.MinimumSharePointRole = SharePointMinimumRole.NotApplicable;
                permission.Guidance = GuidanceNotApplicable;
                permission.DelegatedAvailable = null;
                permission.ApplicationAvailable = null;
                return;
            }

            // Cmdlets which use SharePoint CSOM. The permission needed follows from the site the cmdlet acts on and the operation it performs.
            if (typeof(PnPSharePointCmdlet).IsAssignableFrom(cmdletType))
            {
                var role = InferSharePointRole(cmdletType, cmdletAttribute);
                var (delegatedScope, applicationScope) = GetSharePointScopes(role);

                permission.PermissionSource = CommandPermissionSource.Inferred;
                permission.MinimumSharePointRole = role;
                permission.Guidance = GuidanceInferred;

                // Do not report permissions for a token type the cmdlet declares it cannot be used with
                if (permission.DelegatedAvailable == true)
                {
                    permission.DelegatedPermissions = [CreatePermissionSet(ResourceTypeName.SharePoint, delegatedScope)];
                }

                if (permission.ApplicationAvailable == true)
                {
                    // For an operation scoped to a single site, Sites.Selected combined with a grant on that site is less privileged than any of the tenant wide
                    // scopes, which is why the declared metadata lists it first as well, i.e. on Get-PnPList. It does not apply to cmdlets which run against the
                    // SharePoint Online admin site, as those act tenant wide by definition.
                    permission.ApplicationPermissions = role == SharePointMinimumRole.SharePointAdministrator
                        ? [CreatePermissionSet(ResourceTypeName.SharePoint, applicationScope)]
                        : [CreatePermissionSet(ResourceTypeName.SharePoint, "Sites.Selected"), CreatePermissionSet(ResourceTypeName.SharePoint, applicationScope)];
                }

                return;
            }

            // Cmdlets which call into an API for which no permission attributes have been declared and for which the permission cannot be derived, i.e. Microsoft Graph
            permission.PermissionSource = CommandPermissionSource.Unknown;
            permission.MinimumSharePointRole = SharePointMinimumRole.NotApplicable;
            permission.Guidance = "No permission metadata has been declared on this cmdlet and the permissions required could not be derived. Consult the documentation of this cmdlet for the permissions it needs.";
            permission.DelegatedAvailable = null;
            permission.ApplicationAvailable = null;
        }

        /// <summary>
        /// Maps a declared SharePoint scope to the permission level it corresponds to on a site. Scopes which do not describe a level on a site, such as
        /// Sites.Selected, TermStore.ReadWrite.All and User.ReadWrite.All, have no equivalent and are skipped.
        /// </summary>
        private static readonly Dictionary<string, SharePointMinimumRole> SharePointScopeRoles = new(StringComparer.OrdinalIgnoreCase)
        {
            ["AllSites.Read"] = SharePointMinimumRole.SiteVisitor,
            ["Sites.Read.All"] = SharePointMinimumRole.SiteVisitor,
            ["AllSites.Write"] = SharePointMinimumRole.SiteMember,
            ["Sites.ReadWrite.All"] = SharePointMinimumRole.SiteMember,
            ["AllSites.Manage"] = SharePointMinimumRole.SiteEditor,
            ["Sites.Manage.All"] = SharePointMinimumRole.SiteEditor,
            ["AllSites.FullControl"] = SharePointMinimumRole.SiteOwner,
            ["Sites.FullControl.All"] = SharePointMinimumRole.SiteOwner
        };

        /// <summary>
        /// Determines the minimum SharePoint role from the SharePoint scopes a cmdlet declares itself, which is more accurate than deriving it from the verb.
        /// Within one set the scopes are all required, so the highest level in it applies. The sets are alternatives, so the lowest of those levels is the minimum.
        /// </summary>
        /// <returns>The role, or NULL when the cmdlet declares no SharePoint scope that maps to a permission level on a site</returns>
        private static SharePointMinimumRole? GetRoleFromDeclaredSharePointScopes(Type cmdletType, CommandPermission permission)
        {
            // Cmdlets which run against the SharePoint Online admin site always need the tenant administrator role, whatever they declare
            if (typeof(PnPSharePointOnlineAdminCmdlet).IsAssignableFrom(cmdletType))
            {
                return SharePointMinimumRole.SharePointAdministrator;
            }

            var rolesPerSet = permission.DelegatedPermissions.Concat(permission.ApplicationPermissions)
                .Select(set => set.Permissions
                    .Where(scope => scope.ResourceType == ResourceTypeName.SharePoint && SharePointScopeRoles.ContainsKey(scope.Scope))
                    .Select(scope => SharePointScopeRoles[scope.Scope])
                    .DefaultIfEmpty()
                    .Max())
                .Where(role => role != default)
                .ToArray();

            return rolesPerSet.Length > 0 ? rolesPerSet.Min() : null;
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

            if (IsElevatedNoun(cmdletAttribute.NounName))
            {
                return SharePointMinimumRole.SiteOwner;
            }

            // Changing the structure of a list needs the Manage Lists right, which Contribute does not grant
            return IsListManagementNoun(cmdletAttribute.NounName) ? SharePointMinimumRole.SiteEditor : SharePointMinimumRole.SiteMember;
        }

        /// <summary>
        /// Returns the least privileged SharePoint delegated and application scope which covers the provided role
        /// </summary>
        private static (string Delegated, string Application) GetSharePointScopes(SharePointMinimumRole role) => role switch
        {
            SharePointMinimumRole.SiteVisitor => ("AllSites.Read", "Sites.Read.All"),
            SharePointMinimumRole.SiteMember or SharePointMinimumRole.SiteEditor => ("AllSites.Write", "Sites.ReadWrite.All"),
            _ => ("AllSites.FullControl", "Sites.FullControl.All")
        };

        private static bool IsElevatedNoun(string nounName)
        {
            var noun = nounName.StartsWith("PnP", StringComparison.OrdinalIgnoreCase) ? nounName[3..] : nounName;

            return ElevatedNouns.Contains(noun) || ElevatedNounFragments.Any(fragment => noun.Contains(fragment, StringComparison.OrdinalIgnoreCase));
        }

        private static bool IsListManagementNoun(string nounName)
        {
            var noun = nounName.StartsWith("PnP", StringComparison.OrdinalIgnoreCase) ? nounName[3..] : nounName;

            return ListManagementNouns.Contains(noun);
        }

        /// <summary>
        /// Adds the roles which need to be held next to the API permissions for specific groups of cmdlets
        /// </summary>
        private static void ApplyAdditionalRoles(Type cmdletType, CmdletAttribute cmdletAttribute, CommandPermission permission)
        {
            var roles = new List<string>();
            var isReadOperation = ReadVerbs.Contains(cmdletAttribute.VerbName);

            // A tenant wide application permission such as Sites.ReadWrite.All already grants access to every site, so no role has to be assigned on the site itself.
            // These roles apply to delegated access, where the signed in user is bound by their own rights, and to app only access using Sites.Selected.
            const string appliesTo = "when connecting delegated, or app only using Sites.Selected";

            switch (permission.MinimumSharePointRole)
            {
                case SharePointMinimumRole.SharePointAdministrator:
                    roles.Add("SharePoint Administrator or Global Administrator when connecting delegated");
                    break;
                case SharePointMinimumRole.SiteCollectionAdministrator:
                    roles.Add($"Site collection administrator on the target site collection {appliesTo}");
                    break;
                case SharePointMinimumRole.SiteOwner:
                    roles.Add($"Full Control on the target site, i.e. through the Owners group, {appliesTo}");
                    break;
                case SharePointMinimumRole.SiteEditor:
                    roles.Add($"Edit on the target site, Contribute is not sufficient to manage lists, {appliesTo}");
                    break;
                case SharePointMinimumRole.SiteMember:
                    roles.Add($"Contribute on the target site, i.e. through the Members group, {appliesTo}");
                    break;
                case SharePointMinimumRole.SiteVisitor:
                    roles.Add($"Read on the target site, i.e. through the Visitors group, {appliesTo}");
                    break;
            }

            // Only modifying operations on the term store need a term store role, reading terms does not
            if (cmdletType.Namespace?.Contains(".Taxonomy", StringComparison.OrdinalIgnoreCase) == true && !isReadOperation)
            {
                roles.Add("Term Store Administrator, Group Manager or Contributor in the term store");

                if (permission.PermissionSource == CommandPermissionSource.Inferred)
                {
                    permission.Guidance += " These cmdlets use SharePoint CSOM, so Microsoft Graph TermStore permissions do not apply.";
                }
            }

            // Only for the user profile cmdlets which act on SharePoint. The namespace also holds Microsoft Graph only cmdlets such as Get-PnPUserProfilePhoto,
            // for which no SharePoint role applies at all.
            if (cmdletType.Namespace?.Contains(".UserProfiles", StringComparison.OrdinalIgnoreCase) == true && typeof(PnPSharePointCmdlet).IsAssignableFrom(cmdletType))
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
