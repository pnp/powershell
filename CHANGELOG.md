# PnP.PowerShell Changelog

*Please do not commit changes to this file, it is maintained by the repo owner.*

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/).


## [Current Nightly]

### Added
- Added `-NewFileName` parameter to `Convert-PnPFile` cmdlet to choose custom output file name.
- Added `-User` parameter to `Get-PnPTeamsTeam` cmdlet to allow fetching list of teams a user has access to.
- Added `Get-PnPBrandCenterFont` cmdlet to retrieve the available fonts in the Brand Center [#4970](https://github.com/pnp/powershell/pull/4970)
- Added `Add-PnPBrandCenterFontPackage` cmdlet to allow creating a font package in the Brand Center [#4970](https://github.com/pnp/powershell/pull/4970)
- Added support for `-FederatedIdentity` in `Connect-PnPOnline` to support Federated Identity.
- Added support for SSO in WSL (Windows Subsystem for Linux) and Linux distributions. You can now use `Connect-PnPOnline` with `-OSLogin` which helps with more secure auth such as FIDO, Conditional Access policies etc.

### Changed
- Improved `Get-PnPTerm` cmdlet to show a better error message. [#4933](https://github.com/pnp/powershell/pull/4933)
- **PnP PowerShell now requires PowerShell 7.4.0 or newer**

### Fixed
- Fix `Set-PnPView -Aggregations` parameter not showing aggregations in SharePoint online. [#4868](https://github.com/pnp/powershell/pull/4868)
- Fix `-CreateDrive` parameter not working correctly in `Connect-PnPOnline`. [#4869](https://github.com/pnp/powershell/pull/4869)
- Fix `Get/Remove/Restore-PnPFileVersion` cmdlets to properly handle file names which have encoded values.
- Fixed Teams related cmdlets to properly handle quotes in the display name of Teams team.
- Fix `Set-PnPListItem` cmdlet to properly handle multi-choice fields when used with batch parameter.
- Fix `Get-PnPCopilotAgent` cmdlet to properly handle pagination in large lists.
- Fix `Send-PnPMail` cmdlet to send mail via Graph API and SPO. It was facing parameter set issue. [#4922](https://github.com/pnp/powershell/pull/4922)
- Fix `Unregister-PnPHubSite` cmdlet to properly handle null reference error. [#4944](https://github.com/pnp/powershell/pull/4944)
- Fix `Add/Publish/Unpublish/Remove-PnPApp` cmdlets to properly handle the issue in no-script sites.
- Fix `Get-PnPHubSiteChild` cmdlet to handle vanity domains. [#4965](https://github.com/pnp/powershell/pull/4965)

### Removed

### Contributors

- Koen Zomers [koenzomers]
- Janne Holm [jhholm]
- Marc D Anderson [sympmarc]
- [abwlodar]
- Reshmee Auckloo [reshmee011]
- [wuxiaojun514]
- [pajeffery]
- Giacomo Pozzoni [jackpoz]
- [davidschenkUPG]

## [3.1.0]

### Added

### Fixed
- Fixed issues with cmdlets not being able to read embedded resources

### Removed

### Contributors
- Erwin van Hunen [erwinvanhunen]
- Bert Jansen [jansenbe]

## [3.0.0]

### Added

- Added `-PersistLogin` on `Connect-PnPOnline` which will utilize a persist cache containing your access token. The cache is encrypted and stored in a subfolder in your $HOME folder on Windows. On MacOS the cache is stored in the encrypted KeyChain. You only have to specify `-PersistLogin` once when doing a Connect-PnPOnline, after that the cache entry will be used. The cache is persisted between PowerShell sesions and system reboots. To clear an entry from the cache use `Disconnect-PnPOnline -ClearPersistedLogin`. 
- Added tab completers for all cmdlets using a ListPipeBind parameter (e.g. `Get-PnPList -Identity`), all cmdlets using a FieldPipeBind parameter (e.g. `Get-PnPField -Identity`), `Get-PnPPropertyBag`, ContentType related cmdlets (`Get-PnPContentType` etc.) and Page related (`Get-PnPPage` etc.) cmdlets. The argument lookup will timeout after 2 seconds. This value can controlled by setting an environment variables called "PNPPSCOMPLETERTIMEOUT" and set the value to a number specifying milliseconds (e.g. 2000 is 2 seconds). If you want to disable the completer functionality on tabs, set the timeout value to 0 (zero).
- Added `Reset-PnPDocumentID` cmdlet to request resetting the document ID for a document [#4238](https://github.com/pnp/powershell/pull/4238)
- Added `Reset-PnPDocumentID` cmdlet to request resetting the document IDs for all documents in a library using a specific content type [#4755](https://github.com/pnp/powershell/pull/4755)
- Added `Get-PnPPriviledgedIdentityManagementEligibleAssignment`, `Get-PnPPriviledgedIdentityManagementRole` and `Enable-PnPPriviledgedIdentityManagement` cmdlets to allow scripting of enabling Privileged Identity Management roles for a user [#4039](https://github.com/pnp/powershell/pull/4039)
- Added `Add-PnPTenantRestrictedSearchAllowedList` which allows setting up a list of allowed URLs for Restricted SharePoint Search [#3993](https://github.com/pnp/powershell/pull/3993)
- Added optional `-IsCopilotSearchable` to `Add-PnPOrgAssetsLibrary` which allows for an organizational assets library to be accessible to Microsoft 365 CoPilot for searching corporate images [#4254](https://github.com/pnp/powershell/pull/4254)
- Added `Set-PnPOrgAssetsLibrary` cmdlet which allows for updating the settings of an existing organizational assets library [#4254](https://github.com/pnp/powershell/pull/4254)
- Added additional Graph permissions to the GraphPermissions parameter set. [#4283](https://github.com/pnp/powershell/pull/4283)
- Added `-SignInAudience` parameter to `Register-PnPEntraIDApp` and `Register-PnPEntraIDAppForInteractiveLogin` which allows you to make the EntraID app support creation of multi-tenant apps. [#4289](https://github.com/pnp/powershell/pull/4289)
- Added `-OutputInstance` parameter to `Export-PnPPage` cmdlet to allow export of the page as in-memory template. [#4323](https://github.com/pnp/powershell/pull/4323)
- Added `-X509KeyStorageFlags` parameter to `Connect-PnPOnline` cmdlet which allows setting of how private keys are to be imported. [#4324](https://github.com/pnp/powershell/pull/4324)
- Added `-RestrictContentOrgWideSearch` parameter to `Set-PnPSite` which allows for applying the Restricted Content Discoverability (RCD) setting to a site [#4335](https://github.com/pnp/powershell/pull/4335)
- Added `-LaunchBrowser` parameter to `Register-PnPEntraIDAppForInteractiveLogin` and `Register-PnPEntraIDApp` cmdlets to open browser and continue app registration in the browser. [#4347](https://github.com/pnp/powershell/pull/4347) & [#4348](https://github.com/pnp/powershell/pull/4348)
- Added `Get-PnPSearchExternalItem` which allows retrieving the current external items for a specific external connection [#4375](https://github.com/pnp/powershell/pull/4375)
- Added `Remove-PnPSearchExternalItem` which allows for removal of an external item from the Microsoft Search index [#4378](https://github.com/pnp/powershell/pull/4378)
- Added `-Scopes` parameter to `Get-PnPAccessToken` cmdlet to retrieve access token with specific scopes. [#4398](https://github.com/pnp/powershell/pull/4398)
- Added `-Icon` and `-Color` parameters to `Set-PnPList` cmdlet. [#4409](https://github.com/pnp/powershell/pull/4409)
- Added `Remove-PnPTenantRestrictedSearchAllowedList` cmdlet to removes site URLs from the allowed list when Restricted SharePoint Search is enabled. [#4399](https://github.com/pnp/powershell/pull/4399)
- Added `Get-PnPDeletedFlow` cmdlet to retrieve a list of flows which are soft deleted. [#4396](https://github.com/pnp/powershell/pull/4396)
- Added `Restore-PnPFlow` cmdlet which allows for undeleting a flow within 21 days of deletion. [#4415](https://github.com/pnp/powershell/pull/4415)
- Added `-ExcludeDeprecated` to `Export-PnpTaxonomy` which allows for deprecated terms to be excluded from the export [#4053](https://github.com/pnp/powershell/pull/4053)
- Added `-HidePeoplePreviewingFiles` to `Set-PnPSite` which allows for hiding the people previewing files feature on a site [#4416](https://github.com/pnp/powershell/pull/4416)
- Added `-AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled` to `Set-PnPTenant` which allows for updating of web property bag when DenyAddAndCustomizePages is enabled [#4508](https://github.com/pnp/powershell/pull/4508)
- Added `SiteId` to the output of `Get-PnPTenantSite` [#4527](https://github.com/pnp/powershell/pull/4527)
- Added `Add-PnPFileSensitivityLabel` which allows for assigning sensitivity labels to SharePoint files [#4538](https://github.com/pnp/powershell/pull/4538)
- `Add-PnPApp` , `Publish-PnPApp` , `Remove-PnPApp` and `Unpublish-PnPApp` now have `-Force` parameter to change the site to allow scripts to be temporarily enabled. [#4554](https://github.com/pnp/powershell/pull/4554)
- Added `-CanSyncHubSitePermissions` parameter to `Set-PnPSite` cmdlet to set value of allowing syncing hub site permissions to this associated site. [#4555](https://github.com/pnp/powershell/pull/4555)
- Added `Get-PnPProfileCardProperty`, `New-PnPProfileCardProperty` and `Remove-PnPProfileCardProperty` cmdlets to manage showing additional properties on the Microsoft 365 user profile [#4548](https://github.com/pnp/powershell/pull/4548)
- Added `Get-PnPCopilotAdminLimitedMode` and `Set-PnPCopilotAdminLimitedMode` which allows for managing the setting that controls whether Microsoft 365 Copilot in Teams Meetings users can receive responses to sentiment-related prompts [#4562](https://github.com/pnp/powershell/pull/4562)
- Added `-Contributors` and `-Managers` parameters to `New-PnPTermGroup` and `Set-PnPTermGroup` cmdlets.
- Added `-Files` parameter for `Send-PnPMail` cmdlet to allow files to be downloaded from SharePoint and then sent as attachments.
- Added `-Force` parameter to `Set-PnPPropertyBagValue` cmdlet to toggle NoScript status of the site.
- Added `-Batch` parameter to `Invoke-PnPGraphMethod` cmdlet to allow adding request in a batch.
- Added `-List` parameter to `Get-PnPFolderItem`, `Get-PnPFileInFolder` and `Get-PnPFolderInFolder` which allows them to work with a document library containing more than 5,000 items [#4611](https://github.com/pnp/powershell/pull/4611)
- Added `Start-PnPTraceLog`, `Stop-PnPTraceLog` and `Get-PnPTraceLog` cmdlets to handle tracelogging. Removed `Set-PnPTraceLog` cmdlet.
- Added `-ListPermissionScopes` parameter on `Get-PnPAccessToken` cmdlet to list the current permission scopes on the current access token.
- Added `Get-PnPCopilotAgent` cmdlet that returns the Microsoft Copilot Agents in a site collection.
- Added `Get-PnPFileRetentionLabel` cmdlet to fetch the file retention labels. [#4603](https://github.com/pnp/powershell/pull/4603)
- Added `Get/Set/Remove-PnPUserProfilePhoto` cmdlets to download, upload or remove the profile photo of the specified user.
- Added `New/Get/Remove/Update-PnPTodoList` cmdlets to manage Todo lists.
- Added `Set-PnPFileRetentionLabel` which allows setting a retention label on a file in SharePoint or locking/unlocking it. [#4457](https://github.com/pnp/powershell/pull/4457)
- Added `Get-PnPFileCheckedOut` cmdlet to retrieve all files that are currently checked out in a library [#4682](https://github.com/pnp/powershell/pull/4682)
- Added `Get-PnPTenantPronounsSetting` and `Set-PnPTenantPronounsSetting` cmdlets to manage the availability of using pronouns in the organization [#4660](https://github.com/pnp/powershell/pull/4660)
- Added `HidePeopleWhoHaveListsOpen` parameter to `Set-PnPSite` cmdlet to hide people who simultaneously have lists open [#4699](https://github.com/pnp/powershell/pull/4699)
- Added `-WhoCanShareAllowListInTenant`, `-LegacyBrowserAuthProtocolsEnabled`, `-EnableDiscoverableByOrganizationForVideos`, `-RestrictedAccessControlforSitesErrorHelpLink`, `-Workflow2010Disabled`, `-AllowSharingOutsideRestrictedAccessControlGroups`, `-HideSyncButtonOnDocLib`, `-HideSyncButtonOnODB`, `-StreamLaunchConfig`, `-EnableMediaReactions`, `-ContentSecurityPolicyEnforcement` and `-DisableSpacesActivation` to `Set-PnPTenant` [#4681](https://github.com/pnp/powershell/pull/4681)
- Added `Start-PnPEnterpriseAppInsightsReport` and `Get-PnPEnterpriseAppInsightsReport` which allow working with App Insights repors [#4713](https://github.com/pnp/powershell/pull/4713)
- Added `Set-PnPSiteDocumentIdPrefix` which allows changing of the document id prefix on a site collection [#4765](https://github.com/pnp/powershell/pull/4765)
- Added `Get-PnPMicrosoft365Roadmap` which allows retrieval of the Microsoft 365 Roadmap items [#4764](https://github.com/pnp/powershell/pull/4764)
- Added `-Name` parameter to `Add-PnPApplicationCustomizer` cmdlet to allow for specifying the name of the application customizer [#4767](https://github.com/pnp/powershell/pull/4767)
- Added `Get-PnPTraceLog` cmdlet which allows reading from the detailed in memory logs of the PnP PowerShell cmdlet execution [#4794](https://github.com/pnp/powershell/pull/4794)
- Added `-Transitive` parameter to `Get-PnPAzureADGroupMember` cmdlet to allow members of groups inside groups to be retrieved [#4799](https://github.com/pnp/powershell/pull/4799)
- Added the ability to `Get-PnPPage` to return all site pages in the site by omitting the `-Identity` parameter [#4805](https://github.com/pnp/powershell/pull/4805)
- Added `Copy-PnPPage`, `Move-PnPPage` and `Get-PnPPageCopyProgress` cmdlets to allow for copying and moving site pages between sites [#4806](https://github.com/pnp/powershell/pull/4806)
- Added `Get-PnPBrandCenterConfig` to retrieve the configuration of the Brand Center on the tenant [#4830](https://github.com/pnp/powershell/pull/4830)
- Added `Get-PnPBrandCenterFontPackage` to retrieve the available font packages from the various Brand Centers [#4830](https://github.com/pnp/powershell/pull/4830)
- Added `Use-PnPBrandCenterFontPackage` to apply the specified font package from the Brand Center to the current site [#4830](https://github.com/pnp/powershell/pull/4830)
- Added `Add-PnPBrandCenterFont` to upload a font to the tenant Brand Center [#4830](https://github.com/pnp/powershell/pull/4830)
- Added `-Stream` parameter to `Invoke-PnPSiteTemplate` which allows an in memory .pnp site template to be applied to a site [#4845](https://github.com/pnp/powershell/pull/4845)

### Changed

- **PnP PowerShell is now .NET 8.0 based, and requires PowerShell 7.4.6 or newer**
- **`-Interactive` login is now the default.**
- The Popup based authentication for Interactive Login has been removed and replaced by a browser flow
- `-LaunchBrowser` has been removed for interactive login
- `-LaunchBrowser` for Device Login authentication flows has been renamed to `OpenBrowser`
- **Rate limiting is now enabled by default for all cmdlets which are being executed under application permissions.**
- Changed the UI experience when logging in with Interactive login and specifying `-LaunchBrowser` on `Connect-PnPOnline`. This experience is the default on MacOS.
- In case of errors when Graph batch method is used, it will now throw a clearer error message about what was the issue. [#4327](https://github.com/pnp/powershell/pull/4327/)
- `Get-PnPAccessToken`, `Request-PnPAccessToken` and `Get-PnPGraphAccessToken` output type is changed to `Microsoft.IdentityModel.JsonWebTokens.JsonWebToken`. Earlier they returned `System.IdentityModel.Tokens.Jwt`.
- `New-PnPContainerType` will temporarily disable standard containers to be created due to changed being applied at Microsoft to allow this to be possible in the future [#4125](https://github.com/pnp/powershell/pull/4125)
- Renamed `Get-PnPLabel` cmdlet to `Get-PnPRetentionLabel`. [#4387](https://github.com/pnp/powershell/pull/4387)
- `Add-PnPNavigationNode` cmdlet updated to now support `-OpenInNewTab` parameter for different types of navigation. [#3969](https://github.com/pnp/powershell/pull/3969)
- `Remove-PnPFile` , `Rename-PnPFile`, `Set-PnPFileCheckedIn`, `Set-PnPFileCheckedOut` & `Undo-PnPFileCheckedOut` cmdlets now use PnP Core SDK behind the scenes. [#4389](https://github.com/pnp/powershell/pull/4389)
- `Set-PnPFileCheckedIn` cmdlet now expects `CheckInType` to be of type `PnP.Core.Model.SharePoint.CheckinType` instead of the earlier one based on CSOM. [#4389](https://github.com/pnp/powershell/pull/4389)
- `Disable-PnPFeature` and `Enable-PnPFeature` now use PnP Core SDK for processing requests. [#4390](https://github.com/pnp/powershell/pull/4390)
- `Remove-PnPContentType` and `Remove-PnPContentTypeFromList` now use PnP Core SDK for processing requests. [#4390](https://github.com/pnp/powershell/pull/4390)
- `Clear-PnPRecycleBinItem` , `Move-PnPRecycleBinItem` and `Restore-PnPRecycleBinItem` cmdlets now use PnP Core SDK for processing requests. [#4393](https://github.com/pnp/powershell/pull/4393/)
- `Get-PnPSearchCrawlLog` cmdlet now shows a warning in case application permissions are used. [#4391](https://github.com/pnp/powershell/pull/4391)
- All Power Platform cmdlets no longer require an environment to be specified. If omitted, the default environment will be retrieved and used. [#4415](https://github.com/pnp/powershell/pull/4415)
- When passing in an existing connection using `-Connection` on `Connect-PnPOnline`, the clientid from the passed in connection will be used for the new connection [#4425](https://github.com/pnp/powershell/pull/4425)
- Removed `-Confirm` parameter from `Remove-PnPUser` and `Remove-PnPAvailableSiteClassification` cmdlets. Use `-Force` instead. [#4455](https://github.com/pnp/powershell/pull/4455)
- `Get-PnPFile` now also supports passing in a full SharePoint Online URL [#4480](https://github.com/pnp/powershell/pull/4480)
- `Add-PnPApp` , `Publish-PnPApp` , `Remove-PnPApp` and `Unpublish-PnPApp` now support disabling script settings if tenant app catalog is a no-script site.
- `Send-PnPMail` now throws a warning about the retirement of the SharePoint SendEmail API.
- `Get-PnPCustomAction` now supports a completer for `-Identity` and uses the PnP Core SDK to return custom actions.
- `Set-PnPPropertyBagValue` and `Remove-PnPPropertyBagValue` now toggle the NoScript status of the site to allow setting/removing property bag values, but only if the tenant wide `AllowWebPropertyBagUpdateWhenDenyAddAndCustomizePagesIsEnabled` is not enabled [#4680](https://github.com/pnp/powershell/pull/4680)
- `Get-PnPTenant` now uses nullable types for the properties that can return null if the property is not set or could not be retrieved. Beware that the property `PublicCdnOrigins` has been renamed to `PublicCdnOriginParsed `. All other property names will remain the same. [#4722](https://github.com/pnp/powershell/pull/4722)
- Removed `New-PnPMicrosoft365Group` setting the group visibility options twice when providing `-HideFromAddressLists` and/or `-HideFromOutlookClients` and adding logging around trying to set the group visibility [#4791](https://github.com/pnp/powershell/pull/4791)
- Visual Studio Code launch profiles have been cleaned up and restructured [#4808](https://github.com/pnp/powershell/pull/4804)
- The cmdlet `Get-PnPContainer` now has a new optional parameter `-ArchiveStatus` which allows filtering SharePoint Online Embedded containers by archival status [#4821](https://github.com/pnp/powershell/pull/4821)

### Fixed

- Fixed issue with `Set-PnPSearchExternalSchema` cmdlet when used with the `-Wait` parameter throwing a warning [#4253](https://github.com/pnp/powershell/pull/4253)
- Fix `Get-PnPSearchExternalSchema` not returning the label properly for a property linked to 'iconUrl' [#4245](https://github.com/pnp/powershell/pull/4245)
- Fix `Restore-PnPListItemVersion` cmdlet to also restore fields with TaxonomyFieldType. [#4262](https://github.com/pnp/powershell/pull/4262)
- Fix `Set-PnPMicrosoft365GroupSetting` cmdlet to not throw runtime error. [#4274](https://github.com/pnp/powershell/pull/4274)
- Fix `New-PnPMicrosoft365GroupSetting` cmdlet to not throw runtime error. [#4277](https://github.com/pnp/powershell/pull/4277)
- Fix `Get-PnPSiteTemplate -PersistMultiLanguageResources` not working correctly for xml file types. [#4326](https://github.com/pnp/powershell/pull/4326)
- Fix `Add-PnPDataRowsToSiteTemplate` setting keyColumn to null when not passed. [#4325](https://github.com/pnp/powershell/pull/4325)
- Fix `Connect-PnPOnline` not working correctly when `-DeviceLogin` and `-LaunchBrowser` both are specified. It used to open it in a popup. Now it correctly launches the browser. [#4325](https://github.com/pnp/powershell/pull/4345)
- `Export-PnPUserInfo`, `Export-PnPUserProfile` and `Remove-PnPUserProfile` cmdlets now work properly with proper `-Connection` parameter if specified. [#4389](https://github.com/pnp/powershell/pull/4389)
- Fixed `Get-PnPAzureADAppSitePermission`, `Grant-PnPAzureADAppSitePermission` and `Revoke-PnPAzureADAppSitePermission` cmdlets throwing an error when the site URL is not specified and the app registration used only having Graph permissions [#4421](https://github.com/pnp/powershell/pull/4421)
- Fixed `Get-PnPTerm` cmdlet not working correctly when `-ParentTerm` parameter is specified. [#4454](https://github.com/pnp/powershell/pull/4454)
- Fixed the PnP PowerShell version check to only check nightly version in nightly builds and major version in release builds. [#4453](https://github.com/pnp/powershell/pull/4453)
- Fixed `-ConsistencyLevelEventual` flag on `Invoke-PnPGraphMethod` to work correctly. [#4523](https://github.com/pnp/powershell/pull/4523)
- Fixed `Get-PnPServiceHealthIssue` returning an error when certain service states were active [#4530](https://github.com/pnp/powershell/pull/4530)
- Fixed `Add-PnPFileSensitivityLabel` cmdlet to allow empty string value to reset file sensitivity label.
- Fix `Connect-PnPOnline` cmdlet not working with On Prem related cmdlets. [#4622](https://github.com/pnp/powershell/pull/4622)
- Fix `Get\Invoke-PnPSiteTemplate` cmdlet not working in vanity domains. [#4630](https://github.com/pnp/powershell/pull/4630)
- Fixed passing a `Get-PnPRecycleBinItem` result or a GUID to `Restore-PnPRecycleBinItem` not working correctly. [#4667](https://github.com/pnp/powershell/pull/4667)
- Fixed `Get-PnPChangeLog` not returning the changelog [#4707](https://github.com/pnp/powershell/pull/4707)
- Fixed `-Description` and `-Sequence` not being applied when providing these through `Add-PnPApplicationCustomizer` [#4767](https://github.com/pnp/powershell/pull/4767)
- Fixed `-RetryCount` parameter being ignored with `Submit-PnPSearchQuery` [#4784](https://github.com/pnp/powershell/pull/4784)
- Fixed `Get-PnPSiteScriptFromWeb` throwing a file not found error when providing a web URL through `-Url` that differed from the connected to URL [#4785](https://github.com/pnp/powershell/pull/4785)
- Fixed `Set-PnPListItem -Values @{}` passing in a taxonomy field with a guid typed value throwing an error [#4811](https://github.com/pnp/powershell/pull/4811)
- Fixed `Get-PnPFolder` throwing an exception when a lot of files and folders are present [#4819](https://github.com/pnp/powershell/pull/4819)
- Fixed `Set-PnPTerm -Name "New Name" -Lcid 1043` changing the default name of the taxonomy item, ignoring the provided language id and changing the name for the default language instead. [#4824](https://github.com/pnp/powershell/pull/4824)
- Fixed `Get-PnPPropertyBag` not returning updated values after running it for the first time [#4823](https://github.com/pnp/powershell/pull/4823)
- Fixed local build of PnP PowerShell using a local Core SDK build not being debuggable and optimized PnP PowerShell debug profiles for Visual Studio Code [#4838](https://github.com/pnp/powershell/pull/4838)
- Fixed Batched requests using a DELETE or MERGE throwing an exception because of a double IF-MATCH header [Core #1635](https://github.com/pnp/pnpcore/pull/1635)

### Removed

- Removed `-LaunchBrowser`, `-NoPopup` and credential based auth on `Register-PnPEntraIDApp` and `Register-PnPEntraIDAppForInteractiveLogin` cmdlets. The default auth method is now Interactive.
- Removed `-LaunchBrowser` option on `Connect-PnPOnline` for interactive logins and device logins as it is default now and the popup based authentication window has been removed.
- Removed `-UseWebLogin` option on `Connect-PnPOnline`. It used a very old, questionable approach to authentication. Use `-Interactive` or if you require an ACS connection `-ClientId` and `-ClientSecret`
- Removed `Invoke-PnPTransformation` cmdlet as it was never supported.
- Removed `Publish-PnPCompanyApp` cmdlet as it was not supported anymore. [#4387](https://github.com/pnp/powershell/pull/4387)
- Removed `-UserVoiceForFeedbackEnabled` parameter from `Set-PnPTenant` cmdlet as it was obsolete. [#4387](https://github.com/pnp/powershell/pull/4387)
- Removed `Set-PnPLabel` and `Reset-PnPLabel` aliases. You need to use `Set-PnPRetentionLabel` and `Reset-PnPRetentionLabel` respectively. [#4387](https://github.com/pnp/powershell/pull/4387)
- Removed `Get-PnPPowerPlatformConnector` alias. You need to use `Get-PnPPowerPlatformCustomConnector`. [#4387](https://github.com/pnp/powershell/pull/4387)
- Removed `-IsFavoriteByDefault` parameter from `Add-PnPTeamsChannel` cmdlet. It was obsolete and not supported by Graph API. [#4387](https://github.com/pnp/powershell/pull/4387)
- Removed `Get-PnPAppAuthAccessToken` , `Remove-PnPGraphAccessToken` and `Request-PnPAccessToken` cmdlets. Use `Get-PnPAccessToken` instead. [#4398](https://github.com/pnp/powershell/pull/4398)
- Removed support for sending mail via SMTP in `Send-PnPMail`. It's usage is not recommended by .NET due to its lack of support for modern protocols.
- Removed `-Title` and `-Header` parameters from `Remove-PnPNavigationNode`. They were marked obsolete.
- Removed `-FileUrl` parameter from `Get-PnPSharingLink`. It was marked obsolete.
- Removed `-WebLogin` parameter from `Connect-PnPOnline` cmdlet. It was marked obsolete and was a security risk.
- Removed `Set-PnPMinimalDownloadStrategy` as it's not applicable anymore to SharePoint Online. If you need the functionality you can always turn on the feature with `Enable-PnPFeature -Id 87294c72-f260-42f3-a41b-981a2ffce37a` or turn it off with `Disable-PnPFeature -Id 87294c72-f260-42f3-a41b-981a2ffce37a`
- Removed `-SPOManagementShell` parameter from `Connect-PnPOnline` cmdlet. It reduces the risk of changes coming from Microsoft. Use your own Entra ID app instead.
- Removed `Set-PnPTraceLog` cmdlet and introduced `Start-PnPTraceLog` and `Stop-PnPTraceLog` with similar parameters.
- Removed `-DelayDenyAddAndCustomizePagesEnforcement` parameter from `Set-PnPTenant` cmdlet as it is no longer valid , removed from underlying SDK and its value can't be changed.
- Removed `-EnableVersionExpirationSetting` parameter from `Set-PnPTenant` cmdlet as it is now enabled by default.
 
### Contributors

- [svermaak]
- [PitSysAdmin]
- Abhijeet Jadhav [TekExpo]
- [abwlodar]
- [jgfgoncalves]
- Stephen Cox [stephen-cox-nzx]
- Marijn Somers [Marijnsomers]
- Janne Holm [jhholm]
- Paul Bullock [pkbullock]
- Arjan Cornelissen [arjancornelissen]
- Konrad K. [wilecoyotegenius]
- Antti K. Koskela [koskila]
- Steve Beaugé [stevebeauge]
- [reusto]
- Fredrik Thorild [fthorild]
- San [sankarkumar23]
- Christian Veenhuis [ChVeen]
- Nishkalank Bezawada [NishkalankBezawada]
- Dan Toft [Tanddant]
- Jürgen Rosenthal-Buroh [JuergenRB]
- [PeterRevsbech]
- Peter Paul Kirschner [petkir]
- Giacomo Pozzoni [jackpoz]
- wuxiaojun514
- Reshmee Auckloo [reshmee011]
- Koen Zomers [koenzomers]
- Erwin van Hunen [erwinvanhunen]
- Gautam Sheth [gautamdsheth]

## [2.12.0]

### Added

- Added support for `WAM` login for Windows OS to support Windows Hello, FIDO keys, Conditional Access policies and other secure authentication modes. 
- Added `-SkipCertCreation` parameter in `Register-PnPAzureADApp` cmdlet to prevent creation and uploading of certificates in the Entra ID app.
- Added support to `-ValidateConnection` in managed identity authentication.
- Added `New-PnPSearchExternalConnection`, `Get-PnPSearchExternalConnection`, `Set-PnPSearchExternalConnection` and `Remove-PnPSearchExternalConnection` cmdlets to manage external connections for Microsoft Search [#4231](https://github.com/pnp/powershell/pull/4231)
- Added `Get-PnPSearchExternalSchema` and `Set-PnPSearchExternalSchema` cmdlets to manage the schema for external connections for Microsoft Search [#4231](https://github.com/pnp/powershell/pull/4231)
- Added `OverrideSharingCapability`, `RequestFilesLinkExpirationInDays` & `RequestFilesLinkEnabled` parameters to `Set-PnPTenantSite` cmdlet.

### Changed

- *Release due to deprecation/shutdown of support for the PnP Management Shell* Refer to https://pnp.github.io/powershell/articles/registerapplication.html on how to registration your own application
- Added output for clientid/Entra App Id when using `-Verbose` with `Connect-PnPOnline`
- Added `-OutputTask` switch to `Add-PnPPlannerTask` cmdlet which will return the just created task so inspect its ID, ETag, etc. values.
- Improved `Invoke-PnPGraphMethod` cmdlet now to also support a hashtable value for the AdditionalHeaders parameter besides the current Dictionary<string,string>. See documentation.
- Improved managed identity authentication for connecting to different M365 services.
- Improved error message for `Export-PnPPage` cmdlet when the page doesn't exist.
- Improved `Register-PnPEntraIDApp` & `Register-PnPEntraIDAppForInteractiveLogin` cmdlets to better work in non-commercial cloud environments.
- Improved `Get-PnPDiagnostics` cmdlet to properly handle a scenrio where there's no correlationId.

### Fixed

- Removed version check dependency on version.txt located in Github repo. Use metadata from powershellgallery.com instead.
- Fix issue with `Get-PnPDiagnostics` cmdlet not working correctly if `CorrelationId` is null.
- Fix issue with App-only authentication not properly fetching tokens.
- Fix issue with Power Platform cmdlets not working correctly in non-commercial cloud environments.
- Fix issue with `Get-PnPFlow` not working correctly when `-AsAdmin` parameter is specified due to API changes. [#4244](https://github.com/pnp/powershell/pull/4244)
- Fix `Connect-PnPOnline` not returning correct `ClientId` in the connection object.

### Removed

### Contributors

- Erwin van Hunen [erwinvanhunen]
- Giacomo Pozzoni [jackpoz]
- Nishkalank Bezawada [NishkalankBezawada]
- Reshmee Auckloo [reshmee011]
- Koen Zomers [koenzomers]

## [2.10.0]

### Added

- Added Register-PnPEntraIdAppForInteractiveLogin cmdlet

### Fixed

### Removed

### Contributors

- Erwin van Hunen [erwinvanhunen]

## [2.9.0]

### Fixed
Fixed app registration on Windows

### Contributors

- Erwin van Hunen [erwinvanhunen]

## [2.8.0]

### Added

- Added in depth verbose logging to all cmdlets which is revealed by adding `-Verbose` to the cmdlet execution [#4023](https://github.com/pnp/powershell/pull/4023)
- Added `-CoreDefaultShareLinkScope` and `-CoreDefaultShareLinkRole` parameters to `Set-PnPTenant` cmdlet. [#4067](https://github.com/pnp/powershell/pull/4067)
- Added `-Identity` parameter to the `Get-PnPFileSharingLink` cmdlet allowing for the retrieval of sharing links based on the file's unique identifier, file instance, listitem instance, or server relative path and supporting retrieval of sharing links for multiple files, such as all in a document library [#4093](https://github.com/pnp/powershell/pull/4093)
- Added `Remove-PnPAzureADUser` which allows removal of a user from Azure Active Directory / Entra ID [#4123](https://github.com/pnp/powershell/pull/4123)
- Added support for `GuestSharingGroupAllowListInTenantByPrincipalIdentity` and `OneDriveSharingCapability` parameters in `Set-PnPTenant` cmdlet. [#4122](https://github.com/pnp/powershell/pull/4122)
- Added `-AsListItem` paramter to `Get-PnPFolder` cmdlet to return as folder as a list item object. [#4151](https://github.com/pnp/powershell/pull/4151)
- Added support for handling `$ErrorActionPreference` global variable to make it work with PnP PowerShell cmdlets. [#4079](https://github.com/pnp/powershell/pull/4079)
- Added `-Folder` parameter to `Add-PnPDocumentSet` cmdlet to allow creation of document sets in a specific folder instead of the list root folder. [#4029](https://github.com/pnp/powershell/pull/4029)
  
### Fixed

- `Get-PnPTeamsChannel` and `Get-PnPTeamsPrimaryChannel` returning `unknownFutureValue` as MembershipType instead of `shared` [#4054](https://github.com/pnp/powershell/pull/4054)
- Fixed using a AzureADUserPipeBind with `New-PnPAzureADUserTemporaryAccessPass`, `Get-PnPAvailableSensitivityLabel` and `Set-PnPSearchExternalItem` to not work when passing in the User ID GUID [#4123](https://github.com/pnp/powershell/pull/4123)
- Fixed issue with `Get-PnPWebHeader` cmdlet not working properly in Group connected SharePoint sites. [#4147](https://github.com/pnp/powershell/pull/4147)
- Fixed issue with `Get-PnPTeamsChannelFilesFolder` cmdlet to work properly for channels having data more than 2 GB. [#4127](https://github.com/pnp/powershell/pull/4127)

### Changed

- Fixed `Update-PnPTeamsUser` cmdlet to throw a better error message when after a user is removed from a Team but is still in the connected M365 group, for the few seconds that the 2 are out of sync. [#4068](https://github.com/pnp/powershell/pull/4068)
- Changed `-FileUrl` on `Get-PnPFileSharingLink` to become obsolete. Please switch to using `-Identity` instead, passing in the same value [#4093](https://github.com/pnp/powershell/pull/4093)

### Removed

### Contributors

- Dan Cecil [danielcecil]
- Reshmee Auckloo [reshmee011]
- Giacomo Pozzoni [jackpoz] 
- Koen Zomers [koenzomers]
  
## [2.5.0]

### Added

- Added `New-PnPLibraryFileVersionBatchDeleteJob` and `New-PnPSiteFileVersionBatchDeleteJob` cmdlets to queue a job for deleting the file versions based on age. [#3799](https://github.com/pnp/powershell/pull/3799)
- Added `New-PnPLibraryFileVersionExpirationReportJob` and `New-PnPSiteFileVersionExpirationReportJob` cmdlets to queue a job for generating a file version expiration report for all files in a document library or site. [#3799](https://github.com/pnp/powershell/pull/3799)
- Added `Remove-PnPLibraryFileVersionBatchDeleteJob` and `Remove-PnPSiteFileVersionBatchDeleteJob` cmdlets to cancel the job for deleting file versions based on age. [#3799](https://github.com/pnp/powershell/pull/3799)
- Added `Get-PnPLibraryFileVersionExpirationReportJobProgress` and `Get-PnPSiteFileVersionExpirationReportJobProgress` cmdlets to getting the progress for the job for file versions based on age. [#3799](https://github.com/pnp/powershell/pull/3799)
- Added `-UseVersionExpirationReport` parameter to `Get-PnPFileVersion` cmdlet to get the version expiration report for a single file. [#3799](https://github.com/pnp/powershell/pull/3799)
- Added `-DelayDenyAddAndCustomizePagesEnforcement` parameter to `Set-PnPTenant` cmdlet which allows delay of the change to custom script set on the Tenant until mid-November 2024. [#3815](https://github.com/pnp/powershell/pull/3815)
- Added additional permissions for Graph application permission validate sets. [#3835](https://github.com/pnp/powershell/issues/3835)
- Added the ability to upload entire local folders with files and optionally subfolders to SharePoint Online into 'Copy-PnPFolder' [#3850](https://github.com/pnp/powershell/pull/3850)
- Added `LoopDefaultSharingLinkRole`, `DefaultShareLinkScope`, `DefaultShareLinkRole`, `LoopDefaultSharingLinkScope` and `DefaultLinkToExistingAccessReset` parameters to `Set-PnPTenant` cmdlet. [#3874](https://github.com/pnp/powershell/pull/3874)
- Added `Unlock-PnPSensitivityLabelEncryptedFile` which allows the encryption to be removed from a file [#3864](https://github.com/pnp/powershell/pull/3864)
- Added `Get-PnPLibraryFileVersionBatchDeleteJobStatus` and `Get-PnPSiteFileVersionBatchDeleteJobStatus` to check on the status of applying file based version expiration based on age on a library and site level [#3828](https://github.com/pnp/powershell/pull/3828)
- Added support for `Get-PnPSiteCollectionAppCatalog` and `Get-PnPTenantSite` to be used with vanity domain tenants [#3895](https://github.com/pnp/powershell/pull/3895)
- Added support for using vanity domain tenants with `Grant-PnPTenantServicePrincipalPermission`, `Revoke-PnPTenantServicePrincipalPermission`, `Set-PnPWebTheme`, `Invoke-PnPListDesign`, `Set-PnPSite`, `Add-PnPSiteDesignTask`, `Get-PnPSiteDesignRun`, `Get-PnPSiteDesignTask` and `Invoke-PnPSiteDesign` cmdlets [#3898](https://github.com/pnp/powershell/pull/3898)
- Added `-Detailed` to `Get-PnPMicrosoft365Group` which allows retrieval of the AllowExternalSenders, IsSubscribedByMail and AutoSubscribeNewMembers properties of the group [#3958](https://github.com/pnp/powershell/pull/3958)
- Added `-RequireSenderAuthenticationEnabled` and `-AutoSubscribeNewMembers` to `Set-PnPMicrosoft365Group` which allows setting these properties on a group [#3958](https://github.com/pnp/powershell/pull/3958)
- Added `Get-PnPContainerType` cmdlet to retrieve the list of Container Types created for a SharePoint Embedded Application in the tenant. [#3946](https://github.com/pnp/powershell/pull/3946)
- Added `-RecycleBinRetentionPeriod`,`-OneDriveBlockGuestsAsSiteAdmin`,`-OneDriveDefaultShareLinkRole`,`-OneDriveDefaultShareLinkScope` and `-OneDriveDefaultLinkToExistingAccess` parameters to the `Set-PnPTenant` cmdlet. [#3977](https://github.com/pnp/powershell/pull/3977)
- Added `Get-PnPTenantRestrictedSearchMode` and `Set-PnPTenantRestrictedSearchMode` cmdlets to enable and set up Restricted SharePoint Search. [#3976](https://github.com/pnp/powershell/pull/3976)
- Added `Get-PnPTenantInternalSetting` cmdlet to retrieve internal tenant settings not exposed via CSOM SDK. [#3902](https://github.com/pnp/powershell/pull/3902)
- Added `Add-PnPHomeSite` cmdlet to add a home site to your tenant. [#3989](https://github.com/pnp/powershell/pull/3989)
- Added `Get-PnPPageSchedulingEnabled` cmdlet to get the state of the modern page schedule feature in the library. [PR](https://github.com/pnp/powershell/commit/4ac757fc2072233529b38b2b39c36ea6b941e003)
- Added `-IncludeSensitivityLabels` parameter to `Get-PnPMicrosoft365Group` cmdlet to retrieve sensitivity labels assigned to M365 Groups. [#3991](https://github.com/pnp/powershell/pull/3991)
- Added `Get-PnPFileSensitivityLabelInfo` cmdlet to retrieve sensitivity label information about a file in a SharePoint site. [#3994](https://github.com/pnp/powershell/pull/3994)
- Added `Get-PnPTenantRestrictedSearchAllowedList` cmdlet to retrieve existing list of URLs in the allowed list. [#3997](https://github.com/pnp/powershell/pull/3997)
- Added `-IsSharePointAddInsDisabled` to the `Set-PnPTenant` cmdlet which allows disabling SharePoint Add-ins [#4032](https://github.com/pnp/powershell/pull/4032)
- Added `-RestrictContentOrgWideSearch`, `-ReadOnlyForUnmanagedDevices` and `-InheritVersionPolicyFromTenant` parameters to `Set-PnPTenantSite` cmdlet. [#4024](https://github.com/pnp/powershell/pull/4024)

### Fixed

- Fixed `Get-PnPChangeLog -Version 2.3.0` not returning the changelog for that version [#3804](https://github.com/pnp/powershell/pull/3804)
- Fixed `Get-PnPFlow` cmdlet throwing time out error due to incorrect URL used in HTTP request. [#3820](https://github.com/pnp/powershell/pull/3820)
- Fixed `Copy-PnPList` cmdlet to better handle lookup columns. [#3870](https://github.com/pnp/powershell/pull/3870)
- Fixed NullDereferenceException happening when an exception is logged in PnPConnectedCmdlet but the connection passed through -Connection parameter is not the last one. [#3885](https://github.com/pnp/powershell/pull/3885)
- Fixed NullDereferenceException in `Get-PnPUserProfileProperty` cmdlet when the user profile doesn't exist, showing a better error message. [#3891](https://github.com/pnp/powershell/pull/3891)
- Fixed the dev build process on Mac OS devices. [#3907](https://github.com/pnp/powershell/pull/3907)
- Fixed `Get-PnPContainer` cmdlet to also handle pagination in case of large no. of containers in a tenant. [#3990](https://github.com/pnp/powershell/pull/3990)
- Fixed `New-PnPTeamsTeam` cmdlet to better handle error specifically such as `Conflict (409): Team already exists`. [#3992](https://github.com/pnp/powershell/pull/3992)
- Fixed `Remove-PnPTeamsChannel` issue where it was throwing incorrect exception. [#4036](https://github.com/pnp/powershell/pull/4036)

### Changed
- Renamed `Get-PnPSiteFileVersionExpirationReportJobProgress` to `Get-PnPSiteFileVersionExpirationReportJobStatus` [#3828](https://github.com/pnp/powershell/pull/3828)
- Renamed `Get-PnPSiteVersionPolicyProgress` to `Get-PnPSiteVersionPolicyStatus` [#3828](https://github.com/pnp/powershell/pull/3828)
- `Remove-PnPGroupMember` cmdlet now supports removing members from pipeline. [#3955](https://github.com/pnp/powershell/pull/3955)
- Changed `Set-PnPTenantCdnPolicy` cmdlet to allow PolicyValue parameter to be an empty string or $null, while still being mandatory. [#3937](https://github.com/pnp/powershell/pull/3937)
- Marked `UserVoiceForFeedbackEnabled` as obsolete in `Set-PnPTenant` cmdlet as Microsoft doesn't support this. [#3985](https://github.com/pnp/powershell/pull/3985)
- `Get-PnPTenantSite` cmdlet now returns additional properties like `ArchiveStatus`, `EnableAutoExpirationVersionTrim` and many more. [#3987](https://github.com/pnp/powershell/pull/3987)
- `Add-PnPListFoldersToSiteTemplate` cmdlet now wont export RoleBindings which are `Limited Access`. It caused issues while applying the template. [#3918](https://github.com/pnp/powershell/pull/3918)

### Removed

- Removed `UserVoiceForFeedbackEnabled` property from `Get-PnPTenant` as it is deprecated. [PR](https://github.com/pnp/powershell/commit/190ef864d2e20249658eff93feadf0effb24882d)

### Contributors

- Maxime Hazebroucq [mhazebroucq]
- Paolo Pialorsi [PaoloPia]
- Marc Studer [Studermarc]
- Mark Gort [markgort86]
- Christian Veenhuis [ChVeen]
- Tobias Maestrini [tmaestrini]
- WCONFR [WCONFR]
- Jenny Wu [msjennywu]
- Reshmee Auckloo [reshme011]
- Aimery Thomas [a1mery]
- Arleta Wanat [PowershellScripts]
- Giacomo Pozzoni [jackpoz]
- [blarrywangmsft]
- Koen Zomers [koenzomers]
- Erwin van Hunen [erwinvanhunen]

## [2.4.0]

### Added

- Added `-IsDataAccessInCardDesignerEnabled` to `Set-PnPTenant` which allows for configuring Viva Connections Adaptive Cards to connect to backend services for their data [#3635](https://github.com/pnp/powershell/pull/3635)
- Added `Remove-PnPContainer` cmdlet to remove the SharePoint embed container. [#3629](https://github.com/pnp/powershell/pull/3629)
- Added `Convert-PnPFile` cmdlet which allows for a file to be converted to from one format to another. [#3435](https://github.com/pnp/powershell/pull/3435) & [#3643](https://github.com/pnp/powershell/pull/3643)
- Added `Merge-PnPTerm` cmdlet which allows merging of one term into another. [#3638](https://github.com/pnp/powershell/pull/3638)
- Added `Get-PnPDeletedContainer` cmdlet which returns a list of all deleted Containers in the recycle bin. [#3648](https://github.com/pnp/powershell/pull/3648)
- Added `-Batch` parameter to `Add-PnPGroupMember` cmdlet which allows adding members to a SharePoint group in a batch. [#3651](https://github.com/pnp/powershell/pull/3651)
- Added `Get-PnPContainerTypeConfiguration` cmdlet which fetches the container type configuration values. [#3660](https://github.com/pnp/powershell/pull/3660)
- Added `-AppBypassInformationBarriers` and `-DefaultOneDriveInformationBarrierMode` parameters to `Set-PnPTenant` cmdlet. [#3679](https://github.com/pnp/powershell/pull/3679)
- Added `Add-PnPFileAnalyticsData` cmdlet to allow retrieval of file analytics data. [#3644](https://github.com/pnp/powershell/pull/3644)
- Added `Add-PnPSiteAnalyticsData` cmdlet to allow retrieval of site analytics data. [#3645](https://github.com/pnp/powershell/pull/3645)
- Added `Get-PnPPowerPlatformSolution` cmdlet to Power Platform solutions. [#3675](https://github.com/pnp/powershell/pull/3675)
- Added `New-PnPContainerType` cmdlet to create a new SharePoint container type. [#3669](https://github.com/pnp/powershell/pull/3669)
- Added `Remove-PnPContainerType` cmdlet which removes a specific container type. [#3689](https://github.com/pnp/powershell/pull/3689/)
- Added `Restore-PnPDeletedContainer` cmdlet which recovers a deleted Container from the Recycle Bin. [#3661](https://github.com/pnp/powershell/pull/3661)
- Added the ModerationSettings to be returned with `Get-PnPTeamsChannel` when passing in `-IncludeModerationSettings` and using `-Identity <channelId>` [#3580](https://github.com/pnp/powershell/pull/3580)
- Added `AllowNewMessageFromBots`, `AllowNewMessageFromConnectors`, `ReplyRestriction` and `UserNewMessageRestriction` to `Set-PnPTeamsChannel` which allows setting the moderation settings on a Teams channel [#3580](https://github.com/pnp/powershell/pull/3580)
- Added `Get-PnPWebPermission` cmdlet which retrieves permission given by user for specific web. [#3685](https://github.com/pnp/powershell/pull/3685)
- Added `-HorizontalQuickLaunch` parameter to `Set-PnPWeb` cmdlet to allow navigation orientation to be horizontal. [#3722](https://github.com/pnp/powershell/pull/3722)
- Added support for different sovereign cloud environment for Power Platform related cmdlets [#3725](https://github.com/pnp/powershell/pull/3725)
- Added `Set-PnPRetentionLabel` and `Reset-PnPRetentionLabel` cmdlets to support setting a retention label on one or more items [#3599](https://github.com/pnp/powershell/pull/3599)
- Added `-SiteThumbnailUrl` parameter to `Set-PnPWebHeader` cmmdlet to support setting thumbnail of the site. [#3746](https://github.com/pnp/powershell/pull/3746)
- Added `-Like` parameter to `Set-PnPPage` cmdlet to support liking/unliking a modern page. [#3788](https://github.com/pnp/powershell/pull/3788)
- Added `Get-PnPPageLikedByInformation` cmdlet to retrieve list of users who liked a modern page. [#3781](https://github.com/pnp/powershell/pull/3781)

### Fixed

- Fixed `Grant-PnPAzureADAppSitePermission` cmdlet which allows it to work in multi-geo environment. [#3658](https://github.com/pnp/powershell/pull/3658)
- Fixed `Get-PnPTeamsChannelMessageReply` cmdlet which didn't work correctly when `-IncludeDeleted` parameter was not specified. [#3676](https://github.com/pnp/powershell/pull/3676)
- Fixed `Add-PnPNavigationNode` cmdlet to also search for nodes in child navigation items. [#3625](https://github.com/pnp/powershell/pull/3625)
- Fixed `Get-PnPFlow` cmdlet to use the newer Flow URLs instead of the old ARM URLs. [#3677](https://github.com/pnp/powershell/pull/3677)
- Fixed `Get-PnPPowerPlatformConnector`, `Get-PnPPowerPlatformEnvironment`, `Get-PnPPowerApp`, `Add-PnPFlowOwner`, `Disable-PnPFlow`, `Enable-PnPFlow`, `Export-PnPFlow`, `Get-PnPFlowOwner`, `Get-PnPFlowRun`, `Remove-PnPFlow`, `Remove-PnPFlowOwner` , `Restart-PnPFlow` and `Stop-PnPFlowRun` cmdlets to use the new HTTP endpoints. [#3687](https://github.com/pnp/powershell/pull/3687)
- Fixed `Add-PnPHubSiteAssociation` cmdlet to allow support for multi-geo scenario. [#3568](https://github.com/pnp/powershell/pull/3568)
- Fixed `Enable/Disable-PnPPageScheduling` cmdlet to also work with Viva connections enabled site. [#3713](https://github.com/pnp/powershell/pull/3713)
- Fixed `Register-PnPManagementShellAccess` and `Register-PnPAzureADApp` cmdlets to also work with custom environment. [#3763](https://github.com/pnp/powershell/pull/3763)
- Fixed `Set-PnPPPage` cmdlet to only change layout of the page if the parameter is specified. [#3777](https://github.com/pnp/powershell/pull/3777)
- Fixed `New-PnPGroup` cmdlet to correctly show the group description with HTML making it similar to `Set-PnPGroup`.

### Changed

- `-IsFavoriteByDefault` parameter is now obsolete in `Add-PnPTeamsChannel` cmdlet due to deprecation by Microsoft Graph API. [#3712](https://github.com/pnp/powershell/pull/3712)
- `Get-PnPSiteTemplate` will now only contain `PersistPublishingFiles`, `IncludeNativePublishingFiles`, `IncludeTermGroupsSecurity`, `IncludeSearchConfiguration`, `SkipVersionCheck` and `PersistMultiLanguageResources` if these are provided with the cmdlet as switch parameters [#3715](https://github.com/pnp/powershell/pull/3715)
- Due to backend changes in Microsoft Graph, `Get-PnPUnifiedAuditLog` cmdlet requires some more permissions. Updated the cmdlet to handle that. [#3745](https://github.com/pnp/powershell/pull/3745)

### Contributors

- Arleta Wanat [PowershellScripts]
- Jenny Wu [msjennywu]
- Aimery Thomas [a1mery]
- Nils Andresen [nils-a]
- Gautam Sheth [gautamdsheth]
- Nishkalank Bezawada [NishkalankBezawada]
- Konrad K. [wilecoyotegenius]
- Leon Armston [LeonArmston]
- Daniel Cecil [danielcecil]
- Rohit Devmore [rohit404404]
- Konrad K. [wilecoyotegenius]
- Kunj Balkrishna Sangani [kunj-sangani]
- Koen Zomers [koenzomers]
- Reshmee Auckloo [reshme011]
- Nishkalank Bezawada [NishkalankBezawada]
- Jørgen Wiik [joHKwi]
- Siddharth Vaghasia [siddharth-vaghasia]
- Jürgen Rosenthal-Buroh [JuergenRB]

## [2.3.0]

### Added

- Added `-MediaTranscription` and `-MediaTranscriptionAutomaticFeatures` to `Set-PnPTenant` which allows for configuring the media transcription settings. [#3238](https://github.com/pnp/powershell/pull/3238)
- Added `-Includes` option to `Get-PnPListItem` which allows for specifying additional fields to be retrieved. [#3270](https://github.com/pnp/powershell/pull/3270)
- Added `-AllowCommentsTextOnEmailEnabled` parameter to `Set-PnPTenant` which allows including the surrounding document context in email notification when user is mentioned in document comments. [#3268](https://github.com/pnp/powershell/pull/3268)
- Added `Export-PnPPowerApp` cmdlet which will export a specified PowerApp as zip package. [#2990](https://github.com/pnp/powershell/pull/2990)
- Added `AzureADLoginEndPoint` and `MicrosoftGraphEndPoint` parameters to `Connect-PnPOnline` cmdlet for use in custom Azure environments. [#2925](https://github.com/pnp/powershell/pull/2925)
- Added `Get-PnPFolder` cmdlet without any parameters to return the folder representing the root of the current web [#3319](https://github.com/pnp/powershell/pull/3319)
- Added `Get-PnPFileInFolder` cmdlet which allows retrieval of all files in a folder or site and optionally request additional properties from them [#3319](https://github.com/pnp/powershell/pull/3319)
- Added `Get-PnPFolderInFolder` cmdlet which allows retrieval of all folders in a folder or site and optionally request additional properties from them [#3319](https://github.com/pnp/powershell/pull/3319)
- Added `-SharingStatus` parameter to `Get-PnPFlow` which allows for filtering flows based on their sharing status. [#3287](https://github.com/pnp/powershell/pull/3287)
- Added `-AzureADLoginEndPoint` and `-MicrosoftGraphEndPoint` parameters to `Connect-PnPOnline` cmdlet for use in custom Azure environments. [#2925](https://github.com/pnp/powershell/pull/2925)
- Added `SiteOwnerManageLegacyServicePrincipalEnabled` parameter to `Set-PnPTenant` cmdlet. With this parameter site owners will not be able to register/update apps unless the tenant admin explicitly allows it. [#3318](https://github.com/pnp/powershell/pull/3318)
- Added `-EnableAutoExpirationVersionTrim`, `-ExpireVersionsAfterDays`, `-MajorVersions`, `-MinorVersions`, `-InheritTenantVersionPolicySettings`, `-StartApplyVersionPolicySettingToExistingDocLibs` and `-CancelApplyVersionPolicySettingToExistingDocLibs` to `Set-PnPSite` to allow for time based version expiration on the site level [#3373](https://github.com/pnp/powershell/pull/3373)
- Added `-ReduceTempTokenLifetimeEnabled`, `-ReduceTempTokenLifetimeValue`, `-ViewersCanCommentOnMediaDisabled`, `-AllowGuestUserShareToUsersNotInSiteCollection`, `-ConditionalAccessPolicyErrorHelpLink`, `-CustomizedExternalSharingServiceUrl`, `-IncludeAtAGlanceInShareEmails` and `-MassDeleteNotificationDisabled` to `Set-PnPTenant` [#3348](https://github.com/pnp/powershell/pull/3348)
- Added `Add-PnPFlowOwner` and `Remove-PnPFlowOwner` cmdlets which allow granting or removing permissions to a Power Automate flow [#3343](https://github.com/pnp/powershell/pull/3343)
- Added `Get-PnPFlowOwner` cmdlet which allows retrieving the owners of a Power Automate flow [#3314](https://github.com/pnp/powershell/pull/3314)
- Added `-AvailableForTagging` to `Set-PnPTerm` which allows the available for tagging property on a Term to be set [#3321](https://github.com/pnp/powershell/pull/3321)
- Added `Get-PnPPowerPlatformConnector` cmdlet which allows for all custom connectors to be retrieved [#3309](https://github.com/pnp/powershell/pull/3309)
- Added `Set-PnPSearchExternalItem` cmdlet which allows ingesting external items into the Microsoft Search index for custom connectors. [#3420](https://github.com/pnp/powershell/pull/3420)
- Added `Get-PnPTenantInfo` which allows retrieving tenant information by its Id or domain name [#3414](https://github.com/pnp/powershell/pull/3414)
- Added option to create a Microsoft 365 Group with dynamic membership by passing in `-DynamicMembershipRule` [#3426](https://github.com/pnp/powershell/pull/3426)
- Added option to pass in a Stream or XML string to `Read-PnPTenantTemplate` allowing the tenant template to be modified before being applied. [#3431](https://github.com/pnp/powershell/pull/3431)
- Added `Get-PnPTenantInfo` which allows retrieving tenant information by its Id or domain name. [#3414](https://github.com/pnp/powershell/pull/3414)
- Added option to create a Microsoft 365 Group with dynamic membership by passing in `-DynamicMembershipRule` [#3426](https://github.com/pnp/powershell/pull/3426)
- Added `Get-PnPSiteVersionPolicy` which allows retrieval of the version policy settings for a site [#3470](https://github.com/pnp/powershell/pull/3470)
- Added `RestrictedAccessControl`, `ClearRestrictedAccessControl`, `RemoveRestrictedAccessControlGroups`, `AddRestrictedAccessControlGroups` and `RestrictedAccessControlGroups` parameters to `Set-PnPTenantSite` cmdlet to handle restricted access control. [#3463](https://github.com/pnp/powershell/pull/3463)
- Added `Get-PnPRetentionLabel` cmdlet to retrieve Purview retention labels. [#3459](https://github.com/pnp/powershell/pull/3459)
- Added GCC support for `Get-PnPAzureADUser` , `Add-PnPFlowOwner` , `Remove-PnPFlowOwner`, `Sync-PnPSharePointUserProfilesFromAzureActiveDirectory`, `New-PnPAzureADUserTemporaryAccessPass` and `Get-PnPAvailableSensitivityLabel` cmdlets. [#3484](https://github.com/pnp/powershell/pull/3484)
- Added `-Detailed` option to `Get-PnPTenantDeletedSite` to optionally fetch more information on the deleted sites [#3550](https://github.com/pnp/powershell/pull/3550)
- Added a devcontainer for easily building a minimal environment necessary to contribute to the project. [#3497](https://github.com/pnp/powershell/pull/3497)
- Added `-RelativeUrl` parameter to `Connect-PnPOnline` cmdlet to allow specifying custom URLs for usage with `-WebLogin` method. [#3530](https://github.com/pnp/powershell/pull/3530)
- Added `-RetryCount` to `Submit-PnPSearchQuery` which allows for specifying the number of retries to perform when an exception occurs [#3528](https://github.com/pnp/powershell/pull/3528)
- Added `-MailNickname` parameter to `Set-PnPMicrosoft365Group` cmdlet to allow changing of this property on a Microsoft 365 Group [#3529](https://github.com/pnp/powershell/pull/3529)
- Added `-SanNames` to `New-PnPAzureCertificate` which allows for controlling the Subject Alternative Names set on the generated certificate [#3555](https://github.com/pnp/powershell/pull/3555)
- Added Information Barriers information to the output of `Get-PnPTenantSite` [#3556](https://github.com/pnp/powershell/pull/3556)
- Added `RequestFilesLinkEnabled` and `RequestFilesLinkExpirationInDays` to the output of `Get-PnPSite` [#3557](https://github.com/pnp/powershell/pull/3557)
- Added `CoreRequestFilesLinkEnabled`, `CoreRequestFilesLinkExpirationInDays`, `OneDriveRequestFilesLinkEnabled`, `OneDriveRequestFilesLinkExpirationInDays`, `BusinessConnectivityServiceDisabled` to the output of `Get-PnPTenant` [#3557](https://github.com/pnp/powershell/pull/3557)
- Added `-BusinessConnectivityServiceDisabled` parameter to `Set-PnPTenant` cmdlt to allow disabling the Business Connectivity Service [#3562](https://github.com/pnp/powershell/pull/3562)
- Added support for executing the 'Invoke-PnPSPRestMethod' cmdlet in a batch [#3565](https://github.com/pnp/powershell/pull/3565)
- Added `Get-PnPSiteSetVersionPolicyProgress` cmdlet which allows for getting the progress of setting a version policy for existing document libraries on a site [#3564](https://github.com/pnp/powershell/pull/3564)
- Added `EnableSensitivityLabelForPDF` to `Set-PnPTenant` and `Get-PnPTenant` [#3581](https://github.com/pnp/powershell/pull/3581)
- Changed `Restore-PnPRecycleBinItem` , made `-Identity` parameter as non-mandatory. [#2499](https://github.com/pnp/powershell/pull/2499)
- Added the ability to set Manage and FullControl permissions directly when using Sites.Selected with `Grant-PnPAzureADAppSitePermission` [#3617](https://github.com/pnp/powershell/pull/3617)
- Added `Remove-PnPMicrosoft365GroupPhoto` cmdlet which allows removal of profile picture of M365 Group. [#3607](https://github.com/pnp/powershell/pull/3607)

### Fixed

- Fixed `Add-PnPContentTypeToList` cmdlet to better handle piped lists. [#3244](https://github.com/pnp/powershell/pull/3244)
- Fixed `Get-PnPUserProfileProperty` cmdlet not allowing basic user profile properties to be retrieved using `-Properties` [#3247](https://github.com/pnp/powershell/pull/3247)
- Fixed `Export-PnPTermGroupToXml` cmdlet issue with exporting site collection term groups. [#3256](https://github.com/pnp/powershell/pull/3256)
- Fixed `Register-PnPAzureADApp` cmdlet issue with creation of Azure AD application. [#3265](https://github.com/pnp/powershell/pull/3265)
- Fixed `Get-PnPServiceHealthIssue` cmdlet issue with null reference objection. [#3286](https://github.com/pnp/powershell/pull/3286)
- Fixed `New-PnPSite` cmdlet issue with team site creation when using a connection object. [#3285](https://github.com/pnp/powershell/pull/3285)
- Fixed issue with colliding assemblies when using PnP PowerShell with other modules. [#3280](https://github.com/pnp/powershell/pull/3280)
- Fixed issue with `-ArchiveRedirectUrl` parameter not working correctly in `Set-PnPTenant` cmdlet. [#3289](https://github.com/pnp/powershell/pull/3289)
- Fixed `New-PnPAzureADGroup` cmdlet throwing null reference error when owners and members are not specified. [#3308](https://github.com/pnp/powershell/pull/3308)
- Fixed `New-PnPTeamsTeam` cmdlet, it will now throw error if it fails to teamify a Microsoft 365 group. [#3310](https://github.com/pnp/powershell/pull/3310)
- Fixed `Get-PnPFolderItem` cmdlet not accepting piping a folder instance to it [#3319](https://github.com/pnp/powershell/pull/3319)
- Fixed `Get-PnPFolderItem` cmdlet not working well with the `-Recursive` and `-Identity` parameters provided together [#3319](https://github.com/pnp/powershell/pull/3319)
- Fixed `Get-PnPFolderItem` cmdlet not working well with the `-ItemName` parameter [#3319](https://github.com/pnp/powershell/pull/3319)
- Fixed `Connect-PnPOnline` cmdlet throwing host not reachable errors. [#3337](https://github.com/pnp/powershell/pull/3337)
- Fixed `Set-PnPTerm` cmdlet throwing object reference error when only the term Id is specified. [#3341](https://github.com/pnp/powershell/pull/3341)
- Fixed `New-PnPTeamsTeam` cmdlet throwing an error when specifying members [#3351](https://github.com/pnp/powershell/pull/3351)
- Fixed `New-PnPTeamsTeam` cmdlet not working well with a managed identity [#3351](https://github.com/pnp/powershell/pull/3351)
- Fixed `Copy-PnPFile`, `Copy-PnPFolder` and `Move-PnPFile` to better handle copying or moving operations to OneDrive or Multi-geo environments. [#3245](https://github.com/pnp/powershell/pull/3245)
- Fixed `Get-PnPTenantTemplate` not doing anything when the `-SiteUrl` parameter had not been specified. It will now use the currently connected site when the parameter is omitted. [#3431](https://github.com/pnp/powershell/pull/3431)
- Fixed `Enable-PnPPageScheduling` and `Disable-PnPPageScheduling` cmdlets not working due to changes in backend code. [#3469](https://github.com/pnp/powershell/pull/3469)
- Fixed an issue when trying to download a file using `Get-PnPFile` from a location that's deeply nested into folders and/or has a really long filename [PnP Core #1290](https://github.com/pnp/pnpcore/pull/1290)
- Fixed retrieving error detail in `Get-UPABulkImportStatus` cmdlet. [#3494](https://github.com/pnp/powershell/pull/3494)
- Fixed `Rename-PnPTenantSite` cmdlet to allow support for vanity tenant URLs. [#3533](https://github.com/pnp/powershell/pull/3533)
- Fixed `Get-PnPAzureADUser`, `Get-PnPEntraIDUser`, `Add-PnPFlowOwner` and `Remove-PnPFlowOwner` not working when an UPN containing an apostrophe was passed in [#3570](https://github.com/pnp/powershell/pull/3570)

### Changed

- Improved `Set-PnPListItem` cmdlet handling of Purview labels. [#3340](https://github.com/pnp/powershell/pull/3340)
- The `Publish-PnPCompanyApp` cmdlet is now obsolete. It will be removed in the next version. [#3349](https://github.com/pnp/powershell/pull/3349)
- Verbose output will no longer show the access token [#3352](https://github.com/pnp/powershell/pull/3352)
- Improved `Add-PnPFile` cmdlet. It will now automatically checkout the file if `-CheckinType` parameter is specified. [#3403](https://github.com/pnp/powershell/pull/3403)
- Improved the error message thrown when using `-ValidateConnection` with `Connect-PnPOnline` and it failing due to i.e. an expired ClientSecret so the reason of the failed connect becomes more clear. [#3440](https://github.com/pnp/powershell/pull/3440)
- If a cmdlet gets renamed and an alias gets added for it for backwards compatibility, a cmdlet page for the alias will automatically be created so it can still be found in the documentation [#3455](https://github.com/pnp/powershell/pull/3455)
- Improved `Remove-PnPFlow` cmdlet to throw error if the Flow doesn't exist and also added verbose logging. [#3474](https://github.com/pnp/powershell/pull/3474)
- Changed `Get-PnPContentType` to now also support `-Includes` to allow retrieval of additional properties of the content type [#3518](https://github.com/pnp/powershell/pull/3518)
- `Get-PnPTeamsTeam` cmdlet throws error message if the team isn't found when `-Identity` parameter is specified. [#3502](https://github.com/pnp/powershell/pull/3502)
- Improved `Get-PnPSiteCollectionAdmin ` cmdlet to allow retrieval of additional properties when `-Includes` parameter is specified. [#3521](https://github.com/pnp/powershell/pull/3521)

### Removed

- Removed `-DisableListSync` and `-SyncAadB2BManagementPolicy` from `Set-PnPTenant` as the underlying properties have been removed from SharePoint CSOM as well [#3388](https://github.com/pnp/powershell/pull/3388)

### Contributors

- Pieter Veenstra [Pieter-Veenstra]
- Konrad K. [wilecoyotegenius]
- Dan Cecil [danielcecil]
- Antti K. Koskela [koskila]
- Christian Veenhuis [ChVeen]
- Kunj Balkrishna Sangani [kunj-sangani]
- Dave Paylor [paylord]
- [smsdaniel]
- Jim Duncan [sparkitect]
- Jonathan Smith [jonathan-m-smith]
- Carl Joakim Damsleth [damsleth]
- Rodel Pacurib [ryder-cayden]
- [CatSchneider]
- [msjennywu]
- Reshmee Auckloo [reshmee011]
- Per Østergaard [per-oestergaard]
- Nishkalank Bezawada [NishkalankBezawada]
- [PowerBugi]
- Ganesh Sanap [ganesh-sanap]
- Siddharth Vaghasia [siddharth-vaghasia]
- Giacomo Pozzoni [jackpoz]
- Martin Lingstuyl [martinlingstuyl]
- Arleta Wanat [PowershellScripts]
- Nils Andresen [nils-a]
- Koen Zomers [koenzomers]

## [2.2.0]

### Added

- Added `DisableDocumentLibraryDefaultLabeling`, `DisableListSync`, `IsEnableAppAuthPopUpEnabled`, `ExpireVersionsAfterDays`, `MajorVersionLimit` and `EnableAutoExpirationVersionTrim`, `OneDriveLoopSharingCapability`, `OneDriveLoopDefaultSharingLinkScope`, `OneDriveLoopDefaultSharingLinkRole`, `CoreLoopSharingCapability`, `CoreLoopDefaultSharingLinkScope`, `CoreLoopDefaultSharingLinkRole` , `DisableVivaConnectionsAnalytics` , `CoreDefaultLinkToExistingAccess`, `HideSyncButtonOnTeamSite` , `CoreBlockGuestsAsSiteAdmin`, `IsWBFluidEnabled`, `IsCollabMeetingNotesFluidEnabled`, `AllowAnonymousMeetingParticipantsToAccessWhiteboards`, `IBImplicitGroupBased`, `ShowOpenInDesktopOptionForSyncedFiles` , `OneDriveRequestFilesLinkExpirationInDays` and `ShowPeoplePickerGroupSuggestionsForIB` parameters to the `Set-PnPTenant` cmdlet. [#2979](https://github.com/pnp/powershell/pull/2979) and [#3015](https://github.com/pnp/powershell/pull/3015)
- Added `-OutFile` to `Invoke-PnPGraphMethod` which allows for the response to be written to a file [#2971](https://github.com/pnp/powershell/pull/2971)
- Added `-OutStream` to `Invoke-PnPGraphMethod` which allows for the response to be written to a memory stream [#2976](https://github.com/pnp/powershell/pull/2976)
- Added `-PreviousNode` to `Add-PnPNavigationNode` which allows for adding a navigation node after a specific node [#2940](https://github.com/pnp/powershell/pull/2940)
- Added `EnableAzureADB2BIntegration` to be returned by `Get-PnPTenant` [#3022](https://github.com/pnp/powershell/pull/3022)
- Added `-SkipUrlValidation` to `Get-PnPSiteCollectionAppCatalog` which allows for skipping the URL validation when retrieving the site collection app catalog making it faster but potentially returning URLs that have been renamed [#2305](https://github.com/pnp/powershell/pull/3025)
- Added `Get-PnPLargeListOperationStatus` cmdlet to retrieve the status of a large list operation. [#3033](https://github.com/pnp/powershell/pull/3033)
- Added `-BlockDownloadFileTypePolicy`, `-BlockDownloadFileTypeIds` and `-ExcludedBlockDownloadGroupIds` parameters to `Set-PnPTenant` cmdlet. [#3081](https://github.com/pnp/powershell/pull/3081)
- Added `-BlockDownloadPolicy`, `-ExcludeBlockDownloadPolicySiteOwners` and `ExcludedBlockDownloadGroupIds` parameters to `Set-PnPTenantSite` and `Set-PnPSite` cmdlets. [#3084](https://github.com/pnp/powershell/pull/3084)
- Added `-OpenInNewTab` parameter to `Add-PnPNavigationNode` cmdlet to allow links to be opened in a new tab. [#3094](https://github.com/pnp/powershell/pull/3094)
- Added `-ArchiveRedirectUrl` to `Set-PnPTenant` allowing the configuration of a custom page to be shown when navigating to an archived SharePoint Online site [#3100](https://github.com/pnp/powershell/pull/3100)
- Added `-BlockSendLabelMismatchEmail` to `Set-PnPTenant` allowing the warning e-mail being sent when uploading a file with a higher sensitivity label than the site it is being uploaded to to be disabled. [#3113](https://github.com/pnp/powershell/pull/3113)
- Added `Move-PnPTerm` and `Move-PnPTermSet` cmdlets to allow moving the terms and termsets. [#2989](https://github.com/pnp/powershell/pull/2989)
- Added `-VerticalZoneEmphasis` parameter to `Add-PnPPageSection` cmdlet to allow setting the emphasis value for vertical columns. [#3129](https://github.com/pnp/powershell/pull/3129)
- Added `-AllowDeletion` parameter to `Set-PnPList` cmdlet to allow or prevent deletion of list from the SharePoint UI. [#3142](https://github.com/pnp/powershell/pull/3142)
- Added `EnableClassicAudienceTargeting` and `EnableModernAudienceTargeting` parameters to `Set-PnPList` cmdlet to enable audience targeting in lists. [#3153](https://github.com/pnp/powershell/pull/3153)
- Added `-Attachments` parameter to `Send-PnPMail` cmdlet to allow sending attachments via Microsoft Graph API. [#3157](https://github.com/pnp/powershell/pull/3157)
- Added cmdlets for list item version history: `Get-PnPListItemVersion`, `Restore-PnPListItemVersion` and `Remove-PnPListItemVersion`. [#3086](https://github.com/pnp/powershell/pull/3086)
- Added `Add-PnPFileOrganizationalSharingLink` and `Add-PnPFolderOrganizationalSharingLink` cmdlets to allow creating organizational sharing links for files and folders. [#3177](https://github.com/pnp/powershell/pull/3177)
- Added `Add-PnPFileUserSharingLink` and `Add-PnPFolderUserSharingLink` cmdlets to allow creating sharing links for files and folders to share them with specified list of users. [#3178](https://github.com/pnp/powershell/pull/3178)
- Added `Get-PnPFileSharingLink` and `Get-PnPFolderSharingLink` cmdlets to retrieve sharing links for files and folders respectively. [#3181](https://github.com/pnp/powershell/pull/3181)
- Added `Add-PnPFileAnonymousSharingLink` and `Add-PnPFolderAnonymousSharingLink` cmdlets to create anonymous sharing links. [#3198](https://github.com/pnp/powershell/pull/3198)
- Added `Add-PnPFileSharingInvite` and `Add-PnPFolderSharingInvite` cmdlets to invite users to a file or a folder. [#3199](https://github.com/pnp/powershell/pull/3199)
- Added `Remove-PnPFileSharingLink` and `Remove-PnPFolderSharingLink` cmdlets to delete sharing links associated with files and folders. [#3200](https://github.com/pnp/powershell/pull/3200)
- Added `Get-PnPUnfurlLink` cmdlet to support unfurling a link to get more information about the link. [#3224](https://github.com/pnp/powershell/pull/3224)
- Added `ListsShowHeaderAndNavigation` parameter to always show lists with site elements intact when specified in `Set-PnPTenantSite` and `Set-PnPSite` cmdlets. [#3233](https://github.com/pnp/powershell/pull/3233)
- Added `-AzureADWorkloadIdentity` parameter to `Connect-PnPOnline` cmdlet to support Azure AD Workload Identity authentication. [#3097](https://github.com/pnp/powershell/pull/3097)
- Added Managed identity support for Power Platform related cmdlets. [#3097](https://github.com/pnp/powershell/pull/3097)
- Added `-ShowInPublishDate` parameter to `Set-PnPPage` to allow the publication date to be shown or hidden on a page [#3417](https://github.com/pnp/powershell/pull/3417)

### Fixed

- Fixed issue with `Grant-PnPAzureADAppSitePermission` cmdlet where users are not able to set selected site in the `Sites.Selected` permission. [#2983](https://github.com/pnp/powershell/pull/2983)
- Fixed issue with `Get-PnPList` cmdlet not working with site-relative URL as identity. [#3005](https://github.com/pnp/powershell/pull/3005)
- Fixed issue with `Add-PnPNavigationNode` cmdlet where the target audience would not correctly be set when creating a node as a child of a parent node [#2940](https://github.com/pnp/powershell/pull/2940)
- Fixed regressions within the following cmdlets `Get-PnPTenantCdnEnabled`, `Get-PnPTenantCdnOrigin`, `Get-PnPTenantCdnPolicies`, `Get-PnPTenantDeletedSite`, `Get-PnPTenantInstance` [#3030](https://github.com/pnp/powershell/pull/3030)
- Fixed issue where `Add-PnPSiteCollectionAdmin -PrimarySiteCollectionAdmin <user>` would require owners to be specified as well. [#3035](https://github.com/pnp/powershell/pull/3035)
- Fixed `Get-PnPAzureADGroup` returns only 100 results even if there are more groups present in Azure AD. [#3085](https://github.com/pnp/powershell/pull/3085)
- Fixed `Get-PnPAccessToken` cmdlet to correctly retrieve the access token for the specified resource URL. [#3091](https://github.com/pnp/powershell/pull/3091)
- Fixed issue with `Get-PnPTenantSyncClientRestriction` cmdlet not populating the necessary properties. [#3099](https://github.com/pnp/powershell/pull/3099)
- Fixed `Add/Set/Get-PnPPage` cmdlets when using multilingual translation parameters which caused some exceptions. [#3120](https://github.com/pnp/powershell/pull/3120)
- Fixed `New-PnPSite` cmdlet now supports creating Team site in non-commercial cloud environments. [#885](https://github.com/pnp/pnpframework/pull/885)
- Fixed issue where `Get-PnPField` cmdlet was throwing error in case `-Includes` parameter was used. [#3225](https://github.com/pnp/powershell/pull/3225)
- Fixed issue with `Add-PnPDataRowsToSiteTemplate` cmdlet where it would throw property not initialized error when using `-IncludeSecurity` parameter. [#3226](https://github.com/pnp/powershell/pull/3226)

### Changed

- Improved `Set-PnPSite` cmdlet when using the `ScriptSafeDomainName` parameter. If a domain is already existing, it will now throw a warning. [#3073](https://github.com/pnp/powershell/pull/3073)
- Change `Set-PnPWebhookSubscription` to use the same default expiration date as `Add-PnPWebhookSubsription` cmdlet which is 180 days instead of 6 months.[#3106](https://github.com/pnp/powershell/pull/3106)
- Improved `Set-PnPHomePage` cmdlet to handle forward slash issue. [#3128](https://github.com/pnp/powershell/pull/3128)
- Improved `Connect-PnPOnline` cmdlet to handle ping on the destination server to check if it exists. [PR](https://github.com/pnp/powershell/commit/cc3c5564fca9ce96b1a2ac47c7aabdc8b90136d0) and [#3154](https://github.com/pnp/powershell/pull/3154)
- Improved `Invoke-PnPGraphMethod` cmdlet to show a better error message when sufficient permissions are not available. [#3133](https://github.com/pnp/powershell/pull/3133)
- Improved `Add-PnPDataRowsToSiteTemplate` cmdlet to retrieve list item with pagination. [#3152](https://github.com/pnp/powershell/pull/3152)
- Improved error message when caused using Graph endpoints used via PnP Core SDK. [#3179](https://github.com/pnp/powershell/pull/3179)
- Improved `Add-PnPDataRowsToSiteTemplate` cmdlet performance to not load and execute every list item. [#3226](https://github.com/pnp/powershell/pull/3226)

### Removed

- Removed alias `Get-PnPSiteCollectionAppCatalogs` for `Get-PnPSiteCollectionAppCatalog` [#2305](https://github.com/pnp/powershell/pull/3025)
- Removed alias `Export-PnPClientSidePage`, please use `Export-PnPPage` cmdlet. [#3158](https://github.com/pnp/powershell/pull/3158)
- Removed alias `Export-PnPClientSidePageMapping`, please use `Export-PnPPageMapping` cmdlet. [#3158](https://github.com/pnp/powershell/pull/3158)
- Removed alias `Get-PnPClientSideComponent`, please use `Get-PnPPageComponent` cmdlet. [#3158](https://github.com/pnp/powershell/pull/3158)
- Removed alias `Remove-PnPClientSideComponent`, please use `Remove-PnPPageComponent` cmdlet. [#3158](https://github.com/pnp/powershell/pull/3158)
- Removed alias `Set-PnPClientSideText`, please use `Set-PnPPageTextPart` cmdlet. [#3158](https://github.com/pnp/powershell/pull/3158)
- Removed alias `Set-PnPClientSideWebPart`, please use `Set-PnPPageWebPart` cmdlet. [#3158](https://github.com/pnp/powershell/pull/3158)

### Contributors

- Arleta Wanat [PowershellScripts]
- Michał Romiszewski [mromiszewski]
- Kasper Larsen [kasperbolarsen]
- Ganesh Sanap [ganesh-sanap]
- Giacomo Pozzoni [jackpoz]
- James Eccles [jameseccles]
- Kunj Balkrishna Sangani [kunj-sangani]
- Dayana Hristova [makarovv]
- Rodrigo Pinto [ScoutmanPt]
- Reshmee Auckloo [reshmee011]
- Dan Toft [Tanddant]
- [reusto]
- [dhiabedoui]
- Koen Zomers [koenzomers]

## [2.1.1]

### Fixed

- Fixed an issue with the signing of the PnP PowerShell 2.1.0 release

## [2.1.0]

### Added

- Added support for `.NET 6.0` since `.NET Core 3.1` support is getting deprecated. We have **removed** support for .NET Core 3.1, so users will have to update from `PowerShell 7.0.x` to `PowerShell 7.2.x or later` [#2292](https://github.com/pnp/powershell/pull/2292)
- Added default table formatted output for `Get-PnPVivaConnectionsDashboardACE`
- Added `Get-PnPAzureADServicePrincipal` which allows for service principals/application registrations to be retrieved from Azure Active Directory [#2551](https://github.com/pnp/powershell/pull/2551)
- Added `Get-PnPAzureADServicePrincipalAssignedAppRole` which allows for the assigned app roles of a service principal/application registration to be retrieved from Azure Active Directory [#2551](https://github.com/pnp/powershell/pull/2551)
- Added `Get-PnPAzureADServicePrincipalAvailableAppRole` which allows for the available app roles of a service principal/application registration to be retrieved from Azure Active Directory [#2551](https://github.com/pnp/powershell/pull/2551)
- Added `Add-PnPAzureADServicePrincipalAppRole` which allows for app roles to be assigned to a service principal/application registration in Azure Active Directory [#2551](https://github.com/pnp/powershell/pull/2551)
- Added `Remove-PnPAzureADServicePrincipalAppRole` which allows for app roles to be removed from a service principal/application registration in Azure Active Directory [#2551](https://github.com/pnp/powershell/pull/2551)
- Added latest added SharePoint Online Site Templates to `Set-PnPBuiltInSiteTemplateSettings` allowing them to be hidden or shown [#2586](https://github.com/pnp/powershell/pull/2586)
- Added `-EnableAzureADB2BIntegration` and `-SyncAadB2BManagementPolicy` parameters to `Set-PnPTenant` [#2631](https://github.com/pnp/powershell/pull/2631)
- Added `-ShowInFiltersPane` to `Set-PnPField` which allows fields to be shown or hidden in the filters pane [#2623](https://github.com/pnp/powershell/pull/2632)
- Added `-KeyColumn` to `Add-PnPDataRowsToSiteTemplate` which allows for overwriting existing list items in a site template [#2616](https://github.com/pnp/powershell/pull/2616)
- Added `Get-PnPFolderStorageMetric` which allows storage usage of a specific folder to be retrieved [#2646](https://github.com/pnp/powershell/pull/2646)
- Added `IsTeamsConnected`, `IsTeamsChannelConnected` and `TeamChannelType` to be returned when `Get-PnPTenantSite` cmdlet is executed. [#2656](https://github.com/pnp/powershell/pull/2656)
- Added `HTTP/2` support for all HTTP requests to improve performance for HTTP requests. [#2687](https://github.com/pnp/powershell/pull/2687)
- Added `-EnvironmentVariable` parameter to `Connect-PnPOnline` to connect using Azure environment variables. [#2681](https://github.com/pnp/powershell/pull/2681)
- Added `ResponseHeadersVariable` parameter to the `Invoke-PnPSPRestMethod` which if specified will store the response headers values in the PowerShell variable name that is specified. [#2711](https://github.com/pnp/powershell/pull/2711)
- Added `-Filter` parameter to `Get-PnPAzureADServicePrincipal` cmdlet to retrieve specific application registrations based on filter conditions. It supports simple and advanced queries. [#2710](https://github.com/pnp/powershell/pull/2710)
- Added `-Filter` parameter to `Get-PnPMicrosoft365Group` cmdlet to retrieve specific M365 groups based on filter conditions. It supports simple and advanced queries. [#2709](https://github.com/pnp/powershell/pull/2709)
- Added `-CoreRequestFilesLinkExpirationInDays` and `-CoreRequestFilesLinkEnabled` to `Set-PnPTenant` and `-RequestFilesLinkExpirationInDays` to `Set-PnPSite` to allow configuration of the new receive files to SharePoint Online folder feature [#2719](https://github.com/pnp/powershell/pull/2719)
- Added `PSVersion` tracking, which tracks the PowerShell version being used. [#Commit](https://github.com/pnp/powershell/commit/1c6c787883cb45f65c217f7fc68969e44ec73283)
- Added `-Bcc` option to `Send-PnPMail` [#2726](https://github.com/pnp/powershell/pull/2726)
- Added `-AudienceIds` option to `Add-PnPNavigationNode` to allow setting the audience for a navigation node [#2736](https://github.com/pnp/powershell/pull/2736)
- Added `-PrimarySiteCollectionAdmin` to `Add-PnPSiteCollectionAdmin` to allow for the primary site collection admin to be set on the current site [#2750](https://github.com/pnp/powershell/pull/2750)
- Added `-PrimarySiteCollectionAdmin` to `Set-PnPTenantSite` to allow for the primary site collection admin to be set on a provided site [#2750](https://github.com/pnp/powershell/pull/2750)
- Added additional fallback logic for retrieving tokens in Azure VM scenario using well-know endpoint when using Managed Identity authentication. [#2761](https://github.com/pnp/powershell/pull/2761)
- Added `-IsVivaConnectionsDefaultStartForCompanyPortalSiteEnabled` parameter to `Get-PnPHomeSite` which returns information on whether Viva Connections landing experience is set to the SharePoint home site. [#2779](https://github.com/pnp/powershell/pull/2779)
- Added `-VivaConnectionsDefaultStart` parameter to `Set-PnPHomeSite` which sets the home site to the provided site collection url and keeps the Viva Connections landing experience to the SharePoint home site. [#2779](https://github.com/pnp/powershell/pull/2779)
- Added `-LargeList` parameter to `Remove-PnPList` cmdlet which improves the list recycling experience for Lists containing huge number of items. [#2778](https://github.com/pnp/powershell/pull/2778)
- Added support for specifying the ContentUrl and WebsiteUrl configuration in `Add-PnPTeamsTab` cmdlet when trying to add a SharePoint page or list as a tab in Teams channel. [#2807](https://github.com/pnp/powershell/pull/2807)
- Added `-CheckinType` parameter to `Add-PnPFile` cmdlet which provides the option to specify the checkin type for a file. The default value is set to `MinorCheckIn`. [#2806](https://github.com/pnp/powershell/pull/2806)
- Added `-DefaultSensitivityLabelForLibrary` to `Set-PnPList` which allows setting the default sensitivity label for a library. [#2825](https://github.com/pnp/powershell/pull/2825)
- Added `-ApplicationId` as alias for `-ClientId` in `Connect-PnPOnline` and `Request-PnPAccessToken` cmdlets. [#2805](https://github.com/pnp/powershell/pull/2805)
- Added `-Connection` option to `Connect-PnPOnline` which allows of reusing an authenticated connection to connect to a different site [#2821](https://github.com/pnp/powershell/pull/2821)
- Added `-UserAssignedManagedIdentityAzureResourceId` and `-UserAssignedManagedIdentityClientId` as alternatives to `-UserAssignedManagedIdentityObjectId` for `Connect-PnPOnline -ManagedIdentity` to provide an user managed identity to authenticate with. [#2813](https://github.com/pnp/powershell/pull/2813)
- Added clearer error message when connecting using an expired client secret and trying to execute a command.[#2828](https://github.com/pnp/powershell/pull/2828)
- Added `Undo-PnPFileCheckedOut` which allows a checked out file to discard its changes and revert to the last checked in version. [#2837](https://github.com/pnp/powershell/pull/2837)
- Added option for sending e-mail using Microsoft Graph and through a SMTP server of choice in the `Send-PnPMail` cmdlet [#2880](https://github.com/pnp/powershell/pull/2880)
- Added `-OpenDocumentsMode` option to `Set-PnPList` which allows configuring if documents should be opened in the browser or in the local client [#2873](https://github.com/pnp/powershell/pull/2873)
- Added `-Properties` parameter to `Get-PnPUserProfileProperty` cmdlet which allows retrieval of specific properties if specified. [#2840](https://github.com/pnp/powershell/pull/2840)
- Added support for specifying the `-ContentUrl` configuration in `Add-PnPTeamsTab` cmdlet when trying to add a Planner as a tab in Teams channel. [#2850](https://github.com/pnp/powershell/pull/2850)
- Added `Get-PnPSharePointAddIn` cmdlet to retrive list of SharePoint addins installed in the site collection. [#2920](https://github.com/pnp/powershell/pull/2920)
- Added `Get-PnPAzureACSPrincipal` cmdlet to retrieve list of installed Azure ACS Principals in the site collection or tenant. [#2920](https://github.com/pnp/powershell/pull/2920)
- Added `-LogoFilePath` parameter to `Register-PnPAzureADApp` cmdlet to allow setting the logo for the Azure AD app. [#2881](https://github.com/pnp/powershell/pull/2881)
- Added support for `-Verbose` in `Move-PnPFile` which will show if it has problems determining if the destination location is a folder or a file [#2888](https://github.com/pnp/powershell/pull/2888)
- Added `CalendarMemberReadOnly` and `ConnectorsDisabled` as `-ResourceBehaviorOptions` to `New-PnPMicrosoft365Group` [#2929](https://github.com/pnp/powershell/pull/2929)
- Added `-Identity` option to `Get-PnPPowerPlatformEnvironment` which allows retrieval of one specific environment by its displayname or id. [#2794](https://github.com/pnp/powershell/pull/2794)
- Added `Get-PnPPowerApp` which allows PowerApps to be retrieved [#2794](https://github.com/pnp/powershell/pull/2794)
- Added `-DisableCommenting` to `Set-PnPList` which allows enabling or disabling commenting on a list. [#2939](https://github.com/pnp/powershell/pull/2939)
- Added `-EnableAutoExpirationVersionTrim` and `-ExpireVersionsAfterDays` to `Set-PnPList` which allows enabling or disabling auto expiration of versions on a list or library based on the days passed. [#2869](https://github.com/pnp/powershell/pull/2869)
- Added `-Detailed` parameter to `Get-PnPHomeSite` which will return detailed information on all home sites configured on the tenant [#2954](https://github.com/pnp/powershell/pull/2954)

### Changed

- `Update-PnPVivaConnectionsDashboardACE` is now called `Set-PnPVivaConnectionsDashboardACE` but `Update-PnPVivaConnectionsDashboardACE` will still work as an alias.
- All `Set|Get|Remove-PnPVivaConnectionsDashboardACE` cmdlets now take either the InstanceId (preferred), Title, the Id, or an actual ACE for the Identity property. If multiple ACEs of the same id or with the same title are found a warning will be shown if Id or Title is used to find the ACE.
- Made PropertiesJSON an optional parameter on `Add-PnPVivaConnectionsDashboardACE` as it is not always required when adding an ACE
- Added a 10 second timeout on the new version check on `Connect-PnPOnline` to prevent the cmdlet from hanging when the connection is slow, GitHub being blocked by a firewall or GitHub being unavailable [#2550](https://github.com/pnp/powershell/pull/2550)
- Improved `Add-PnPField`, `Get-PnPListItem` and `Get-PnPSiteDesignRun` cmdlets by improving null checks based on warnings from compiler. [#PR1](https://github.com/pnp/powershell/commit/791b031d5fa844f1e6961b1136df9f79f19bfdcd) and [#PR2](https://github.com/pnp/powershell/commit/d56f3cd497be79170f68b29be490b222bf042aaa)
- Improved `Register-PnPAzureADApp` and `Register-PnPManagementShellAccess` cmdlets to reuse existing HTTP client instead of creating a new one. [#2682](https://github.com/pnp/powershell/pull/2682)
- Improved `Register-PnPAzureADApp` cmdlet based on compiler warnings. [#2682](https://github.com/pnp/powershell/pull/2682)
- `Connect-PnPOnline` will now throw a much clearer error message if the site to be connected doesn't exist when using the legacy Client Id with Secret (ACS) authentication mode. [#2707](https://github.com/pnp/powershell/pull/2707)
- Properties of `Get-PnPAzureADServicePrincipal` are now all typed instead of some of them returning unparsed JSON fragments. [#2717](https://github.com/pnp/powershell/pull/2717)
- Changed `Add-PnPTeamsChannel` to no longer require an `-OwnerUPN` to be provided when specifying `-ChannelType Standard` [#2786](https://github.com/pnp/powershell/pull/2786)
- Changed `Add-PnPFile` by default to upload a file as a draft with a minor version now instead of publishing it as a major version. `-CheckinType MajorCheckIn` can be used to still upload the file as a major published version [#2806](https://github.com/pnp/powershell/pull/2806)
- Changed `Send-PnPMail` to support `-Verbose` and provide feedback if sending the e-mail has failed [#2880](https://github.com/pnp/powershell/pull/2880)
- Improved `Restore-PnPRecycleBinItem` and `Clear-PnPRecycleBinItem` cmdlets to better work with large items in recycle bin. [#2866] (https://github.com/pnp/powershell/pull/2866)
- Changed `Get-PnPUserProfileProperty` to no longer return additional user profile properties under UserProfileProperties but instead directly on the returned instance. [#2840](https://github.com/pnp/powershell/pull/2840)

### Removed

- Removed support for PowerShell 5, only PowerShell 7.2 and later will be supported from here onwards [#2764](https://github.com/pnp/powershell/pull/2764)
- Removed `Get-PnPSubscribeSharePointNewsDigest` and `Set-PnPSubscribeSharePointNewsDigest` as the implementation behind these features has been changed in SharePoint Online causing them no longer to work. At present, there's no alternative for this that we can call into thus we will have to remove these in a future version. There is a Design Change Request open with the Program Group to add back APIs for doing this. If that will be accepted and implemented, we will add these back again. [#2720](https://github.com/pnp/powershell/pull/2720)
- Removed `-ReturnTyped` parameter from the `Get-PnPField` cmdlet. The retrieved fields will always be returned by their `TypeKind`. [#2849](https://github.com/pnp/powershell/pull/2849)
- Removed alias `Get-PnPFlowEnvironment` from `Get-PnPPowerPlatformEnvironment`. Please use the latter going forward. [#2794](https://github.com/pnp/powershell/pull/2794)
- Marked `BlockEdit` and `BlockDeletion` parameters as obsolete in `Set-PnPLabel` cmdlet. [#2934](https://github.com/pnp/powershell/pull/2934)

### Fixed

- Fixed issue with -CreateDrive on `Connect-PnPOnline` throwing exception on non-existing context
- Fixed issue with non-existing ItemProxy cmdlet aliases being registered
- Fixed issue with `-TranslationLanguageCode` failures in `Add-PnPPage` and `Set-PnpPage` cmdlets. [#2634](https://github.com/pnp/powershell/pull/2634)
- Fixed issue with `Export-PnPUserInfo` and `Remove-PnPUserInfo` cmdlets not working due to issue with parameter validation. [#2688](https://github.com/pnp/powershell/pull/2688)
- Fixed issue with `Add-PnPNavigationNode` not always showing the new navigation node without requiring a manual edit and save of the menu first [#2736](https://github.com/pnp/powershell/pull/2736)
- Fixed issue with `Get-PnPFolder` ignoring `-Includes` parameter when passing in a specific list through `-List` [#2735](https://github.com/pnp/powershell/pull/2735)
- Fixed the handling of `-ErrorAction` so it follows the standard PowerShell behavior [#2741](https://github.com/pnp/powershell/pull/2741)
- Fixed issue with `Set-PnPContentType` not allowing you to update basic properties of a content type [#2760](https://github.com/pnp/powershell/pull/2760)
- Fixed `Add-PnPField` not supporting a ReturnType to be set for calculated fields when created on the site level [#2765](https://github.com/pnp/powershell/pull/2765)
- Fixed issue with `Invoke-PnPSPRestMethod` throwing error when the response string is empty. [#2784](https://github.com/pnp/powershell/pull/2784)
- Removed `Get-PnPSubscribeSharePointNewsDigest` and `Set-PnPSubscribeSharePointNewsDigest` cmdlet as the implementation behind these features has been changed in SharePoint Online causing them no longer to work. At present, there's no alternative for this that we can call into.
- Fixed issue with `Invoke-PnPSPRestMethod` and `Invoke-PnPGraphMethod` throwing error when passing complex JSON object as payload. [#2802](https://github.com/pnp/powershell/pull/2802)
- Fixed issue with `Add-PnPListItem` and `Set-PnPListItem` not correctly setting the Purview `Unlocked by default`. [#2800](https://github.com/pnp/powershell/pull/2800)
- Fixed issue with `Get-PnPListItem` cmdlet not respecting `RowLimit` in the CAML query. [#2804](https://github.com/pnp/powershell/pull/2804)
- Fixed `Connect-PnPOnline -ManagedIdentity -UserAssignedManagedIdentityClientId` not working in Azure Automation Runbooks as it required usage of the object_id parameter instead of the principal_id to get an access token. [#2813](https://github.com/pnp/powershell/pull/2813)
- Fixed issue with `Send-PnPMail` not being able to send out e-mail due to the deprecation of basic authentication on Exchange Online [#2880](https://github.com/pnp/powershell/pull/2880)
- Fixed `Register-PnPAzureADApp` cmdlet to not change or generate certificate if `-CertificatePath` parameter is already specified. [#2878](https://github.com/pnp/powershell/pull/2878)
- Fixed `New-PnPSite` cmdlet to work with non-commercial cloud environments.
- Fixed `Set-PnPSearchSettings` cmdlet not working with vanity domain tenants [#2884](https://github.com/pnp/powershell/pull/2884)
- Fixed `Add-PnPFieldFromXml` cmdlet. It will now return the correct typed field if the added field was of type `Taxonomy`. [#2926](https://github.com/pnp/powershell/pull/2926)
- Fixed `New-PnPSitetemplateFromFolder` removing the first character of filenames [#2944](https://github.com/pnp/powershell/pull/2944)

### Contributors

- Sumit Kumar [sumitkumar0608]
- [msjennywu]
- [enthusol]
- Chris R. [ChrisRo89]
- Aimery Thomas [a1mery]
- Ganesh Sanap [ganesh-sanap]
- Markus Hanisch [m-hanisch]
- Kasper Larsen [kasperbolarsen]
- Arnaud Rompen [rompenar]
- [reusto]
- Ronald Mavarez [ronaldmavarez]
- [lilealdai]
- Martin Lingstuyl [martinlingstuyl]
- Reshmee Auckloo [reshmee011]
- Arleta Wanat [PowershellScripts]
- Leon Armston [LeonArmston]
- Robin Meure [robinmeure]
- Rohit Varghese [rohitvarghese96]
- Erwin van Hunen [erwinvanhunen]
- Marc Studer [studermarc]
- [vin-ol]
- Koen Zomers [koenzomers]

## [1.12.0]
### Added

- Added `-DisableGridEditing` option to `Set-PnPList` which allows gridview editing to be enabled or disabled on a list [#2188](https://github.com/pnp/powershell/pull/2188)
- Added verbose logging for `Invoke-PnPSiteSwap`, `Restore-PnPTenantSite` and cmdlets which depend on `SpoOperation`. [#2207](https://github.com/pnp/powershell/pull/2207)
- Added support for `DisplayNamesOfFileViewers` and `DisplayNamesOfFileViewersInSpo` properties in `Get-PnPTenant` and `Set-PnPTenant` cmdlets to show/hide viewers in property pane for a file. [#2271](https://github.com/pnp/powershell/pull/2271)
- Added `MailEnabled`, `PreferredDataLocation`, `PreferredLanguage` and `SecurityEnabled` parameters to `New-PnPMicrosoft365Group` cmdlet. [#2268](https://github.com/pnp/powershell/pull/2268)
- Added `-DraftVersionVisibility` parameter to the `Set-PnPList` cmdlet to specify draft item security for list items. [#2285](https://github.com/pnp/powershell/pull/2285)
- Added support for `-ErrorAction:Stop` to PnP PowerShell cmdlets. Notice that if you were using this in combination with the specific try/catch [System.Management.Automation.PSInvalidOperationException], it will no longer catch the exception. It will throw an `System.Management.Automation.ErrorRecord` exception instead. Remove the `-ErrorAction:Stop` parameter from your cmdlet or catch this new exception type to avoid this behavior. [#2288](https://github.com/pnp/powershell/pull/2288)
- Added ability to create shared Teams channels using `Add-PnPTeamsChannel -ChannelType Shared` [#2308](https://github.com/pnp/powershell/pull/2308)
- Added support for `IsLoopEnabled` properties in `Get-PnPTenant` and `Set-PnPTenant` cmdlets to to enable/disable loop components in the tenant. [#2307](https://github.com/pnp/powershell/pull/2307)
- Added support for `SubscribeMembersToCalendarEventsDisabled` resource behavior option in `New-PnPMicrosoft365Group` and `New-PnPTeamsTeam` cmdlet. [#2349](https://github.com/pnp/powershell/pull/2349)
- Added `-OneDriveRequestFilesLinkEnabled` option to `Set-PnPTenant` to allow configuring the request files anonymously feature on the tenant level [#2360](https://github.com/pnp/powershell/pull/2360)
- Added `-RequestFilesLinkEnabled` option to `Set-PnPSite` to allow configuring the request files anonymously feature on a per site collection level [#2360](https://github.com/pnp/powershell/pull/2360)
- Added `ScriptSafeDomainName` option to `Set-PnPSite` to allow contributors to insert iframe from specified domains only. [#2363](https://github.com/pnp/powershell/pull/2363)
- Added `AlertTemplateName` paramter to `Add-PnPAlert` to allow configuring the Alert Template type name in the email. [#2362](https://github.com/pnp/powershell/pull/2362)
- Added `Get-PnPAzureADActivityReportDirectoryAudit` to retrieve the audit logs generated by Azure AD. [#2095](https://github.com/pnp/powershell/pull/2095)
- Added `-Path` option to `Set-PnPList` which allows the url of a list to be changed within the same site [#2381](https://github.com/pnp/powershell/pull/2381)
- Added `-Force` option to `Set-PnPListem` to force it to update a list item even without changing something. Can be useful in i.e. triggering a webhook. [#2396](https://github.com/pnp/powershell/pull/2396)
- Added `ImageUrl`, `PageImageAlignment`, `ImageHeight` and `ImageWidth` parameters to `Add-PnPPageTextPart` cmdlet so that users can add an inline image into a text webpart. [#2401](https://github.com/pnp/powershell/pull/2401)
- Added `TextBeforeImage` and `TextAfterImage` parameters to `Add-PnPPageTextPart` cmdlet so that users can add before and after text for an inline image into a text webpart. [#2403](https://github.com/pnp/powershell/pull/2403)
- Added `Add-PnPPageImageWebPart` cmdlet to allow users to easily add Image to a modern page. [#2406](https://github.com/pnp/powershell/pull/2406)
- Added system assigned Managed Identity support for SharePoint Online cmdlets. [#2354](https://github.com/pnp/powershell/pull/2354)
- Added user assigned Managed Identity support for Microsoft Graph and SharePoint Online cmdlets. [#2491](https://github.com/pnp/powershell/pull/2491)
- Added `Get-PnPTeamsTag` cmdlet to retrieve Team tags information. [#2414](https://github.com/pnp/powershell/pull/2414)
- Added `Properties` attribute to `Update-PnPVivaConnectionsDashboardACE` to allow for updating the properties of a Viva Connections dashboard ACE component using its typed properties [#2433](https://github.com/pnp/powershell/pull/2433)
- Added `Set-PnPTeamsTag` cmdlet to update Team tags information. [#2419](https://github.com/pnp/powershell/pull/2419)
- Added `Remove-PnPTeamsTag` cmdlet to delete a Team tag. [#2419](https://github.com/pnp/powershell/pull/2419)
- Added `Disable-PnPPowerShellTelemetry` cmdlet to disable telemetry collection. [#2432](https://github.com/pnp/powershell/pull/2432)
- Added `Enable-PnPPowerShellTelemetry` cmdlet to enable telemetry collection. [#2432](https://github.com/pnp/powershell/pull/2432)
- Added `Get-PnPAzureADActivityReportSignIn` cmdlet to enable retrieving of Azure AD sign ins. [#2436](https://github.com/pnp/powershell/pull/2436)
- Added support to remove the site collection app catalog by using Id of the site collection in `Remove-PnPSiteCollectionAppCatalog` cmdlet. [#2452](https://github.com/pnp/powershell/pull/2452)
- Added support for the `EnableRestrictedAccessControl` parameter to `Set-PnPTenant` and `RestrictedAccessControl` for `Set-PnPSite` to restrict site access to members of a Microsoft 365 group. [#2462](https://github.com/pnp/powershell/pull/2462)
- Added `Set-PnPImageListItemColumn` cmdlet to support setting of the new image/thumbnail value for a SharePoint list item.[#2468](https://github.com/pnp/powershell/pull/2468)
- Added `-Filter` parameter to `Get-PnPTeamsTeam` cmdlet to retrieve specific teams based on filter conditions. It supports simple and advanced queries. [#2467](https://github.com/pnp/powershell/pull/2467) , [#2474](https://github.com/pnp/powershell/pull/2474)
- Added `Get-PnPMicrosoft365ExpiringGroup` cmdlet to retrieve Microsoft 365 groups which are nearing expiration.[#2466](https://github.com/pnp/powershell/pull/2466)
- Added additional parameters to `Set-PnPContentType` cmdlet to support SPFx form customizer related properties.[#2456](https://github.com/pnp/powershell/pull/2456)
- Added `-Filter` parameter to `Get-PnPAzureADApp` cmdlet to retrieve specific Azure AD apps based on filter conditions. It suppports simple and advanced queries. [#2477](https://github.com/pnp/powershell/pull/2477)
- Added `Get-PnPDeletedTeam` cmdlet to retrieve all deleted Microsoft Teams teams [#2487](https://github.com/pnp/powershell/pull/2487)
- Added `-ServerRelativePath` and `-Path` parameters to `Set-PnPImageListItemColumn` cmdlet to allow for file to be uploaded for the Image type column. [#2503](https://github.com/pnp/powershell/pull/2503)
- Added support for sovereign tenants in `Get-PnPTenandId` by utilizing the `-AzureEnvironment` parameter. [#2512](https://github.com/pnp/powershell/pull/2512)
- Added `Set-PnPTeamsTeamPicture` which allows setting the picture of a Teams team [#3590](https://github.com/pnp/powershell/pull/3590)

### Changed

- Changed to no longer require `https://` to be prefixed when using `Connect-PnPOnline -Url tenant.sharepoint.com` [#2139](https://github.com/pnp/powershell/pull/2139)
- `Get-PnPAvailableSensitivityLabel` cmdlet now uses the non-deprecated Graph API to retrieve sensitivity label. [#2234](https://github.com/pnp/powershell/pull/2234)
- Improved `Get-PnPMicrosoft365Group` cmdlet to better check the Id, DisplayName and MailNickname of Microsoft 365  Group. [#2258](https://github.com/pnp/powershell/pull/2258)
- Improved `Get-PnPStorageEntity` cmdlet when `Key` parameter is specified. [#2275](https://github.com/pnp/powershell/pull/2275)
- Improved `Get-PnPAuthenticationRealm` cmdlet to use `HTTP Client` instead of `WebRequest`. [#2304](https://github.com/pnp/powershell/pull/2304)
- Changed `Get-PnPRoleDefinition` so that it now also supports the role definition Id to be used with `-Identity` as well as the role definition name [#2336](https://github.com/pnp/powershell/pull/2336)
- Creating private Teams channels formerly using  `Add-PnPTeamsChannel -Private` should now use `Add-PnPTeamsChannel -ChannelType Private` instead [#2308](https://github.com/pnp/powershell/pull/2308)
- Improved `Get-PnPAuthenticationRealm` cmdlet to use `HTTPClient` instead of `WebRequest`. [#2304](https://github.com/pnp/powershell/pull/2304)
- Improved `Connect-PnPOnline` with ACS method. Replace the usage of `WebRequest` with `HTTPClient`. [#2352](https://github.com/pnp/powershell/pull/2352)
- Improved `Remove-PnPFieldFromContentType` cmdlet to ensure proper null check for non-existing fields. It will now throw proper `Field not found` error. [#2407](https://github.com/pnp/powershell/pull/2407)
- Changed the Microsoft 365 Groups cmdlets to use the `v1.0` endpoint instead of the `beta` [#2426](https://github.com/pnp/powershell/pull/2426)
- Changed `Add-PnPMicrosoft365GroupToSite` to longer require the `-Url` parameter to be specified. If its not provided, the currently connected to site will be groupified. [#2496](https://github.com/pnp/powershell/pull/2496)

### Removed

- Marked `-Resource` parameter from `Get-PnPAccessToken` cmdlet as obsolete as it was not used anymore anyway. It will be removed in a future version. [#2182](https://github.com/pnp/powershell/pull/2182)
- Removed `-SystemUpdate` option from `Set-PnPListItem` as it has been deprecated two years ago. Use `-UpdateType SystemUpdate` instead [#2396](https://github.com/pnp/powershell/pull/2396)
- Removed `-Force` parameter from `New-PnPTenantSite`. It was marked obsolete and not used anymore in the code.
- Removed `-BlockDownloadOfNonViewableFiles` parameter from `Set-PnPTenantSite` cmdlet. It was marked obsolete. Instead use `-AllowDownloadingNonWebViewableFiles` parameter.
- Removed `-NoTelemetry` and `-NoVersionCheck` parameters from `Connect-PnPOnline` cmdlet. They were marked obsolete. Instead you need to specify `PNP_DISABLETELEMETRY` and `PNPPOWERSHELL_UPDATECHECK` as environment variable.
- Removed `-Connection` parameter from `Disconnect-PnPOnline` cmdlet. For more information on how to deal with this, please read [this documentation](https://pnp.github.io/powershell/cmdlets/Disconnect-PnPOnline.html).
- Removed `-Web` parameter support for cmdlets that are currently using it. Instead you should use `Connect-PnPOnline` to connect to a specific Web instance like a sub-site or a site under a sub-site.
- Removed `-Resource` parameter from `Get-PnPAccessToken` cmdlet. It was marked as obsolete and not used anymore.
- Removed `-SkipSourceFolderName` parameter from `Copy-PnPFile` cmdlet. It was marked as obsolete and not used anymore.
- Removed `-ExcludeSiteUrl` , `-IncludeClassification` and `-IncludeHasTeam` parameters from `Get-PnPMicrosoft365Group` cmdlet. They were marked as obsolete. The site URL(s) are excluded by default, instead use `-IncludeSiteUrl` parameter if you want to retrieve site URL(s). The classification will always be retrieved. The `HasTeam` value is always retrieved.
- The `UserType` and `Environment` properties will not be retrieved when using `Get-PnPFlow` and `Remove-PnPFlow` cmdlets. They were marked as obsolete. You should used `Properties.Creator.UserType` and `EnvironmentDetails` instead.
- The `OnlyAllowMembersViewMembership` and `SetAssociatedGroup` parameters have been removed. They were already marked as obsolete. You should use `-DisallowMembersViewMembership` parameter to disallow group members viewing membership and use `Set-PnPGroup` cmdlet to set groups as associated groups.
- Removed `-Owner` paramter from `New-PnPTeamsTeam` cmdlet. It was marked as obsolete. You should instead use `-Owners` parameter.
- Removed support for `2019-03` version of the PnP Provisioning Schema from `Convert-PnPSiteTemplate` , `New-PnPSiteTemplateFromFolder` and other cmdlets which are using this schema. It was already marked as deprecated in the PnP Provisioning engine. You should use a newer version of the schema.
- Removed `-NoBaseTemplate` parameter from `Get-PnPSiteTemplate` cmdlet. It was marked as obsolete. It will not use the default web base template of the connected site.
- Removed `-Private` parameter from `Add-PnPTeamsChannel` cmdlet. It was marked as obsolete. You should use `TeamMembershipType` parameter instead.


### Fixed

- Fixed issue where passing in `-Connection` to `Disconnect-PnPOnline` would throw an exception [#2093](https://github.com/pnp/powershell/pull/2093)
- Fixed `Get-PnPSiteSearchQueryResults` throwing `Value cannot be null` exception [#2138](https://github.com/pnp/powershell/pull/2138)
- Fixed `New-PnPUPABulkImportJob` not returing the job Id [#2144](https://github.com/pnp/powershell/pull/2144)
- Fixed `Get-PnPSiteCollectionAppCatalog` throwing an exception when the site was deleted [#2201](https://github.com/pnp/powershell/pull/2201)
- Fixed `Set-PnPTermGroup` throwing an exception even when the group existed. [#2232](https://github.com/pnp/powershell/pull/2232)
- Fixed `Remove-PnPFile` cmdlet parameter set error. [#2230](https://github.com/pnp/powershell/pull/2230)
- Fixed `Get-PnPAccessToken` cmdlet to be able to work with different site collections as well as require an actual connection first. [#2270](https://github.com/pnp/powershell/pull/2270)
- Fixed `Copy-PnPList` cmdlet to be able to copy the list structure to the destination web. [#2313](https://github.com/pnp/powershell/pull/2313)
- Fixed `Add-PnPField` cmdlet , it was throwing null reference error when `-Type` was not specified and after the prompt you entered the correct type. [#2338](https://github.com/pnp/powershell/pull/2338)
- Fixed regression issue with `New-Microsoft365Group` cmdlet. [#2349](https://github.com/pnp/powershell/pull/2349)
- Fixed issue with `Add-PnPTaxonomyField`, it was throwing error when using `-TaxonomyItemId` parameter. [#2351](https://github.com/pnp/powershell/pull/2351)
- Fixed `Import-PnPTermGroupFromXml` issue where a valid template was not working. [#2353](https://github.com/pnp/powershell/pull/2353)
- Fixed `Set-PnPTenant` cmdlet not working when `-Force` parameter is specified. [#2373](https://github.com/pnp/powershell/pull/2373)
- Fixed `Add-PnPTeamsTab` cmdlet not working with certain types when using dynamic parameters. [#2405](https://github.com/pnp/powershell/pull/2405)
- Fixed `Get-PnPVivaConnectionsDashboardACE` missing the `isVisible` property under `CardButtonActions` causing using `Update-PnPVivaConnectionsDashboardACE` to hide card buttons [#2433](https://github.com/pnp/powershell/pull/2433
- Fixed issue with `Set-PnPTeamsChannel -IsFavoriteByDefault` throwing a `Nullable object must have a value` under certain circumstances [#2425](https://github.com/pnp/powershell/pull/2425)
- Fixed `Register-PnPManagementShellAccess` for non-commercial cloud environment. Users must enter the tenant name if the environment is a non-commercial cloud environment. [#2437](https://github.com/pnp/powershell/pull/2437)
- Fixed issue with writing warning or error messages in Azure automation or screens with small width. [#2438](https://github.com/pnp/powershell/pull/2438)
- Fixed issue with `Enable-PnPTenantServicePrincipal` not respecting `-Force` parameter. [#2448](https://github.com/pnp/powershell/pull/2448)
- Fixed issue with `Get-PnPRecycleBinItem` not working when there are large number of items in recycle bin.[#2472](https://github.com/pnp/powershell/pull/2472)
- Fixed Microsoft Graph based cmdlets not showing detailed error results when a call fails [#2490](https://github.com/pnp/powershell/pull/2490)
- Fixed `Restore-PnPRecycleBinItem` cmdlet not working with `-RowLimit` parameter. [#2499](https://github.com/pnp/powershell/pull/2499)
- Fixed cmdlets throwing error when `-ErrorAction SilentlyContinue` was specified. [#2510](https://github.com/pnp/powershell/pull/2510)
- Fixed `Get-PnPAzureADAppSitePermission` not returning the roles assigned to each permission [#2523](https://github.com/pnp/powershell/pull/2523)
- Fixed `Add-PnPListItem` cmdlet issue when using `-Batch` cmdlet, we were not able to set the SharePoint group in people or group fields. [#2879] (https://github.com/pnp/powershell/pull/2879)

### Contributors

- Valeras Narbutas [ValerasNarbutas]
- Russell Gove [russgove]
- Jasper Beerens
- Aleksandr Sapozhkov [shurick81]
- James Eccles [jameseccles]
- Martin Lingstuyl [martinlingstuyl]
- Antti K. Koskela [koskila]
- Dan Toft [tandddant]
- Yannick Plenevaux [ypcode]
- Rob Lempens [RobLempens]
- Marc Studer [Studermarc]
- Giacomo Pozzoni [jackpoz]
- Adam Wójcik [Adam-it]
- reusto
- Mikael Svenson [wobba]
- Josef Benda [SmarterJB]
- Alex Grover [groveale]
- Nik Charlebois [NikCharlebois]
- Milan Holemans [milanholemans]
- Miguel A. Tena [mikewaretena]
- Reshmee Auckloo [reshmee011]
- Leon Armston [LeonArmston]
- Giacomo Pozzoni [jackpoz]
- James May [fowl2]
- Jimmy Hang [JimmyHang]
- Marcus Blennegård [mblennegard]
- Arleta Wanat [PowerShellScripts]
- Koen Zomers [koenzomers]

## [1.11.0]

### Added

- Added `-Wait` and `-Verbose` optional paramarers to `New-PnPUPABulkImportJob` [#1752](https://github.com/pnp/powershell/pull/1752)
- Added `Add-PnPTeamsChannelUser` which allows members and owners to be added to private channels in Teams [#1735](https://github.com/pnp/powershell/pull/1735)
- Added `Channel` parameter to `Add-PnPTeamsUser` cmdlet which if specified will allow owners and members to be added to private channels in a Teams Team. [#1772](https://github.com/pnp/powershell/pull/1772)
- Added the ability to retrieve site collection information by its Id using `Get-PnPTenantSite -Identity <id>` [#1766](https://github.com/pnp/powershell/pull/1766)
- Added `ResourceBehaviorOptions` option in `New-PnPMicrosoft365Group` cmdlet to set `ResourceBehaviorOptions` while provisioning a Microsoft 365 Group. [#1774](https://github.com/pnp/powershell/pull/1774)
- Added `Add-PnPTeamsChannelUser` which allows members and owners to be added to private channels in Teams [#1735](https://github.com/pnp/powershell/pull/1735)
- Added `ExcludeVisualPromotedResults` parameter to `Get-PnPSearchConfiguration` which excludes promoted results [#1750](https://github.com/pnp/powershell/pull/1750)
- Added `MediaTranscription` parameter to `Set-PnPTenantSite` and `Set-PnPSite` cmdlets which when enabled allows videos to have transcripts generated on demand or generated automatically in certain scenarios.
- Added `-SensitivityLabels` parameter to `New-PnPTeamsTeam` and `New-PnPMicrosoft365Group` cmdlets to apply sensitivity label to the Microsoft 365 Group and Team.
- Added `-SensitivityLabels` parameter to `Set-PnPMicrosoft365Group` cmdlets to apply sensitivity label to the Microsoft 365 Group and Team.
- Added `MediaTranscription` parameter to `Set-PnPTenantSite` and `Set-PnPSite` cmdlets which when enabled allows videos to have transcripts generated on demand or generated automatically in certain scenarios
- Added `Get-PnPTeamsChannelFilesFolder` cmdlet to retrieve metadata for the location where files of a Teams channel are stored. [#1799](https://github.com/pnp/powershell/pull/1799)
- Added `Get-PnPVivaConnectionsDashboardACE` to retrieve the Adaptive Card extensions from the Viva connections dashboard page. [#1805](https://github.com/pnp/powershell/pull/1805)
- Added `Add-PnPVivaConnectionsDashboardACE` to add an Adaptive Card extension to the Viva connections dashboard page. [#1805](https://github.com/pnp/powershell/pull/1805)
- Added `Update-PnPVivaConnectionsDashboardACE` to update an Adaptive Card extension in the Viva connections dashboard page. [#1805](https://github.com/pnp/powershell/pull/1805)
- Added `Remove-PnPVivaConnectionsDashboardACE` to remove an Adaptive Card extension in the Viva connections dashboard page. [#1805](https://github.com/pnp/powershell/pull/1805)
- Added `Accept` parameter to `Invoke-PnPSPRestMethod` cmdlet which if specified will pass the Accept HTTP request header. [#1795](https://github.com/pnp/powershell/pull/1795)
- Added `Get-PnPFlowRun` cmdlet to retrieve a specific run, or all runs from a specific Power Automate flow. [#1819](https://github.com/pnp/powershell/pull/1819)
- Added `Invoke-PnPGraphMethod` cmdlet to invoke generic Microsoft Graph API Methods. [#1820](https://github.com/pnp/powershell/pull/1820)
- Added `TimeZone` parameter to `New-PnPSite` cmdlet which allows setting of the site collection in the specified timezone.
- Added `Stop-PnPFlowRun` cmdlet to stop/cancel a specific Power Automate flow run. [#1838](https://github.com/pnp/powershell/pull/1838)
- Added `Remove-PnPTeamsChannelUser` cmdlet to remove a user from a private channel. [#1840](https://github.com/pnp/powershell/pull/1840)
- Added `Get-PnPListItemPermission` cmdlet to retrieve item level permissions. [#1534](https://github.com/pnp/powershell/pull/1534)
- Added `Get-PnPTeamsChannelMessageReply` to retrieve all replies or a specific reply of a message in a Teams channel [#1885](https://github.com/pnp/powershell/pull/1885)
- Added `-Identity` parameter to `Get-PnPTeamsChannelMessage` cmdlet to retrieve a specific message [#1887](https://github.com/pnp/powershell/pull/1887)
- Added new `PnP.PowerShell` image which also gets published to Docker Hub. [#1580](https://github.com/pnp/powershell/pull/1794)
- Added capability to Debug the module in Visual Studio. [#1880](https://github.com/pnp/powershell/pull/1880)
- Added `Set-PnPTeamsChannelUser` cmdlet to update the role of user in a private channel. [#1865](https://github.com/pnp/powershell/pull/1865)
- Added `Restart-PnPFlowRun` which allows for a failed Power Automate flow run to be retried [#1915](https://github.com/pnp/powershell/pull/1915)
- Added optional `-Connection` parameter to `Get-PnPConnection`, `Get-PnPContext` and `Set-PnPContext` which allows for using any of these for a specific connection [#1919](https://github.com/pnp/powershell/pull/1919)
- Added `-IncludeDeprecated` parameter to `Get-PnPTerm` cmdlet to fetch deprecated terms if specified [#1903](https://github.com/pnp/powershell/pull/1903)
- Added `-IncludeContentType` parameter, which if specified will retrieve content type information of the list items. [#1921](https://github.com/pnp/powershell/pull/1921)
- Added optional `-ValidateConnection` to `Connect-PnPOnline` which will check if the site you are connecting to exists and if not, will throw an exception [#1924](https://github.com/pnp/powershell/pull/1924)
- Added `-Description` and `-Priority` to `Set-PnPPlannerTask` [#1947](https://github.com/pnp/powershell/pull/1947)
- Added `AllowTenantMoveWithDataMigration` to `Get-PnPPlannerConfiguration` and `Set-PnPPlannerConfiguration` [#1934](https://github.com/pnp/powershell/pull/1934)
- Added the ability to retrieve a Planner plan by only its Id using `Get-PnPPlannerPlan -Identity <id>` [#1935](https://github.com/pnp/powershell/pull/1935)
- Added `Add-PnPListItemAttachment` cmdlet to provide ability to upload a file as an attachment to a SharePoint list item. [#1932](https://github.com/pnp/powershell/pull/1932)
- Added `Remove-PnPListItemAttachment` cmdlet to provide ability to delete a list item attachment. [#1932](https://github.com/pnp/powershell/pull/1932)
- Added `Get-PnPListItemAttachment` cmdlet to download the attachments from a list item. [#1932](https://github.com/pnp/powershell/pull/1932)
- Added `-ReturnTyped` parameter to `Get-PnPField` cmdlet so that it returns specific type instead of the generic field type. [#1888](https://github.com/pnp/powershell/pull/1888)
- Added `Add-PnPViewsFromXML` cmdlet to create one or more views in a list based on an XML string. [#1963](https://github.com/pnp/powershell/pull/1963)
- Added `-ExemptFromBlockDownloadOfNonViewableFiles` parameter to `Set-PnPList` cmdlet to configure access capabilites for unmanaged devices at list level. [#1973](https://github.com/pnp/powershell/pull/1973)
- Added `-PercentComplete`, `-Priority`, `-StartDateTime`, `-DueDateTime` and `-Description` to `Add-PnPPlannerTask` [#1964](https://github.com/pnp/powershell/pull/1964)
- Added `Set-PnPContentType` cmdlet to update the properties of the Content Types in a list or a web. [#1981](https://github.com/pnp/powershell/pull/1981)
- Added `-SharingCapability` parameter to the `New-PnPTenantSite` cmdlet to update the Sharing capabilties of the newly provisioned classic site collection. [#1994](https://github.com/pnp/powershell/pull/1994)
- Added optional `-IncludeAllLists` to `Get-PnPSiteScriptFromWeb` which will include the JSON definition of all custom lists of the current site in the output [#1987](https://github.com/pnp/powershell/pull/1987)
- Added `-UpdateChildren` parameter to `Add-PnPFieldToContentType` cmdlet. This allows users to skip pushing the fields to child content types. [#1992](https://github.com/pnp/powershell/pull/1992)
- Added optional `-SensitivityLabel` to `Set-PnPSite` which allows for a Microsoft Purview sensitivitylabel to be set [#2024](https://github.com/pnp/powershell/pull/2024)
- Added `-UpdateChildren` parameter to `Add-PnPFieldToContentType` cmdlet. This allows users to skip pushing the fields to child content types. [#1092](https://github.com/pnp/powershell/pull/1992)
- Added `Get-PnPAvailableSensitivityLabel` cmdlet to retrieve Microsoft Purview sensitivity labels available on the tenant [#2023](https://github.com/pnp/powershell/pull/2023)
- Added `Get-PnPSiteSensitivityLabel` cmdlet to retrieve the Microsoft Purview sensitivity label set on the current site [#2036](https://github.com/pnp/powershell/pull/2036)
- Added `Set-PnPSiteClassification` cmdlet which allows setting a classic site classification on the current site [#2036](https://github.com/pnp/powershell/pull/2036)
- Added `Set-PnPSiteSensitivityLabel` cmdlet which allows setting a Microsoft Purview sensitivity label on the current site [#2036](https://github.com/pnp/powershell/pull/2036)
- Added `Remove-PnPSiteSensitivityLabel` cmdlet which allows removing the Microsoft Purview sensitivity label from the current site [#2036](https://github.com/pnp/powershell/pull/2036)
- Added `Get-PnPSensitivityLabel` cmdlet to retrieve Microsoft Purview sensitivity labels available on the tenant [#2023](https://github.com/pnp/powershell/pull/2023)
- Added `Get-Microsoft365GroupYammerCommunity` cmdlet to retrieve details on the Yammer Community connected to a Microsoft 365 Group [#2038](https://github.com/pnp/powershell/pull/2038)
- Added `Get-Microsoft365GroupTeam` cmdlet to retrieve details on the Microsoft Teams team connected to a Microsoft 365 Group [#2038](https://github.com/pnp/powershell/pull/2038)
- Added `Get-Microsoft365GroupEndpoints` cmdlet to retrieve details on all endpoints connected to a Microsoft 365 Group [#2038](https://github.com/pnp/powershell/pull/2038)
- Added `-ExcludeDeletedSites` optional parameter to `Get-PnPSiteCollectionAppCatalogs` which allows for site collections with App Catalogs that are in the recycle bin to be exluded from the results [#2044](https://github.com/pnp/powershell/pull/2044)
- Added `-CurrentSite` optional parameter to `Get-PnPSiteCollectionAppCatalogs` which allows for checking if the currently connected to site has a site collection App Catalogs provisioned on it [#2044](https://github.com/pnp/powershell/pull/2044)
- Added `ExternalUserExpirationRequired` and `ExternalUserExpireInDays` parameters to `Set-PnPTenant` cmdlet to handle expiration policy for External users.

### Changed

- Changed `Sync-PnPSharePointUserProfilesFromAzureActiveDirectory` to map users based on their Ids instead which should resolve some issues around user identities reporting not to exist. You can use the new `-IdType` option to switch it back to `PrincipalName` if needed.  [#1752](https://github.com/pnp/powershell/pull/1752)
- Changed `Add-PnPField` now returns specific type taxonomy field type instead of the generic type. [#1888](https://github.com/pnp/powershell/pull/1888)
- Changed `Get-PnPOrgAssetsLibrary` to return a proper value of the organisation assets libraries. [#1889](https://github.com/pnp/powershell/pull/1889)
- Bumped .NET Framework version to 4.6.2 as the 4.6.1 is not supported anymore. [#1856](https://github.com/pnp/powershell/pull/1856)
- Changed `Add-PnPDataRowsToSiteTemplate`, it will now export a datetime field value as UTC string. [#1900](https://github.com/pnp/powershell/pull/1900)
- The cmdlets `Remove-PnPFile`, `Remove-PnPFolder`, `Move-PnPListItemToRecycleBin`, `Remove-PnPList`, `Remove-PnPListItem` and `Remove-PnPPage` will now return the corresponding recycle bin item if they get deleted to the recycle bin. Before they would not return anything. [#1783](https://github.com/pnp/powershell/pull/1783)
- Cmdlets backed by a Microsoft Graph call will now return detailed information when the Graph call fails [#1923](https://github.com/pnp/powershell/pull/1923)
- Changed `Get-PnPPlannerBucket` to return the buckets in the correct (reversed) order as you see them through the web interface [#1922](https://github.com/pnp/powershell/pull/1922)
- Changed `Connect-PnPOnline -Interactive` and `Connect-PnPOnline -DeviceLogin` to no longer suppress errors which should allow for certificate logins to be used. [#1933](https://github.com/pnp/powershell/pull/1933)
- `Set-PnPTeamsChannel` now uses the Graph v1 endpoint, previously it used the beta endpoint. [#1938](https://github.com/pnp/powershell/pull/1938)
- Service Health cmdlets have been improved and are now consistent with other cmdlets to handle pagination [#1938](https://github.com/pnp/powershell/pull/1938)
- Changed that almost every cmdlet now supports passing in a specific connection using `-Connection`. If omitted, the default connection will be used. [#1949](https://github.com/pnp/powershell/pull/1949), [#2011](https://github.com/pnp/powershell/pull/2011), [#1958](https://github.com/pnp/powershell/pull/1958)
- Changed connecting with `Connect-PnPOnline -Credentials` now throwing a clear exception when making a typo in the hostname instead of getting stuck [#686](https://github.com/pnp/pnpframework/pull/686)
- Renamed `Get-PnPSiteClassification` to `Get-PnPAvailableSiteClassification` to fall in line with `Get-PnPAvailableSensitivityLabel`. Old name will stay as an alias for backwards compatibility for now, but will be removed in a future version. [#2036](https://github.com/pnp/powershell/pull/2036)
- Renamed `Add-PnPSiteClassification` to `Add-PnPAvailableSiteClassification` to fall in line with `Get-PnPAvailableSensitivityLabel`. Old name will stay as an alias for backwards compatibility for now, but will be removed in a future version. [#2036](https://github.com/pnp/powershell/pull/2036)
- Renamed `Update-PnPSiteClassification` to `Update-PnPAvailableSiteClassification` to fall in line with `Get-PnPAvailableSensitivityLabel`. Old name will stay as an alias for backwards compatibility for now, but will be removed in a future version. [#2036](https://github.com/pnp/powershell/pull/2036)
- Renamed `Remove-PnPSiteClassification` to `Remove-PnPAvailableSiteClassification` to fall in line with `Get-PnPAvailableSensitivityLabel`. Old name will stay as an alias for backwards compatibility for now, but will be removed in a future version. [#2036](https://github.com/pnp/powershell/pull/2036)
- Changed `Get-PnPHubSiteChild` to have its `-Identity` parameter become optional. If not provided, the currently connected to site will be used. [#2033](https://github.com/pnp/powershell/pull/2033)
- Changed `Get-PnPSiteCollectionAppCatalogs` (plural) to `Get-PnPSiteCollectionAppCatalog` (singular) to follow the naming convention [#2044](https://github.com/pnp/powershell/pull/2044)

### Fixed

- Fixed `Get-PnPTenantSite` cmdlet so that it will return data even if the template name is specified in a different case. [#1773](https://github.com/pnp/powershell/pull/1773)
- Fixed `Add-PnPDocumentSet` cmdlet so that it will support Document Set Content Type Id specified at the web level. [#1796](https://github.com/pnp/powershell/pull/1796)
- Fixed `Get-PnPGroup` , `Get-PnPGroupPermissions` and `Set-PnPGroupPermissions` cmdlets by making them more consistent. They will also throw error if a group is not found. [#1808](https://github.com/pnp/powershell/pull/1808)
- Fixed `Get-PnPFile` issue with every 3rd file download in PS 5.
- Fixed `Add-PnPContentTypesFromContentTypeHub`, if `Site` parameter is specified, it will be used now to sync content types from content type hub site.
- Fixed `Get-PnPTeamsTeam`, the cmdlet now also returns additional properties like `WebUrl, CreatedDateTime, InternalId` [#1825](https://github.com/pnp/powershell/pull/1825)
- Fixed `Add/Set-PnPListItem` , the cmdlet now works correctly with `-Batch` parameter for field types other than string. [#1890](https://github.com/pnp/powershell/pull/1890)
- Fixed `Get-PnPTeamsTeam`, the cmdlet now also returns additional properties like `WebUrl, CreatedDateTime, InternalId`. [#1825](https://github.com/pnp/powershell/pull/1825)
- Fixed `Set-PnPListPermission`, it will now throw error if the list does not exist. [#1891](https://github.com/pnp/powershell/pull/1891)
- Fixed `Invoke-PnPSPRestMethod` invalid parsing for SharePoint number columns. [#1877](https://github.com/pnp/powershell/pull/1879)
- Fixed issue with `Add/Set-PnPListItem` not throwing correct exception for invalid taxonomy values. [#1870](https://github.com/pnp/powershell/pull/1870)
- Fixed `Sync-PnPSharePointUserProfilesFromAzureActiveDirectory` throwing an "Object reference not set to an instance of an object" exception when providing an empty users collection or incorrect user mapping [#1896](https://github.com/pnp/powershell/pull/1896)
- Fixed `Connect-PnPOnline -ReturnConnection` also setting the current connection instead of just the returned connection [#1919](https://github.com/pnp/powershell/pull/1919)
- Fixed `Disconnect-PnPOnline -Connection` also disconnecting other connections next to the provided connection [#1919](https://github.com/pnp/powershell/pull/1919)
- Fixed `Set-PnPContext` not properly applying the provided context [#1919](https://github.com/pnp/powershell/pull/1919)
- Fixed Graph endpoints for non-commercial clouds for Managed Identity and Teams cmdlets [#1944](https://github.com/pnp/powershell/pull/1944)
- Fixed `Add-PnPTeamsUser`, the parameter `-Channel` is now not required. [#1953](https://github.com/pnp/powershell/pull/1953)
- Fixed `Get-PnPPlannerTask` throwing an object reference exception for completed tasks [#1956](https://github.com/pnp/powershell/issues/1956)
- Fixed `Get-PnPUserOneDriveQuota` returning the maximum possible quota instead of the actual configured quota on a OneDrive for Business site [#1902](https://github.com/pnp/powershell/pull/1902)
- Fixed `Get-PnPFile` throwing an exception when trying to download a file containing the plus character [#1990](https://github.com/pnp/powershell/pull/1990)
- Fixed `Get-PnPTeamsChannel` not working correctly with PowerShell select. [#1988](https://github.com/pnp/powershell/pull/1988)
- Fixed `Update-PnPSiteClassification`, it was ignoring the `Settings` parameter. It will now be processed. [#1989](https://github.com/pnp/powershell/pull/1989)
- Fixed `Register-PnPAzureADApp` issue with app creation after the connection related changes. [#1993](https://github.com/pnp/powershell/pull/1993)
- Fixed `Get-PnPFileVersion` not able to correctly use piping on the returned object. [#1997](https://github.com/pnp/powershell/pull/1997)
- Fixed `Add-PnPListItem` not showing field name when it has an improper value assigned to it [#2002](https://github.com/pnp/powershell/pull/202)
- Fixed `Update-PnPSiteClassification` not allowing the `-UsageGuidelinesUrl` to be set without also setting `-DefaultClassification` [#2036](https://github.com/pnp/powershell/pull/2036)
- Fixed the browser consent dialog throwing an exception when trying to close it [#2037](https://github.com/pnp/powershell/pull/2037)
- Fixed `Get-PnPHubSiteChild` throwing an exception when passing in a URL that is actually not a hub site [#2033](https://github.com/pnp/powershell/pull/2033)
- Fixed `Add-PnPListItem` not showing field name when it has an improper value assigned to it [#2002](https://github.com/pnp/powershell/pull/2002)
- Fixed connecting using `Connect-PnPOnline -Interactive -ClientId` not working well when already having an App-Only connection using the same ClientId [#2035](https://github.com/pnp/powershell/pull/2035)
- Fixed `Get-PnPSiteCollectionAppCatalog` not returning updated site collection URLs if they had been renamed [#2044](https://github.com/pnp/powershell/pull/2044)
- Fixed cmdlets inheriting from PnPAdminCmdlet not working well on vanity domain SharePoint Online tenants [#2052](https://github.com/pnp/powershell/pull/2052)
- Fixed `Copy-PnPList` throwing an unauthorized exception when using it with a non SharePoint Online administrator user [#2054](https://github.com/pnp/powershell/pull/2054)

### Removed

- Removed `Get-PnPAvailableClientSideComponents`. Use `Get-PnPPageComponent -Page -ListAvailable` instead.  [#1833](https://github.com/pnp/powershell/pull/1833)
- Removed `NextLink` property from `Get-PnPAzureADUser` cmdlet, as it was causing confusion. [#1930](https://github.com/pnp/powershell/pull/1930)
- Deprecated the `-Connection` parameter of `Disconnect-PnPOnline` cmdlet, as it was technically not capable of clearing a connection reference anyway [#1949](https://github.com/pnp/powershell/pull/1949)

### Contributors

- Ali Robertson [alirobe]
- Leif Frederiksen [Leif-Frederiksen]
- Emily Mancini [eemancini]
- Jim Duncan [sparkitect]
- Arleta Wanat [PowershellScripts]
- Yuriy Samorodov [YuriySamorodov]
- Arleta Wanat [PowershellScripts]
- Marc D Anderson [sympmarc]
- dc366 [dc366]
- Bart-Jan Dekker [bjdekker]
- Aleksandr Sapozhkov [shurick81]
- [spg-iwilson]
- Jago Pauwels [jagopauwels]
- [4ndri]
- Martin Lingstuyl [martinlingstuyl]
- James May [fowl2]
- Milan Holemans [milanholemans]
- Arleta Wanat [PowershellScripts]
- Koen Zomers [koenzomers]
- Mikael Svenson [wobba]
- Gautam Sheth [gautamdsheth]

## [1.10.0]

### Added

- Added additional properties to the users returned by `Get-PnPMicrosoft365GroupMember` such as `userType` [#1474](https://github.com/pnp/powershell/pull/1474)
- Added `Update-PnPTeamsUser` cmdlet to change the role of a user in an existing Teams team [#1499](https://github.com/pnp/powershell/pull/1499)
- Added `Get\New\Remove\Set-PnPMicrosoft365GroupSettings` cmdlets to interact with Microsoft 365 Group settings.
- Added `Get-PnPMicrosoft365GroupSettingTemplates` cmdlet to retrieve system wide Microsoft 365 Group setting templates.
- Added `Add\Remove\Invoke\Get-PnPListDesign` cmdlets to add a list design, remove a list design and apply the list design.
- Added `DisablePersonalListCreation` parameter to `Set-PnPTenant` cmdlet to provide ability to disable personal lists creation [#1545](https://github.com/pnp/powershell/pull/1545)
- Added `DisabledModernListTemplateIds` and `EnableModernListTemplateIds` parameters to `Set-PnPTenant` cmdlet to provide ability to disable or enable modern lists with specific Ids [#1545](https://github.com/pnp/powershell/pull/1545)
- Added `DisablePersonalListCreation` and `DisabledModernListTemplateIds` values to be displayed when using `Get-PnPTenant` cmdlet [#1545](https://github.com/pnp/powershell/pull/1545)
- Added `Add\Remove\Set-PnPAdaptiveScopeProperty` cmdlets to add/update/remove a property bag value while dealing with the noscript toggling in one cmdlet [#1556](https://github.com/pnp/powershell/pull/1556)
- Added support to add multiple owners and members in `New-PnPTeamsTeam` cmdlet [#1241](https://github.com/pnp/powershell/pull/1241)
- Added the ability to set the title of a new modern page in SharePoint Online using `Add-PnPPage` to be different from its filename by using `-Title`
- Added optional `-UseBeta` parameter to `Get-PnPAzureADUser` to force it to use the Microsoft Graph beta endpoint. This can be necessary when i.e. using `-Select "PreferredDataLocation"` to query for users with a specific multi geo location as this property is only available through the beta endpoint. [#1559](https://github.com/pnp/powershell/pull/1559)
- Added `-Content` option to `Add-PnPFile` which allows creating a new file on SharePoint Online and directly providing its textual content, i.e. to upload a log file of the execution [#1559](https://github.com/pnp/powershell/pull/1559)
- Added `Get-PnPTeamsPrimaryChannel` to get the primary Teams channel, general, of a Team [#1572](https://github.com/pnp/powershell/pull/1572)
- Added `IgnoreDefaultProperties` parameter to `Get-PnPAzureADUser` to allow for the default properties not to be retrieved but instead just the ones you specify using `Select` [#1575](https://github.com/pnp/powershell/pull/1575)
- Added `Publish\Unpublish-PnPContentType` to allow for content types to be published or unpublished on hub sites [#1597](https://github.com/pnp/powershell/pull/1597)
- Added `Get-PnPContentTypePublishingStatus` to get te current publication state of a content type in the content type hub site [#1597](https://github.com/pnp/powershell/pull/1597)
- Added ability to pipe the output of `Get-PnPTenantDeletedSite` to either `Restore-PnPTenantDeletedSite` or `Remove-PnPTenantDeletedSite` [#1596](https://github.com/pnp/powershell/pull/1596)
- Added `Rename-PnPTenantSite` to rename a SharePoint Online site URL [#1606](https://github.com/pnp/powershell/pull/1606)
- Added optional `-Wait` option to `Sync-PnPSharePointUserProfilesFromAzureActiveDirectory` to allow for the script to wait until the user profile sync has completed
- Added optional `-Verbose` option to `Sync-PnPSharePointUserProfilesFromAzureActiveDirectory` to allow for seeing the progress of the synchronization process
- Added `Copy-PnPTeamsTeam` which allows an existing Teams team to be copied into a new Team [#1624](https://github.com/pnp/powershell/pull/1624)
- Added `Set-PnPMessageCenterAnnouncementAsRead` which allows setting one or more message center announcements as read for the current user [#1151](https://github.com/pnp/powershell/pull/1151)
- Added `Set-PnPMessageCenterAnnouncementAsUnread` which allows setting one or more message center announcements as unread for the current user [#1151](https://github.com/pnp/powershell/pull/1151)
- Added `Set-PnPMessageCenterAnnouncementAsArchived` which allows setting one or more message center announcements as archived for the current user [#1151](https://github.com/pnp/powershell/pull/1151)
- Added `Set-PnPMessageCenterAnnouncementAsNotArchived` which allows setting one or more message center announcements as not archived for the current user [#1151](https://github.com/pnp/powershell/pull/1151)
- Added `Set-PnPMessageCenterAnnouncementAsFavorite` which allows setting one or more message center announcements as favorite for the current user [#1151](https://github.com/pnp/powershell/pull/1151)
- Added `Set-PnPMessageCenterAnnouncementAsNotFavorite` which allows setting one or more message center announcements as not favorite for the current user [#1151](https://github.com/pnp/powershell/pull/1151)
- Added `-AsMemoryStream` option to `Get-PnPFile` to allow for downloading of a file from SharePoint Online in memory for further processing [#1638](https://github.com/pnp/powershell/pull/1638)
- Added `-Stream` option to `Read-PnPSiteTemplate` to allow for processing on a PnP Provisioning Template coming from memory [#1638](https://github.com/pnp/powershell/pull/1638)
- Added `New-PnPAzureADUserTemporaryAccessPass` which allows creation of a Temporary Access Pass for a specific user in Azure Active Directory
- Added `-Force` option to `Set-PnPTenant` to allow skipping the confirmation question for certain other parameters like `SignInAccelerationDomain,EnableGuestSignInAcceleration,BccExternalSharingInvitations,OrphanedPersonalSitesRetentionPeriod,OneDriveForGuestsEnabled,AllowDownloadingNonWebViewableFiles`.
- Added `Get-PnPCompatibleHubContentTypes` which allows the list of content types present in the content type hub site that can be added to the root web or a list on a target site to be returned [#1678](https://github.com/pnp/powershell/pull/1678)

### Changed

- Improved `Add-PnPTeamsUser` cmdlet. The cmdlet executes faster and we can now add users in batches of 200. [#1548](https://github.com/pnp/powershell/pull/1548)
- The `Move\Remove\Rename-PnPFolder` cmdlets now support pipebinds.
- Changed `Add-PnPDataRowsToSiteTemplate`, it will return a warning if user(s) are not found during list item extraction. Earlier it used to throw error and stop extraction of list items.
- Changed the return type of `Sync-PnPSharePointUserProfilesFromAzureActiveDirectory` to return our own entity instead of the one returned by CSOM [#1559](https://github.com/pnp/powershell/pull/1559)
- Changed running `Sync-PnPSharePointUserProfilesFromAzureActiveDirectory` with `-WhatIf` to also provide a return entity providing the path to where the JSON file has been uploaded to [#1559](https://github.com/pnp/powershell/pull/1559)
- Disabling telemetry collection now requires either setting the environment variable or creating the telemetry file ([documentation](https://pnp.github.io/powershell/articles/configuration.html)) [#1504](https://github.com/pnp/powershell/pull/1504)
- Changed `Get-PnPAzureADUser` to now return all the users in Azure Active Directory by default, instead of only the first 999, unless you specified `-EndIndex:$null` [#1565](https://github.com/pnp/powershell/pull/1565)
- Changed `Get-PnPTenantDeletedSite -Identity` no longer returning an unknown exception when no site collection with the provided Url exists in the tenant recycle bin but instead returning no output to align with other cmdlets [#1596](https://github.com/pnp/powershell/pull/1596)
- Changed `Connect-PnPOnline -UseWebLogin` to no longer suppress errors which should allow for certificate logins to be used [#1706](https://github.com/pnp/powershell/issues/1706)
- The cmdlet `New-PnPTeamsTeam` no longer supports adding members or owners through their e-mail addresses, if they differ from their UPNs. The User Principal Names must be used instead [#1241](https://github.com/pnp/powershell/pull/1241)
- Improved `New-PnPUPABulkImportJob` by optimizing it and also adding support for `WhatIf` paramater. [#2040](https://github.com/pnp/powershell/pull/2040)

### Fixed

- Fixed `Set-PnPSite` not working with `DisableCompanyWideSharingLinks` parameter.
- Fixed `Get-PnPListPermissions` returning wrong information in case of broken inheritance.
- Fixed `Submit-PnPSearchQuery -Query "somequery"` yielding an error when no results [#1520](https://github.com/pnp/powershell/pull/1520)
- Fixed `Set-PnPTenantSite` not setting SharingCapability property correctly.
- Fixed `Get-PnPMicrosoft365Group` retrieving non-Unified groups when parameters are not specified.
- Fixed `Get-PnPRecycleBinListItem` not retrieving second stage items if only `RowLimit` is specified.
- Fixed `Add-PnPDataRowsToSiteTemplate` issue with PnP templates when it contained multilingual references.
- Fixed `Copy-PnPItemProxy` is not recognized as the name of a cmdlet, function, script file, or operable program error with the cmdlet.
- Fixed `Add-PnPMicrosoft365GroupMember`, `Remove-PnPMicrosoft365GroupMember`, `Add-PnPTeamsUser` and `Remove-PnPTeamsUser` not being able to handle guest accounts containing # characters [#1523](https://github.com/pnp/powershell/pull/1523)
- Fixed not being able to pipe `Get-PnPHubSite` to `Get-PnPHubSiteChild` to retrieve all site collections which are associated to any hub site [#1571](https://github.com/pnp/powershell/pull/1571)
- Fixed `Add-PnPFileToSiteTemplate` when used alongside `Get-PnPFile` where the FileStream tried to overwrite an already open filestream when a file was located in the same directory as the template file itself.
- Fixed `Get-PnPMessageCenterAnnouncement` returning an error [#1607](https://github.com/pnp/powershell/pull/1607)
- Fixed `New-PnPTeamsTeam` issue when adding Owners and Members.
- Fixed running an admin cmdlet not always returning to the same context as before running the cmdlet [#1611](https://github.com/pnp/powershell/pull/1611)
- Fixed [an issue](https://github.com/pnp/powershell/issues/1501) where `Sync-PnPSharePointUserProfilesFromAzureActiveDirectory` would not correctly sync characters which are not part of the Western European encoding (iso-8859-1)
- Fixed [an issue](https://github.com/pnp/powershell/issues/1692) where `Sync-PnPSharePointUserProfilesFromAzureActiveDirectory` would not correctly sync user profiles if a value contained a backslash (\) [#1711](https://github.com/pnp/powershell/pull/1711)

### Removed

- Removed `Add-PnPClientSidePageSection`, use `Add-PnPPageSection` instead [#1563](https://github.com/pnp/powershell/pull/1563)
- Removed `Add-PnPClientSideWebPart`, use `Add-PnPPageWebPart` instead [#1563](https://github.com/pnp/powershell/pull/1563)
- Removed `Add-PnPClientSideText`, use `Add-PnPPageTextPart` instead [#1563](https://github.com/pnp/powershell/pull/1563)
- Removed `Get-PnPAADUser`, use `Get-PnPAzureADUser` instead [#1568](https://github.com/pnp/powershell/pull/1568)
- Removed `Get-PnPOffice365CurrentServiceStatus`, `Get-PnPOffice365HistoricalServiceStatus`, `Get-PnPOffice365ServiceMessage` and `Get-PnPOffice365Services` as Microsoft has removed the underlying Office Management APIs. Use `Get-PnPMessageCenterAnnouncent`, `Get-PnPServiceCurrentHealth` and `Get-PnPServiceHealthIssue` instead. [#1608](https://github.com/pnp/powershell/pull/1608)

### Contributors

- Michael Vasiloff [mikevasiloff]
- [svermaak]
- Russell Gove [russgove]
- Mike Park [mikeparkie]
- Jerker Vallbo [jerval53]
- Gaurav Mahajan [mahajangaurav]
- Dennis [expiscornovus]
- Jasey Waegebaert [Jwaegebaert]
- Swapnil Shrivastava [swapnil1993]
- Hugo Bernier [hugoabernier]
- brenle
- Johan Brännmar [brannmar]
- Lschockaert
- Leon Armston [LeonArmston]
- Reshmee Auckloo [reshmee011]
- Arleta Wanat [PowershellScripts]
- Brendon Lee [brenle]
- Guillaume Bordier [gbordier]
- [reusto]
- Reshmee Auckloo [reshmee011]
- Veronique Lengelle [veronicageek]
- Gautam Sheth [gautamdsheth]
- Koen Zomers [koenzomers]

## [1.9.0]

### Added

- Added `Get-PnPTenantInstance` which will return one or more tenant instances, depending if you have a multi-geo or single-geo (default) tenant.
- Added optional `-ScheduledPublishDate` parameter to `Add-PnPPage` and `Set-PnPPage` to allow for scheduling a page to be published
- Added `-RemoveScheduledPublish` to `Set-PnPPage` to allow for a page publish schedule to be removed
- Added support for off peak SharePoint Syntex content classification and extraction for lists and folders via new `-OffPeak` and `-Folder` parameters for `Request-PnPSyntexClassifyAndExtract`
- Added `Get\Set-PnPPlannerConfiguration` to allow working with the Microsoft Planner tenant configuration
- Added `Get\Set-PnPPlannerUserPolicy` to allow setting Microsoft Planner user policies for specific users
- Added `Get\Add\Remove-PnPPlannerRoster` which allows a Microsoft Planner Roster to be created, retrieved or removed
- Added `Get\Add\Remove-PnPPlannerRosterMember` to be able to read, add and remove members from a Microsoft Planner Roster
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
- Added `-BookmarkStatus` parameter to `Get-PnPSearchConfiguration` cmdlet to call REST endpoint to fetch promoted results defined via query rules and output them in Bookmark supported CSV format.

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

- Mikael Svenson [wobba]
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

### Changed

- Renamed `Get-PnPFlowEnvironment` to `Get-PnPPowerAutomateEnvironment`
- Changed `Get-PnPSiteScriptFromWeb` to get a site script of the currently connected to site if `-Url` is omitted.
- Improved `Find-PnPFile` error message
- `Get-PnPFileVersion` cmdlet documentation improved with additional example.
- `Add-PnPNavigationNode` cmdlet documentation improved with additional example feature which shows how to add a navigation node as a label.
- Changed `Get-PnPSiteDesign` and `Invoke-PnPSiteDesign` to when providing a name through `-Identity` to be able to work with all site designs having that same name instead of just the first one
- Changed `Set-PnPListItemPermission` to support piping in a roledefinition for `-AddRole` and `-RemoveRole`
- Changed that `Get-PnPSiteScript -Identity` now also works with the site script name instead of just the site script Id
- Changed that `Get-PnPUnifiedAuditLog` returns the error being returned by the Office Management API service, in case something goes wrong [#1631](https://github.com/pnp/powershell/pull/1631)

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
- Fixed an issue where running `Get-PnPSiteDesign -Identity` passing in an identifier that did not exist would return an exception [#1622](https://github.com/pnp/powershell/pull/1622)

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

- Reorganized Connect-PnPOnline and simplified/cleared up usage. See <https://pnp.github.io/powershell/cmdlets/connect-pnponline.html> and <https://pnp.github.io/powershell/articles/connecting.html> for more information.
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
- Updated `Invoke-PnPBatch` to fully execute a batch by default if one of the requests in the large batch throws an exception. Specify the `-StopOnException` switch to immmediately stop after an exception occurs. The rest of the batch will be skipped where possible. See <https://pnp.github.io/powershell/articles/batching> for more information.
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
