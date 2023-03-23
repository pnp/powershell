# Upgrading from the Legacy version of PnP PowerShell

First make sure to uninstall any edition of PnP PowerShell that runs on the Windows PowerShell version (the one with the blue background).

```powershell
Uninstall-Module -Name "SharePointPnPPowerShellOnline" -AllVersions -Force
```

Now install the new module:

```powershell
Install-Module -Name "PnP.PowerShell"
```
## Setup authentication. 

The way PnP PowerShell authenticates you to your tenant has changed. We now use OAuth behind the scenes to authenticate you. We support username/password auth, device code auth and app-only authentication.

If you were using `Connect-PnPOnline` with the `-Credentials` you will have to register first an Azure AD application on your tenant. This is very straightforward and simple:

```powershell
Register-PnPManagementShellAccess
```

This cmdlet will ask you to authenticate, and then provide consent to the PnP Management Shell application. A registration will be added to the Azure AD of your tenant. This is a one-time action per tenant. After you provided consent you will be able to authenticate using:

```powershell
Connect-PnPOnline -Url https://[tenant].sharepoint.com -Credentials (Get-Credential)
```

Read [more information about authentication](./authentication.md).

## Changes

Check your scripts. As this is a major release, we have deprecated cmdlets, renamed cmdlets, and we marked some parameters as obsolete. 

> [!Important]
> We marked the `-Web` parameter as obsolete on many cmdlets. The `-Web` parameter allows you to execute cmdlets to a subweb underneath the current web. Due to API changes under the hood of PnP Powershell we marked this parameter as obsolete. The parameter will be removed in a future release. Notice though that it is still fully possible to connect to a subweb by using the full url to the subweb with Connect-PnPOnline.

|Old|New/Replaced with|Notes|
|----|----|---|
|`Apply-PnPProvisioningTemplate`|`Invoke-PnPSiteTemplate`||
|`Get-PnPProvisioningTemplate`|`Get-PnPSiteTemplate`||
|`Add-PnPDataRowsToProvisioningTemplate`|`Add-PnPDataRowsToSiteTemplate`||
|`Add-PnPFileToProvisioningTemplate`|`Add-PnPFileToSiteTemplate`||
|`Add-PnPListFoldersToProvisioningTemplate`|`Add-PnPListFoldersToSiteTemplate`||
|`Convert-PnPFolderToProvisioningTemplate`|`Convert-PnPFolderToSiteTemplate`||
|`Convert-PnPProvisioningTemplate`|`Convert-PnPSiteTemplate`||
|`Export-PnPListToProvisioningTemplate`|`Export-PnPListToSiteTemplate`||
|`New-PnPProvisioningTemplate`|`New-PnPSiteTemplate`||
|`New-PnPProvisioningTemplateFromFolder`|`New-PnPSiteTemplateFromFolder`||
|`Read-PnPProvisioningTemplate`|`Read-PnPSiteTemplate`||
|`Remove-PnPFileFromProvisioningTemplate`|`Remove-PnPFileFromSiteTemplate`||
|`Save-PnPProvisioningTemplate`|`Save-PnPSiteTemplate`||
|`Set-PnPProvisioningTemplateMetadata`|`Set-PnPSiteTemplateMetadata`||
|`Add-PnPProvisioningTemplate`|`Add-PnPSiteTemplate`||
|`Apply-PnPTenantTemplate`|`Invoke-PnPTenantTemplate`||
|`Get-PnPAppInstance`|`Get-PnPApp`|Different parameters|
|`Import-PnPAppPackage`|`Install-PnPApp`|Different parameters|
|`Uninstall-PnPAppInstance`|`Uninstall-PnPApp`|Different parameters|
|`Get-PnPHealthScore`||Deprecated|
|`Enable-PnPResponsiveUI`||Deprecated|
|`Disabled-PnPResponsiveUI`||Deprecated|
|`Disable-PnPInplaceRecordsManagementForSite`|`Set-PnPInPlaceRecordsManagement -Enabled $false`||
|`Enable-PnPInplaceRecordsManagementForSite`|`Set-PnPInPlaceRecordsManagement -Enabled $true`||
|`Measure-PnPResponseTime`||Deprecated. Consider using Fiddler for more detailed data|
|`Get-PnPManagementApiAccessToken`|`Connect-PnPOnline -Scopes [scopes]`||
|`Connect-PnPHubSite`|`Add-PnPHubSiteAssociation`|Removed alias|
|`Disconnect-PnPHubSite`|`Remove-PnPHubSiteAssociation`|Removed alias|
|`Add-PnPOffice365GroupToSite`|`Add-PnPMicrosoft365GroupToSite`|Removed alias|
|`Add-PnPUnifiedGroupMember`|`Add-PnPMicrosoft365GroupMember`|Removed alias|
|`Add-PnPUnifiedGroupOwner`|`Add-PnPMicrosoft365GroupOwner`|Removed alias|
|`Clear-PnPUnifiedGroupMember`|`Clear-PnPMicrosoft365GroupMember`|Removed alias|
|`Clear-PnPUnifiedGroupOwner`|`Clear-PnPMicrosoft365GroupOwner`|Removed alias|
|`Get-PnPDeletedUnifiedGroup`|`Get-PnPDeletedMicrosoft365Group`|Removed alias|
|`Get-PnPUnifiedGroup`|`Get-PnPMicrosoft365Group`|Removed alias|
|`Get-PnPUnifiedGroupMembers`|`Get-PnPMicrosoft365GroupMembers`|Removed alias|
|`Get-PnPUnifiedGroupOwners`|`Get-PnPMicrosoft365GroupOwners`|Removed alias|
|`New-PnPUnifiedGroup`|`New-PnPMicrosoft365Group`|Removed alias|
|`Remove-PnPDeletedUnifiedGroup`|`Remove-PnPDeletedMicrosoft365Group`|Removed alias|
|`Remove-PnPUnifiedGroup`|`Remove-PnPMicrosoft365Group`|Removed alias|
|`Remove-PnPUnifiedGroupMember`|`Remove-PnPMicrosoft365GroupMember`|Removed alias|
|`Remove-PnPUnifiedGroupOwner`|`Remove-PnPMicrosoft365GroupOwner`|Removed alias|
|`Restore-PnPDeletedUnifiedGroup`|`Restore-PnPDeletedMicrosoft365Group`|Removed alias|
|`Set-PnPUnifiedGroup`|`Set-PnPMicrosoft365Group`|Removed alias|
|`Execute-PnPQuery`|`Invoke-PnPQuery`|Removed alias|
|`Ensure-PnPFolder`|`Resolve-PnPFolder`|Removed alias|
|`Install-PnPSolution`||Deprecated|
|`Add-PnPWorkflowDefinition`||Deprecated|
|`Add-PnPWorkflowSubscription`||Deprecated|
|`Get-PnPWorkflowDefinition`||Deprecated|
|`Get-PnPWorkflowInstances`||Deprecated|
|`Get-PnPWorkflowSubscription`||Deprecated|
|`Remove-PnPWorkflowDefinition`||Deprecated|
|`Remove-PnPWorkflowSubscription`||Deprecated|
|`Resume-PnPWorkflowInstance`||Deprecated|
|`Start-PnPWorkflowInstance`||Deprecated|
|`Stop-PnPWorkflowInstance`||Deprecated|
|`Test-PnPOffice365AliasIsUsed`|`Test-PnPMicrosoft365AliasIsUsed`||

## Parameter Changes

|Cmdlet|Parameter|Changes|
|------|---------|-------|
|`Register-PnPManagementShell`|`SiteUrl`|Removed. Not required anymore|
|`Connect-PnPOnline`|`MinimalHealthScore`|Removed. Not applicable anymore|
|`Connect-PnPOnline`|`SkipTenantAdminCheck`|Removed.|
|`Remove-PnPTenantSite`|`FromRecycleBin`|Use `Clear-PnPTenantRecycleBinItem`|
|`Set-PnPTenantSite`|`UserCodeMaximumLevel`|Sandbox solutions have been deprecated, no more applicable|
|`Set-PnPTenantSite`|`UserCodeWarningLevel`|Sandbox solutions have been deprecated, no more applicable|
|`New-PnPAzureCertificate`|`Out`|Use `OutPfx` instead|
|`Get-PnPAvailableLanguage`|`Identity`|Removed as it does not apply to SharePoint Online|

