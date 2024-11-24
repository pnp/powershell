---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPTenantSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPTenantSite
---

# Set-PnPTenantSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Updates settings of a site collection

## SYNTAX

### Set Properties

```
Set-PnPTenantSite [-Identity] <String> [-Title <String>] [-LocaleId <UInt32>]
 [-AllowSelfServiceUpgrade] [-PrimarySiteCollectionAdmin <String>] [-Owners <String[]>]
 [-DenyAddAndCustomizePages] [-SharingCapability <SharingCapabilities>] [-StorageQuota <Int64>]
 [-StorageQuotaWarningLevel <Int64>] [-StorageQuotaReset]
 [-BlockDownloadLinksFileType <BlockDownloadLinksFileTypes>] [-ResourceQuota <Double>]
 [-ResourceQuotaWarningLevel <Double>] [-EnablePWA <Boolean>]
 [-DefaultLinkPermission <SharingPermissionType>] [-DefaultSharingLinkType <SharingLinkType>]
 [-DefaultLinkToExistingAccess <Boolean>] [-ExternalUserExpirationInDays <Int32>]
 [-SharingAllowedDomainList <String>] [-SharingBlockedDomainList <String>]
 [-ShowPeoplePickerSuggestionsForGuestUsers <Boolean>] [-AllowDownloadingOfNonWebViewableFiles]
 [-LimitedAccessFileType <SPOLimitedAccessFileType>] [-AllowEditing <Boolean>]
 [-SharingDomainRestrictionMode <SharingDomainRestrictionModes>] [-CommentsOnSitePagesDisabled]
 [-DisableAppViews <AppViewsPolicy>]
 [-DisableCompanyWideSharingLinks <CompanyWideSharingLinksPolicy>] [-DisableFlows <FlowsPolicy>]
 [-AnonymousLinkExpirationInDays <Int32>] [-SensitivityLabel <String>] [-RemoveLabel]
 [-AddInformationSegment <Guid[]>] [-RemoveInformationSegment <Guid[]>]
 [-OverrideTenantAnonymousLinkExpirationPolicy] [-InformationBarriersMode <InformationBarriersMode>]
 [-MediaTranscription <MediaTranscriptionPolicyType>] [-BlockDownloadPolicy <Boolean>]
 [-ExcludeBlockDownloadPolicySiteOwners <Boolean>] [-ExcludedBlockDownloadGroupIds <Guid[]>]
 [-ListsShowHeaderAndNavigation <Boolean>] [-DefaultLinkToExistingAccessReset <SwitchParameter>]
 [-DefaultShareLinkRole <Role>] [-DefaultShareLinkScope <SharingScope>]
 [-LoopDefaultSharingLinkRole <Role>] [-LoopDefaultSharingLinkScope <SharingScope>]
 [-RestrictContentOrgWideSearch <Boolean>] [-ReadOnlyForUnmanagedDevices <Boolean>]
 [-RequestFilesLinkExpirationInDays <Int32>] [-RequestFilesLinkEnabled <Boolean>]
 [-OverrideSharingCapability <Boolean>] [-RestrictedAccessControl <Boolean>]
 [-ClearRestrictedAccessControl <SwitchParameter>] [-RestrictedAccessControlGroups <Guid[]>]
 [-AddRestrictedAccessControlGroups <Guid[]>] [-Wait] [-Connection <PnPConnection>]
```

### Set Lock State

```
Set-PnPTenantSite [-Identity] <String> [-LockState <SiteLockState>] [-Wait]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

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

## PARAMETERS

### -AddInformationSegment

This parameter allows you to add a segment to a SharePoint site. This parameter is only applicable for tenants who have enabled Microsoft 365 Information barriers capability. Please read https://learn.microsoft.com/sharepoint/information-barriers documentation to understand Information barriers in SharePoint Online.

```yaml
Type: Guid[]
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

### -AddRestrictedAccessControlGroups

You can add the specified security groups for restricted access control configuration.

```yaml
Type: GUID []
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

### -AllowDownloadOfNonViewableFiles

Specifies if non web viewable files can be downloaded.

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

### -AllowEditing

Prevents users from editing Office files in the browser and copying and pasting Office file contents out of the browser window.

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

### -BlockDownloadLinksFileType

The valid values are:

WebPreviewableFiles
ServerRenderedFilesOnly

Note: ServerRendered (Office Only) and WebPreviewable (All supported files).

The site value is compared with the tenant level setting and the stricter one wins. For example, if the tenant is set to ServerRenderedFilesOnly then that will be used even if the site is set to WebPreviewableFiles.

```yaml
Type: BlockDownloadLinksFileTypes
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

### -ClearRestrictedAccessControl

To reset restricted access control configuration for a site.

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

### -CommentsOnSitePagesDisabled

Specifies if comments on site pages are enabled.

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

### -DefaultLinkToExistingAccessReset

To reset the default link to existing access configuration for a site.

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

### -DefaultShareLinkRole

To set the default share link role. Available values are `None`, `Edit`, `Review`, `RestrictedView` and `View`.

```yaml
Type: Role
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
- Edit
- Review
- RestrictedView
- View
HelpMessage: ''
```

### -DefaultShareLinkScope

To set the default sharing link scope. Available values are `Anyone`, `Organization`, `SpecificPeople`, `Uninitialized`.

```yaml
Type: SharingScope
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
- Anyone
- Organization
- SpecificPeople
- Uninitialized
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

### -DenyAddAndCustomizePages

Determines whether the Add And Customize Pages right is denied in the site collection. For more information about permission levels, see User permissions and permission levels in SharePoint.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- NoScriptSite
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

Determines whether flows are disabled in the site collection.

```yaml
Type: FlowsPolicy
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

### -DisableSharingForNonOwners

Specifies whether non-owners should be prevented from inviting new users to the site. Setting this will also disable Access Request Emails.

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

### -EnablePWA

Determines whether site can include Project Web App. For more information about Project Web App, see Plan SharePoint groups in Project Server.

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

### -ExternalUserExpirationInDays

Specifies number of days the external users remain active on the site.

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

### -Identity

Specifies the URL of the site.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Url
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -InformationBarriersMode

Specifies the information barrier mode which helps strengthen access, sharing, and membership of a site based on its information barrier mode and segments associated with the site. Expected values are `Open`, `OwnerModerated` , `Implicit` and `Explicit`. For more information, see https://learn.microsoft.com/sharepoint/information-barriers#information-barriers-modes-and-sharepoint-sites

```yaml
Type: InformationBarriersMode
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
- Open
- OwnerModerated
- Implicit
- Explicit
HelpMessage: ''
```

### -InheritVersionPolicyFromTenant

Clears the file version setting at site level.

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

### -LimitedAccessFileType

The following parameters can be used with -ConditionalAccessPolicy AllowLimitedAccess for both the organization-wide setting and the site-level setting.

OfficeOnlineFilesOnly: Allows users to preview only Office files in the browser. This option increases security but may be a barrier to user productivity.

WebPreviewableFiles (default): Allows users to preview Office files and other file types (such as PDF files and images) in the browser. Note that the contents of file types other than Office files are handled in the browser. This option optimizes for user productivity but offers less security for files that aren't Office files.

OtherFiles: Allows users to download files that can't be previewed, such as .zip and .exe. This option offers less security.

```yaml
Type: SPOLimitedAccessFileType
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

Specifies the language of this site collection. For more information, see Locale IDs supported by SharePoint at https://github.com/pnp/PnP-PowerShell/wiki/Supported-LCIDs-by-SharePoint. To get the list of supported languages on a SharePoint environment use: (Get-PnPWeb -Includes RegionalSettings.InstalledLanguages).RegionalSettings.InstalledLanguages.

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

Sets the lockState of a site

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

### -LoopDefaultSharingLinkRole

To set the loop default sharing link role. Available values are `None`, `Edit`, `Review`, `RestrictedView` and `View`.

```yaml
Type: Role
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
- Edit
- Review
- RestrictedView
- View
HelpMessage: ''
```

### -LoopDefaultSharingLinkScope

To set the loop default sharing link scope. Available values are Anyone, Organization, SpecificPeople, Uninitialized.

```yaml
Type: SharingScope
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
- Anyone
- Organization
- SpecificPeople
- Uninitialized
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

### -OverrideSharingCapability

Specifies whether to override the sharing capability for the site.

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

Specifies owner(s) to add as secondary site collection administrators. They will be added as additional secondary site collection administrators. Existing administrators will stay. Can be both users and groups.

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

### -PrimarySiteCollectionAdmin

Specifies the user to set as the primary site collection administrator. Will replace the current primary site collection administrator. To add additional site collection administrators, use the -Owners parameter.

```yaml
Type: PnP.PowerShell.Commands.Base.PipeBinds.UserPipeBind
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

### -ReadOnlyForUnmanagedDevices

To set the site as read-only for unmanaged devices.

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

### -RemoveInformationSegment

This parameter allows you to remove a segment from a SharePoint site. This parameter is only applicable for tenants who have enabled Microsoft 365 Information barriers capability. Please read https://learn.microsoft.com/sharepoint/information-barriers documentation to understand Information barriers with SharePoint Online.

```yaml
Type: Guid[]
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

### -RemoveLabel

Remove the assigned sensitivity label of a site.

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

### -RemoveRestrictedAccessControlGroups

You can remove the specified security group from restricted access control configuration. Members of the security group are no longer be able to access site content while the policy is enforced on the site.

```yaml
Type: GUID []
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

Enables or disables the Request Files link on the site.

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

### -RequestFilesLinkExpirationInDays

Specifies the number of days after which the request files link will expire.
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

### -ResourceQuota

Specifies the resource quota in megabytes of the site collection. The default value is 0.

```yaml
Type: Double
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

### -ResourceQuotaWarningLevel

Specifies the warning level in megabytes of the site collection to warn the site collection administrator that the site is approaching the resource quota.

```yaml
Type: Double
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

To restrict content from being searchable organization-wide and Copilot.

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

### -RestrictedAccessControl

To apply restricted access control to a group-connected or Teams-connected site.

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

### -RestrictedAccessControlGroups

To edit a restricted access control group for a non-group site

```yaml
Type: GUID []
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

### -SensitivityLabel

Set the sensitivity label.

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

### -SharingAllowedDomainList

Specifies a list of email domains that is allowed for sharing with the external collaborators. Use the "," (comma) character as the delimiter for entering multiple values. For example, "contoso.com, fabrikam.com". Effective when SharingDomainRestrictionMode is set to AllowList.

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

### -SharingBlockedDomainList

Specifies a list of email domains that is blocked for sharing with the external collaborators. Use the space character as the delimiter for entering multiple values. For example, "contoso.com fabrikam.com". Effective when SharingDomainRestrictionMode is set to BlockList.

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

### -SharingCapability

Specifies what the sharing capabilities are for the site. Possible values: Disabled, ExternalUserSharingOnly, ExternalUserAndGuestSharing, ExistingExternalUserSharingOnly

```yaml
Type: SharingCapabilities
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Sharing
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

### -SharingDomainRestrictionMode

Specifies the external sharing mode for domains.

```yaml
Type: SharingDomainRestrictionModes
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
- AllowList
- BlockList
HelpMessage: ''
```

### -ShowPeoplePickerSuggestionsForGuestUsers

To enable the option to search for existing guest users at Site Collection Level, set this parameter to $true.

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

### -StorageQuota

Specifies the storage quota for this site collection in megabytes. This value must not exceed the company's available quota.

```yaml
Type: Int64
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- StorageMaximumLevel
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

### -StorageQuotaReset

Resets the OneDrive for Business storage quota to the tenant's new default storage space

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

### -StorageQuotaWarningLevel

Specifies the warning level for the storage quota in megabytes. This value must not exceed the values set for the StorageQuota parameter.

```yaml
Type: Int64
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- StorageQuotaMaximumLevel
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

### -Title

Specifies the title of the site.

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

### -Wait

Wait for the operation to complete

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
