---
Module Name: PnP.PowerShell
title: Set-PnPSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSite.html
---
 
# Set-PnPSite

## SYNOPSIS
Sets site collection properties.

## SYNTAX

### Set Properties
```powershell
Set-PnPSite 
 [-Identity <String>]
 [-Classification <String>]
 [-DisableFlows]
 [-LogoFilePath <String>]
 [-Sharing <SharingCapabilities>]
 [-StorageMaximumLevel <Int64>]
 [-StorageWarningLevel <Int64>]
 [-AllowSelfServiceUpgrade]
 [-DisableClassicPageBaselineSecurityMode <Boolean>]
 [-DisableSiteBranding <Boolean>]
 [-NoScriptSite]
 [-Owners <System.Collections.Generic.List<[System.String]>]
 [-CommentsOnSitePagesDisabled]
 [-DefaultLinkPermission <SharingPermissionType>]
 [-DefaultSharingLinkType <SharingLinkType>]
 [-DefaultLinkToExistingAccess <Boolean>]
 [-DefaultLinkToExistingAccessReset]
 [-DisableAppViews <AppViewsPolicy>]
 [-DisableCompanyWideSharingLinks <CompanyWideSharingLinksPolicy>]
 [-DisableSharingForNonOwners]
 [-LocaleId <UInt32>]
 [-RestrictedToGeo <RestrictedToRegion>]
 [-SocialBarOnSitePagesDisabled]
 [-AnonymousLinkExpirationInDays <Int32>]
 [-RequestFilesLinkExpirationInDays <Int32>]
 [-AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled <Boolean>]
 [-IsAuthoritative <Boolean>]
 [-RestrictedContentDiscoveryForCopilotAndAgents <Boolean>]
 [-OverrideTenantAnonymousLinkExpirationPolicy]
 [-MediaTranscription <MediaTranscriptionPolicyType>]
 [-SensitivityLabel <Guid>]
 [-RequestFilesLinkEnabled <Boolean>]
 [-ScriptSafeDomainName <string>]
 [-BlockDownloadPolicy <Boolean>] [-ExcludeBlockDownloadPolicySiteOwners <Boolean>]
 [-ExcludedBlockDownloadGroupIds <Guid[]>]
 [-ExcludeBlockDownloadSharePointGroups <String[]>]
 [-ReadOnlyForBlockDownloadPolicy]
 [-ListsShowHeaderAndNavigation <Boolean>]
 [-RestrictContentOrgWideSearch <Boolean>]
 [-CanSyncHubSitePermissions <SwitchParameter>]
 [-ClearGroupId]
 [-InheritVersionPolicyFromTenant]
 [-EnableAutoExpirationVersionTrim <Boolean>]
 [-ExpireVersionsAfterDays <Int32>]
 [-MajorVersionLimit <Int32>]
 [-MajorWithMinorVersionsLimit <Int32>]
 [-FileTypesForVersionExpiration <String[]>]
 [-RemoveVersionExpirationFileTypeOverride <String[]>]
 [-ApplyToNewDocumentLibraries]
 [-ApplyToExistingDocumentLibraries]
 [-Force]
 [-HidePeoplePreviewingFiles <Boolean>]
 [-HidePeopleWhoHaveListsOpen <Boolean>]
 [-RestrictedAccessControl <Boolean>]
 [-OverrideTenantOrganizationSharingLinkExpirationPolicy <Boolean>]
 [-OrganizationSharingLinkRecommendedExpirationInDays <int>] 
 [-OrganizationSharingLinkMaxExpirationInDays <int>]
 [-Connection <PnPConnection>]
```

### Set Lock State
```powershell
Set-PnPSite [-Identity <String>] [-LockState <SiteLockState>] [-Wait] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to modify a site properties.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSite -Classification "HBI"
```

Sets the current site classification tag to HBI

### EXAMPLE 2
```powershell
Set-PnPSite -Classification $null
```

Un-sets the current site classification tag

### EXAMPLE 3
```powershell
Set-PnPSite -DisableFlows
```

Disables Microsoft Flow for this site, and also hides the Flow button from the ribbon

### EXAMPLE 4
```powershell
Set-PnPSite -DisableFlows:$false
```

Enables Microsoft Flow for this site

### EXAMPLE 5
```powershell
Set-PnPSite -LogoFilePath c:\images\mylogo.png
```

Sets the logo if the site is a modern team site without a Microsoft 365 Group behind it. Check the [notes below](#-logofilepath) for options for other types of sites.

### EXAMPLE 6
```powershell
Set-PnPSite -NoScriptSite $false
```

Allows custom script on a specific site. See [Allow or prevent custom script](https://learn.microsoft.com/sharepoint/allow-or-prevent-custom-script) for more information.

### EXAMPLE 7
```powershell
Set-PnPSite -EnableAutoExpirationVersionTrim $false -ExpireVersionsAfterDays 180 -MajorVersionLimit 100 -MajorWithMinorVersionsLimit 10
```

Sets the site version policy for both new and existing document libraries to keep 100 major versions, 10 minor versions, and expire versions after 180 days.

### EXAMPLE 8
```powershell
Set-PnPSite -EnableAutoExpirationVersionTrim $true -ApplyToNewDocumentLibraries -FileTypesForVersionExpiration "pdf","docx"
```

Sets an automatic version trim policy for new document libraries only and limits the override to the specified file types.

### EXAMPLE 9
```powershell
Set-PnPSite -ApplyToNewDocumentLibraries -RemoveVersionExpirationFileTypeOverride "pdf","docx"
```

Removes the specified file type version expiration overrides from the site policy for new document libraries.

### EXAMPLE 10
```powershell
Set-PnPSite -InheritVersionPolicyFromTenant
```

Resets the site version policy so new document libraries inherit the tenant-level defaults.

## PARAMETERS

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

### -RequestFilesLinkExpirationInDays
Specifies the number of days before a Request Files link expires for the site.

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

### -AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled
Enables or disables adding and updating web property bag values when DenyAddAndCustomizePages is enabled.

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

### -Classification
The classification tag to set. This is the old classification/labeling method. Set it to $null to remove the classification entirely.

```yaml
Type: String
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SensitivityLabel
The Microsoft Purview sensitivity label to set. This is the new classification/labeling method.

```yaml
Type: String
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CommentsOnSitePagesDisabled
Specifies if comments on site pages are enabled or disabled.

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
Disables Microsoft Flow for this site.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableSharingForNonOwners
Specifies whether non-owners should be prevented from inviting new users to the site.
Setting this will also disable Access Request Emails.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

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

### -HidePeoplePreviewingFiles
Allows hiding of the presence indicators of users simultaneously editing files.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Url

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HidePeopleWhoHaveListsOpen
Allows hiding of the presence indicators of users simultaneously working in lists.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Url

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The url of the site collection.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Url

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InheritVersionPolicyFromTenant
Resets the site version policy so new document libraries inherit the tenant-level version policy settings.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LocaleId
Specifies the language of this site collection.

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
Sets the lockState of a site collection.

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

### -LogoFilePath
Sets the logo of the site if it is a modern team site without a Microsoft 365 Group behind it. Provide a full path to a local image file on your disk which you want to use as the site logo. The logo will be uploaded automatically to SharePoint.

If you want to set the logo for a classic site, use [Set-PnPWeb -SiteLogoUrl](https://pnp.github.io/powershell/cmdlets/Set-PnPWeb.html#-sitelogourl) instead.

If the modern site has a Microsoft 365 Group behind it, use [Set-PnPWebHeader -SiteLogoUrl](https://pnp.github.io/powershell/cmdlets/Set-PnPWebHeader.html#-sitelogourl) instead.

```yaml
Type: String
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

### -NoScriptSite
Specifies if a site allows custom script or not. See [Allow or prevent custom script](https://learn.microsoft.com/sharepoint/allow-or-prevent-custom-script) for more information.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties
Aliases: DenyAndAddCustomizePages

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

### -Owners
Specifies owner(s) to add as site collection administrators. They will be added as additional site collection administrators. Existing administrators will stay. Can be both users and groups.

```yaml
Type: System.Collections.Generic.List`1[System.String]
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RestrictedToGeo
Specifies the Geo/Region restrictions of this site.

```yaml
Type: RestrictedToRegion
Parameter Sets: Set Properties
Accepted values: NoRestriction, BlockMoveOnly, BlockFull, Unknown

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Sharing
Specifies what the sharing capabilities are for the site. Possible values: Disabled, ExternalUserSharingOnly, ExternalUserAndGuestSharing, ExistingExternalUserSharingOnly

```yaml
Type: SharingCapabilities
Parameter Sets: Set Properties
Accepted values: Disabled, ExternalUserSharingOnly, ExternalUserAndGuestSharing, ExistingExternalUserSharingOnly

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SocialBarOnSitePagesDisabled
Disables or enables the Social Bar for site collection.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StorageMaximumLevel
Specifies the storage quota for this site collection in megabytes. This value must not exceed the company's available quota.

```yaml
Type: Int64
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StorageWarningLevel
Specifies the warning level for the storage quota in megabytes. This value must not exceed the values set for the StorageMaximumLevel parameter

```yaml
Type: Int64
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
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

### -RequestFilesLinkEnabled
Allows configuring whether users will be able to create anonymous requests for people to upload files regardless of the Share with anyone link configuration setting for this particular site collection.

```yaml
Type: Boolean
Parameter Sets: (All)

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

### -ScriptSafeDomainName
Allow contributors to insert iframe only from the specified domains only

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RestrictedAccessControl
To enable restricted access control on a group-connected or Teams-connected site

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RestrictContentOrgWideSearch 
Allows for applying the Restricted Content Discoverability (RCD) setting to a site

```yaml
Type: Boolean
Parameter Sets: (All)

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

### -CanSyncHubSitePermissions
Sets value if syncing hub site permissions to this associated site is allowed.

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
Parameter Sets: Set Lock State

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

