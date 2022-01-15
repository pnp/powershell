# PnP.PowerShell Changelog
*Please do not commit changes to this file, it is maintained by the repo owner.*

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/).

## [Current Nightly]

### Added

- Added `Add\Remove\Invoke-PnPListDesign` cmdlets to add a list design, remove a list design and apply the list design.

### Changed


### Fixed

- Fixed `Set-PnPSite` not working with `DisableCompanyWideSharingLinks` parameter.
- Fixed `Get-PnPListPermissions` returing wrong information in case of broken inheritance.
- Fixed `Submit-PnPSearchQuery -Query "somequery"` yielding an error when no results [#1520](https://github.com/pnp/powershell/pull/1520)
- Fixed `Set-PnPTenantSite` not setting SharingCapability property correctly.
- Fixed `Get-PnPMicrosoft365Group` retrieving non-Unified groups when parameters are not specified.


### Removed


### Contributors

- Leon Armston [LeonArmston]
- Reshmee Auckloo [reshmee011]

## [1.9.0]

### Added
- Added `Get-PnPTenantInstance` which will return one or more tenant instances, depending if you have a multi-geo or single-geo (default) tenant.
- Added optional `-ScheduledPublishDate` parameter to `Add-PnPPage` and `Set-PnPPage` to allow for scheduling a page to be published
- Added `-RemoveScheduledPublish` to `Set-PnPPage` to allow for a page publish schedule to be removed
- Added support for off peak SharePoint Syntex content classification and extraction for lists and folders via new `-OffPeak` and `-Folder` parameters for `Request-PnPSyntexClassifyAndExtract`
- Added `Get\Set-PnPPlannerConfiguration` to allow working with the Microsoft Planner tenant configuration
- Added `Get\Set-PnPPlannerUserPolicy` to allow setting Microsoft Planner user policies for specific users
- Added `Get\Add\Remove-PnPPlannerRoster` which allows a Microsoft Planner Roster to be created, retrieved or removed
- Added `Get\Add\Remove-PnPPlannerRosterMember` to be able to read, add and remove members from a Microsft Planner Roster
- Added `Get-PnPPlannerRosterPlan` to be able to retrieve the Microsoft Planner plans inside a Microsoft Planner Roster or the ones belonging to a specific user
- Added support for off peak SharePoint Syntex content classification and extraction for lists and folders via new `-OffPeak` and `-Folder` parameters for `Request-PnPSyntexClassifyAndExtract`
- Added `Invoke-PnPSiteScript` which allows for a Site Script to be executed on a site without needing to have it registered in a site design or site script first
- Added `Copy-PnPList` which allows for a copy of a SharePoint list to be made in the same site or to another site. Copying along list item data is not yet possible but will follow in a later release.
- Added `Get\Set-PnPWebHeader` to work with the Change the look > Header options of a site
- Added `Enable-PnPPageScheduling` and `Disable-PnPPageScheduling` to enable or disable page publishing scheduling on modern pages
- Added ability to add multiple users to a Teams team in the `Add-PnPTeamsUser` cmdlet
- Added `-Credentials $cred` or `-CurrentCredentials` to be allowed to be used in combination with `Connect-PnPOnline -SPOManagementshell`
- Added `-InformationBarriersMode` in the `Set-PnPTenantSite` cmdlet which allows fine tuning of the information barriers mode per site collection
- Added `-InformationBarriersSuspension` in the `Set-PnPTenant` cmdlet which allows information barriers to be enabled or disabled in a tenant   
- Added `-Recycle` parameter to `Remove-PnPPage` to delete the page and send it to the recycle bin. This prevents permanently deleting the page and you can also restore it.
- Added `-DemoteNewsArticle` parameter to the `Set-PnPPage` cmdlet to demote an existing news post to a regular page.
- Added `-Translate` and `-TranslationLanguageCodes` parameters to `Set-PnPPage` and `Add-PnPPage`. This enables multilingual page creation in sites.
- Added `DisableSpacesActivation` state to be returned with `Get-PnPTenant`
- Added `-AllowFilesWithKeepLabelToBeDeletedSPO` and `-AllowFilesWithKeepLabelToBeDeletedODB` options to `Set-PnPTenant` which allows configuration of files on SharePoint Online and OneDrive for Business being blocked by a retention policy to be possible to be deleted anyway and then moved to the preservation hold library. The default for SharePoint Online for this will change as announced in Message Center announcement MC264360. This will allow reverting it. The current values can be retrieved using `Get-PnPTenant`.
- Added `DisableAddToOneDrive` state to be returned with `Get-PnPTenant` cmdlet.
- Added `-DisableAddToOneDrive` to `Set-PnPTenant` cmdlet to enable/disable users from adding shortcuts to OneDrive.
- Added optional `-Site` parameter to `Add-PnPContentTypesFromContenTypeHub` which allows a specific site to be specified to add the content type hub content types to
- Added `Set-PnPBuiltInSiteTemplateSettings` and `Get-PnPBuiltInSiteTemplateSettings` to allow making the built in SharePoint Online site templates visible or hidden and getting their current settings
- Added support for Channel sites (ID 69) to `Add-PnPSiteDesign`, `Set-PnPSiteDesign` and `Add-PnPSiteDesignFromWeb`
- Added optional `-IsDefault` option to `Get-PnPPowerPlatformEnvironment` which allows just the default or non default environments to be returned. If not provided, all environments will be returned as was the case before this addition.
- Added `ResourceBehaviorOptions` option in `New-PnPTeamsTeam` cmdlet to set `ResourceBehaviorOptions` while provisioning a Team
- Added alias on `Copy-PnPFile` for `Copy-PnPFolder`. It could already be used to copy a folder, but to make this more clear, and as we already had a `Copy/Move-PnPFolder` as well, the same cmdlet is now also available under its alternative cmdlet name.
- Added `IsFluidEnabled` state to be returned with `Get-PnPTenant` cmdlet.
- Added `-IsFluidEnabled` to `Set-PnPTenant` cmdlet to enable/disable users from using Fluid components.
- Added `Add\Get\Remove-PnPListItemComment` cmdlets to deal with list item comments. Using these cmdlets, you will now be able to add, retrieve and delete list item comments. [#1462](https://github.com/pnp/powershell/pull/1462)
- Added `-ResourceTypeName` and `-ResourceUrl` parameters to `Get-PnPAccessToken` to fetch access token of specified resource. [#1451](https://github.com/pnp/powershell/pull/1451)

### Changed
- Improved `Get-PnPFile` cmdlet to handle large file downloads
- Updated `Sync-PnPSharePointUserProfilesFromAzureActiveDirectory` to also allow results from `Get-PnPAzureADUser -Delta` to be provided through `-Users`.
- A clearer error message will now be returned when using `Add-PnPListItem -List` and specifying an invalid list name.
- Response of `Add-PnPContentTypesFromContenTypeHub` is now returned in the root of the response as well as under Value as it was previously for backwards compatibility.
- Improved synopsis documentation for `Update-PnPUserType` cmdlet.
- Improved documentation of `Add-PnPField`, reflects the missing `-AddToAllContentTypes` parameter.
- Improved documentation of `Get-PnPTaxonomyItem` with addition of new example and removing obsolete parameters.
- Improved documentation of `Get-PnPTerm`, fixed typos.
- Improved `Add-PnPHubToHubAssociation`. It will now throw error if both, source and destination, sites are not Hub sites, currently it fails silently without any information to the user. [#1390](https://github.com/pnp/powershell/pull/1390)
     
### Fixed
- Fixed `Get-PnPGroupMember -User` not properly returning the specified user
- Fixed group member retrieval through `Get-PnPAzureADGroupOwner` and `Get-PnPAzureAdGroupMember` throwing an exception when a security group has been placed in the Azure Active Directory group being queried
- Fixed an issue where `Set-PnPPage` would not be able to find a page if you would start the `-Identity` with a forward slash
- Fixed an issue where `Set-PnPPage` would not return its parent Folder
- Fixed `Set-PnPListItem` not working when using `Label` and `Values` parameters together
- Fixed documentation for `Get-PnPFlow` and `Enable-PnPFlow` cmdlets
- Fixed issue with `Add-PnPListFoldersToProvisioningTemplate` not working when having nested folder structure   
- Fixed documentation for `Get-PnPFlow` and `Enable-PnPFlow` cmdlets
- Fixed `Sync-PnPSharePointUserProfilesFromAzureActiveDirectory` not being able to deal with multi value properties on the Azure Active Directory side, such as `BusinessPhones`
- Fixed `Add-PnPListItem` issue with setting MultiChoice columns when using `-Batch` parameter
- Fixed issue with `Remove-PnPListItem` when trying to use it with `Batch` parameter
- Fixed `Add-PnPDataRowsToSiteTemplate` not exporting TaxonomyFieldValues properly
- Fixed `Add/Set-PnPListItem` issue with managed metadata / taxonomy field value failing in a batched request.
- Fixed `Set-PnPListItem` issue with setting `Modified` date value properly when using `-Batch` parameter.
- Fixed `Get-PnPTeamsTeam -Identity` throwing an exception if the name of the team would contain special characters
- Fixed `Get-PnPTerm` throwing an exception when used in combination with `-Includes` [#1384](https://github.com/pnp/powershell/pull/1384)
- Fixed `Get-PnPDiagnostics` throwing an unable to cast exception under some circumstances [#1380](https://github.com/pnp/powershell/pull/1380)
- Fixed `Get-PnPTeamsTab` issue with missing TeamsApp object values. It will now populate TeamsApp object with Id, DisplayName, ExternalId and DistributionMethod properties if available. [#1459](https://github.com/pnp/powershell/pull/1459)

### Removed
- Removed `Add-PnPClientSidePage` as that was marked deprecated. Use `Add-PnPPage` instead.
- Removed `Get-PnPSubWebs` as that was marked deprecated a year ago. Use `Get-PnPSubWeb` instead. [#1394](https://github.com/pnp/powershell/pull/1394)

### Contributors
- Koen Zomers [koenzomers]
- Bert Jansen [jansenbe]
- Gautam Sheth [gautamdsheth]
- [reusto]
- Asad Refai [asadrefai]
- Daniel Huber [daniel0611]
- Bart-Jan Dekker [bjdekker]
- Giacomo Pozzoni [jackpoz]
- Chris Kent [thechriskent]
- Filip Bosmans [FilipBosmans]
- [zylantha]
- Justin [pagejustin]
- Collin Argo [SCollinA]
- Leon Armston [LeonArmston]
- Lars Höög [h00g]
- [kachihro]
- [Andy-Dawson]
- David Aeschlimann [TashunkoWitko]
- [outorted]
- [dkardokas]
- Asad Refai [asadrefai]

## [1.8.0]

### Added
- Added flexibility to mix and pipe `Add\Get\Remove-PnPListItem` with `Get-PnPList`
- Added ability to remove all list items from a list using `Remove-PnPListItem -List <listname>` and not providing a list item identifier.
- Added `Get-PnPMessageCenterAnnouncent`, `Get-PnPServiceCurrentHealth` and `Get-PnPServiceHealthIssue` cmdlets which pull their data out of the Microsoft Graph API and are replacing the former `Get-PnPOffice365CurrentServiceStatus`, `Get-PnPOffice365HistoricalServiceStatus` and `Get-PnPoffice365ServiceMessage` cmdlets which pull their data from the Office Health and Communications API which is to be deprecated on December 17, 2021. If you're using any of these last three cmdlets, please rewrite your functionality to start using one of the first three cmdlets before this date.
- Added option which allows new SharePoint 2013 Workflow creation to be disabled tenant wide by using `Set-PnPTenant -StopNew2013Workflows` and requesting its current setting using `Get-PnPTenant | Select StopNew2013Workflows`
- Added lots of extra information getting returned when using `Get-PnPFlow`.
- Added option which allows the Explorer View for Microsoft Edge to be enabled tenant wide by using `Set-PnPTenant -ViewInFileExplorerEnabled` and requesting its current setting using `Get-PnPTenant | Select ViewInFileExplorerEnabled`. It can be that this feature is not enabled on your tenant yet, in which case it will return an error. Try it again later in that case.
- Added lots of extra information getting returned when using `Get-PnPPowerPlatformEnvironment`
- Added the option to use `-Verbose` with `Export-PnPFlow` so it wil show details on why an export failed when it is not possible to export the flow.
- Added option to add/list/remove event receivers from the site scope using `Add-PnPEventReceiver -Scope <Site/Web>`, `Get-PnPEventReceiver -Scope <All/Site/Web>` and `Remove-PnPEventReceiver -Scope <All/Site/Web>`
- Added `-Url` parameter to `New-PnPUPABulkImportJob` which allows providing a URL to an existing SharePoint User Profile import mapping instruction file stored on SharePoint Online
- Added `Add-PnPSiteDesignFromWeb` which combines `Get-PnPSiteScriptFromWeb`, `Add-PnPSiteScript` and `Add-PnPSiteDesign` into one cmdlet to allow for a specific site to directly be added as a site design to allow other sites to be configured similarly
- Added `Update-PnPSiteDesignFromWeb` which combines `Get-PnPSiteScriptFromWeb` and `Set-PnPSiteScript` into one cmdlet to allow for a specific site design to directly be updated based on an existing site which can function as a template
- Added `Sync-PnPSharePointUserProfilesFromAzureActiveDirectory` cmdlet which allows direct synchronization of user profile properties of choice between user profiles in Azure Active Directory and their SharePoint Online User Profile Service user profile equivallents
-   
### Changed
- Renamed `Get-PnPFlowEnvironment` to `Get-PnPPowerAutomateEnvironment`
- Changed `Get-PnPSiteScriptFromWeb` to get a site script of the currently connected to site if `-Url` is omitted.
- Improved `Find-PnPFile` error message
- `Get-PnPFileVersion` cmdlet documentation improved with additional example.
- `Add-PnPNavigationNode` cmdlet documentation improved with additional example feature which shows how to add a navigation node as a label.
- Changed `Get-PnPSiteDesign` and `Invoke-PnPSiteDesign` to when providing a name through `-Identity` to be able to work with all site designs having that same name instead of just the first one
- Changed `Set-PnPListItemPermission` to support piping in a roledefinition for `-AddRole` and `-RemoveRole`
- Changed that `Get-PnPSiteScript -Identity` now also works with the site script name instead of just the site script Id

### Fixed
- Fixed `Get-PnPChangeLog -Nightly` not returning anything
- Fixed issue with `Get-PnPUser -Identity x` ignoring additional requested attributes using `-Includes`
- Fixed issue with `Set-PnPDefaultColumnValues -List "Documents" -Folder "Földer" -Field "Text" -Value "123"` not working when having a folder name with special characters in it.
- Fixed `Get-PnPException` throwing an exception and not showing the last exception if the last cmdlet throwing an exception used `-ErrorAction Stop`
- Fixed `Get-PnPException -All` throwing an exception.
- Fixed an issue with `Set-PnPSite -Identity <url> -Owner <upn>` not working if the URL would be a OneDrive for Business site.
- Fixed an issue with `Get-PnPSiteScriptFromWeb` requiring an Include parameter next to providing lists and fixed specifying lists through List\ListName not working.
- Fixed issue with 'Remove-PnPSiteDesign -Identity` not accepting a site design name, only a GUID.
- Fixed unable to piping the output of `Get-PnPRoleDefinition` to i.e. filter by RoleTypeKind.
- Fixed an issue with several PnP PowerShell cmdlets such as `Get-PnPTeamsUser` where not all results would be returned
- Fixed issue with `Remove-PnPSiteDesign -Identity` not accepting a site design name, only a GUID.
- Fixed issue with `Get-PnPUPABulkImportStatus` where it did not allow you to pipe its output to i.e. get the most recent one using `Select -Latest 1` or the ones that failed using `? State -ne "Succeeded"`  
    
### Removed
- Removed `ConvertTo-PnPClientSidePage` cmdlet as it has been replaced by `ConvertTo-PnPPage`
this option is not enabled yet on your tenant in which case trying to set it results in to `Set-PnPTenant: The requested operation is part of an experimental feature that is not supported in the current environment.`. In that case try again later.
- Removed `Add-PnPUserToGroup` as it has been replaced by `Add-PnPGroupMember`
- Removed `Get-PnPGroupMembers` cmdlet alias and related warning. The cmdlet `Get-PnPGroupMember` (singular) is available.
- Removed `Remove-PnPUserFromGroup` cmdlet alias and related warning. The cmdlet `Remove-PnPGroupMember` is available.
- Removed `Initialize-PnPPowerShellAuthentication` cmdlet alias and related warning. The cmdlet `Register-PnPAzureADApp` is the replacement.

### Contributors

- Koen Zomers [koenzomers]
- Yuriy Samorodov [YuriySamorodov]
- Asad Refai [asadrefai]
- James Eccles [jameseccles]
- Giacomo Pozzoni [jackpoz]
- Todd Klindt [ToddKlindt]
- Rolands Strakis [wonderplayer]
- Bhishma Bhandari [bhishma]
- [reusto]
- [4ndri]
- [WimVandierendonck]

## [1.7.0]

### Changed

- Updated CSOM release
- Fixes issue with Get-PnPTenantSite

## [1.6.0]

### Added

### Changed

- Get-PnPPage now can load pages living in a folder by specifying "folder/page.aspx"
- Added `-DisableBackToClassic` option to Set-PnPTenant

### Contributors
- [thomassmart]
- Bert Jansen [jansenbe]  

## [1.5.0]

### Added

- Added `Request-PnPSyntexClassifyAndExtract` cmdlet to request classification and extraction of a file or all files in a list
- Added `Get-PnPSyntexModel` cmdlet to list the defined SharePoint Syntex models in a SharePoint Syntex content center site
- Added `Publish-PnPSyntexModel` cmdlet to publish a SharePoint Syntex model to a library
- Added `Unpublish-PnPSyntexModel` cmdlet to unpublish a SharePoint Syntex model from a library
- Added `Get-PnPSyntexModelPublication` cmdlet to list the libraries to which a SharePoint Syntex model was published

### Changed

### Contributors
- Bert Jansen [jansenbe]
- Koen Zomers [koenzomers]
- Gautam Sheth [gautamdsheth]
- Veronique Lengelle [veronicageek]
  
## [1.4.0]

### Added
- Added `-IncludeOwners` to `Get-PnPMicrosoft365Group`.
- Added `-AssignedTo` to `Add-PnPPlannerTask` and `Set-PnPPlannerTask` allowing you to assign users to a task.
- Added `Get-PnPAzureADApp`, `Get-PnPAzureADAppPermission` and `Remove-PnPAzureADApp` to manage Azure AD apps.
- Added All Graph permissions and all SharePoint permissions for selection to `Register-PnPAzureADApp`.
- Added `-Template` parameter to New-PnPTeamsTeam to create teams with EDU templates (your tenant needs an EDU license)
- Added fixes for authentication to GCC, GCC High and GCC DoD environments using certificate or interactive login.
- Added `Grant-PnPAzureADAppSitePermission`, `Get-PnPAzureADAppSitePermission`, `Set-PnPAzureADAppSitePermission` and `Revoke-PnPAzureADAppSitePermission`
- Added `-SkipHiddenWebParts` parameter to the `ConvertTo-PnPPage` cmdlet that allows to skip hidden webparts during page transformation

### Changed
- Improved batching speed when creating or updating multiple items that set similar values for taxonomy fields.
- Changed `Register-PnPAzureADApp` registration to by default turn on the ability to authenticate with credentials for a newly created Azure App registration (`allowPublicClient: true`).
- Refactored `Register-PnPAzureADApp`. Marked `-Scopes` as obsolete and introduced `-GraphApplicationPermissions`, `-GraphDelegatePermissions`, `-SharePointApplicationPermissions` and `-SharePointDelegatePermissions`. Added additional permission scopes.
- Re-enabled Console Logging with Set-PnPTraceLog -On
- Fixed warning showing to use -Interactive instead of -UseWebLogin to show correct url.
- Documentation updates

### Contributors
- Mahesh Chintha [chinthamahesh]
- John Bontjer [JohnBontjer]
- Todd Klindt [ToddKlindt]
- Koen Zomers [koenzomers]
- Veronique Lengelle [veronicageek]
- Mike Jensen [michael-jensen]
- Leon Armston [leonarmston]
- Ganesh Sanap [ganesh-sanap]
- vin-ol [vin-ol]
- Bert Jansen [jansenbe]

## [1.3.0]

### Added
- Added `-HideTitleInHeader` parameter to `Set-PnPWeb` to hide or show the title in the header. Use `-HideTitleInHeader` to hide it and `-HideTitleInHeader:$false` to show it.
- Added `-ShowContentUrl` parameter to `Register-PnPManagementShellAccess` retrieve the url to consent to the PnP Management Shell application by an administrator.
- Added `-IsFavoriteByDefault` parameter on Set-PnPTeamsChannel and Add-PnPTeamsChannel
- Added `-GroupIdDefined` boolean parameter to Get-PnPTenantSite to allow filtering on sites which belong to a Microsoft 365 Group
- Added `-Interactive` login option to `Connect-PnPOnline` which is similar to `-UseWebLogin` but without the limitations of the latter. The `-UseWebLogin` is using cookie based authentication towards SharePoint and cannot access Graph tokens. Using `-Interactive` we use Azure AD Authentication and as a result we are able to acquire Graph tokens.

### Changed
- Fixed certificate clean up issue on Windows platform when using `Connect-PnPOnline` with a certificate.
- Fixed issues with `Register-PnPAzureADApp` when using the various auth options (-DeviceLogin / -Interactive)
- Renamed the `-PnPManagementShell` option to `-DeviceLogin` on `Connect-PnPOnline`. `-PnPManagementShell` is still available as an alias.
- Added `-ClientId` option to `-DeviceLogin` allowing device code authentication with custom app registrations.
- Changed `-Url` parameter on Get-PnPTenantSite and Set-PnPTenantSite to `-Identity`. Made `-Url` available as an alias.
- Updated `Set-PnPTenantSite` to support same parameters as `Set-SPOSite`
- Updated `Get-PnPTenantSite` to return same properties as `Get-SPOSite`
- Fixed issue where `-Interactive` on `Connect-PnPOnline` would prompt for credentials when connecting to new site within same tenant. Added -ForceLogin parameter to force
- Get-PnPUser and any other cmdlet that takes a UserPipeBind parameter as input now allows users to be specified by name besides loginname or id.
- Fixed issue where retrieving a single site with Get-PnPTenantSite vs retrieving all sites showed different properties.
- Invoke-PnPSPRestMethod now returns usable objects
- Updated `Set-PnPListItem` to have an `UpdateType` parameter. Obsoleted `SystemUpdate`. Also updated the backend logic so can now also specify `UpdateOverwriteVersion` to update the editor, author, modified and created fields. 
- `Register-PnPAzureADApp` now outputs the base64 encoded version of the certificate which can be used with `Connect-PnPOnline -ClientId -CertificateBase64Encoded`
- Fixed issue with moving and copying files to subfolder, Issue #165. 
- fixed issue where Get-PnPTenantSite was not returning all properties correct, Issue #151
- Added `-Interactive` login option to Register-PnPManagementApp which allows for an interactive authentication flow not using device login for environments that require Multi-Factor Authentication.
- Updated all Microsoft365Group cmdlets to only load the SiteUrl of the underlying Microsoft 365 Group where required. This means that `Get-PnPMicrosoft365Group -Identity` will not by default load the site url. Specify `-IncludeSiteUrl` to include it.

### Contributors
- Mike Jensen [michael-jensen]
- Koen Zomers [koenzomers]
- Gautam Sheth [gautamdsheth]
- Todd Klindt [ToddKlindt]
- Giacomo Pozzoni [jackpoz]


## [1.2.0]

### Added
- Added `-NoWait` switch to `Copy-PnPFile` and `Move-PnPFile`
- Added `Receive-PnPCopyMoveJobStatus` cmdlet which works in combination with the `-NoWait` parameter on `Copy-PnPFile` and `Move-PnPFile`. See the documentation for usage.

### Changed
- Fixed issue with `Invoke-PnPSPRestMethod` to throw an non authorized exception in certain scenarios.
- Fixed issue with using `-UseWebLogin` and site names longer than the length of the managed path it resides in.
- Fixed issue with tenant admin site detection on environment with vanity domains
- Fixed issues with `Copy-PnPFile` and `Move-PnPFile`
- Updated `Get-PnPTenantSite` to support `-DisableSharingForNonOwnersStatus`

## [1.1.3-nightly]

### Added
- Added `-NoWait` switch to `Copy-PnPFile` and `Move-PnPFile`
- Added `Receive-PnPCopyMoveJobStatus` cmdlet which works in combination with the `-NoWait` parameter on `Copy-PnPFile` and `Move-PnPFile`. See the documentation for usage.

### Changed
- Fixed issue with `Invoke-PnPSPRestMethod` to throw an non authorized exception in certain scenarios.
- Fixed issue with using `-UseWebLogin` and site names longer than the length of the managed path it resides in.

## [1.1.2-nightly]

### Changed

- Fixed issue with tenant admin site detection on environment with vanity domains

## [1.1.1-nightly]

- Fixed issues with `Copy-PnPFile` and `Move-PnPFile`
- Updated `Get-PnPTenantSite` to support `-DisableSharingForNonOwnersStatus`

## [1.1.0]

First released version of PnP PowerShell


## [0.3.40-nightly]

### Added
- Added `Get-PnPFlow`, `Get-PnPFlowEnvironment`, `Enable-PnPFlow`, `Disable-PnPFlow`, `Remove-PnPFlow`, `Export-PnPFlow` cmdlets

### Changed
- Documentation updates

### Contributors
- Yannick Reekmans [YannickRe]


## [0.3.38-nightly]

### Added
- Reintroduced `-CertificateBase64Encoded` on `Connect-PnPOnline`

### Changed

### Contributors


## [0.3.37-nightly]

### Added

### Changed
- Reorganized Connect-PnPOnline and simplified/cleared up usage. See https://pnp.github.io/powershell/cmdlets/connect-pnponline.html and https://pnp.github.io/powershell/articles/connecting.html for more information.
- Reorganized internals with regards to access token handling.

### Contributors


## [0.3.36-nightly]

### Added

### Changed
- Fixed issue with `Set-PnPGroupPermissions` not removing roles from list correctly.

### Contributors
- Leon Armston [leonarmston]
- Koen Zomers [koenzomers]

## [0.3.34-nightly]

### Added

### Changed
- Updated certificate handling for `Register-PnPAzureADApp` and `New-PnPAzureCertificate`
- Updated `Register-PnPAzureApp` to use popup windows on Microsoft Windows. Added the `-NoPopup` switch to disable this behavior.
- Updated `Invoke-PnPBatch` to fully execute a batch by default if one of the requests in the large batch throws an exception. Specify the `-StopOnException` switch to immmediately stop after an exception occurs. The rest of the batch will be skipped where possible. See https://pnp.github.io/powershell/articles/batching for more information.
- Documentation updates

### Contributors
- Leon Armston [leonarmston]

## [0.3.33-nightly]

### Added
- Added -ClientSideHostProperties to `Set-PnPApplicationCustomizer`
- Documentation updates for Teams cmdlets

### Changed

### Contributors
- Leon Armston [leonarmston]


## [0.3.32-nightly]

### Added
- Added batching support to `Remove-PnPListItem`

### Changed

### Contributors


## [0.3.31-nightly]

### Added
- Added initial batching support. See `New-PnPBatch`, `Invoke-PnPBatch`, `Add-PnPListItem` and `Set-PnPListItem`
- Updated documentation

### Changed
- Deprecated the use of the `-Web` cmdlet parameters due to API reasons. Use `Connect-PnPOnline -Url [fullsubweburl]` instead to connect to a subweb. 
- Updated `Get-PnPLabel` to allow returning available compliance tags for a site
- Updated several cmdlets to use the Code SDK behind the scenes

### Contributors
- Veronique Lengelle [veronicageek]
- Leon Armston [leonarmston]

## [0.3.27-nightly]

### Added
- Added `Get-PnPListPermissions` cmdlet.

### Changed
- Fixed issue where using `Connect-PnPOnline` in a loop would through after several iterations an exception message from MSAL not being able to retrieve a token due to a looped request. We fixed this by trying to reuse the in-memory token cache in scenarios where non-interactive logins are being used.
- `Connect-PnPOnline -Url [url] -PnPManagementShell -LaunchBrowser` will not try to attempt to close the popup window automatically anymore.
- `Set-PnPLabel` will now check first if a label is available.
- Documentation fixes

### Contributors
- Leon Armston [leonarmston]
- Bhishma Bhandari [bhishma]

## [0.3.24-nightly]

### Added

### Changed

- Renamed `Add-PnPUserToGroup` to `Add-PnPGroupMember`. Alias for the old cmdlet name is available.
- Renamed `Remove-PnPUserFromGroup` to `Remove-PnPGroupMember`. Alias for the old cmdlet name is available.
- Renamed `Get-PnPGroupMembers` to `Get-PnPGroupMember`. Alias for the old cmdlet name is available.

## [0.3.22-nightly]

### Added

### Changed
- Fixed issue when using `Connect-PnPOnline` using either `-UseWebLogin` or `-SPOManagementShell` and invoking a site template containing modern pages, or when trying to create or update modern pages using the PnP Cmdlets.

## [0.3.20-nightly]

### Added

### Changed
- `Register-PnPManagementShellAccess` will not automatically close the window after consent anymore.
- `Connect-PnPOnline -UseWebLogin` now allows you to return the connection with `-ReturnConnection` [PR #71](https://github.com/pnp/powershell/71)
- `Remove-PnPTermGroup` now includes a `-Force` parameter [PR #70](https://github.com/pnp/powershell/pull/70)
- `Get-PnPListItem` now can filter on both the GUID or the UniqueId value by specifying the -UniqueId parameter. [PR #68](https://github.com/pnp/powershell/pull/68)

### Contributors
- Gautam Sheth [gautamdsheth]

## [0.3.*-nightly]

### Added
- Added `-ReadSecurity` and `-WriteSecurity` to `Set-PnPList` cmdlet (0.3.15)

### Changed
- Renamed `Add-PnPClientSidePage` to `Add-PnPPage` (0.3.15)
- Renamed `Add-PnPClientSidePageSection` to `Add-PnPPageSection` (0.3.15)
- Renamed `Add-PnPClientSideText` to `Add-PnPPageTextPart` (0.3.15)
- Renamed `Add-PnPClientSideWeb` to `Add-PnPPageWebPart` (0.3.15)
- Renamed `Export-PnPClientSidePage` to `Export-PnPPage` (0.3.15)
- Renamed `Export-PnPClientSidePageMapping` to `Export-PnPPageMapping` (0.3.15)
- Deprecated `Get-AvailableClientSidePageComponents`. Use `Get-PnPPageComponents -Page <yourpage> -ListAvailable` (0.3.15)
- Renamed `Get-PnPClientSidePageComponents` to `Get-PnPPageComponents` (0.3.15)
- Renamed `Get-PnPClientSidePage` to `Get-PnPPage` (0.3.15)
- Renamed `Move-PnPClientSidePageComponent` to `Move-PnPPageComponent` (0.3.15)
- Renamed `Remove-PnPClientSidePage` to `Remove-PnPPage` (0.3.15)
- Renamed `Remove-PnPClientSideComponent` to `Remove-PnPPageComponent` (0.3.15)
- Renamed `Save-PnPClientSidePageConversionLog` to `Save-PnPPageConversionLog` (0.3.15)
- Renamed `Set-PnPClientSidePage` to `Set-PnPPage` (0.3.15)
- Renamed `Set-PnPClientSideText` to `Set-PnPPageTextPart` (0.3.15)
- Renamed `Set-PnPClientSideWebPart` to `Set-PnPPageWebPart` (0.3.15)
- Removed '-Url' parameter from `Set-PnPWebPermission`. Use `-Identity` instead. (0.3.13)
- Renamed `Get-PnPSubWebs` to `Get-PnPSubWeb` which is in line with PowerShell naming standards which state that cmdlets should use a singalar noun. Alias for `Get-PnPSubWebs` is available. (0.3.13)
- `Register-PnPManagementShellAccess` now uses a popup window to authenticate you when your run the cmdlet on Windows (0.3.10)
- Breaking change: we changed `Grant-PnPTenantServicePrincipalPermission` and `Revoke-PnPTenantServicePrincipalPermission` to use the Graph behind the scenes. This is a breaking change when it comes to the required permissions, but the new approach is more future proof. (0.3.8)
- Refactored internal code to use hardcoded PnP prefixes for cmdlets, which allows $PSDefaultParameterValues in PowerShell to work as expected. (0.3.7)
- Added `-HeaderLayoutType` parameter to `Set-PnPClientSidePage` (0.3.6)
- Fixed documentation for `Clear-PnPRecycleBinItem` (0.3.6)
- Fixed issue with `Invoke-PnPTenantTemplate` not being able to acquire correct access token (0.3.6)
- Added `GrouplessTeamSite` option to `-WebTemplate` parameter for `Add-PnPSiteDesign` and `Set-PnPSiteDesign` (0.3.6)

### Contributors
- Gautam Sheth [gautamdsheth]
- Todd Klindt [toddklindt]
- Michael Jensen [michael-jensen]
- Frank Potrafky [FPotrafky]
- Veronique Lengelle [veronicageek]
- James May [fowl2]

## [0.2.*-nightly]

### Added
- Added `Convert-PnPSiteTemplateToMarkdown` to convert an existing XML based template to a markdown report. Notice that this is work in progress and the functionality will increase. See also the 'Changed' section below for information about `Get-PnPSiteTemplate` (0.3.5)
- Added `-UseWeblogin` and `-ForceAuthentication` to `Connect-PnPOnline` to allow using Multi-Factor Authentication. Notice this uses cookie based authentication, which is limited in its functionality. Using -UseWebLogin we will for instance not be able to acquire an access token for the Graph, and as a result none of the Graph related cmdlets will work. Also some of the functionality of the provisioning engine (`Get-PnPSiteTemplate`, `Get-PnPTenantTemplate`, `Invoke-PnPSiteTemplate`, `Invoke-PnPTenantTemplate`) will not work because of this reason. The cookies will in general expire within a few days and if you use `-UseWebLogin` within that time popup window will appear that will dissappear immediately, this is expected. Use `-ForceAuthentication` to reset the authentication cookies and force a new login. (0.2.25)
- Allowed to specify -ClientId when logging in with credentials so people can use their own Azure AD apps and credentials for authentication towards SharePoint Online (0.2.17)
- Added environment variable check (set `PNPPOWERSHELL_UPDATECHECK` to `Off`) to Connect-PnPOnline to skip version check if set. (0.2.15)
- Added `Get-PnPChangeLog` cmdlet which returns this changelog. (0.2.14)
- Added `-DeviceLogin` parameter to `Register-PnPAzureADApp` (renamed from Initialize-PnPPowerShellAuthentication in 0.2.15) which allows for users with MFA to setup a custom app (0.2.12)
- Added `NoVersionCheck` optional flag to `Connect-PnPOnline` [PR#28](https://github.com/pnp/powershell/pull/28) (0.2.9)
- Added native support for Secret Management Modules (0.1.34)
- Marked -ExcludeSiteUrl as obselete on `Get-PnPMicrosoft365Group` for performance reasons. Use -IncludeSiteUrl instead.
- Added -CloudShell switch to `Connect-PnPOnline` which can be used in the Azure Cloud Shell. If specified you will automatically authenticate using the current identity you're logged in with to the Azure Cloud Shell. Notice: only Graph based cmdlets (Teams, Microsoft Groups etc.) will function. For SharePoint connectivity use one of the other connection options with `Connect-PnPOnline`.
- Added -DisableCustomAppAuthentication to `Set-PnPTenant` and added support for DisableCustomAppAuthentication in `Get-PnPTenant`.

## [0.1.*-nightly]

### Added
- Added `Add-PnPHubToHubAssociation` cmdlet.
- Added `Export-PnPUserInfo` cmdlet.
- Added `Add-PnPSiteScriptPackage` cmdlet.
- Added `Export-PnPUserProfile` cmdlet (0.1.6).
- Added `Get-PnPAppErrors` cmdlet (0.1.6)
- Added `Get-PnPAppInfo` cmdlet (0.1.6)
- Added `Get-PnPBrowserIdleSignOut` cmdlet (0.1.6)
- Added `Set-PnPBrowserIdleSignOut` cmdlet (0.1.6)
- Added `Get-PnPBuiltInDesignPackageVisibility` cmdlet (0.1.6)
- Added `Set-PnPBuiltInDesignPackageVisibility` cmdlet (0.1.7)
- Added `Get-PnPExternalUser` cmdlet (0.1.7)
- Added `Remove-PnPExternalUser` cmdlet (0.1.7)
- Added `Get-PnPSiteCollectionAppCatalogs` cmdlet (0.1.7)
- Added `Request-PnPPersonalSite` cmdlet (0.1.7)
- Added `Get-PnPSiteGroup` cmdlet (0.1.8)
- Added `Get-PnPSiteUserInvitations` cmdlet (0.1.8)
- Added `Invoke-PnPSiteSwap` cmdlet (0.1.8)
- Added `New-PnPSdnProvider` cmdlet (0.1.8)
- Added `Remove-PnPSdnProvider` cmdlet (0.1.8)
- Added `Remove-PnPHubToHubAssocation` cmdlet (0.1.9)
- Added `Remove-PnPSiteUserInvitations` cmdlet (0.1.9)
- Added `Remove-PnPTenantSyncClientRestriction` cmdlet (0.1.9)
- Added `Remove-PnPUserInfo` cmdlet (0.1.9)
- Added `Remove-PnPUserProfile` cmdlet (0.1.9)
- Added `Repair-PnPSite` cmdlet (0.1.9)
- Added `Test-PnPSite` cmdlet (0.1.9)
- Added `Revoke-PnPUserSession` cmdlet (0.1.9)
- Added `New-PnPSiteGroup` cmdlet (0.1.9)
- Added `Remove-PnPSiteGroup` cmdlet (0.1.9)
- Added `Set-PnPSiteGroup` cmdlet (0.1.9)
- Added `Set-PnPSiteScriptPackage` cmdlet (0.1.9)
- Added `Update-PnPUserType` cmdlet (0.1.9)
- Added `Get-SPOStructuralNavigationCacheSiteState` cmdlet (0.1.10)
- Added `Get-SPOStructuralNavigationCacheWebState` cmdlet (0.1.10)
- Added `Set-SPOStructuralNavigationCacheSiteState` cmdlet (0.1.10)
- Added `Set-SPOStructuralNavigationCacheWebState` cmdlet (0.1.10)
- Added `Add-PnPTermToTerm` cmdlet (0.1.11)
- Added `Get-PnPTermLabel` cmdlet (0.1.11)
- Added `Remove-PnPTermLabel` cmdlet (0.1.11)
- Added `Remove-PnPTerm` cmdlet (0.1.11)
- Added `Set-PnPTerm` cmdlet (0.1.11)
- Added `Add-PnPPlannerBucket` cmdlet (0.1.16)
- Added `Add-PnPPlannerTask` cmdlet (0.1.16)
- Added `Get-PnPPlannerBucket` cmdlet (0.1.16)
- Added `Get-PnPPlannerPlan` cmdlet (0.1.16)
- Added `Get-PnPPlannerTask` cmdlet (0.1.16)
- Added `New-PnPPlannerPlan` cmdlet (0.1.16)
- Added `Set-PnPPlannerBucket` cmdlet (0.1.16)
- Added `Set-PnPPlannerPlan` cmdlet (0.1.16)
- Added `Remove-PnPPlannerBucket` cmdlet (0.1.17)
- Added `Remove-PnPPlannerPlan` cmdlet (0.1.17)
- Added `Remove-PnPPlannerTask` cmdlet (0.1.17)
- Added `Set-PnPPlannerTask` cmdlet (0.1.18)

### Changed
- Added filename support for .md file with `Get-PnPSiteTemplate` to generate a markdown file. e.g. you can now execute for instance `Get-PnPSiteTemplate -Out .\myreport.md -Handlers Lists,ContentTypes,Features,WebSettings` to generate an markdown report of those artifacts. This is work in progress.
- Fixed issue with `-UseWebLogin` throws a 403 error when connecting to a different site collection than the root site collection.
- Removed `Enable-PnPPowerShellTelemetry` and `Disable-PnPPowerShellTelemetry`. See [Configure PnP PowerShell](https://pnp.github.io/powershell) for more information on how to enable or disable telemetry collection (0.2.22)
- Obsoleted `-NoTelemetry` and `-NoVersionCheck` on `Connect-PnPOnline`. You can set these as environment variables now. See [Configure PnP PowerShell](https://pnp.github.io/powershell) for more information (0.2.22)
- Updated telemetry handling. If the environment variable `PNPPOWERSHELL_DISABLETELEMETRY` is set to `true`, no telemetry will be recorded. If the .pnppowershelltelemetry file is present in the home folder of the user then the the contents of this file will override the environment variable settings. See `Enable-PnPPowerShellTelemetry` and `Disable-PnPPowerShellTelemetry` to manage this file. (0.2.21)
- Renamed `-CloudShell` to `-ManagedIdentity` on `Connect-PnPOnline` (0.2.20)
- `-CertificatePath` on `Connect-PnPOnline` now accepts a relative path (0.2.19)
- Reintroduced `-SPOManagementShell` as login option with `Connect-PnPOnline` (0.2.18)
- Fixed issue where Connect-PnPOnline -Url [url] -AccessToken [token] is not creating an client context. (0.2.17)
- Renamed `Initialize-PnPPowerShellAuthentication` to `Register-PnPAzureADApp` (0.2.15)
- Updated `Get-PnPAzureCertificate` to work on Windows and Non-Windows OSes. (0.2.15)
- Updated `Get-PnPAzureCertificate` to use `-Path` and `-Password` parameters instead of `-CertificatePath` and `-CertificatePassword` parameters (0.2.15)
- Fixed using `New-PnPAzureCertificate` and `Get-PnPAzureCertificate` throwing an exception [PR #30](https://github.com/pnp/powershell/pull/30) (0.2.15)
- Updated `Initialize-PnPPowerShellAuthentication` so it can generate self-signed certs on other platforms than Windows.
- Updated `Get-PnPUnifiedAuditLog` to support paged results.
- Removed SiteUrl parameter from `Register-PnPManagementShell` as it is not required anymore
- Fixed documentation on Add-PnPTeamsChannel [PR#9](https://github.com/pnp/powershell/pull/9)
- Fixed documentation on Remove-PnPTeamsUser [PR#10](https://github.com/pnp/powershell/pull/10)
- Renamed `Apply-PnPProvisioningTemplate` to `Invoke-PnPSiteTemplate`.
- Renamed `Get-PnPProvisioningTemplate` to `Get-PnPSiteTemplate`.
- Renamed `Add-PnPDataRowsToProvisioningTemplate` to `Add-PnPDataRowsToSiteTemplate`.
- Renamed `Add-PnPFileToProvisioningTemplate` to `Add-PnPFileToSiteTemplate`.
- Renamed `Add-PnPListFoldersToProvisioningTemplate` to `Add-PnPListFoldersToSiteTemplate`.
- Renamed `Convert-PnPFolderToProvisioningTemplate` to `Convert-PnPFolderToSiteTemplate`.
- Renamed `Convert-PnPProvisioningTemplate` to `Convert-PnPSiteTemplate`.
- Renamed `Export-PnPListToProvisioningTemplate` to `Export-PnPListToSiteTemplate`.
- Renamed `New-PnPProvisioningTemplate` to `New-PnPSiteTemplate`.
- Renamed `New-PnPProvisioningTemplateFromFolder` to `New-PnPSiteTemplateFromFolder`.
- Renamed `Read-PnPProvisioningTemplate` to `Read-PnPSiteTemplate`.
- Renamed `Remove-PnPFileFromProvisioningTemplate` to `Remove-PnPFileFromSiteTemplate`.
- Renamed `Save-PnPProvisioningTemplate` to `Save-PnPSiteTemplate`.
- Renamed `Set-PnPProvisioningTemplateMetadata` to `Set-PnPSiteTemplateMetadata`.
- Renamed `Add-PnPProvisioningTemplate` to `Add-PnPSiteTemplate`.
- Renamed `Apply-PnPTenantTemplate` to `Invoke-PnPTenantTemplate`.
- Removed `Get-PnPAppInstance`. Use `Get-PnPApp` instead.
- Removed `Import-PnPAppPackage`. Use `Instal-PnPApp` instead.
- Removed `Uninstall-AppInstance`. Use `Uninstall-PnPApp` instead.
- Removed `Get-PnPHealthScore` as the value reported is only applicable to on-premises.
- Removed `-MinimalHealthScore` from `Connect-PnPOnline` as the value reported from the server only applies to on-premises.
- Removed `-SkipTenantAdminCheck` from `Connect-PnPOnline`. Check will be executed everytime where applicable.
- Removed Obsolete parameter `-FromRecycleBin` from `Remove-PnPTenantSite`. Use `Clear-PnPTenantRecycleBinItem` instead.
- Removed `-UserCodeMaximumLevel` and `-UserCodeWarningLevel` from `Set-PnPTenantSite`: sandboxed solutions have been deprecated from SharePoint Online and these values are not applicable anymore.
- Removed `-Out` parameter on `New-PnPAzureCertificate`. Use `-OutPfx` instead.
- Removed `Enable-PnPResponsiveUI` and `Disable-PnPResponsiveUI`.
- Removed `Disable-PnPInPlaceRecordsManagementForSite`. Use `Set-PnPInPlaceRecordsManagement -Enabled $false`.
- Removed `Enable-PnPInPlaceRecordsManagementForSite`. Use `Set-PnPInPlaceRecordsManagement -Enabled $true`.
- Removed `Measure-PnPResponseTime`. Use Fiddler for more detailed data instead.
- Removed `-Identity` from Get-PnPAvailableLanguage as it does not apply to SharePoint Online.
- Removed `Get-PnPManagementApiAccessToken` and `Get-PnPOfficeManagementApiAccessToken` cmdlets. Use Connect-PnPOnline instead with either the -Scopes parameter and other optional parameters
- Removed alias `Connect-PnPHubsite`. Use `Add-PnPHubSiteAssociation`.
- Removed alias `Disconnect-PnPHubSite`. Use `Remove-PnPHubSiteAssociation`.
- Removed alias `Add-PnPOffice365GroupToSite`. Use `Add-PnPMicrosoft365GroupToSite`.
- Removed alias `Add-PnPUnifiedGroupMember`. Use `Add-PnPMicrosoft365GroupMember`.
- Removed alias `Add-PnPUnifiedGroupOwner`. Use `Add-PnPMicrosoft365GroupOwner`.
- Removed alias `Clear-PnPUnifiedGroupMember`. Use `Clear-PnPMicrosoft365GroupMember`.
- Removed alias `Clear-PnPUnifiedGroupOwner`. Use `Clear-PnPMicrosoft365GroupOwner`.
- Removed alias `Get-PnPDeletedUnifiedGroup`. Use `Get-PnPDeletedMicrosoft365Group`.
- Removed alias `Get-PnPUnifiedGroup`. Use `Get-PnPMicrosoft365Group`.
- Removed alias `Get-PnPUnifiedGroupMembers`. Use `Get-PnPMicrosoft365GroupMembers`.
- Removed alias `Get-PnPUnifiedGroupOwners`. Use `Get-PnPMicrosoft365GroupOwners`.
- Removed alias `New-PnPUnifiedGroup`. Use `New-PnPMicrosoft365Group`.
- Removed alias `Remove-PnPDeletedUnifiedGroup`. Use `Remove-PnPDeletedMicrosoft365Group`.
- Removed alias `Remove-PnPUnifiedGroup`. Use `Remove-PnPMicrosoft365Group`.
- Removed alias `Remove-PnPUnifiedGroupMember`. Use `Remove-PnPMicrosoft365GroupMember`.
- Removed alias `Remove-PnPUnifiedGroupOwner`. Use `Remove-PnPMicrosoft365GroupOwner`.
- Removed alias `Restore-PnPDeletedUnifiedGroup`. Use `Restore-PnPDeletedMicrosoft365Group`.
- Removed alias `Set-PnPUnifiedGroup`. Use `Set-PnPMicrosoft365Group`.
- Removed alias `Execute-PnPQuery`. Use `Invoke-PnPQuery`.
- Removed alias `Ensure-PnPFolder`. Use `Resolve-PnPFolder`.
- Removed `Install-PnPSolution`. Sandboxed solutions have been deprecated.
- Removed `Add-PnPWorkflowDefinition`, `Add-PnPWorkflowSubscription`, `Get-PnPWorkflowDefinition`, `Get-PnPWorkflowInstances`, `Get-PnPWorkflowSubscription`, `Remove-PnPWorkflowDefinition`, `Remove-PnPWorkflowSubscription`, `Resume-PnPWorkflowInstance`, `Start-PnPWorkflowInstance` and `Stop-PnPWorkflowInstance` due to deprecated of the Workflow Services in SharePoint Online.
- Renamed `Test-PnPOffice365AliasIsUsed` to `Test-PnPMicrosoft365AliasIsUsed`
- Refactored some of the Taxonomy cmdlet parameters. See documentation.
- Change in `Copy-PnPFile` which should resolve some issues you may run into when copying files [PR #2796](https://github.com/pnp/PnP-PowerShell/pull/2796)
- Fixed several issues with `Get-PnPSubwebs` and added optional parameter `-IncludeRootWeb` [PR #3011](https://github.com/pnp/PnP-PowerShell/pull/3011)

### Contributors
- Koen Zomers [koenzomers]
- Carlos Marins Jr [kadu-jr]
- Aimery Thomas [a1mery]
- Veronique Lengelle [veronicageek]
