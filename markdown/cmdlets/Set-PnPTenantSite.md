---
Module Name: PnP.PowerShell
title: Set-PnPTenantSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTenantSite.html
---
 
# Set-PnPTenantSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Updates settings of a site collection

## SYNTAX

### Set Properties
```powershell
Set-PnPTenantSite [-Identity] <String> [-Title <String>] [-LocaleId <UInt32>] [-AllowSelfServiceUpgrade]
 [-DisableClassicPageBaselineSecurityMode <Boolean>] [-DisableSiteBranding <Boolean>]
 [-PrimarySiteCollectionAdmin <String>]
 [-Owners <String[]>] [-DenyAddAndCustomizePages]
 [-SharingCapability <SharingCapabilities>] [-StorageQuota <Int64>] [-StorageQuotaWarningLevel <Int64>] [-StorageQuotaReset] [-BlockDownloadLinksFileType <BlockDownloadLinksFileTypes>]
 [-ResourceQuota <Double>] [-ResourceQuotaWarningLevel <Double>] [-EnablePWA <Boolean>]
 [-DefaultLinkPermission <SharingPermissionType>] [-DefaultSharingLinkType <SharingLinkType>] [-DefaultLinkToExistingAccess <Boolean>] [-ExternalUserExpirationInDays <Int32>]
 [-SharingAllowedDomainList <String>] [-SharingBlockedDomainList <String>] [-ShowPeoplePickerSuggestionsForGuestUsers <Boolean>] [-AllowDownloadingOfNonWebViewableFiles]
 [-LimitedAccessFileType <SPOLimitedAccessFileType>] [-AllowEditing <Boolean>]
 [-AllowFileArchive <Boolean>]
 [-SharingDomainRestrictionMode <SharingDomainRestrictionModes>] [-CommentsOnSitePagesDisabled]
 [-DisableAppViews <AppViewsPolicy>] [-DisableCompanyWideSharingLinks <CompanyWideSharingLinksPolicy>]
 [-DisableFlows <FlowsPolicy>] [-AnonymousLinkExpirationInDays <Int32>] [-SensitivityLabel <String>] [-RemoveLabel] [-AddInformationSegment <Guid[]>] [-RemoveInformationSegment <Guid[]>]
 [-OverrideTenantAnonymousLinkExpirationPolicy] [-InformationBarriersMode <InformationBarriersMode>] 
 [-MediaTranscription <MediaTranscriptionPolicyType>] 
 [-BlockDownloadPolicy <Boolean>] [-ExcludeBlockDownloadPolicySiteOwners <Boolean>]
 [-ExcludedBlockDownloadGroupIds <Guid[]>]
 [-ExcludeBlockDownloadSharePointGroups <String[]>]
 [-ReadOnlyForBlockDownloadPolicy]
 [-ListsShowHeaderAndNavigation <Boolean>]
 [-DefaultLinkToExistingAccessReset <SwitchParameter>] [-DefaultShareLinkRole <Role>]
 [-DefaultShareLinkScope <SharingScope>] [-LoopDefaultSharingLinkRole <Role>]
 [-LoopDefaultSharingLinkScope <SharingScope>] [-RestrictContentOrgWideSearch <Boolean>] [-ReadOnlyForUnmanagedDevices <Boolean>]
 [-RequestFilesLinkExpirationInDays <Int32>] [-RequestFilesLinkEnabled <Boolean>] [-OverrideSharingCapability <Boolean>]
 [-RestrictedAccessControl <Boolean>] [-ClearRestrictedAccessControl <SwitchParameter>] [-RestrictedAccessControlGroups <Guid[]>]
 [-AddRestrictedAccessControlGroups <Guid[]>] [-RemoveRestrictedAccessControlGroups <Guid[]>] [-InheritVersionPolicyFromTenant]
 [-EnableAutoExpirationVersionTrim <Boolean>] [-ExpireVersionsAfterDays <Int32>] [-MajorVersionLimit <Int32>]
 [-MajorWithMinorVersionsLimit <Int32>] [-FileTypesForVersionExpiration <String[]>]
 [-RemoveVersionExpirationFileTypeOverride <String[]>] [-ApplyToNewDocumentLibraries] [-ApplyToExistingDocumentLibraries] [-Force]
 [-AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled <Boolean>] [-IsAuthoritative <Boolean>] [-RestrictedContentDiscoveryForCopilotAndAgents <Boolean>]
 [-OverrideTenantOrganizationSharingLinkExpirationPolicy <Boolean>]
 [-OrganizationSharingLinkRecommendedExpirationInDays <int>] 
 [-OrganizationSharingLinkMaxExpirationInDays <int>]
 [-ClearGroupId]
 [-Wait] 
 [-Connection <PnPConnection>] 
```

### Set Lock State
```powershell
Set-PnPTenantSite [-Identity] <String> [-LockState <SiteLockState>] [-Wait] [-Connection <PnPConnection>]
 
```

## DESCRIPTION
Allows settings of a site collection to be updated

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTenantSite -Identity "https://contoso.sharepoint.com" -Title "Contoso Website" -SharingCapability Disabled
```

This will set the title of the site collection with the URL 'https://contoso.sharepoint.com' to 'Contoso Website' and disable sharing on this site collection.

### EXAMPLE 2
```powershell
Set-PnPTenantSite -Identity "https://contoso.sharepoint.com" -Title "Contoso Website" -StorageWarningLevel 8000 -StorageMaximumLevel 10000
```

This will set the title of the site collection with the URL 'https://contoso.sharepoint.com' to 'Contoso Website', set the storage warning level to 8GB and set the storage maximum level to 10GB.

### EXAMPLE 3
```powershell
Set-PnPTenantSite -Identity "https://contoso.sharepoint.com/sites/sales" -Owners "user@contoso.onmicrosoft.com"
```

This will add user@contoso.onmicrosoft.com as an additional site collection owner at 'https://contoso.sharepoint.com/sites/sales'.

### EXAMPLE 4
```powershell
Set-PnPTenantSite -Identity "https://contoso.sharepoint.com/sites/sales" -Owners @("user1@contoso.onmicrosoft.com", "user2@contoso.onmicrosoft.com")
```

This will add user1@contoso.onmicrosoft.com and user2@contoso.onmicrosoft.com as additional site collection owners at 'https://contoso.sharepoint.com/sites/sales'.

### EXAMPLE 5
```powershell
Set-PnPTenantSite -Identity "https://contoso.sharepoint.com/sites/sales" -DenyAddAndCustomizePages:$false
```

This will enable script support for the site 'https://contoso.sharepoint.com/sites/sales'

### EXAMPLE 6
```powershell
Set-PnPTenantSite -Identity "https://contoso.sharepoint.com/sites/marketing" -AllowFileArchive $true
```

This will enable file-level archiving on the specified site. The tenant-level `Set-PnPTenant -AllowFileArchive $true` setting must also be enabled or the site setting will not take effect.

### EXAMPLE 7
```powershell
Set-PnPTenantSite -Identity "https://contoso.sharepoint.com/sites/marketing" -EnableAutoExpirationVersionTrim $false -ExpireVersionsAfterDays 180 -MajorVersionLimit 100 -MajorWithMinorVersionsLimit 10
```

This will set the site version policy for both new and existing document libraries to keep 100 major versions, 10 minor versions, and expire versions after 180 days.

### EXAMPLE 8
```powershell
Set-PnPTenantSite -Identity "https://contoso.sharepoint.com/sites/marketing" -EnableAutoExpirationVersionTrim $true -ApplyToNewDocumentLibraries -FileTypesForVersionExpiration "pdf","docx"
```

This will set an automatic version trim policy for new document libraries only and limit the override to the specified file types.

### EXAMPLE 9
```powershell
Set-PnPTenantSite -Identity "https://contoso.sharepoint.com/sites/marketing" -ApplyToNewDocumentLibraries -RemoveVersionExpirationFileTypeOverride "pdf","docx"
```

This will remove the specified file type version expiration overrides from the site policy for new document libraries.

### EXAMPLE 10
```powershell
Set-PnPTenantSite -Identity "https://contoso.sharepoint.com/sites/marketing" -InheritVersionPolicyFromTenant
```

This will reset the site version policy so new document libraries inherit the tenant-level defaults.

## PARAMETERS

### -AddInformationSegment
This parameter allows you to add a segment to a SharePoint site. This parameter is only applicable for tenants who have enabled Microsoft 365 Information barriers capability. Please read https://learn.microsoft.com/sharepoint/information-barriers documentation to understand Information barriers in SharePoint Online.

```yaml
Type: Guid[]
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowSelfServiceUpgrade
Specifies if the site administrator can upgrade the site collection.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableClassicPageBaselineSecurityMode
Enables or disables classic page baseline security mode for the site collection.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableSiteBranding
Enables or disables site branding for the site collection.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowEditing
Prevents users from editing Office files in the browser and copying and pasting Office file contents out of the browser window.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowFileArchive
Enables or disables file-level archiving for the site.

This setting only takes effect when the tenant-level `AllowFileArchive` setting is enabled. If the tenant-level setting is disabled, users will not be able to archive files on the site even when this parameter is set to `$true`.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AnonymousLinkExpirationInDays
Specifies all anonymous/anyone links that have been created (or will be created) will expire after the set number of days. Only applies if OverrideTenantAnonymousLinkExpirationPolicy is set to true.

To remove the expiration requirement, set the value to zero (0).

```yaml
Type: Int32
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ApplyToExistingDocumentLibraries
Applies the configured site version policy to existing document libraries. If neither this parameter nor `-ApplyToNewDocumentLibraries` is provided, the cmdlet targets both new and existing document libraries.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ApplyToNewDocumentLibraries
Applies the configured site version policy to new document libraries. If neither this parameter nor `-ApplyToExistingDocumentLibraries` is provided, the cmdlet targets both new and existing document libraries.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowDownloadOfNonViewableFiles
Specifies if non web viewable files can be downloaded.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BlockDownloadLinksFileType
The valid values are:

WebPreviewableFiles
ServerRenderedFilesOnly

Note: ServerRendered (Office Only) and WebPreviewable (All supported files).

The site value is compared with the tenant level setting and the stricter one wins. For example, if the tenant is set to ServerRenderedFilesOnly then that will be used even if the site is set to WebPreviewableFiles.

```yaml
Type: BlockDownloadLinksFileTypes
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CommentsOnSitePagesDisabled
Specifies if comments on site pages are enabled.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultLinkPermission
Specifies the default link permission for the site collection. None - Respect the organization default link permission. View - Sets the default link permission for the site to "view" permissions. Edit - Sets the default link permission for the site to "edit" permissions

```yaml
Type: SharingPermissionType
Parameter Sets: Set Properties
Accepted values: None, View, Edit

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultSharingLinkType
Specifies the default link type for the site collection. None - Respect the organization default sharing link type. AnonymousAccess - Sets the default sharing link for this site to an Anonymous Access or Anyone link. Internal - Sets the default sharing link for this site to the "organization" link or company shareable link. Direct - Sets the default sharing link for this site to the "Specific people" link

```yaml
Type: SharingLinkType
Parameter Sets: Set Properties
Accepted values: None, Direct, Internal, AnonymousAccess

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultLinkToExistingAccess
When set to $true, the DefaultSharingLinkType will be overridden and the default sharing link will be All People with Existing Access link (which does not modify permissions). When set to $false (the default), the default sharing link type is controlled by the DefaultSharingLinkType parameter

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -DenyAddAndCustomizePages
Determines whether the Add And Customize Pages right is denied in the site collection. For more information about permission levels, see User permissions and permission levels in SharePoint.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties
Aliases: NoScriptSite

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableAppViews
Determines whether the App Views feature is disabled in the site collection.

```yaml
Type: AppViewsPolicy
Parameter Sets: Set Properties
Accepted values: Unknown, Disabled, NotDisabled

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableAutoExpirationVersionTrim
Enables or disables automatic version trim for the site version policy. Set this to `$true` to use automatic trimming. Set it to `$false` to provide explicit values for `-ExpireVersionsAfterDays` and `-MajorVersionLimit`, and also `-MajorWithMinorVersionsLimit` when existing document libraries are included.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExpireVersionsAfterDays
Sets the number of days after which versions expire when `-EnableAutoExpirationVersionTrim` is `$false`. Use `0` to keep versions indefinitely. Allowed values are `0` or from `30` through `36500`.

```yaml
Type: Int32
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FileTypesForVersionExpiration
Limits the site version policy override to the specified file types. This parameter can only be used when `-EnableAutoExpirationVersionTrim` is also provided and cannot be combined with `-ApplyToExistingDocumentLibraries`.

```yaml
Type: String[]
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Bypasses the confirmation prompt when applying site version policy changes that target new document libraries, existing document libraries, or both.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableSharingForNonOwners
Specifies whether non-owners should be prevented from inviting new users to the site. Setting this will also disable Access Request Emails.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableCompanyWideSharingLinks
Determines whether company-wide sharing links are disabled in collection.

```yaml
Type: CompanyWideSharingLinksPolicy
Parameter Sets: Set Properties
Accepted values: Unknown, Disabled, NotDisabled

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableFlows
Determines whether flows are disabled in the site collection.

```yaml
Type: FlowsPolicy
Parameter Sets: Set Properties
Accepted values: Unknown, Disabled, NotDisabled

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


### -ExternalUserExpirationInDays
Specifies number of days the external users remain active on the site.

```yaml
Type: Int32
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


### -EnablePWA
Determines whether site can include Project Web App. For more information about Project Web App, see Plan SharePoint groups in Project Server.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LimitedAccessFileType
The following parameters can be used with -ConditionalAccessPolicy AllowLimitedAccess for both the organization-wide setting and the site-level setting.

OfficeOnlineFilesOnly: Allows users to preview only Office files in the browser. This option increases security but may be a barrier to user productivity.

WebPreviewableFiles (default): Allows users to preview Office files and other file types (such as PDF files and images) in the browser. Note that the contents of file types other than Office files are handled in the browser. This option optimizes for user productivity but offers less security for files that aren't Office files.

OtherFiles: Allows users to download files that can't be previewed, such as .zip and .exe. This option offers less security.

```yaml
Type: SPOLimitedAccessFileType
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LocaleId
Specifies the language of this site collection. For more information, see Locale IDs supported by SharePoint at https://github.com/pnp/PnP-PowerShell/wiki/Supported-LCIDs-by-SharePoint. To get the list of supported languages on a SharePoint environment use: (Get-PnPWeb -Includes RegionalSettings.InstalledLanguages).RegionalSettings.InstalledLanguages.

```yaml
Type: UInt32
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LockState
Sets the lockState of a site

```yaml
Type: SiteLockState
Parameter Sets: Set Lock State
Accepted values: Unlock, NoAccess, ReadOnly

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OverrideTenantAnonymousLinkExpirationPolicy
Specifies whether to use company-wide or a site collection level anonymous links expiration policy. Set it to true to get advantage of AnonymousLinkExpirationInDays.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PrimarySiteCollectionAdmin
Specifies the user to set as the primary site collection administrator. Will replace the current primary site collection administrator. To add additional site collection administrators, use the -Owners parameter.

```yaml
Type: PnP.PowerShell.Commands.Base.PipeBinds.UserPipeBind
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Owners
Specifies owner(s) to add as secondary site collection administrators. They will be added as additional secondary site collection administrators. Existing administrators will stay. Can be both users and groups.

```yaml
Type: System.Collections.Generic.List`1[System.String]
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SensitivityLabel
Set the sensitivity label.

```yaml
Type: String
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveLabel
Remove the assigned sensitivity label of a site.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharingAllowedDomainList
Specifies a list of email domains that is allowed for sharing with the external collaborators. Use the "," (comma) character as the delimiter for entering multiple values. For example, "contoso.com, fabrikam.com". Effective when SharingDomainRestrictionMode is set to AllowList.

```yaml
Type: String
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharingBlockedDomainList
Specifies a list of email domains that is blocked for sharing with the external collaborators. Use the space character as the delimiter for entering multiple values. For example, "contoso.com fabrikam.com". Effective when SharingDomainRestrictionMode is set to BlockList.

```yaml
Type: String
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharingCapability
Specifies what the sharing capabilities are for the site. Possible values: Disabled, ExternalUserSharingOnly, ExternalUserAndGuestSharing, ExistingExternalUserSharingOnly

```yaml
Type: SharingCapabilities
Parameter Sets: Set Properties
Aliases: Sharing
Accepted values: Disabled, ExternalUserSharingOnly, ExternalUserAndGuestSharing, ExistingExternalUserSharingOnly

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharingDomainRestrictionMode
Specifies the external sharing mode for domains.

```yaml
Type: SharingDomainRestrictionModes
Parameter Sets: Set Properties
Accepted values: None, AllowList, BlockList

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShowPeoplePickerSuggestionsForGuestUsers
To enable the option to search for existing guest users at Site Collection Level, set this parameter to $true.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StorageQuota
Specifies the storage quota for this site collection in megabytes. This value must not exceed the company's available quota.

```yaml
Type: Int64
Parameter Sets: Set Properties
Aliases: StorageMaximumLevel

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StorageQuotaWarningLevel
Specifies the warning level for the storage quota in megabytes. This value must not exceed the values set for the StorageQuota parameter.

```yaml
Type: Int64
Parameter Sets: Set Properties
Aliases: StorageQuotaMaximumLevel

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StorageQuotaReset
Resets the OneDrive for Business storage quota to the tenant's new default storage space

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveInformationSegment
This parameter allows you to remove a segment from a SharePoint site. This parameter is only applicable for tenants who have enabled Microsoft 365 Information barriers capability. Please read https://learn.microsoft.com/sharepoint/information-barriers documentation to understand Information barriers with SharePoint Online.

```yaml
Type: Guid[]
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceQuota
Specifies the resource quota in megabytes of the site collection. The default value is 0.

```yaml
Type: Double
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceQuotaWarningLevel
Specifies the warning level in megabytes of the site collection to warn the site collection administrator that the site is approaching the resource quota.

```yaml
Type: Double
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InformationBarriersMode
Specifies the information barrier mode which helps strengthen access, sharing, and membership of a site based on its information barrier mode and segments associated with the site. Expected values are `Open`, `OwnerModerated` , `Implicit` and `Explicit`. For more information, see https://learn.microsoft.com/sharepoint/information-barriers#information-barriers-modes-and-sharepoint-sites

```yaml
Type: InformationBarriersMode
Parameter Sets: Set Properties
Accepted values: Open, OwnerModerated, Implicit, Explicit

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
Specifies the title of the site.

```yaml
Type: String
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
Specifies the URL of the site.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Url

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -MediaTranscription
When the feature is enabled, videos can have transcripts generated on demand or generated automatically in certain scenarios. This is the default because the policy is default on. If a video owner decides they don’t want the transcript, they can always hide or delete it from that video. 

```yaml
Type: MediaTranscriptionPolicyType
Parameter Sets: Set Properties
Accepted values: Enabled, Disabled

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BlockDownloadPolicy
Set this to true to block download of files from SharePoint sites or OneDrive

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludeBlockDownloadPolicySiteOwners
Set this to true to exempts site owners from the block download policy so that they can fully download any content for the site.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludedBlockDownloadGroupIds
Exempts users from the mentioned groups from this policy and they can fully download any content for the site.

```yaml
Type: GUID[]
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludeBlockDownloadSharePointGroups
Exempts users from the specified SharePoint groups from the block download policy. Users in these groups can fully download any content for the site.

```yaml
Type: String[]
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ReadOnlyForBlockDownloadPolicy
When enabled in combination with BlockDownloadPolicy, users will only be able to view the content in read-only mode but will not be able to download or sync files.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListsShowHeaderAndNavigation
Set a property on a site collection to make all lists always load with the site elements intact.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RestrictedAccessControl
To apply restricted access control to a group-connected or Teams-connected site.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClearRestrictedAccessControl
To reset restricted access control configuration for a site.

```yaml
Type: Switch Parameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClearGroupId
This parameter allows you to remove the assigned Microsoft 365 group ID on a site, when the group is permanently deleted.

```yaml
Type: Switch Parameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveRestrictedAccessControlGroups
You can remove the specified security group from restricted access control configuration. Members of the security group are no longer be able to access site content while the policy is enforced on the site. 

```yaml
Type: GUID []
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AddRestrictedAccessControlGroups
You can add the specified security groups for restricted access control configuration.

```yaml
Type: GUID []
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RestrictedAccessControlGroups
To edit a restricted access control group for a non-group site

```yaml
Type: GUID []
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultLinkToExistingAccessReset
To reset the default link to existing access configuration for a site.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultShareLinkRole
To set the default share link role. Available values are `None`, `Edit`, `Review`, `RestrictedView` and `View`.

```yaml
Type: Role
Parameter Sets: Set Properties
Accepted values: None, Edit, Review, RestrictedView, View

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultShareLinkScope
To set the default sharing link scope. Available values are `Anyone`, `Organization`, `SpecificPeople`, `Uninitialized`.

```yaml
Type: SharingScope
Parameter Sets: Set Properties
Accepted values: Anyone, Organization, SpecificPeople, Uninitialized

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LoopDefaultSharingLinkRole
To set the loop default sharing link role. Available values are `None`, `Edit`, `Review`, `RestrictedView` and `View`.

```yaml
Type: Role
Parameter Sets: Set Properties
Accepted values: None, Edit, Review, RestrictedView, View

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LoopDefaultSharingLinkScope
To set the loop default sharing link scope. Available values are Anyone, Organization, SpecificPeople, Uninitialized.

```yaml
Type: SharingScope
Parameter Sets: Set Properties
Accepted values: Anyone, Organization, SpecificPeople, Uninitialized

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RestrictContentOrgWideSearch
To restrict content from being searchable organization-wide and Copilot.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ReadOnlyForUnmanagedDevices
To set the site as read-only for unmanaged devices.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InheritVersionPolicyFromTenant
Resets the site version policy to inherit the default version policy from the tenant.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MajorVersionLimit
Sets the maximum number of major versions to retain when `-EnableAutoExpirationVersionTrim` is `$false`. Allowed values are from `1` through `50000`.

```yaml
Type: Int32
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MajorWithMinorVersionsLimit
Sets the maximum number of major and minor versions to retain when `-EnableAutoExpirationVersionTrim` is `$false` and the policy applies to existing document libraries. Allowed values are from `0` through `50000`.

```yaml
Type: Int32
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RequestFilesLinkExpirationInDays
Specifies the number of days after which the request files link will expire.
The value can be from 0 to 730 days.

```yaml
Type: Int32
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


### -OverrideSharingCapability
Specifies whether to override the sharing capability for the site.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RequestFilesLinkEnabled
Enables or disables the Request Files link on the site.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveVersionExpirationFileTypeOverride
Removes one or more file type version expiration overrides from the site policy for new document libraries. This parameter must be combined with `-ApplyToNewDocumentLibraries` and cannot be combined with the other version policy setting parameters.

```yaml
Type: String[]
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled
Enables or disables adding and updating web property bag values when the DenyAddAndCustomizePages is enabled.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsAuthoritative
Marks the site collection as authoritative or not authoritative.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RestrictedContentDiscoveryForCopilotAndAgents
Restricts content discovery for Copilot and agents on the site collection.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OverrideTenantOrganizationSharingLinkExpirationPolicy
Allows to set organization sharing link expiration policy for this SharePoint site, which will override the tenant-level policy when set to true. When this is set to true, you can configure the organization sharing link expiration policy for this site collection using the OrganizationSharingLinkRecommendedExpirationInDays and OrganizationSharingLinkMaxExpirationInDays parameters.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OrganizationSharingLinkRecommendedExpirationInDays
This parameter specifies the recommended number of days before organization sharing links expire in the SharePoint site. Users can still choose a different expiration period if permitted by policy, but this value is presented as the recommended default.

The valid values :

- Can be from 7 to 730 days and must be less than or equal to the maximum expiration value set by OrganizationSharingLinkMaxExpirationInDays.
- When set to 0 (default), the default value will be OrganizationSharingLinkMaxExpirationInDays.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OrganizationSharingLinkMaxExpirationInDays
This parameter specifies the maximum number of days that organization sharing links can remain active before they expire for the SharePoint site.

The valid values :

- can be from 7 to 730 days.
- `0` (default) - No maximum expiration limit is enforced.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Wait
Wait for the operation to complete

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

