# SharePointPnP.PowerShell Changelog
*Please do not commit changes to this file, it is maintained by the repo owner.*

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/).

## [0.9.0]

### Changed

- `Get-PnPAppInstance` has been removed. Use `Get-PnPApp` instead.
- `Import-PnPAppPackage` has been removed. Use `Instal-PnPApp` instead.
- `Uninstall-AppInstance` has been removed. Use `Uninstall-PnPApp` instead.
- Removed `Get-PnPHealthScore` as the value reported is only applicable to on-premises.
- Removed `-MinimalHealthScore` from `Connect-PnPOnline` as the value reported from the server only applies to on-premises.
- Removed `-SkipTenantAdminCheck` from `Connect-PnPOnline`. Check will be executed everytime where applicable.
- Removed Obsolete parameter `-FromRecycleBin` from `Remove-PnPTenantSite`. Use `Clear-PnPTenantRecycleBinItem` instead.
- Removed `-UserCodeMaximumLevel` and `-UserCodeWarningLevel` from `Set-PnPTenantSite`: sandboxed solutions have been deprecated from SharePoint Online and these values are not applicable anymore.
- Removed `-Out` parameter on `New-PnPAzureCertificate`. Use `-OutPfx` instead.
- Removed `Enable-PnPResponsiveUI` and `Disable-PnPResponsiveUI`.
- Removed `Disable-PnPInPlaceRecordsManagementForSite`. Use `Set-PnPInPlaceRecordsManagement -Enabled $false`.
- Removed `Enable-PnPInPlaceRecordsManagementForSite`. Use `Set-PnPInPlaceRecordsManagement -Enabled $true`.
- Added -DisableCustomAppAuthentication to Set-PnPTenant and added support for DisableCustomAppAuthentication in Get-PnPTenant.
- Removed `Measure-PnPResponseTime`. Use Fiddler for more detailed data instead.
- Removed `-Identity` from Get-PnPAvailableLanguage as it does not apply to SharePoint Online.
- Removed `Get-PnPManagementApiAccessToken` and `Get-PnPOfficeManagementApiAccessToken` cmdlets. Use Connect-PnPOnline instead with either the -Scopes parameter and other optional parameters
