# Connect to the SharePoint Online using Application Permissions

This PowerShell sample demonstrates how to deploy and use the PnP PowerShell to connect to SharePoint Online
using App-Only within Azure Automation. This is useful for demonstrating connecting to SharePoint Online with App-Only permissions
as well as provisioning Azure Automation with the required modules.

Applies to

- Office 365 Multi-Tenant (MT)

## Prerequisites

- PnP.PowerShell Module (1.2.0)
- Azure AD - Global Admin (for app consent)
- Azure PowerShell Module [https://learn.microsoft.com/powershell/azure/install-az-ps](https://learn.microsoft.com/powershell/azure/install-az-ps)

## Scripts

The following script samples as part of the solution:

- Deploy-AzureAppOnly.ps1 - this uses the new cmdlet "Register-PnPAzureADApp" to create the certificate and create Azure AD app
- Deploy-AzureAutomation.ps1 - this creates an Azure Automation account, configures the account for hosting credentials, registering the PnP module and publishing a runbook
- Deploy-FullAutomation.ps1 - this is a combination of both above scripts in one run
- test-connection-runbook.ps1 - sample runbook that connects to SharePoint Online with a certificate and example connections to tenant level and site level

**Why are they provided as split and combined?**
Quite often in larger enterprises, there are teams that support the Azure Services and separately Azure Directory services. Admin consent is required for the App this generates. Also, there are organisations where staff have combined roles so the scripts support both enterprise scenarios.

### Note
Not all regions support Azure Automation - check here for your region: [https://azure.microsoft.com/en-us/global-infrastructure/services/?products=automation&regions=all](https://azure.microsoft.com/en-us/global-infrastructure/services/?products=automation&regions=all)

## Getting Started

The following are example of calling the scripts to setup the services.

### Example 1: Creation of an Azure AD App

```powershell

./Deploy-AzureAppOnly.ps1 `
    -Tenant "yourtenant.onmicrosoft.com" `
    -SPTenant "yourtenant" `
    -AppName "PnP.PowerShell Automation" `
    -ValidForYears 2 `
    -CertCommonName "PnP.PowerShell Automation"

```
Note: Certificate Password will be obtained from a prompt to enter.


### Example 2: Provisioning Azure Automation with PnP Module, credentials and publishing runbook

```powershell

./Deploy-AzureAutomation.ps1 `
    -Tenant "yourtenant.onmicrosoft.com" `
    -SPTenant "yourtenant" `
    -AzureAppId "<app id generated from the first script>" `
    -CertificatePath "C:\Temp\PnP.PowerShell Automation.pfx" `
    -AzureResourceGroupName "pnp-dot-powershell-automation-rg" `
    -AzureRegion "northeurope" `
    -AzureAutomationName "pnp-dot-powershell-auto" `
    -SubscriptionId "ea5f2ce8-98c4-413e-8617-081995b71593" `
    -CreateResourceGroup

```

Notes:
- Certificate Password will be obtained from a prompt to enter.
- The CertificatePath is the location where the certificate was stored locally in example 1

### Example 3: Combination Script of both Example 1 and 2

```powershell

./Deploy-FullAutomation.ps1 `
    -Tenant "yourtenant.onmicrosoft.com" `
    -SPTenant "yourtenant" `
    -AppName "PnP.PowerShell Automation" `
    -ValidForYears 2 `
    -CertCommonName "PnP.PowerShell Automation" `
    -AzureResourceGroupName "pnp-dot--powershell-automation-rg" `
    -AzureRegion "northeurope"  `
    -AzureAutomationName "pnp-dot-powershell-auto"  `
    -SubscriptionId "ea5f2ce8-98c4-413e-8617-081995b71593"
    -CreateResourceGroup
```

## Version history ##
Version  | Date | Author(s) | Comments
---------| ---- | --------- | ---------|
1.0  | December 4th 2020 | Paul Bullock (CaPa Creative Ltd) | Initial release


## **Disclaimer** 
THIS CODE IS PROVIDED AS IS WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.