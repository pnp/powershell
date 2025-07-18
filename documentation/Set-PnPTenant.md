---
Module Name: PnP.PowerShell
title: Set-PnPTenant
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTenant.html
---
 
# Set-PnPTenant

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Sets organization-level tenant properties

## SYNTAX

```powershell
Set-PnPTenant [-SpecialCharactersStateInFileFolderNames <SpecialCharactersState>]
 [-MinCompatibilityLevel <Int32>]
 [-MaxCompatibilityLevel <Int32>]
 [-ExternalServicesEnabled <Boolean>]
 [-NoAccessRedirectUrl <String>]
 [-SharingCapability <SharingCapabilities>]
 [-DisplayStartASiteOption <Boolean>]
 [-StartASiteFormUrl <String>] [-ShowEveryoneClaim <Boolean>]
 [-ShowAllUsersClaim <Boolean>]
 [-ShowEveryoneExceptExternalUsersClaim <Boolean>]
 [-SearchResolveExactEmailOrUPN <Boolean>]
 [-OfficeClientADALDisabled <Boolean>]
 [-LegacyAuthProtocolsEnabled <Boolean>]
 [-RequireAcceptingAccountMatchInvitedAccount <Boolean>]
 [-ProvisionSharedWithEveryoneFolder <Boolean>]
 [-SignInAccelerationDomain <String>]
 [-EnableGuestSignInAcceleration <Boolean>]
 [-UsePersistentCookiesForExplorerView <Boolean>]
 [-BccExternalSharingInvitations <Boolean>]
 [-BccExternalSharingInvitationsList <String>]
 [-UserVoiceForFeedbackEnabled <Boolean>]
 [-PublicCdnEnabled <Boolean>]
 [-PublicCdnAllowedFileTypes <String>]
 [-RequireAnonymousLinksExpireInDays <Int32>]
 [-SharingAllowedDomainList <String>]
 [-SharingBlockedDomainList <String>]
 [-SharingDomainRestrictionMode <SharingDomainRestrictionModes>]
 [-OneDriveStorageQuota <Int64>]
 [-OneDriveForGuestsEnabled <Boolean>]
 [-IPAddressEnforcement <Boolean>]
 [-IPAddressAllowList <String>]
 [-IPAddressWACTokenLifetime <Int32>]
 [-UseFindPeopleInPeoplePicker <Boolean>]
 [-DefaultSharingLinkType <SharingLinkType>]
 [-ODBMembersCanShare <SharingState>]
 [-ODBAccessRequests <SharingState>]
 [-PreventExternalUsersFromReSharing <Boolean>]
 [-ShowPeoplePickerSuggestionsForGuestUsers <Boolean>]
 [-FileAnonymousLinkType <AnonymousLinkType>]
 [-FolderAnonymousLinkType <AnonymousLinkType>]
 [-NotifyOwnersWhenItemsReShared <Boolean>]
 [-NotifyOwnersWhenInvitationsAccepted <Boolean>]
 [-NotificationsInOneDriveForBusinessEnabled <Boolean>]
 [-NotificationsInSharePointEnabled <Boolean>]
 [-OwnerAnonymousNotification <Boolean>]
 [-CommentsOnSitePagesDisabled <Boolean>]
 [-SocialBarOnSitePagesDisabled <Boolean>]
 [-OrphanedPersonalSitesRetentionPeriod <Int32>]
 [-DisallowInfectedFileDownload <Boolean>]
 [-DefaultLinkPermission <SharingPermissionType>]
 [-ConditionalAccessPolicy <SPOConditionalAccessPolicyType>]
 [-AllowDownloadingNonWebViewableFiles <Boolean>]
 [-AllowEditing <Boolean>]
 [-ApplyAppEnforcedRestrictionsToAdHocRecipients <Boolean>]
 [-FilePickerExternalImageSearchEnabled <Boolean>]
 [-EmailAttestationRequired <Boolean>]
 [-EmailAttestationReAuthDays <Int32>]
 [-HideDefaultThemes <Boolean>]
 [-DisabledWebPartIds <Guid[]>]
 [-EnableAIPIntegration <Boolean>]
 [-DisableCustomAppAuthentication <Boolean>] 
 [-EnableAutoNewsDigest <Boolean>]
 [-CommentsOnListItemsDisabled <Boolean>]
 [-CommentsOnFilesDisabled <Boolean>]
 [-AllowCommentsTextOnEmailEnabled <Boolean>]
 [-DisableBackToClassic <Boolean>]
 [-InformationBarriersSuspension <Boolean>] 
 [-AllowFilesWithKeepLabelToBeDeletedODB <Boolean>]
 [-AllowFilesWithKeepLabelToBeDeletedSPO <Boolean>]
 [-ExternalUserExpirationRequired <Boolean>]
 [-ExternalUserExpireInDays <Boolean>]
 [-OneDriveRequestFilesLinkEnabled <Boolean>]
 [-EnableRestrictedAccessControl <Boolean>]
 [-EnableAzureADB2BIntegration <Boolean>]
 [-CoreRequestFilesLinkEnabled <Boolean>]
 [-CoreRequestFilesLinkExpirationInDays <Int32>]
 [-LabelMismatchEmailHelpLink <String>]
 [-DisableDocumentLibraryDefaultLabeling <Boolean>]
 [-IsEnableAppAuthPopUpEnabled <Boolean>]
 [-ExpireVersionsAfterDays <Int32>]
 [-MajorVersionLimit <Int32>]
 [-EnableAutoExpirationVersionTrim <Boolean>]
 [-OneDriveLoopSharingCapability <SharingCapabilities>]
 [-OneDriveLoopDefaultSharingLinkScope <SharingScope>]
 [-OneDriveLoopDefaultSharingLinkRole <Role>]
 [-CoreLoopSharingCapability <SharingCapabilities>]
 [-CoreLoopDefaultSharingLinkScope <SharingScope>]
 [-CoreLoopDefaultSharingLinkRole <Role>]
 [-DisableVivaConnectionsAnalytics <Boolean>]
 [-CoreDefaultLinkToExistingAccess <Boolean>]
 [-HideSyncButtonOnTeamSite <Boolean>]
 [-CoreBlockGuestsAsSiteAdmin <SharingState>]
 [-IsWBFluidEnabled <Boolean>]
 [-IsCollabMeetingNotesFluidEnabled <Boolean>]
 [-AllowAnonymousMeetingParticipantsToAccessWhiteboards <SharingState>]
 [-IBImplicitGroupBased <Boolean>]
 [-ShowOpenInDesktopOptionForSyncedFiles <Boolean>]
 [-ShowPeoplePickerGroupSuggestionsForIB <Boolean>]
 [-BlockDownloadFileTypePolicy <Boolean>]
 [-BlockDownloadFileTypeIds <SPBlockDownloadFileTypeId[]>]
 [-ExcludedBlockDownloadGroupIds <GUID[]>]
 [-ArchiveRedirectUrl <String>]
 [-StopNew2013Workflows <Boolean>]
 [-MediaTranscription <MediaTranscriptionPolicyType>]
 [-MediaTranscriptionAutomaticFeatures <MediaTranscriptionAutomaticFeaturesPolicyType>]
 [-SiteOwnerManageLegacyServicePrincipalEnabled <Boolean>]
 [-ReduceTempTokenLifetimeEnabled <Boolean>]
 [-ReduceTempTokenLifetimeValue <Int32>]
 [-ViewersCanCommentOnMediaDisabled <Boolean>]
 [-AllowGuestUserShareToUsersNotInSiteCollection <Boolean>]
 [-ConditionalAccessPolicyErrorHelpLink <String>]
 [-CustomizedExternalSharingServiceUrl <String>]
 [-IncludeAtAGlanceInShareEmails <Boolean>]
 [-MassDeleteNotificationDisabled <Boolean>]
 [-BusinessConnectivityServiceDisabled <Boolean>]
 [-EnableSensitivityLabelForPDF <Boolean>]
 [-IsDataAccessInCardDesignerEnabled <Boolean>]
 [-CoreSharingCapability <SharingCapabilities>]
 [-BlockUserInfoVisibilityInOneDrive <TenantBrowseUserInfoPolicyValue>]
 [-AllowOverrideForBlockUserInfoVisibility <Boolean>]
 [-AllowEveryoneExceptExternalUsersClaimInPrivateSite <Boolean>]
 [-AIBuilderEnabled <Boolean>]
 [-AllowSensitivityLabelOnRecords <Boolean>]
 [-AnyoneLinkTrackUsers <Boolean>]
 [-EnableSiteArchive <Boolean>]
 [-ESignatureEnabled <Boolean>]
 [-BlockUserInfoVisibilityInSharePoint <TenantBrowseUserInfoPolicyValue>]
 [-MarkNewFilesSensitiveByDefault <SensitiveByDefaultState>]
 [-OneDriveDefaultShareLinkScope <SharingScope>]
 [-OneDriveDefaultShareLinkRole <Role>]
 [-OneDriveDefaultLinkToExistingAccess <Boolean>]
 [-OneDriveBlockGuestsAsSiteAdmin <SharingState>]
 [-RecycleBinRetentionPeriod <Int32>]
 [-IsSharePointAddInsDisabled <Boolean>]
 [-CoreDefaultShareLinkScope <SharingScope>]
 [-CoreDefaultShareLinkRole <Role>] 
 [-GuestSharingGroupAllowListInTenantByPrincipalIdentity <string[]>]
 [-OneDriveSharingCapability <SharingCapabilities>]
 [-AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled <Boolean>]
 [-SelfServiceSiteCreationDisabled <Boolean>]
 [-SyncAadB2BManagementPolicy]
 [-ExtendPermissionsToUnprotectedFiles <Boolean>]
 [-WhoCanShareAllowListInTenant <String>]
 [-LegacyBrowserAuthProtocolsEnabled <Boolean>]
 [-EnableDiscoverableByOrganizationForVideos <Boolean>]
 [-RestrictedAccessControlforSitesErrorHelpLink <String>]
 [-Workflow2010Disabled <Boolean>]
 [-AllowSharingOutsideRestrictedAccessControlGroups <Boolean>]
 [-HideSyncButtonOnDocLib <Boolean>]
 [-HideSyncButtonOnODB <Boolean>]
 [-StreamLaunchConfig <Int32>]
 [-EnableMediaReactions <Boolean>]
 [-ContentSecurityPolicyEnforcement <Boolean>]
 [-DisableSpacesActivation <Boolean>]
 [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION
Sets organization-level tenant properties which impact the entire tenant.

You must have the SharePoint Online admin or Global admin role to run the cmdlet.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTenantSite -Identity "https://contoso.sharepoint.com/sites/team1" -LockState NoAccess
Set-PnPTenant -NoAccessRedirectUrl "http://www.contoso.com"
```

This example blocks access to https://contoso.sharepoint.com/sites/team1 and redirects traffic to http://www.contoso.com.

### EXAMPLE 2
```powershell
Set-PnPTenant -ShowEveryoneExceptExternalUsersClaim $false
```

This example hides the "Everyone Except External Users" claim in People Picker.

### EXAMPLE 3
```powershell
Set-PnPTenant -ShowAllUsersClaim $false
```

This example hides the "All Users" claim group in People Picker.

### EXAMPLE 4
```powershell
Set-PnPTenant -UsePersistentCookiesForExplorerView $true
```

This example enables the use of special persisted cookie for Open with Explorer.

### EXAMPLE 5
```powershell
Set-PnPTenant  -GuestSharingGroupAllowListInTenantByPrincipalIdentity {c:0o.c|federateddirectoryclaimprovider|ee0f40fc-b2f7-45c7-b62d-11b90dd2ea8e}
```

This example sets the guest sharing group allow list in the tenant to the specified principal identity.

### EXAMPLE 6
```powershell
Set-PnPTenant  -GuestSharingGroupAllowListInTenantByPrincipalIdentity {}
```

This example clears the guest sharing group allow list in the tenant.

## PARAMETERS

### -AllowDownloadingNonWebViewableFiles

```yaml
Type: Boolean
Parameter Sets: (All)

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
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled
Enables or disables web property bag update when DenyAddAndCustomizePages is enabled. When AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled is set to $true, web property bag can be updated even if DenyAddAndCustomizePages is turned on when the user had AddAndCustomizePages (prior to DenyAddAndCustomizePages removing it).

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ApplyAppEnforcedRestrictionsToAdHocRecipients
When the feature is enabled, all guest users are subject to conditional access policy. By default guest users who are accessing SharePoint Online files with pass code are exempt from the conditional access policy.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ArchiveRedirectUrl
Can be used to configure a custom page to show when a user is navigating to a SharePoint Online site that has been archived using Microsoft Syntex Archiving.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BccExternalSharingInvitations
When the feature is enabled, all external sharing invitations that are sent will blind copy the e-mail messages listed in the BccExternalSharingInvitationsList.

The valid values are:
False (default) - BCC for external sharing is disabled.
True - All external sharing invitations that are sent will blind copy the e-mail messages listed in the BccExternalSharingInvitationsList.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BccExternalSharingInvitationsList
Specifies a list of e-mail addresses to be BCC'd when the BCC for External Sharing feature is enabled.
Multiple addresses can be specified by creating a comma separated list with no spaces.

The valid values are:
"" (default) - Blank by default, this will also clear any value that has been set.
Single or Multiple e-mail addresses - joe@contoso.com or joe@contoso.com,bob@contoso.com

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BlockSendLabelMismatchEmail
Allows blocking of the automated e-mail being sent when somebody uploads a document to a site that's protected with a sensitivity label and their document has a higher priority sensitivity label than the sensitivity label applied to the site. [More information](
https://learn.microsoft.com/microsoft-365/compliance/sensitivity-labels-teams-groups-sites?view=o365-worldwide#auditing-sensitivity-label-activities).

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BusinessConnectivityServiceDisabled
Allows blocking of Business Connectivity Services to be used on SharePoint Online. This feature is set to be retired on September 30, 2024. [More information](https://techcommunity.microsoft.com/t5/microsoft-sharepoint-blog/support-update-for-business-connectivity-services-in-microsoft/ba-p/3938773).

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableSensitivityLabelForPDF
Allows turning on support for PDFs with sensitivity labels for the following scenarios:

- Applying a sensitivity label in Office for the web.
- Uploading a labeled document, and then extracting and displaying that sensitivity label.
- Search, eDiscovery, and data loss prevention.
- Auto-labeling policies and default sensitivity labels for SharePoint document libraries.

The valid values are:
True - Enables support for PDFs.
False (default) - Disables support for PDFs.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CommentsOnSitePagesDisabled
Disables or enables the commenting functionality on all site pages in the tenant.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ConditionalAccessPolicy
Blocks or limits access to SharePoint and OneDrive content from un-managed devices.

```yaml
Type: SPOConditionalAccessPolicyType
Parameter Sets: (All)
Accepted values: AllowFullAccess, AllowLimitedAccess, BlockAccess, ProtectionLevel

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
Sets the default permission of the link in the sharing dialog box in OneDrive for Business and SharePoint Online. This applies to anonymous access, internal and direct links.

```yaml
Type: SharingPermissionType
Parameter Sets: (All)
Accepted values: None, View, Edit

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultSharingLinkType
Lets administrators choose what type of link appears is selected in the "Get a link" sharing dialog box in OneDrive for Business and SharePoint Online.

For additional information about how to change the default link type, see Change the default link type when users get links for sharing.

Note:
Setting this value to "none" will default "get a link" to the most permissive link available. If anonymous links are enabled, the default link will be anonymous access; if they are disabled, then the default link will be internal.

```yaml
Type: SharingLinkType
Parameter Sets: (All)
Accepted values: None, Direct, Internal, AnonymousAccess

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableBackToClassic
Disables the back to classic link for libraries and lists.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableCustomAppAuthentication
Configure if ACS-based app-only authentication should be disabled or not.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisabledWebPartIds
Guids of out of the box modern web part id's to disallow from being added to pages and remove from pages where they already have been used. Currently only the following web parts can be disabled in such a way:

Amazon Kindle: 46698648-fcd5-41fc-9526-c7f7b2ace919
YouTube: 544dd15b-cf3c-441b-96da-004d5a8cea1d
Twitter: f6fdf4f8-4a24-437b-a127-32e66a5dd9b4
Embed: 490d7c76-1824-45b2-9de3-676421c997fa
Microsoft Bookings: d24a7165-c455-4d43-8bc8-fedb04d6c1b5
Stream: 275c0095-a77e-4f6d-a2a0-6a7626911518

To block one of them, simply pass in the GUID behind the parameter. To disable more than one, separate the GUIDs with a comma. To unblock web parts, just set this property leaving out the one(s) you wish to unblock, leaving the ones that you would like to remain blocked. To unblock all web parts, use `-DisabledWebPartIds @()`. To see which one(s) are currently blocked, use `Get-PnPTenant | Select DisabledWebPartIds`.

```yaml
Type: Guid[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisallowInfectedFileDownload
Prevents the Download button from being displayed on the Virus Found warning page.

Accepts a value of true (enabled) to hide the Download button or false (disabled) to display the Download button. By default this feature is set to false.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayStartASiteOption
Determines whether tenant users see the Start a Site menu option.

The valid values are:
True (default) - Tenant users will see the Start a Site menu option.
False - Start a Site is hidden from the menu.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EmailAttestationReAuthDays
Sets the number of days for email attestation re-authentication. Value can be from 1 to 365 days.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EmailAttestationRequired
Sets email attestation to required.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableAIPIntegration
Boolean indicating if Azure Information Protection (AIP) should be enabled on the tenant. For more information, see https://learn.microsoft.com/microsoft-365/compliance/sensitivity-labels-sharepoint-onedrive-files#use-powershell-to-enable-support-for-sensitivity-labels

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableGuestSignInAcceleration
Accelerates guest-enabled site collections as well as member-only site collections when the SignInAccelerationDomain parameter is set.

Note:
If enabled, your identity provider must be capable of authenticating guest users. If it is not, guest users will be unable to log in and access content that was shared with them.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExternalServicesEnabled
Enables external services for a tenant.
External services are defined as services that are not in the Office 365 data centers.

The valid values are:
True (default) - External services are enabled for the tenant.
False - External services that are outside of the Office 365 data centers cannot interact with SharePoint.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FileAnonymousLinkType
Sets whether anonymous access links can allow recipients to only view or view and edit. The value can be set separately for folders and separately for files.

```yaml
Type: AnonymousLinkType
Parameter Sets: (All)
Accepted values: None, View, Edit

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FilePickerExternalImageSearchEnabled
Sets whether webparts that support inserting images, like for example Image or Hero webpart, the Web search (Powered by Bing) should allow choosing external images. The default is enabled.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FolderAnonymousLinkType
Sets whether anonymous access links can allow recipients to only view or view and edit. The value can be set separately for folders and separately for files.

```yaml
Type: AnonymousLinkType
Parameter Sets: (All)
Accepted values: None, View, Edit

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HideDefaultThemes
Defines if the default themes are visible or hidden.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IPAddressAllowList
Configures multiple IP addresses or IP address ranges (IPv4 or IPv6).

Use commas to separate multiple IP addresses or IP address ranges. Verify there are no overlapping IP addresses and ensure IP ranges use Classless Inter-Domain Routing (CIDR) notation. For example, 172.16.0.0, 192.168.1.0/27.

Note:
The IPAddressAllowList parameter only lets administrators set IP addresses or ranges that are recognized as trusted. To only grant access from these IP addresses or ranges, set the IPAddressEnforcement parameter to $true.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IPAddressEnforcement
Allows access from network locations that are defined by an administrator.

The values are $true and $false. The default value is $false which means the setting is disabled.

Before the IPAddressEnforcement parameter is set, make sure you add a valid IPv4 or IPv6 address to the IPAddressAllowList parameter.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IPAddressWACTokenLifetime
Allows to set the session timeout. If you are a tenant administrator and you begin IP address enforcement for OneDrive for Business in Office 365, this enforcement automatically activates a tenant parameter IPAddressWACTokenLifetime. The default value is 15 minutes, when IP Address Enforcement is True.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsDataAccessInCardDesignerEnabled
Allows turning on support for data access in the Viva Connections Adaptive Card Designer.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsSharePointAddInsDisabled
When the feature is enabled, all the add-ins features will be disabled.

The valid values are:
- False (default) - All the add-ins features are supported.
- True - All the add-ins features will be disabled.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LegacyAuthProtocolsEnabled
By default this value is set to $true.

Setting this parameter prevents Office clients using non-modern authentication protocols from accessing SharePoint Online resources.

A value of $true - Enables Office clients using non-modern authentication protocols(such as, Forms-Based Authentication (FBA) or Identity Client Runtime Library (IDCRL)) to access SharePoint resources.

A value of $false - Prevents Office clients using non-modern authentication protocols from accessing SharePoint Online resources.

Note:
This may also prevent third-party apps from accessing SharePoint Online resources.Also, this will also block apps using the SharePointOnlineCredentials class to access SharePoint Online resources.For additional information about SharePointOnlineCredentials, see SharePointOnlineCredentials class.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaxCompatibilityLevel
Specifies the upper bound on the compatibility level for new sites.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MinCompatibilityLevel
Specifies the lower bound on the compatibility level for new sites.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoAccessRedirectUrl
Specifies the URL of the redirected site for those site collections which have the locked state "NoAccess"

The valid values are:
""(default) - Blank by default, this will also remove or clear any value that has been set.
Full URL - Example: https://contoso.sharepoint.com/Pages/Locked.aspx

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NotificationsInOneDriveForBusinessEnabled
Enables or disables notifications in OneDrive for Business.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NotificationsInSharePointEnabled
Enables or disables notifications in SharePoint.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NotifyOwnersWhenInvitationsAccepted
When this parameter is set to $true and when an external user accepts an invitation to a resource in a user's OneDrive for Business, the OneDrive for Business owner is notified by e-mail.

For additional information about how to configure notifications for external sharing, see Configure notifications for external sharing for OneDrive for Business.

The values are $true and $false.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NotifyOwnersWhenItemsReShared
When this parameter is set to $true and another user re-shares a document from a user's OneDrive for Business, the OneDrive for Business owner is notified by e-mail.

For additional information about how to configure notifications for external sharing, see Configure notifications for external sharing for OneDrive for Business.

The values are $true and $false.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ODBAccessRequests
Lets administrators set policy on access requests and requests to share in OneDrive for Business.

Values:

On- Users without permission to share can trigger sharing requests to the OneDrive for Business owner when they attempt to share. Also, users without permission to a file or folder can trigger access requests to the OneDrive for Business owner when they attempt to access an item they do not have permissions to.

Off- Prevent access requests and requests to share on OneDrive for Business.

Unspecified- Let each OneDrive for Business owner enable or disable access requests and requests to share on their OneDrive.

```yaml
Type: SharingState
Parameter Sets: (All)
Accepted values: Unspecified, On, Off

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ODBMembersCanShare
Lets administrators set policy on re-sharing behavior in OneDrive for Business.

Values:

On- Users with edit permissions can re-share.

Off- Only OneDrive for Business owner can share. The value of ODBAccessRequests defines whether a request to share gets sent to the owner.

Unspecified- Let each OneDrive for Business owner enable or disable re-sharing behavior on their OneDrive.

```yaml
Type: SharingState
Parameter Sets: (All)
Accepted values: Unspecified, On, Off

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OfficeClientADALDisabled
When set to true this will disable the ability to use Modern Authentication that leverages ADAL across the tenant.

The valid values are:
False (default) - Modern Authentication is enabled/allowed.
True - Modern Authentication via ADAL is disabled.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OneDriveForGuestsEnabled
Lets OneDrive for Business creation for administrator managed guest users. Administrator managed Guest users use credentials in the resource tenant to access the resources.

The valid values are the following:

$true-Administrator managed Guest users can be given OneDrives, provided needed licenses are assigned.

$false- Administrator managed Guest users can't be given OneDrives as functionality is turned off.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OneDriveStorageQuota
Sets a default OneDrive for Business storage quota for the tenant. It will be used for new OneDrive for Business sites created.

A typical use will be to reduce the amount of storage associated with OneDrive for Business to a level below what the License entitles the users. For example, it could be used to set the quota to 10 gigabytes (GB) by default.

If value is set to 0, the parameter will have no effect.

If the value is set larger than the Maximum allowed OneDrive for Business quota, it will have no effect.

```yaml
Type: Int64
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OrphanedPersonalSitesRetentionPeriod
Specifies the number of days after a user's Active Directory account is deleted that their OneDrive for Business content will be deleted.

The value range is in days, between 30 and 3650. The default value is 30.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OwnerAnonymousNotification
Specifies whether an email notification should be sent to the OneDrive for Business owners when an anonymous links are created or changed.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PreventExternalUsersFromReSharing
Prevents external users from resharing files, folders, and sites that they do not own.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProvisionSharedWithEveryoneFolder
Creates a Shared with Everyone folder in every user's new OneDrive for Business document library.

The valid values are:
True (default) - The Shared with Everyone folder is created.
False - No folder is created when the site and OneDrive for Business document library is created.

The default behavior of the Shared with Everyone folder changed in August 2015.
For additional information about the change, see Provision the Shared with Everyone folder in OneDrive for Business (https://support.office.com/article/Provision-the-Shared-with-Everyone-folder-in-OneDrive-for-Business-6bb02c91-fd0b-42ba-9457-3921cb6dc5b2)

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PublicCdnAllowedFileTypes
Sets public CDN allowed file types, if the public CDN is enabled.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PublicCdnEnabled
Enables or disables the public CDN.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RequireAcceptingAccountMatchInvitedAccount

Ensures that an external user can only accept an external sharing invitation with an account matching the invited email address.

Administrators who desire increased control over external collaborators should consider enabling this feature.

Note, this only applies to new external users accepting new sharing invitations. Also, the resource owner must share with an organizational or Microsoft account or the external user will be unable to access the resource.

The valid values are:
False (default) - When a document is shared with an external user, bob@contoso.com, it can be accepted by any user with access to the invitation link in the original e-mail.
True - User must accept this invitation with bob@contoso.com.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RequireAnonymousLinksExpireInDays

Specifies all anonymous links that have been created (or will be created) will expire after the set number of days .

To remove the expiration requirement, set the value to zero (0).

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SearchResolveExactEmailOrUPN

Removes the search capability from People Picker. Note, recently resolved names will still appear in the list until browser cache is cleared or expired.

SharePoint Administrators will still be able to use starts with or partial name matching when enabled.

The valid values are:
False (default) - Starts with / partial name search functionality is available.
True - Disables starts with / partial name search functionality for all SharePoint users, except SharePoint Admins.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharingAllowedDomainList

Specifies a list of email domains that is allowed for sharing with the external collaborators. Use the space character as the delimiter for entering multiple values. For example, "contoso.com fabrikam.com".

For additional information about how to restrict a domain sharing, see Restricted Domains Sharing in Office 365 SharePoint Online and OneDrive for Business

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharingBlockedDomainList

Specifies a list of email domains that is blocked or prohibited for sharing with the external collaborators. Use space character as the delimiter for entering multiple values. For example, "contoso.com fabrikam.com".

For additional information about how to restrict a domain sharing, see Restricted Domains Sharing in Office 365 SharePoint Online and OneDrive for Business

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharingCapability

Determines what level of sharing is available for the site.

The valid values are:

- ExternalUserAndGuestSharing (default) : External user sharing (share by email) and guest link sharing are both enabled. 
- Disabled : External user sharing (share by email) and guest link sharing are both disabled.
- ExternalUserSharingOnly : External user sharing (share by email) is enabled, but guest link sharing is disabled.
- ExistingExternalUserSharingOnly : Only guests already in your organization's directory.

For more information about sharing, see Manage external sharing for your SharePoint online environment (https://learn.microsoft.com/sharepoint/turn-external-sharing-on-or-off).

```yaml
Type: SharingCapabilities
Parameter Sets: (All)
Accepted values: Disabled, ExternalUserSharingOnly, ExternalUserAndGuestSharing, ExistingExternalUserSharingOnly

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharingDomainRestrictionMode

Specifies the external sharing mode for domains.

The following values are: None AllowList BlockList

For additional information about how to restrict a domain sharing, see Restricted Domains Sharing in Office 365 SharePoint Online and OneDrive for Business.

```yaml
Type: SharingDomainRestrictionModes
Parameter Sets: (All)
Accepted values: None, AllowList, BlockList

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShowAllUsersClaim

Enables the administrator to hide the All Users claim groups in People Picker.

When users share an item with "All Users (x)", it is accessible to all organization members in the tenant's Azure Active Directory who have authenticated with via this method. When users share an item with "All Users (x)" it is accessible to all organization members in the tenant that used NTLM to authentication with SharePoint.

Note, the All Users(authenticated) group is equivalent to the Everyone claim, and shows as Everyone.To change this, see - ShowEveryoneClaim.

The valid values are:
True(default) - The All Users claim groups are displayed in People Picker.
False - The All Users claim groups are hidden in People Picker.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShowEveryoneClaim

Enables the administrator to hide the Everyone claim in the People Picker.
When users share an item with Everyone, it is accessible to all authenticated users in the tenant's Azure Active Directory, including any active external users who have previously accepted invitations.

Note, that some SharePoint system resources such as templates and pages are required to be shared to Everyone and this type of sharing does not expose any user data or metadata.

The valid values are:
True (default) - The Everyone claim group is displayed in People Picker.
False - The Everyone claim group is hidden from the People Picker.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShowEveryoneExceptExternalUsersClaim

Enables the administrator to hide the "Everyone except external users" claim in the People Picker.
When users share an item with "Everyone except external users", it is accessible to all organization members in the tenant's Azure Active Directory, but not to any users who have previously accepted invitations.

The valid values are:
True(default) - The Everyone except external users is displayed in People Picker.
False - The Everyone except external users claim is not visible in People Picker.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShowPeoplePickerSuggestionsForGuestUsers

Shows people picker suggestions for guest users. To enable the option to search for existing guest users at Tenant Level, set this parameter to $true.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SignInAccelerationDomain

Specifies the home realm discovery value to be sent to Azure Active Directory (AAD) during the user sign-in process.

When the organization uses a third-party identity provider, this prevents the user from seeing the Azure Active Directory Home Realm Discovery web page and ensures the user only sees their company's Identity Provider's portal.
This value can also be used with Azure Active Directory Premium to customize the Azure Active Directory login page.

Acceleration will not occur on site collections that are shared externally.

This value should be configured with the login domain that is used by your company (that is, example@contoso.com).

If your company has multiple third-party identity providers, configuring the sign-in acceleration value will break sign-in for your organization.

The valid values are:
"" (default) - Blank by default, this will also remove or clear any value that has been set.
Login Domain - For example: "contoso.com"

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SocialBarOnSitePagesDisabled

Disables or enables the Social Bar which appears on all modern SharePoint pages with the exception of the home page of a site. It gives users the ability to like a page, see the number of views, likes, and comments on a page, and see the people who have liked a page.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SpecialCharactersStateInFileFolderNames
Permits the use of special characters in file and folder names in SharePoint Online and OneDrive for Business document libraries. The only two characters that can be managed at this time are the **#** and **%** characters.

```yaml
Type: SpecialCharactersState
Parameter Sets: (All)
Accepted values: NoPreference, Allowed, Disallowed

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StartASiteFormUrl
Specifies URL of the form to load in the Start a Site dialog.

The valid values are:
"" (default) - Blank by default, this will also remove or clear any value that has been set.
Full URL - Example: "https://contoso.sharepoint.com/path/to/form"

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseFindPeopleInPeoplePicker
Note:
When set to $true, users aren't able to share with security groups or SharePoint groups.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UsePersistentCookiesForExplorerView
Lets SharePoint issue a special cookie that will allow this feature to work even when "Keep Me Signed In" is not selected.

"Open with Explorer" requires persisted cookies to operate correctly.
When the user does not select "Keep Me Signed in" at the time of sign -in, "Open with Explorer" will fail.

This special cookie expires after 30 minutes and cannot be cleared by closing the browser or signing out of SharePoint Online.To clear this cookie, the user must log out of their Windows session.

The valid values are:
False(default) - No special cookie is generated and the normal Office 365 sign -in length / timing applies.
True - Generates a special cookie that will allow "Open with Explorer" to function if the "Keep Me Signed In" box is not checked at sign -in.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserVoiceForFeedbackEnabled
Enables or disables the User Voice Feedback button shown at the bottom of all modern SharePoint Online pages. The "Feedback" link allows the end user to fill out a feedback form inside SharePoint Online which then creates an entry in the public SharePoint UserVoice topic.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableAutoNewsDigest
Boolean indicating if a news digest should automatically be sent to end users to inform them about news that they may have missed. On by default. For more information see https://aka.ms/autonewsdigest

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CommentsOnListItemsDisabled
Disables or enables commenting functionality on list items.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CommentsOnFilesDisabled
Disables or enables commenting functionality on files.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowCommentsTextOnEmailEnabled
When this parameter is set to true, an email notification that user receives when is mentioned, includes the surrounding document context. Set it to false to disable this feature.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InformationBarriersSuspension
Allows suspension of the information barriers feature in a Microsoft 365 tenant. Setting this to $true will disable information barriers, setting this to $false will enable information barriers. For more information, see https://learn.microsoft.com/sharepoint/information-barriers.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowFilesWithKeepLabelToBeDeletedSPO
Allows configuration on if files located on SharePoint Online having retention labels on them blocking them from deletion ($false) or if they can be deleted which will move the file to the preservation hold library ($true)

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: $true
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowFilesWithKeepLabelToBeDeletedODB
Allows configuration on if files located on OneDrive for Business having retention labels on them blocking them from deletion ($false) or if they can be deleted which will move the file to the preservation hold library ($true)

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: $true
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableAddToOneDrive
Allows configuration on `Add shortcut to OneDrive` feature in SharePoint document libraries. If set to `$true`, then this feature will be disabled on all sites in the tenant. If set to `$false`, it will be enabled on all sites in the tenant.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases: DisableAddShortcutsToOneDrive
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsFluidEnabled
Allows configuration on whether Fluid components are enabled or disabled in the tenant. If set to `$true`, then this feature will be enabled on all sites in the tenant. If set to `$false`, it will be disabled on all sites in the tenant.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisablePersonalListCreation
Allows configuring whether personal lists created within the OneDrive for Business site of the user is enabled or disabled in the tenant. If set to `$false`, personal lists will be allowed to be created in the tenant. If set to `$true`, it will be disabled in the tenant.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisabledModernListTemplateIds
Guids of out of the box modern list templates to hide when creating a new list

```yaml
Type: Guid[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableModernListTemplateIds
Guids of out of the box modern list templates to show when creating a new list

| Template  | Template Id |
| ------------- | ------------- |
| Media library | 7fdc8cba-3e07-4851-a7ac-b747040ff1ce |
| Learning | 2a31cc9a-a7a2-4978-8104-6b7c0c0ff1ce |
| Invoices | cb3f4b1a-d4d8-40b3-a3e8-c39c470ff1ce |

```yaml
Type: Guid[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExternalUserExpirationRequired
When set to true, it will set enable expiration date for external users.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExternalUserExpireInDays
When a value is set, it means that the access of the external user will expire in those many number of days.

```yaml
Type: Int
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayNamesOfFileViewers
Allows configuring whether display name of people who view the file are visible in the property pane of the site in OneDrive for business sites.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayNamesOfFileViewersInSpo
Allows configuring whether display name of people who view the file are visible in the property pane of the site in SharePoint site collection.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsLoopEnabled
Allows configuring whether loop components are enabled or disabled in the tenant. If set to `$true`, loop components will be allowed to be created in the tenant. If set to `$false`, it will be disabled in the tenant.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OneDriveRequestFilesLinkEnabled
Allows configuring whether users will be able to create anonymous requests for people to upload files regardless of the Share with anyone link configuration setting.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableRestrictedAccessControl
To enable restricted access control in SharePoint Online. You need to wait approximately 1 hour before managing restricted access control for a site collection.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableAzureADB2BIntegration
Allows enablement of SharePoint and OneDrive integration with Azure AD B2B. See [this article](https://learn.microsoft.com/sharepoint/sharepoint-azureb2b-integration) for more information.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CoreRequestFilesLinkEnabled
Enable or disable the Request files link on the core partition for all SharePoint sites (not including OneDrive sites). If this value is not set, Request files will only show for OneDrives with Anyone links enabled.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CoreRequestFilesLinkExpirationInDays
Specifies the number of days before a Request files link expires for all SharePoint sites (not including OneDrive sites).

The value can be from 0 to 730 days.

```yaml
Type: Integer
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LabelMismatchEmailHelpLink

This parameter allows tenant admins to customize the "Help Link" in email with the subject "Incompatible sensitivity label detected." When a sensitivity label mismatch occurs between the label on the document uploaded and the label on the site, SharePoint Online captures an audit record and sends an Incompatible sensitivity label detected email notification to the person who uploaded the document and the site owner. The notification contains details of the document which caused the problem and the label assigned to the document and to the site. The comparison happens between the priority of these two labels. [More information](https://learn.microsoft.com/microsoft-365/compliance/sensitivity-labels-teams-groups-sites?view=o365-worldwide#auditing-sensitivity-label-activities).

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CoreLoopSharingCapability
Gets or sets collaboration type for fluid on core partition

The valid values are:

- ExternalUserAndGuestSharing (default) : External user sharing (share by email) and guest link sharing are both enabled.
- Disabled : External user sharing (share by email) and guest link sharing are both disabled.
- ExternalUserSharingOnly : External user sharing (share by email) is enabled, but guest link sharing is disabled.
- ExistingExternalUserSharingOnly : Only guests already in your organization's directory.

```yaml
Type: SharingCapabilities
Parameter Sets: (All)
Accepted values: Disabled, ExternalUserSharingOnly, ExternalUserAndGuestSharing, ExistingExternalUserSharingOnly

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OneDriveLoopSharingCapability

When sharing a whiteboard in a Teams meeting, Whiteboard creates a sharing link that’s accessible by anyone within the organization and automatically shares the whiteboard with any in-tenant users in the meeting.

There’s an additional capability for temporary collaboration by external and shared device accounts during a meeting. This allows these users to temporarily view and collaborate on whiteboards when they’re shared in a Teams meeting, similar to PowerPoint Live sharing.

If you have the external sharing for OneDrive for Business allowed, no further action is required. If you have external sharing for OneDrive for Business disabled, you can leave it disabled but you must enable this new setting. The setting will not take effect until the SharingCapability 'ExternalUserAndGuestSharing' is also enabled at Tenant level. For more information, see [Enable Microsoft Whiteboard for your organization](https://support.microsoft.com/office/enable-microsoft-whiteboard-for-your-organization-1caaa2e2-5c18-4bdf-b878-2d98f1da4b24).

The valid values are:

- ExternalUserAndGuestSharing (default) : External user sharing (share by email) and guest link sharing are both enabled.
- Disabled : External user sharing (share by email) and guest link sharing are both disabled.
- ExternalUserSharingOnly : External user sharing (share by email) is enabled, but guest link sharing is disabled.
- ExistingExternalUserSharingOnly : Only guests already in your organization's directory.


```yaml
Type: SharingCapabilities
Parameter Sets: (All)
Accepted values: Disabled, ExternalUserSharingOnly, ExternalUserAndGuestSharing, ExistingExternalUserSharingOnly

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OneDriveLoopDefaultSharingLinkScope
Gets or sets default share link scope for fluid on OneDrive sites.

The valid values are:

- Anyone
- Organization
- SpecificPeople
- Uninitialized

```yaml
Type: SharingScope
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CoreLoopDefaultSharingLinkScope

Gets or sets default share link scope for fluid on SharePoint sites.

The valid values are:

- Anyone
- Organization
- SpecificPeople
- Uninitialized

```yaml
Type: SharingScope
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CoreLoopDefaultSharingLinkRole

Gets or sets default share link role for fluid on SharePoint sites.

The valid values are:

- Edit
- None
- RestrictedView
- Review
- View

```yaml
Type: Role
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsWBFluidEnabled

Sets whether Whiteboard is enabled or disabled for OneDrive for Business users. Whiteboard on OneDrive for Business is automatically enabled for applicable Microsoft 365 tenants but can be disabled.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsCollabMeetingNotesFluidEnabled

Gets or sets a value to specify whether CollabMeetingNotes Fluid Framework is enabled. If IsFluidEnabled disabled, IsCollabMeetingNotesFluidEnabled will be disabled automatically. If IsFluidEnabled enabled, IsCollabMeetingNotesFluidEnabled will be enabled automatically. IsCollabMeetingNotesFluidEnabled can be enabled only when IsFluidEnabled is already enabled.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowAnonymousMeetingParticipantsToAccessWhiteboards

When you share a whiteboard in a Teams meeting, Whiteboard creates a sharing link. This link is accessible by anyone within the organization. The whiteboard is also shared with any in-tenant users in the meeting. Whiteboards are shared using company-shareable links, regardless of the default setting. Support for the default sharing link type is planned.

There's more capability for temporary collaboration by external and shared device accounts during a Teams meeting. Users can temporarily view and collaborate on whiteboards that are shared in a meeting, in a similar way to PowerPoint Live sharing.

In this case, Whiteboard provides temporary viewing and collaboration on the whiteboard during the Teams meeting only. A share link isn't created and Whiteboard doesn't grant access to the file.

If you have external sharing enabled for OneDrive for Business, no further action is required.

If you restrict external sharing for OneDrive for Business, you can keep it restricted, and just enable this new setting in order for external and shared device accounts to work. For more information, see [Manage sharing for Microsoft Whiteboard](https://learn.microsoft.com/microsoft-365/whiteboard/manage-sharing-organizations).

```yaml
Type: SharingState
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IBImplicitGroupBased
Gets or sets IBImplicitGroupBased value

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShowOpenInDesktopOptionForSyncedFiles

The ShowOpenInDesktopOptionForSyncedFiles setting (set to false by default) displays the "Open in desktop" option when users go to SharePoint or OneDrive on the web and open the shortcut menu for a file that they're syncing with the OneDrive sync app.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShowPeoplePickerGroupSuggestionsForIB

The ShowPeoplePickerGroupSuggestionsForIB setting (defaulted to false) allows showing group suggestions for information barriers (IBs) in the People Picker.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CoreBlockGuestsAsSiteAdmin

```yaml
Type: SharingState
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HideSyncButtonOnTeamSite

To enable or disable Sync button on Team sites.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CoreDefaultLinkToExistingAccess

Gets or sets default share link to existing access on core partition

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableVivaConnectionsAnalytics

Use this parameter to disable/enable Viva connection analytics.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OneDriveLoopDefaultSharingLinkRole

Gets or sets default share link role for fluid on OneDrive sites.

The valid values are:

- Edit
- None
- RestrictedView
- Review
- View

```yaml
Type: Role
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MajorVersionLimit

When Version History Limits are managed Manually (EnableAutoExpirationVersionTrim $false), admins will need to set the limits to the number of major versions (MajorVersionLimit) and the time period the versions are stored (ExpireVersionsAfterDays).

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExpireVersionsAfterDays

When Version History Limits are managed Manually (EnableAutoExpirationVersionTrim $false), admins will need to set the limits to the number of major versions (MajorVersionLimit) and the time period the versions are stored (ExpireVersionsAfterDays).

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableAutoExpirationVersionTrim

Enable or disable AutoExpiration version trim for the document library. Set to `$true` to enable, `$false` to disable.

Parameter `ExpireVersionsAfterDays` is required when `EnableAutoExpirationVersionTrim` is false. Set `ExpireVersionsAfterDays` to 0 for NoExpiration, set it to greater or equal 30 for ExpireAfter.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsEnableAppAuthPopUpEnabled

Enables or disables users in the organization to authenticate SharePoint applications using popups.

This parameter affects the way code in SharePoint interacts with Azure AD to get tokens to access APIs. In scenarios where third-party cookies are disabled (such as Safari browsers with ITP feature enabled), any code that requires a token to access an API automatically triggers a full page refresh. When IsEnableAppAuthPopUpEnabled is set to $true, SharePoint will instead surface a popup in this scenario.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OneDriveRequestFilesLinkExpirationInDays

Specifies the number of days before a Request files link expires for all OneDrive sites.
The value can be from 0 to 730 days.
To remove the expiration requirement, set the value to zero (0).

```yaml
Type: Int
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableDocumentLibraryDefaultLabeling

Use this turn off setting the default sensitivity label for a document library.
For more information on this feature, please take a look [here](https://learn.microsoft.com/microsoft-365/compliance/sensitivity-labels-sharepoint-default-label?view=o365-worldwide)

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BlockDownloadFileTypePolicy

You can block the download of Teams meeting recording files from SharePoint or OneDrive. This allows users to remain productive while addressing the risk of accidental data loss. Users have browser-only access to play the meeting recordings with no ability to download or sync files or access them through apps.

This policy applies to new meeting recordings across the entire organization. You can exempt people who are members of specified security groups from the policy. This allows you to specify governance or compliance specialists who should have download access to meeting recordings.

After the policy is turned on, any new Teams meeting recording files created by the Teams service and saved in SharePoint and OneDrive are blocked from download.

Because this policy affects meeting recordings stored in OneDrive and SharePoint, you must be a SharePoint administrator to configure it.

Note that this policy doesn't apply to manually uploaded meeting recording files. For more details, see [Block the download of Teams meeting recording files from SharePoint or OneDrive.](https://learn.microsoft.com/microsoftteams/block-download-meeting-recording)

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BlockDownloadFileTypeIds

The File Type IDs which need to specified to prevent download.

```yaml
Type: SPBlockDownloadFileTypeId[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludedBlockDownloadGroupIds

This parameter exempts users in the specified security groups from this policy so that they can download meeting recording files.

```yaml
Type: GUID[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StopNew2013Workflows

This parameter allows disablement of creation of new SharePoint 2013 workflows in the tenant

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MediaTranscription

When the feature is enabled, videos can have transcripts generated on demand or generated automatically in certain scenarios. This is the default because the policy is default on. If a video owner decides they don’t want the transcript, they can always hide or delete it from that video. Possible values: Enabled, Disabled.

```yaml
Type: MediaTranscriptionPolicyType
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MediaTranscriptionAutomaticFeatures

When the feature is enabled, videos can have transcripts generated automatically on upload. The policy is default on. If a tenant admin decides to disable the feature, he can do so by disabling the policy at tenant level. This feature can not be enabled or disabled at site level. Possible values: Enabled, Disabled.

```yaml
Type: MediaTranscriptionAutomaticFeaturesPolicyType
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteOwnerManageLegacyServicePrincipalEnabled

This parameter allows site owners to create or update the service principal.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ReduceTempTokenLifetimeEnabled

Enables reduced session timeout for temporary URLs used by apps for document download scenarios. Reduction occurs when an app redeeming an IP address does not match the original requesting IP. The default value is 15 minutes if ReduceTempTokenLifetimeValue is not set.

Note: Reducing this value may bring degradation in end-user experience by requiring frequent authentication prompts to users.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ReduceTempTokenLifetimeValue

Optional parameter to set the session timeout value for temporary URLs. The value can be set between 5 and 15 minutes and the default value is 15 minutes.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ViewersCanCommentOnMediaDisabled

Controls whether viewers commenting on media items is disabled or not.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowGuestUserShareToUsersNotInSiteCollection

The AllowGuestUserShareToUsersNotInSiteCollection settings (defaulted to false) will allow guests to share to users not in the site.

The valid values are:

- False (default) - Guest users will only be able to share to users that exist within the current site.
- True - Guest users will be able to find user accounts in the directory by typing in the exact email address match.

Note: When the value is set to True, you will also need to enable [SharePoint and OneDrive integration with Azure AD B2B](https://learn.microsoft.com/en-us/sharepoint/sharepoint-azureb2b-integration) for the functionality to work.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ConditionalAccessPolicyErrorHelpLink

A Link for help when Conditional Access Policy blocks a user. This should be in a valid URL format. A valid URL format that begins with http:// or https://.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CustomizedExternalSharingServiceUrl

Specifies a URL that will be appended to the error message that is surfaced when a user is blocked from sharing externally by policy. This URL can be used to direct users to internal portals to request help or to inform them about your organization's policies. An example value is `https://www.contoso.com/sharingpolicies`.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeAtAGlanceInShareEmails

Enables or disables the At A Glance feature in sharing e-mails. This provides the key points and time to read for the shared item if available.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MassDeleteNotificationDisabled

Enables or disables the mass delete detection feature. When MassDeleteNotificationDisabled is set to $true, tenant admins can perform mass deletion operations without triggering notifications.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AppBypassInformationBarriers
Enables of disables applications running in app-only mode to access IB sites.

For more information about information barriers, see [Use information barriers with SharePoint](https://learn.microsoft.com/en-us/purview/information-barriers-sharepoint) for your SharePoint Online environment.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultOneDriveInformationBarrierMode
The DefaultOneDriveInformationBarrierMode sets the information barrier mode for all OneDrive sites.

The valid values are:

- Open
- Explicit
- Implicit
- OwnerModerated
- Mixed
For more information about information barriers, see [Use information barriers with SharePoint](https://learn.microsoft.com/en-us/purview/information-barriers-sharepoint) for your SharePoint Online environment.

```yaml
Type: InformationBarriersMode
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CoreSharingCapability
Sets what level of sharing is available for SharePoint sites (not including OneDrive sites).

The valid values are:

- ExternalUserAndGuestSharing (default) : External user sharing (share by email) and guest link sharing are both enabled.
- Disabled : External user sharing (share by email) and guest link sharing are both disabled.
- ExternalUserSharingOnly : External user sharing (share by email) is enabled, but guest link sharing is disabled.
- ExistingExternalUserSharingOnly : Only guests already in your organization's directory.


```yaml
Type: SharingCapabilities
Parameter Sets: (All)
Accepted values: Disabled, ExternalUserSharingOnly, ExternalUserAndGuestSharing, ExistingExternalUserSharingOnly

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BlockUserInfoVisibilityInOneDrive
Blocks users from accessing User Info if they have Limited Access permission only to the OneDrive. The policy applies to all OneDrives in the organization.

The valid values are:

ApplyToNoUsers (default) - No users are prevented from accessing User Info when they have Limited Access permission only.
ApplyToAllUsers - All users (internal or external) are prevented from accessing User Info if they have Limited Access permission only.
ApplyToGuestAndExternalUsers - Only external or guest users are prevented from accessing User Info if they have Limited Access permission only.
ApplyToInternalUsers - Only internal users are prevented from accessing User Info if they have Limited Access permission only.

```yaml
Type: TenantBrowseUserInfoPolicyValue
Parameter Sets: (All)
Accepted values: ApplyToNoUsers (default), ApplyToAllUsers, ApplyToGuestAndExternalUsers, ApplyToInternalUsers

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


### -BlockUserInfoVisibilityInSharePoint
Blocks users from accessing User Info if they have Limited Access permission only to a SharePoint site. The policy applies to all SharePoint sites in the organization.

The valid values are:

ApplyToNoUsers (default) - No users are prevented from accessing User Info when they have Limited Access permission only to a SharePoint site.

ApplyToAllUsers - All users (internal or external) are prevented from accessing User Info if they have Limited Access permission only to a SharePoint site.

ApplyToGuestAndExternalUsers - Only external or guest users are prevented from accessing User Info if they have Limited Access permission only to a SharePoint site.

ApplyToInternalUsers - Only internal users are prevented from accessing User Info if they have Limited Access permission only to a SharePoint site.

```yaml
Type: Boolean
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowOverrideForBlockUserInfoVisibility
Allow organization level policy for Block User Info Visibility to be overridden for a SharePoint site or OneDrive. 

The valid values are:

False (default) - Do not allow the Block User Info Visibility policy to be overridden for a SharePoint site or OneDrive.

True - Allow the Block User Info Visibility policy to be overridden for a SharePoint site or OneDrive.

```yaml
Type: Boolean
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowEveryoneExceptExternalUsersClaimInPrivateSite
When this parameter is true, the "Everyone except external users" claim is available in the People Picker of a private site. Set it to false to disable this feature.

The valid values are:

True - The "Everyone except external users" claim is available in People Picker of a private site.
False (default) - The "Everyone except external users" claim is not available in People Picker of a private site.

```yaml
Type: Boolean
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AIBuilderEnabled
Enables or disables AI Builder.

```yaml
Type: Boolean
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowSensitivityLabelOnRecords
Allows sensitivity label on records.

```yaml
Type: Boolean
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AnyoneLinkTrackUsers
Enables or disables tracking of users with anyone link.

```yaml
Type: Boolean
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableSiteArchive
Enables or disables site archive.

```yaml
Type: Boolean
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ESignatureEnabled
Enables or disables eSignature.

```yaml
Type: Boolean
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MarkNewFilesSensitiveByDefault
Marks new files as sensitive by default before DLP policies are applied.

```yaml
Type: SensitiveByDefaultState
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OneDriveDefaultShareLinkScope

Sets the default sharing link scope for OneDrive.

The valid values are:

- Anyone : Anyone with the link can access the content.
- Organization : Only people within the organization can access the content.
- SpecificPeople : Only specific individuals (specified by the user) can access the content.
- Uninitialized : The default value, indicating that the default share link scope is not explicitly set


```yaml
Type: SharingScope
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OneDriveDefaultShareLinkRole

Sets the default sharing link role for OneDrive.  It replaces the DefaultSharingLinkType.

Valid values are :

- None: No permissions granted.
- View: View-only permissions.
- Edit: Edit permissions.
- Review: Review permissions.
- RestrictedView: Restricted view permissions.

```yaml
Type: Role
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OneDriveDefaultLinkToExistingAccess

Sets whether OneDrive default links should grant access to existing users.  It replaces the DefaultLinkPermission.

```yaml
Type: Boolean
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OneDriveBlockGuestsAsSiteAdmin

Sets the sharing state for blocking guests as site admin in OneDrive.

Valid values are

- On
- Off
- Unspecified

```yaml
Type: SharingState
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RecycleBinRetentionPeriod

Sets the retention period for the recycle bin.

The value of Recycle Bin Retention Period must be between 14 and 93. By default it is set to 93.

```yaml
Type: Int32
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CoreDefaultShareLinkScope

Sets the default sharing link scope for SharePoint sites. It replaces the DefaultSharingLinkType.

The valid values are:

- Anyone : Anyone with the link can access the content.
- Organization : Only people within the organization can access the content.
- SpecificPeople : Only specific individuals (specified by the user) can access the content.
- Uninitialized : The default value, indicating that the default share link scope is not explicitly set

```yaml
Type: SharingScope
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CoreDefaultShareLinkRole

Sets the default sharing link role for SharePoint sites.  It replaces the DefaultLinkPermission.

Valid values are :

- None: No permissions granted.
- View: View-only permissions.
- Edit: Edit permissions.
- Review: Review permissions.
- RestrictedView: Restricted view permissions.

```yaml
Type: Role
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OneDriveSharingCapability

Determines what level of sharing is available for  OneDrive for Business.

The valid values are:

- ExternalUserAndGuestSharing (default) : External user sharing (share by email) and guest link sharing are both enabled. 
- Disabled : External user sharing (share by email) and guest link sharing are both disabled.
- ExternalUserSharingOnly : External user sharing (share by email) is enabled, but guest link sharing is disabled.
- ExistingExternalUserSharingOnly : Only guests already in your organization's directory.

For more information about sharing, see Manage external sharing for your SharePoint online environment (https://learn.microsoft.com/sharepoint/turn-external-sharing-on-or-off).

```yaml
Type: SharingCapabilities
Parameter Sets: (All)
Accepted values: Disabled, ExternalUserSharingOnly, ExternalUserAndGuestSharing, ExistingExternalUserSharingOnly

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -GuestSharingGroupAllowListInTenantByPrincipalIdentity
Sets the guest sharing group allow list in the tenant by principal identity.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SelfServiceSiteCreationDisabled
Sets whether self-service site creation is disabled.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SyncAadB2BManagementPolicy
This feature allows SharePoint Online to synchronize several Entra B2B collaboration settings [Guest user access restriction and collaboration restriction](https://learn.microsoft.com/en-us/entra/external-id/external-collaboration-settings-configure#configure-settings-in-the-portal), and store them on SharePoint Online tenant store. On sharing, SharePoint checks whether those synchronized settings are blocking sharing before sending invitation requests to Entra B2B invitation manager. The sync might take up to 24 hours to complete if you change those Entra B2B collaboration settings. To make the change effective on SharePoint Online immediately, run 'Set-PnPTenant -SyncAadB2BManagementPolicy' and it forces a sync from Microsoft Entra.


```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExtendPermissionsToUnprotectedFiles
This property can be used to turn on/off the capability called "Extended SharePoint permissions to unprotected files". To learn more about this feature check [here](https://learn.microsoft.com/en-us/purview/sensitivity-labels-sharepoint-extend-permissions)

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhoCanShareAllowListInTenant
Sets a value to handle the tenant who can share settings

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


### -LegacyBrowserAuthProtocolsEnabled
Enables or disables legacy browser authentication protocols.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableDiscoverableByOrganizationForVideos
Enables or disables the sharing dialog to include a checkbox offering the user the ability to share to a security group containing every user in the organization.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RestrictedAccessControlforSitesErrorHelpLink
Sets a custom learn more link to inform users who were denied access to a SharePoint site due to the restricted site access control policy.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Workflow2010Disabled
Sets a value to specify whether Workflow 2010 is disabled.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowSharingOutsideRestrictedAccessControlGroups
Controls whether sharing SharePoint sites and their content is allowed with users and groups who are not allowed as per the Restricted access control policy.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HideSyncButtonOnDocLib
Sets a value to specify whether the sync button on document libraries is hidden.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HideSyncButtonOnODB
Set whether to hide the sync button on OneDrive for Business sites.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StreamLaunchConfig
Sets the default destination for the Stream app launcher tile.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableMediaReactions
Controls whether media reactions are enabled.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ContentSecurityPolicyEnforcement
Controls whether content security policy is enabled.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableSpacesActivation
Enables or disables activation of spaces.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowClassicPublishingSiteCreation
This parameter allows creation of classic publishing site collections (templates `BLANKINTERNETCONTAINER#0, CMSPUBLISHING#0 and BLANKINTERNET#0`) and activation of classic publishing features in sites.

The valid values are:

- False (default) - Classic publishing site collections cannot be created and the publishing features cannot be activated in sites.
- True - Classic publishing site collections can be created and the publishing features can be activated in sites.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DelayDenyAddAndCustomizePagesEnforcementOnClassicPublishingSites
This parameter controls how SharePoint will deal with classic publishing sites (templates `BLANKINTERNETCONTAINER#0, CMSPUBLISHING#0 and BLANKINTERNET#0`) where custom scripts are allowed.

The valid values are:

- False (default) - for classic publishing site collections where administrators enabled the ability to add custom script, SharePoint will revoke that ability within 24 hours from the last time this setting was changed.
- True - All changes performed by administrators to custom script settings are preserved.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
If provided, no confirmation will be requested and the action will be performed

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
