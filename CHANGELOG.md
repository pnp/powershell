# PnP.PowerShell Changelog
*Please do not commit changes to this file, it is maintained by the repo owner.*

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/).

## [0.3.*-nightly]

### Added

### Changed
- Breaking change: we changed Grant-PnPTenantServicePrincipalPermission and Revoke-PnPTenantServicePrincipalPermission to use the Graph behind the scenes. This is a breaking change when it comes to the required permissions, but the new approach is more future proof. (0.3.8)
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
