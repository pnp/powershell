# Updating from PnP PowerShell 2.x to 3.x

_This is a draft document, version 3 is not yet available. You can try this out with the nightly builds starting from 2.99.1 or later_

The 3.x version of PnP PowerShell is based exclusively on .NET 8.0, which means that it will not work on older PowerShell editions like PowerShell 5.1, ISE or PowerShell 7.3 or older. It will work only on **PowerShell 7.4.4 or later editions.**

## Steps to update from 2.x to 3.x

- Download and install the latest PowerShell version from [this GitHub releases link](https://aka.ms/powershell-release?tag=lts)

Or

- For Windows environments, please use [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-windows)

- For Linux based environments, please use [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-linux)

- For Mac OS environments, please use [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-macos)

Once the PowerShell 7.4.4 or later is downloaded and installed in the environment, you can install the PnP PowerShell module like you normally do.

```powershell
Install-Module -Name "PnP.PowerShell"
```

If you want to install or update to the latest nightly built prerelease of PnP PowerShell, run:

```powershell
Install-Module -Name "PnP.PowerShell" -AllowPrerelease
```

## Changes needed in Azure DevOps/GitHub Actions/Pipelines

If you are using PnP PowerShell in Azure Devops, GitHub Actions or other pipeline infrastructure, you will have to update your PowerShell version from v5 to v7.4.4 or later.

Recommend referring to these 2 links:

- [DevOps Snack: Change PowerShell version in YAML](https://microsoft-bitools.blogspot.com/2021/02/devops-snack-change-powershell-version.html)
- [How to enable PowerShell core in Azure Pipeline?](https://theautomationcode.com/how-to-enable-powershell-core-in-azure-pipeline/)

## Breaking changes in 3.0

| **Cmdlet** | **Comment** |
| ----------- | ---------------------- |
| Get-PnPRetentionLabel | The `Get-PnPRetentionLabel` cmdlet has been renamed to `Get-PnPTenantRetentionLabel` |
| Get-PnPLabel | The `Get-PnPLabel` cmdlet has been renamed to `Get-PnPRetentionLabel` |
| Get-PnPPowerPlatformConnector | The `Get-PnPPowerPlatformConnector` cmdlet has been renamed to `Get-PnPPowerPlatformCustomConnector` |
| Connect-PnPOnline | Using `Connect-PnPOnline` without specifying an authentication option will now default to using an interactive login. If you still want to use logon using client credentials, provide them using -Credentials instead |
| Publish-PnPCompanyApp | The `Publish-PnPCompanyApp` cmdlet has been removed |
| Set-PnPLabel | Use `Set-PnPRetentionLabel` |
| Reset-PnPLabel | Use `Reset-PnPRetentionLabel` |
| Add-PnPTeamsChannel | The parameter `IsFavoriteByDefault` has been removed as it was not supported by Graph API |
| Get-PnPAppAuthAccessToken | It has been removed. Use `Get-PnPAccessToken -ResourceTypeName SharePoint` instead to get SharePoint access token. |
| Request-PnPAccessToken | It has been removed. Use `Get-PnPAccessToken` instead. |
| Get-PnPGraphAccessToken | It has been removed. Use `Get-PnPAccessToken` instead. |
| Remove-PnPUser | The parameter `-Confirm` has been removed. Use `-Force` instead. |
| Remove-PnPAvailableSiteClassification | The parameter `-Confirm` has been removed. Use `-Force` instead. |
| Send-PnPMail | It now throws a warning about the retirement of SharePoint SendEmail API, if you are sending mails via SharePoint. To ignore it, use `-ErrorAction SilentlyContinue` along side the cmdlet. |
| Send-PnPMail | The support for sending mails via SMTP servers is now removed. It is the recommendation of .NET as SMTP doesn't support modern protocols. So, the parameters `-EnableSSL` , `-UserName`, `-Password`, `-Server ` and `-ServerPort` are now removed. Use `Send-PnPMail` with [Microsoft Graph](https://pnp.github.io/powershell/cmdlets/Send-PnPMail.html#send-through-microsoft-graph) |


## Other notable changes

## Changes to output type

| **Cmdlet** | **Comment** |
| ----------- | ---------------------- |
| Get-PnPAccessToken | The output type is now `Microsoft.IdentityModel.JsonWebTokens.JsonWebToken`, earlier it was `System.IdentityModel.Tokens.Jwt` |
