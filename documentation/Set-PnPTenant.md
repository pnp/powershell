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

Sets organization-level site collection properties

## SYNTAX

```powershell
Set-PnPTenant [-SpecialCharactersStateInFileFolderNames <SpecialCharactersState>]
 [-MinCompatibilityLevel <Int32>] [-MaxCompatibilityLevel <Int32>] [-ExternalServicesEnabled <Boolean>]
 [-NoAccessRedirectUrl <String>] [-SharingCapability <SharingCapabilities>]
 [-DisplayStartASiteOption <Boolean>] [-StartASiteFormUrl <String>] [-ShowEveryoneClaim <Boolean>]
 [-ShowAllUsersClaim <Boolean>] [-ShowEveryoneExceptExternalUsersClaim <Boolean>]
 [-SearchResolveExactEmailOrUPN <Boolean>] [-OfficeClientADALDisabled <Boolean>]
 [-LegacyAuthProtocolsEnabled <Boolean>] [-RequireAcceptingAccountMatchInvitedAccount <Boolean>]
 [-ProvisionSharedWithEveryoneFolder <Boolean>] [-SignInAccelerationDomain <String>]
 [-EnableGuestSignInAcceleration <Boolean>] [-UsePersistentCookiesForExplorerView <Boolean>]
 [-BccExternalSharingInvitations <Boolean>] [-BccExternalSharingInvitationsList <String>]
 [-UserVoiceForFeedbackEnabled <Boolean>] [-PublicCdnEnabled <Boolean>] [-PublicCdnAllowedFileTypes <String>]
 [-RequireAnonymousLinksExpireInDays <Int32>] [-SharingAllowedDomainList <String>]
 [-SharingBlockedDomainList <String>] [-SharingDomainRestrictionMode <SharingDomainRestrictionModes>]
 [-OneDriveStorageQuota <Int64>] [-OneDriveForGuestsEnabled <Boolean>] [-IPAddressEnforcement <Boolean>]
 [-IPAddressAllowList <String>] [-IPAddressWACTokenLifetime <Int32>] [-UseFindPeopleInPeoplePicker <Boolean>]
 [-DefaultSharingLinkType <SharingLinkType>] [-ODBMembersCanShare <SharingState>]
 [-ODBAccessRequests <SharingState>] [-PreventExternalUsersFromResharing <Boolean>]
 [-ShowPeoplePickerSuggestionsForGuestUsers <Boolean>] [-FileAnonymousLinkType <AnonymousLinkType>]
 [-FolderAnonymousLinkType <AnonymousLinkType>] [-NotifyOwnersWhenItemsReshared <Boolean>]
 [-NotifyOwnersWhenInvitationsAccepted <Boolean>] [-NotificationsInOneDriveForBusinessEnabled <Boolean>]
 [-NotificationsInSharePointEnabled <Boolean>] [-OwnerAnonymousNotification <Boolean>]
 [-CommentsOnSitePagesDisabled <Boolean>] [-SocialBarOnSitePagesDisabled <Boolean>]
 [-OrphanedPersonalSitesRetentionPeriod <Int32>] [-DisallowInfectedFileDownload <Boolean>]
 [-DefaultLinkPermission <SharingPermissionType>] [-ConditionalAccessPolicy <SPOConditionalAccessPolicyType>]
 [-AllowDownloadingNonWebViewableFiles <Boolean>] [-AllowEditing <Boolean>]
 [-ApplyAppEnforcedRestrictionsToAdHocRecipients <Boolean>] [-FilePickerExternalImageSearchEnabled <Boolean>]
 [-EmailAttestationRequired <Boolean>] [-EmailAttestationReAuthDays <Int32>] [-HideDefaultThemes <Boolean>]
 [-DisabledWebPartIds <Guid[]>] [-EnableAIPIntegration <Boolean>] [-DisableCustomAppAuthentication <Boolean>] 
 [-EnableAutoNewsDigest <Boolean>] [-CommentsOnListItemsDisabled <Boolean>] [-CommentsOnFilesDisabled <Boolean>]
 [-DisableBackToClassic <Boolean>] [-InformationBarriersSuspension <Boolean>] 
 [-AllowFilesWithKeepLabelToBeDeletedODB <Boolean>] [-AllowFilesWithKeepLabelToBeDeletedSPO <Boolean>]
 [-Force] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Sets organization-level site collection properties such as StorageQuota, StorageQuotaAllocated, ResourceQuota,
ResourceQuotaAllocated, and SiteCreationMode.

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

```yaml
Type: Boolean
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

### -CommentsOnSitePagesDisabled

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
Setting this value to "none" will default "get a link" to the most permissive link available (that is, if anonymous links are enabled, the default link will be anonymous access; if they are disabled then the default link will be internal.

The values are: None Direct Internal AnonymousAccess

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
Boolean indicating if Azure Information Protection (AIP) should be enabled on the tenant. For more information, see https://docs.microsoft.com/microsoft-365/compliance/sensitivity-labels-sharepoint-onedrive-files#use-powershell-to-enable-support-for-sensitivity-labels

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
External services are defined as services that are not in the Office 365 datacenters.

The valid values are:
True (default) - External services are enabled for the tenant.
False - External services that are outside of the Office 365 datacenters cannot interact with SharePoint.

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
Defines if the default themes are visible or hidden

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

```yaml
Type: Int32
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

### -NotifyOwnersWhenItemsReshared
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

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PreventExternalUsersFromResharing

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
For additional information about the change, see Provision the Shared with Everyone folder in OneDrive for Business (https://support.office.com/en-us/article/Provision-the-Shared-with-Everyone-folder-in-OneDrive-for-Business-6bb02c91-fd0b-42ba-9457-3921cb6dc5b2?ui=en-US&rs=en-US&ad=US)

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
ExternalUserAndGuestSharing (default) - External user sharing (share by email) and guest link sharing are both enabled. Disabled - External user sharing (share by email) and guest link sharing are both disabled.
ExternalUserSharingOnly - External user sharing (share by email) is enabled, but guest link sharing is disabled.

For more information about sharing, see Manage external sharing for your SharePoint online environment (https://docs.microsoft.com/sharepoint/turn-external-sharing-on-or-off).

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
{{ Fill SpecialCharactersStateInFileFolderNames Description }}

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

### -InformationBarriersSuspension
Allows suspension of the information barriers future in a Microsoft 365 tenant. Setting this to $true will disable information barriers, setting this to $falsde will enable information barriers. For more information, see https://docs.microsoft.com/sharepoint/information-barriers.

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
Allows configuring whether personal lists created within the OneDrive for Business site of the user is enabled or disabled in the tenant. If set to `$true`, personal lists will be allowed to be created in the tenant. If set to `$false`, it will be disabled in the tenant.

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
Guids of out of the box modern liststemplates to show when creating a new list

```yaml
Type: Guid[]
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