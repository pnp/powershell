---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPSite
---

# Set-PnPSite

## SYNOPSIS

Sets site collection properties.

## SYNTAX

### Set Properties

```
Set-PnPSite [-Identity <String>] [-Classification <String>] [-DisableFlows] [-LogoFilePath <String>]
 [-Sharing <SharingCapabilities>] [-StorageMaximumLevel <Int64>] [-StorageWarningLevel <Int64>]
 [-AllowSelfServiceUpgrade] [-NoScriptSite]
 [-Owners <System.Collections.Generic.List<[System.String]>] [-CommentsOnSitePagesDisabled]
 [-DefaultLinkPermission <SharingPermissionType>] [-DefaultSharingLinkType <SharingLinkType>]
 [-DefaultLinkToExistingAccess <Boolean>] [-DefaultLinkToExistingAccessReset]
 [-DisableAppViews <AppViewsPolicy>]
 [-DisableCompanyWideSharingLinks <CompanyWideSharingLinksPolicy>] [-DisableSharingForNonOwners]
 [-LocaleId <UInt32>] [-RestrictedToGeo <RestrictedToRegion>] [-SocialBarOnSitePagesDisabled]
 [-AnonymousLinkExpirationInDays <Int32>] [-RequestFilesLinkExpirationInDays <Int32>]
 [-OverrideTenantAnonymousLinkExpirationPolicy] [-MediaTranscription <MediaTranscriptionPolicyType>]
 [-SensitivityLabel <Guid>] [-RequestFilesLinkEnabled <Boolean>] [-ScriptSafeDomainName <string>]
 [-BlockDownloadPolicy <Boolean>] [-ExcludeBlockDownloadPolicySiteOwners <Boolean>]
 [-ExcludedBlockDownloadGroupIds <Guid[]>] [-ListsShowHeaderAndNavigation <Boolean>]
 [-RestrictContentOrgWideSearch <Boolean>] [-CanSyncHubSitePermissions <SwitchParameter>]
 [-Connection <PnPConnection>]
```

### Set Lock State

```
Set-PnPSite [-Identity <String>] [-LockState <SiteLockState>] [-Wait] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

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

## PARAMETERS

### -AllowSelfServiceUpgrade

Specifies if the site administrator can upgrade the site collection.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -AnonymousLinkExpirationInDays

Specifies all anonymous/anyone links that have been created (or will be created) will expire after the set number of days. Only applies if OverrideTenantAnonymousLinkExpirationPolicy is set to true.

To remove the expiration requirement, set the value to zero (0).

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -BlockDownloadPolicy

Set this to true to block download of files from SharePoint sites or OneDrive

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -CanSyncHubSitePermissions

Sets value if syncing hub site permissions to this associated site is allowed.

```yaml
Type: Switch Parameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Classification

The classification tag to set. This is the old classification/labeling method. Set it to $null to remove the classification entirely.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -CommentsOnSitePagesDisabled

Specifies if comments on site pages are enabled or disabled.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DefaultLinkPermission

Specifies the default link permission for the site collection. None - Respect the organization default link permission. View - Sets the default link permission for the site to "view" permissions. Edit - Sets the default link permission for the site to "edit" permissions

```yaml
Type: SharingPermissionType
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- None
- View
- Edit
HelpMessage: ''
```

### -DefaultLinkToExistingAccess

When set to $true, the DefaultSharingLinkType will be overridden and the default sharing link will be All People with Existing Access link (which does not modify permissions). When set to $false (the default), the default sharing link type is controlled by the DefaultSharingLinkType parameter

```yaml
Type: Boolean
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DefaultSharingLinkType

Specifies the default link type for the site collection. None - Respect the organization default sharing link type. AnonymousAccess - Sets the default sharing link for this site to an Anonymous Access or Anyone link. Internal - Sets the default sharing link for this site to the "organization" link or company shareable link. Direct - Sets the default sharing link for this site to the "Specific people" link

```yaml
Type: SharingLinkType
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- None
- Direct
- Internal
- AnonymousAccess
HelpMessage: ''
```

### -DisableAppViews

Determines whether the App Views feature is disabled in the site collection.

```yaml
Type: AppViewsPolicy
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Unknown
- Disabled
- NotDisabled
HelpMessage: ''
```

### -DisableCompanyWideSharingLinks

Determines whether company-wide sharing links are disabled in collection.

```yaml
Type: CompanyWideSharingLinksPolicy
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Unknown
- Disabled
- NotDisabled
HelpMessage: ''
```

### -DisableFlows

Disables Microsoft Flow for this site.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DisableSharingForNonOwners

Specifies whether non-owners should be prevented from inviting new users to the site.
Setting this will also disable Access Request Emails.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ExcludeBlockDownloadPolicySiteOwners

Set this to true to exempts site owners from the block download policy so that they can fully download any content for the site.

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ExcludedBlockDownloadGroupIds

Exempts users from the mentioned groups from this policy and they can fully download any content for the site.

```yaml
Type: GUID[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -HidePeoplePreviewingFiles

Allows hiding of the presence indicators of users simultaneously editing files.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Url
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Identity

The url of the site collection.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Url
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ListsShowHeaderAndNavigation

Set a property on a site collection to make all lists always load with the site elements intact.

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -LocaleId

Specifies the language of this site collection.

```yaml
Type: UInt32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -LockState

Sets the lockState of a site collection.

```yaml
Type: SiteLockState
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Lock State
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Unlock
- NoAccess
- ReadOnly
HelpMessage: ''
```

### -LogoFilePath

Sets the logo of the site if it is a modern team site without a Microsoft 365 Group behind it. Provide a full path to a local image file on your disk which you want to use as the site logo. The logo will be uploaded automatically to SharePoint.

If you want to set the logo for a classic site, use [Set-PnPWeb -SiteLogoUrl](https://pnp.github.io/powershell/cmdlets/Set-PnPWeb.html#-sitelogourl) instead.

If the modern site has a Microsoft 365 Group behind it, use [Set-PnPWebHeader -SiteLogoUrl](https://pnp.github.io/powershell/cmdlets/Set-PnPWebHeader.html#-sitelogourl) instead.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -MediaTranscription

When the feature is enabled, videos can have transcripts generated on demand or generated automatically in certain scenarios. This is the default because the policy is default on. If a video owner decides they don’t want the transcript, they can always hide or delete it from that video.

```yaml
Type: MediaTranscriptionPolicyType
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Enabled
- Disabled
HelpMessage: ''
```

### -NoScriptSite

Specifies if a site allows custom script or not. See [Allow or prevent custom script](https://learn.microsoft.com/sharepoint/allow-or-prevent-custom-script) for more information.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- DenyAndAddCustomizePages
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -OverrideTenantAnonymousLinkExpirationPolicy

Specifies whether to use company-wide or a site collection level anonymous links expiration policy. Set it to true to get advantage of AnonymousLinkExpirationInDays.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Owners

Specifies owner(s) to add as site collection administrators. They will be added as additional site collection administrators. Existing administrators will stay. Can be both users and groups.

```yaml
Type: System.Collections.Generic.List`1[System.String]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -RequestFilesLinkEnabled

Allows configuring whether users will be able to create anonymous requests for people to upload files regardless of the Share with anyone link configuration setting for this particular site collection.

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -RequestFilesLinkExpirationInDays

Specifies the number of days before a Request Files link expires for the site.

The value can be from 0 to 730 days.

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -RestrictContentOrgWideSearch

Allows for applying the Restricted Content Discoverability (RCD) setting to a site

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -RestrictedAccessControl

To enable restricted access control on a group-connected or Teams-connected site

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -RestrictedToGeo

Specifies the Geo/Region restrictions of this site.

```yaml
Type: RestrictedToRegion
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- NoRestriction
- BlockMoveOnly
- BlockFull
- Unknown
HelpMessage: ''
```

### -ScriptSafeDomainName

Allow contributors to insert iframe only from the specified domains only

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SensitivityLabel

The Microsoft Purview sensitivity label to set. This is the new classification/labeling method.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Sharing

Specifies what the sharing capabilities are for the site. Possible values: Disabled, ExternalUserSharingOnly, ExternalUserAndGuestSharing, ExistingExternalUserSharingOnly

```yaml
Type: SharingCapabilities
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Disabled
- ExternalUserSharingOnly
- ExternalUserAndGuestSharing
- ExistingExternalUserSharingOnly
HelpMessage: ''
```

### -SocialBarOnSitePagesDisabled

Disables or enables the Social Bar for site collection.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -StorageMaximumLevel

Specifies the storage quota for this site collection in megabytes. This value must not exceed the company's available quota.

```yaml
Type: Int64
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -StorageWarningLevel

Specifies the warning level for the storage quota in megabytes. This value must not exceed the values set for the StorageMaximumLevel parameter

```yaml
Type: Int64
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Wait

Wait for the operation to complete

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Lock State
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
