# Updating from PnP PowerShell 2.x to 3.x

The 3.x version of PnP PowerShell is based exclusively on .NET 8.0, which means that it will not work on older PowerShell editions like PowerShell 5.1, ISE or PowerShell 7.3 or older. It will work only on **PowerShell 7.4.0 or later editions.**

## Steps to update from 2.x to 3.x

- Download and install the latest PowerShell version from [this GitHub releases link](https://aka.ms/powershell-release?tag=lts)

Or

- For Windows environments, please use [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-windows)

- For Linux based environments, please use [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-linux)

- For Mac OS environments, please use [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-macos)

Once PowerShell 7.4.0 or later is downloaded and installed in your environment, you can install the PnP PowerShell module like you normally do.

```powershell
Install-Module -Name "PnP.PowerShell"
```

If you want to install or update to the latest nightly built prerelease of PnP PowerShell, run:

```powershell
Install-Module -Name "PnP.PowerShell" -AllowPrerelease
```

## Changes needed in Azure DevOps/GitHub Actions/Pipelines

If you are using PnP PowerShell in Azure Devops, GitHub Actions or other pipeline infrastructure, you will have to update your PowerShell version from v5 or v7.2.x to v7.4.0 or later.

Recommend reading these 2 links:

- [DevOps Snack: Change PowerShell version in YAML](https://microsoft-bitools.blogspot.com/2021/02/devops-snack-change-powershell-version.html)
- [How to enable PowerShell core in Azure Pipeline?](https://theautomationcode.com/how-to-enable-powershell-core-in-azure-pipeline/)

## Breaking changes in 3.0

| **Cmdlet** | **Comment** |
| ----------- | ---------------------- |
| Add-PnPTeamsChannel | The parameter `IsFavoriteByDefault` has been removed as it was not supported by Graph API |
| Connect-PnPOnline | Using `Connect-PnPOnline` without specifying an authentication option will now default to using an interactive login. If you still want to use logon using client credentials, provide them using `-Credentials` instead |
| Connect-PnPOnline | Removed `-UseWebLogin` on `Connect-PnPOnline`. It used a very outdated and questionable (reusing an auth cookie) authentication method which implementation broke easily. If you require an ACS connection for certain functionality, consider using `-ClientId` in combination with `-ClientSecret` instead. |
| Connect-PnPOnline | Removed `-LaunchBrowser` option for Interactive login flows. It is the default now and removed the popup based authentication option |
| Connect-PnPOnline | Removed `-LaunchBrowser` option for Device Login flows. It is the default now. | 
| Connect-PnPOnline | Removed `-SPOManagementShell` option for authentication. It reduces the risk of changes from Microsoft which can potentially break the scripts . Use your own Entra ID app instead via `-ClientId` parameter. | 
| Get-PnPRetentionLabel | The `Get-PnPRetentionLabel` cmdlet has been renamed to `Get-PnPTenantRetentionLabel` |
| Get-PnPLabel | The `Get-PnPLabel` cmdlet has been renamed to `Get-PnPRetentionLabel` |
| Get-PnPPowerPlatformConnector | The `Get-PnPPowerPlatformConnector` cmdlet has been renamed to `Get-PnPPowerPlatformCustomConnector` |
| Get-PnPAppAuthAccessToken | This cmdlet has been removed. Use `Get-PnPAccessToken -ResourceTypeName SharePoint` instead to get SharePoint access token. |
| Get-PnPGraphAccessToken | This cmdlet has been removed. Use `Get-PnPAccessToken` instead. |
| Get-PnPSharingLink | The parameter `-FileUrl` has been removed. It was marked obsolete. Use `-Identity` instead. |
| Invoke-PnPTransformation | Removed. It was never supported nor fully implemented. |
| Publish-PnPCompanyApp | The `Publish-PnPCompanyApp` cmdlet has been removed |
| Reset-PnPLabel | Use `Reset-PnPRetentionLabel` |
| Request-PnPAccessToken | This cmdlet has been removed. Use `Get-PnPAccessToken` instead. |
| Remove-PnPUser | The parameter `-Confirm` has been removed. Use `-Force` instead. |
| Remove-PnPAvailableSiteClassification | The parameter `-Confirm` has been removed. Use `-Force` instead. |
| Remove-PnPNavigationNode | The parameters `-Title` and `-Header` have been removed. They were marked obsolete. Use `-Identity` instead. |
| Register-PnPEntraIDApp | Removed `-Interactive`,`-LaunchBrowser`, `-NoPopup` and credential based auth. The default auth method is now Interactive.|
| Register-PnPEntraIDAppForInteractiveLogin | Removed `-Interactive`, `-LaunchBrowser`,`-NoPopup` and credential based auth. The default auth method is now Interactive.|
| Set-PnPLabel | Use `Set-PnPRetentionLabel` |
| Send-PnPMail | It now throws a warning about the [retirement of SharePoint SendEmail API](https://devblogs.microsoft.com/microsoft365dev/retirement-of-the-sharepoint-sendemail-api/), if you are sending mails via SharePoint. To ignore the warning, use `-ErrorAction SilentlyContinue` along side the cmdlet. Recommendation is to use `Send-PnPMail` with [Microsoft Graph](https://pnp.github.io/powershell/cmdlets/Send-PnPMail.html#send-through-microsoft-graph) |
| Send-PnPMail | The support for sending mails via SMTP servers is now removed. It is the recommendation of .NET as SMTP doesn't support modern protocols. So, the parameters `-EnableSSL` , `-UserName`, `-Password`, `-Server ` and `-ServerPort` are now removed. Use `Send-PnPMail` with [Microsoft Graph](https://pnp.github.io/powershell/cmdlets/Send-PnPMail.html#send-through-microsoft-graph) |
| Set-PnPMinimalDownloadStrategy | Removed cmdlet. If you need the functionality you can always turn on the feature with `Enable-PnPFeature -Id 87294c72-f260-42f3-a41b-981a2ffce37a` or turn it off with `Disable-PnPFeature -Id 87294c72-f260-42f3-a41b-981a2ffce37a` |
| Set-PnPTraceLog | Removed cmdlet and introduced `Start-PnPTraceLog` and `Stop-PnPTracelog` with similar parameters and functionality |

## Other notable changes

## Changes to output type

| **Cmdlet** | **Comment** |
| ----------- | ---------------------- |
| Get-PnPAccessToken | The output type is now `Microsoft.IdentityModel.JsonWebTokens.JsonWebToken`, earlier it was `System.IdentityModel.Tokens.Jwt` |
| Get-PnPCustomAction| The output type is now `PnP.Core.Model.SharePoint.UserCustomAction`, earlier it was `Microsoft.SharePoint.Client.UserCustomAction` |
