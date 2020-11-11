# PnP.PowerShell Changelog
*Please do not commit changes to this file, it is maintained by the repo owner.*

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/).

## [0.1.x-nightly]

### Added
- Marked -ExcludeSiteUrl as obselete on `Get-PnPMicrosoft365Group` for performance reasons. Use -IncludeSiteUrl instead.
- Added -CloudShell switch to `Connect-PnPOnline` which can be used in the Azure Cloud Shell. If specified you will automatically authenticate using the current identity you're logged in with to the Azure Cloud Shell. Notice: only Graph based cmdlets (Teams, Microsoft Groups etc.) will function. For SharePoint connectivity use one of the other connection options with `Connect-PnPOnline`.
- Added -DisableCustomAppAuthentication to `Set-PnPTenant` and added support for DisableCustomAppAuthentication in `Get-PnPTenant`.
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
- Added native support for Secret Management Modules (0.1.34)

### Changed

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

### Contributors
- Aimery Thomas [a1mery]
- Veronique Lengelle [veronicageek]
