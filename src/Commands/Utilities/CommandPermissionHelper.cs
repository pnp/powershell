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
        /// Fragments in a noun for which both reading and changing need Full Control. Enumerating and managing permissions, reading the audit configuration and
        /// reading who a site is shared with are rights that only the Full Control permission level holds, so unlike the other categories this one is not limited
        /// to modifying operations.
        /// </summary>
        // Deliberately not matching on "Sharing" as a whole: sharing a file or a folder is something a member can do, only the sharing configuration of the site itself
        // is restricted, which is why SharingForNonOwnersOfSite is listed explicitly.
        private static readonly string[] FullControlNounFragments =
        [
            "Permission", "RoleDefinition", "RoleAssignment", "ExternalUser", "Auditing", "RequestAccessEmails",
            "SearchConfiguration", "SearchSettings", "SharingForNonOwnersOfSite", "SharingSetting", "SiteSharing"
        ];

        /// <summary>
        /// Nouns on which a modifying operation changes the security, the structure, the branding or the configuration of a site. Those need rights such as Manage Web Site,
        /// Apply Themes, Add and Customize Pages or Manage Permissions, none of which the Contribute or Edit permission levels hold. The noun is evaluated without its PnP prefix.
        /// </summary>
        private static readonly HashSet<string> SiteConfigurationNouns = new(StringComparer.OrdinalIgnoreCase)
        {
            // Structure and security of the site
            "Site", "Web", "SubWeb", "Feature", "Group", "SiteGroup", "GroupMember", "GroupOwner", "User",
            "RoleDefinition", "RoleAssignment", "HubSite", "SiteTemplate", "TenantTemplate",

            // Customizations registered on the site itself, which need Manage Web Site
            "CustomAction", "ApplicationCustomizer", "EventReceiver", "WebAction", "JavaScriptBlock", "JavaScriptLink",
            "NavigationNode", "Footer", "WebHeader", "PageScheduling", "CommSite",
            "BrandCenterFont", "BrandCenterFontPackage",

            // Applications and app catalogs
            "App", "AppCatalog", "AppSideLoading", "AppToTeams", "StorageEntity",

            // Site level settings, policies and governance
            "SiteDesign", "SiteScript", "SiteDesignTask", "SiteClassification", "SiteClosure", "SitePolicy",
            "AuditSetting", "PropertyBagValue", "IndexedPropertyBagKey", "IndexedProperty", "IndexedProperties",
            "InPlaceRecordsManagement", "RetentionLabel", "DocumentId", "SiteDocumentIdPrefix", "TeamifyPromptHidden",
            "SiteCollectionTermStore", "SyntexModel", "SyntexClassifyAndExtract", "VivaConnectionsDashboardACE",
            "ReIndexWeb", "WebHook",

            // Tenant wide version trim and cleanup jobs run against the whole site
            "SiteFileVersionBatchDeleteJob", "SiteFileVersionExpirationReportJob",
            "LibraryFileVersionBatchDeleteJob", "LibraryFileVersionExpirationReportJob"
        };

        /// <summary>
        /// Nouns on which a modifying operation customizes pages or branding. Per the SharePoint permission level reference those need the Add and Customize Pages,
        /// Apply Themes and Borders or Apply Style Sheets rights, which are held by Design and Full Control but not by Edit or Contribute.
        /// Modern site pages live in a document library, so those are content operations a member can perform and are deliberately not listed here.
        /// </summary>
        private static readonly HashSet<string> SiteDesignNouns = new(StringComparer.OrdinalIgnoreCase)
        {
            "AvailablePageLayouts", "DefaultPageLayout", "PublishingImageRendition", "PublishingPageLayout",
            "HtmlPublishingPageLayout", "PublishingPage", "WikiPage", "WikiPageContent", "MasterPage",
            "WebPart", "WebPartProperty", "WebPartToWebPartPage", "WebPartToWikiPage",
            "Theme", "WebTheme", "HomePage"
        };

        /// <summary>
        /// Nouns on which a modifying operation changes the structure of a list or library. That needs the Manage Lists right, which the Contribute permission level does not grant,
        /// so Edit is the lowest permission level that suffices. The API scope needed stays the same as for any other write operation.
        /// </summary>
        private static readonly HashSet<string> ListManagementNouns = new(StringComparer.OrdinalIgnoreCase)
        {
            // Deliberately not including content operations such as Folder, DocumentSet and ListItemVersion: adding, moving or deleting items, folders and versions
            // is covered by Contribute and does not need the Manage Lists right.
            "List", "View", "ViewsFromXML", "Field", "TaxonomyField", "FieldFromList", "FieldFromXml",
            "ContentType", "ContentTypeToList", "ContentTypeFromList", "ContentTypeToDocumentSet",
            "ContentTypeFromDocumentSet", "ContentTypesFromContentTypeHub", "DefaultContentTypeToList",
            "FieldFromContentType", "FieldToContentType", "DocumentSetField", "DocumentSetTemplate",
            "ListDesign", "DefaultColumnValues", "ListRecordDeclaration", "ListWebhook", "WebhookSubscription",
            "ListInformationRightsManagement", "ReIndexList"
        };

        private const string GuidanceInferred = "These permissions have been derived from the type of cmdlet and the operation it performs. They are a least privilege estimate and may need to be raised for specific operations. The estimate covers the SharePoint API only, so a cmdlet which also calls another API needs the permissions of that API on top of these.";

        private const string GuidanceNotApplicable = "This cmdlet does not require API permissions on the Entra ID application registration used to connect with PnP PowerShell.";

        /// <summary>
        /// The SharePoint application scope which grants access only to the sites the application has explicitly been granted access to, making it the least privileged of all
        /// </summary>
        private const string SitesSelectedScope = "Sites.Selected";

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

            // Only the explicit markers prove that a cmdlet cannot be used with a token type. The absence of a declared scope for one of the two says nothing:
            // New-PnPSite declares an application permission only, yet runs perfectly well delegated. Such a gap is reported as undetermined rather than unavailable.
            var delegatedUnavailable = Attribute.IsDefined(cmdletType, typeof(ApiNotAvailableUnderDelegatedPermissions));
            var applicationUnavailable = Attribute.IsDefined(cmdletType, typeof(ApiNotAvailableUnderApplicationPermissions));

            var permission = new CommandPermission
            {
                CommandName = $"{cmdletAttribute.VerbName}-{cmdletAttribute.NounName}",
                Aliases = GetAliases(cmdletType),
                DelegatedPermissions = delegatedUnavailable ? [] : ToPermissionSets(delegatedAttributes),
                ApplicationPermissions = applicationUnavailable ? [] : ToPermissionSets(applicationAttributes)
            };

            var resourceDependent = cmdletType.GetCustomAttribute<ApiPermissionsDependOnResource>(false);
            var permissionsNotRequired = cmdletType.GetCustomAttribute<ApiPermissionsNotRequired>(false);

            if (permissionsNotRequired != null)
            {
                permission.PermissionSource = CommandPermissionSource.NotApplicable;
                permission.MinimumSharePointRole = SharePointMinimumRole.NotApplicable;
                permission.Guidance = string.IsNullOrWhiteSpace(permissionsNotRequired.Remarks) ? GuidanceNotApplicable : $"{GuidanceNotApplicable} {permissionsNotRequired.Remarks}";
                permission.ResourceTypes = [];

                ApplyAvailability(permission, delegatedUnavailable, applicationUnavailable);

                return permission;
            }

            if (permission.DelegatedPermissions.Length > 0 || permission.ApplicationPermissions.Length > 0)
            {
                permission.PermissionSource = CommandPermissionSource.Declared;

                // Where SharePoint scopes are declared they say more about the rights needed than the verb does, i.e. Sync-PnPSharePointUserProfilesFromAzureActiveDirectory
                // declares Sites.FullControl.All while its verb would suggest Contribute is enough
                permission.MinimumSharePointRole = GetRoleFromDeclaredSharePointScopes(cmdletType, permission) ?? InferSharePointRole(cmdletType, cmdletAttribute);

                AddSharePointRequirementToDeclaredPermissions(cmdletType, permission, delegatedUnavailable, applicationUnavailable);
            }
            else
            {
                ApplyInferredPermissions(cmdletType, cmdletAttribute, permission, delegatedUnavailable, applicationUnavailable);
            }

            if (resourceDependent != null)
            {
                if (permission.PermissionSource == CommandPermissionSource.Unknown)
                {
                    // Nothing could be determined, so the resource the cmdlet is pointed at is the whole story, i.e. for New-PnPGraphSubscription
                    permission.PermissionSource = CommandPermissionSource.ResourceDependent;
                    permission.MinimumSharePointRole = SharePointMinimumRole.NotApplicable;
                    permission.Guidance = BuildResourceDependentGuidance(resourceDependent);
                }
                else
                {
                    // The cmdlet has permissions of its own and calls another API depending on how it is invoked, i.e. Set-PnPSiteClassification only calls Microsoft
                    // Graph for a site with a Microsoft 365 group behind it. What was determined stays, the conditional requirement is added to the guidance.
                    permission.Guidance = string.IsNullOrWhiteSpace(permission.Guidance)
                        ? BuildResourceDependentGuidance(resourceDependent, isAdditional: true)
                        : $"{permission.Guidance} {BuildResourceDependentGuidance(resourceDependent, isAdditional: true)}";
                }
            }

            permission.DelegatedPermissions = OrderSharePointAlternativesByPrivilege(permission.DelegatedPermissions);
            permission.ApplicationPermissions = OrderSharePointAlternativesByPrivilege(permission.ApplicationPermissions);

            ApplyAvailability(permission, delegatedUnavailable, applicationUnavailable);
            ApplyAdditionalRoles(cmdletType, cmdletAttribute, permission);
            ApplyResourceTypes(permission, resourceDependent);

            return permission;
        }

        /// <summary>
        /// Orders the alternatives from least to most privileged where every alternative is a single SharePoint scope with a known privilege level, i.e. on Get-PnPList.
        /// For those the order is guaranteed rather than dependent on the order in which the attributes happen to be reflected. Any other shape, such as alternatives on
        /// Microsoft Graph for which no privilege model exists, is left in the order the cmdlet declares them in.
        /// </summary>
        private static CommandPermissionSet[] OrderSharePointAlternativesByPrivilege(CommandPermissionSet[] sets)
        {
            if (sets.Length < 2 || !sets.All(set => set.Permissions.Length == 1 &&
                                                    set.Permissions[0].ResourceType == ResourceTypeName.SharePoint &&
                                                    (SharePointScopeRoles.ContainsKey(set.Permissions[0].Scope) || set.Permissions[0].Scope.Equals(SitesSelectedScope, StringComparison.OrdinalIgnoreCase))))
            {
                return sets;
            }

            // Sites.Selected is the least privileged of all, it only grants access to the sites the application has been granted access to explicitly
            return sets.OrderBy(set => set.Permissions[0].Scope.Equals(SitesSelectedScope, StringComparison.OrdinalIgnoreCase)
                ? SharePointMinimumRole.Unknown
                : SharePointScopeRoles[set.Permissions[0].Scope]).ToArray();
        }

        /// <summary>
        /// Determines whether the cmdlet can be used with each of the two token types. FALSE only where the cmdlet explicitly declares it cannot be used with it,
        /// TRUE where permissions are reported for it, and NULL where there is nothing to base it on.
        /// </summary>
        private static void ApplyAvailability(CommandPermission permission, bool delegatedUnavailable, bool applicationUnavailable)
        {
            permission.DelegatedAvailable = delegatedUnavailable ? false : permission.DelegatedPermissions.Length > 0 ? true : null;
            permission.ApplicationAvailable = applicationUnavailable ? false : permission.ApplicationPermissions.Length > 0 ? true : null;
        }

        /// <summary>
        /// Adds the SharePoint permission needed by a cmdlet which uses SharePoint CSOM but only declares permissions on another API, i.e. Set-PnPList which declares
        /// Microsoft Graph information protection permissions while also performing SharePoint CSOM operations. The SharePoint permission is required next to the
        /// declared permissions, so it is added to each of the declared alternatives to keep the AND within a set and the OR between the sets intact.
        /// </summary>
        private static void AddSharePointRequirementToDeclaredPermissions(Type cmdletType, CommandPermission permission, bool delegatedUnavailable, bool applicationUnavailable)
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

            // Some cmdlets reach their goal either through the declared API or through SharePoint, depending on how they are invoked. For those the SharePoint
            // permission is an alternative to the declared ones rather than a requirement on top of them.
            var isAlternative = cmdletType.GetCustomAttribute<ApiPermissionsDependOnResource>(false)?.ApiIsAlternativeToSharePoint == true;

            if (!delegatedUnavailable)
            {
                permission.DelegatedPermissions = isAlternative
                    ? [.. permission.DelegatedPermissions, CreatePermissionSet(ResourceTypeName.SharePoint, delegatedScope)]
                    : AddScopeToEachSet(permission.DelegatedPermissions, delegatedScope);
            }

            if (!applicationUnavailable)
            {
                permission.ApplicationPermissions = isAlternative
                    ? [.. permission.ApplicationPermissions, CreatePermissionSet(ResourceTypeName.SharePoint, applicationScope)]
                    : AddScopeToEachSet(permission.ApplicationPermissions, applicationScope);
            }

            permission.PermissionSource = CommandPermissionSource.DeclaredAndInferred;
            permission.Guidance = isAlternative
                ? "This cmdlet can use SharePoint CSOM instead of the API for which permissions are declared on it. The SharePoint permission listed has been derived from the operation the cmdlet performs and is an alternative to the declared permissions, not a requirement next to them."
                : "This cmdlet uses SharePoint CSOM next to the API for which permissions are declared on it. The SharePoint permission listed has been derived from the operation the cmdlet performs and is required in addition to the declared permissions.";
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
        private static void ApplyInferredPermissions(Type cmdletType, CmdletAttribute cmdletAttribute, CommandPermission permission, bool delegatedUnavailable, bool applicationUnavailable)
        {
            // Cmdlets which do not use the PnP connection, i.e. Get-PnPChangeLog or Connect-PnPOnline. Note that a cmdlet in this category can still call an API using
            // a token it acquires itself, such as Register-PnPEntraIDApp, which is why the guidance is scoped to the application registration PnP PowerShell connects with.
            if (!typeof(PnPConnectedCmdlet).IsAssignableFrom(cmdletType))
            {
                permission.PermissionSource = CommandPermissionSource.NotApplicable;
                permission.MinimumSharePointRole = SharePointMinimumRole.NotApplicable;
                permission.Guidance = GuidanceNotApplicable;
                return;
            }

            // Cmdlets which use SharePoint CSOM. The permission needed follows from the site the cmdlet acts on and the operation it performs.
            if (typeof(PnPSharePointCmdlet).IsAssignableFrom(cmdletType))
            {
                var role = InferSharePointRole(cmdletType, cmdletAttribute);
                var (delegatedScope, applicationScope) = GetSharePointScopes(role);

                // The term store is addressed through its own SharePoint scopes rather than through the site scopes
                if (IsTaxonomyCmdlet(cmdletType))
                {
                    var termStoreScope = ReadVerbs.Contains(cmdletAttribute.VerbName) ? "TermStore.Read.All" : "TermStore.ReadWrite.All";
                    delegatedScope = termStoreScope;
                    applicationScope = termStoreScope;
                }

                permission.PermissionSource = CommandPermissionSource.Inferred;
                permission.MinimumSharePointRole = role;
                permission.Guidance = GuidanceInferred;

                // Do not report permissions for a token type the cmdlet declares it cannot be used with
                if (!delegatedUnavailable)
                {
                    permission.DelegatedPermissions = [CreatePermissionSet(ResourceTypeName.SharePoint, delegatedScope)];
                }

                if (!applicationUnavailable)
                {
                    // For an operation scoped to a single site, Sites.Selected combined with a grant on that site is less privileged than any of the tenant wide
                    // scopes, which is why the declared metadata lists it first as well, i.e. on Get-PnPList. It does not apply to cmdlets which run against the
                    // SharePoint Online admin site, as those act tenant wide by definition.
                    // Not for term store operations either, as Sites.Selected grants access to sites and not to the term store
                    permission.ApplicationPermissions = role == SharePointMinimumRole.SharePointAdministrator || IsTaxonomyCmdlet(cmdletType)
                        ? [CreatePermissionSet(ResourceTypeName.SharePoint, applicationScope)]
                        : [CreatePermissionSet(ResourceTypeName.SharePoint, SitesSelectedScope), CreatePermissionSet(ResourceTypeName.SharePoint, applicationScope)];
                }

                return;
            }

            // Cmdlets which call into an API for which no permission attributes have been declared and for which the permission cannot be derived, i.e. Microsoft Graph
            permission.PermissionSource = CommandPermissionSource.Unknown;
            permission.MinimumSharePointRole = SharePointMinimumRole.NotApplicable;
            permission.Guidance = "No permission metadata has been declared on this cmdlet and the permissions required could not be derived. Consult the documentation of this cmdlet for the permissions it needs.";
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
            ["AllSites.Manage"] = SharePointMinimumRole.SiteDesigner,
            ["Sites.Manage.All"] = SharePointMinimumRole.SiteDesigner,
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

            var noun = StripPnPPrefix(cmdletAttribute.NounName);

            // Creating a site collection is a tenant operation. There is no site to hold a role on yet, so the tenant administrator role applies unless self service
            // site creation has been enabled.
            if (noun.Equals("Site", StringComparison.OrdinalIgnoreCase) && cmdletAttribute.VerbName.Equals(VerbsCommon.New, StringComparison.OrdinalIgnoreCase))
            {
                return SharePointMinimumRole.SharePointAdministrator;
            }

            // Reading and changing who administers a site collection can only be done by a site collection administrator, Full Control through the Owners group is not enough
            if (noun.Contains("SiteCollectionAdmin", StringComparison.OrdinalIgnoreCase))
            {
                return SharePointMinimumRole.SiteCollectionAdministrator;
            }

            // Evaluated before the verb: enumerating permissions, reading the audit configuration and reading how a site is shared are Full Control rights,
            // so for these nouns reading needs just as much as changing does
            if (FullControlNounFragments.Any(fragment => noun.Contains(fragment, StringComparison.OrdinalIgnoreCase)))
            {
                return SharePointMinimumRole.SiteOwner;
            }

            // Term store operations are governed by the term store roles rather than by a permission level on a site, so no site role applies to them
            if (IsTaxonomyCmdlet(cmdletType))
            {
                return SharePointMinimumRole.NotApplicable;
            }

            if (ReadVerbs.Contains(cmdletAttribute.VerbName))
            {
                return SharePointMinimumRole.SiteVisitor;
            }

            // Changing the security, structure or configuration of a site needs rights that only Full Control holds, such as Manage Web Site
            if (SiteConfigurationNouns.Contains(noun))
            {
                return SharePointMinimumRole.SiteOwner;
            }

            // Customizing pages and branding needs Add and Customize Pages or Apply Themes, which Design holds and Edit does not
            if (SiteDesignNouns.Contains(noun))
            {
                return SharePointMinimumRole.SiteDesigner;
            }

            // Changing the structure of a list needs the Manage Lists right, which Contribute does not grant
            return ListManagementNouns.Contains(noun) ? SharePointMinimumRole.SiteEditor : SharePointMinimumRole.SiteMember;
        }

        /// <summary>
        /// Returns the least privileged SharePoint delegated and application scope which covers the provided role
        /// </summary>
        private static (string Delegated, string Application) GetSharePointScopes(SharePointMinimumRole role) => role switch
        {
            SharePointMinimumRole.SiteVisitor => ("AllSites.Read", "Sites.Read.All"),
            SharePointMinimumRole.SiteMember or SharePointMinimumRole.SiteEditor => ("AllSites.Write", "Sites.ReadWrite.All"),
            SharePointMinimumRole.SiteDesigner => ("AllSites.Manage", "Sites.Manage.All"),
            _ => ("AllSites.FullControl", "Sites.FullControl.All")
        };

        private static bool IsTaxonomyCmdlet(Type cmdletType) => cmdletType.Namespace?.Contains(".Taxonomy", StringComparison.OrdinalIgnoreCase) == true;

        private static string StripPnPPrefix(string nounName) => nounName.StartsWith("PnP", StringComparison.OrdinalIgnoreCase) ? nounName[3..] : nounName;


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
                case SharePointMinimumRole.SiteDesigner:
                    roles.Add($"Design on the target site, Edit does not grant the right to customize pages or apply themes, {appliesTo}");
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
