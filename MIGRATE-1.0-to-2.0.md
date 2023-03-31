# Updating from PnP PowerShell 1.x to 2.x

The 2.x version of PnP PowerShell is based exclusively on .NET 6.0, which means that it will not work on older PowerShell editions like PowerShell 5.1 or ISE.

- The 1.x version of PnP PowerShell was based on .NET Framework 4.6.2 and .NET 3.1.

- We had to update the module to .NET 6.0 because Microsoft removed support for .NET 3.1 in early December 2022.

- We decided to drop support for .NET Framework it is not actively developed, doesn't work across-platforms and only receives maintainence and security updates. So, it would add additional code complexity and maintainabilty issues for us going forward in the future.

- The 2.x version of PnP PowerShell will work only on PowerShell 7.2.x or later versions.

## Steps to update from 1.x to 2.x

- Download and install the PowerShell version from [this GitHub releases link](https://aka.ms/powershell-release?tag=lts)

Or

- For Windows environments, please use [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-windows?view=powershell-7.2)

- For Linux based environments, please use [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-linux?view=powershell-7.2)

- For Mac OS envoronments, please use [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-macos?view=powershell-7.2)

Once the PowerShell 7.2.x or later is downloaded and installed in the environment, you can install the PnP PowerShell module like you normally do.

```powershell
Install-Module -Name "PnP.PowerShell"
```

If you want to install or update to the latest nightly built prerelease of PnP PowerShell, run:

```powershell
Install-Module -Name "PnP.PowerShell" -AllowPrerelease
```

## Changes needed in Azure DevOps/GitHub Actions/Pipelines

If you are using PnP PowerShell in Azure Devops, GitHub Actions or other pipeline infrastructure, you will have to update your PowerShell version from v5 to v7.2.x or later.

Recommend referring to these 2 links:

- [DevOps Snack: Change PowerShell version in YAML](https://microsoft-bitools.blogspot.com/2021/02/devops-snack-change-powershell-version.html)
- [How to enable PowerShell core in Azure Pipeline?](https://theautomationcode.com/how-to-enable-powershell-core-in-azure-pipeline/)

## Changes needed in Azure Functions

Using PnP PowerShell in Azure functions ? You might be required to change the PnP PowerShell version used.

- If the Azure function is based on PowerShell 7.0 runtime, then you should keep using PnP PowerShell 1.x versions. The latest version is 1.12.0. The PowerShell 7.0 based runtime is deprecated and not supported anymore by Microsoft. You should consider updating to the 7.2 based runtime.

- If the Azure function is based on PowerShell 7.2 runtime, you should update the PnP PowerShell to the 2.x version. It is currently only available as a nightly build, so you need to explicitly specify the version like `2.0.1-nightly`. This will fix the error that you might be currently facing:  [Could not load type 'Microsoft.Extensions.Logging.Abstractions.Internal.NullScope' from assembly 'Microsoft.Extensions.Logging.Abstractions'](https://github.com/pnp/powershell/issues/2136)

## Breaking changes

| **Cmdlet** | **Obsolete parameter** | **Replacement parameter** | **Comment** |
| ----------- | ---------------------- | -------------------- | --------------------- |
| New-PnPTenantSite | Force | - | The parameter was obsolete and not used. It has been removed. |
| Set-PnPTenantSite | BlockDownloadOfNonViewableFiles | AllowDownloadingNonWebViewableFiles | The parameter was obsolete and hence removed. Use `AllowDownloadingNonWebViewableFiles` |
| Connect-PnPOnline | NoTelemetry | - | The parameter was obsolete and hence removed. Use `$env:PNP_DISABLETELEMETRY` environment variable or `Disable-PnPTelemetry/Enable-PnPTelemetry` cmdlet |
| Connect-PnPOnline | NoVersionCheck | - | The parameter was obsolete and hence removed. Use `$env:PNPPOWERSHELL_UPDATECHECK` environment variable |
| Disconnect-PnPOnline | Connection | - | The parameter was obsolete and hence removed. |
| Request-PnPAccessToken | Resource | Scopes | The parameter was obsolete and hence removed. Use `Scopes` instead |
| Copy-PnPFile | SkipSourceFolderName | - | The parameter was obsolete and has no effect currently nor was it used |
| Get-PnPMicrosoft365Group | ExcludeSiteUrl | IncludeSiteUrl | The parameter was obsolete and hence removed. The site url is now excluded by default. Use `IncludeSiteUrl` instead to include the site url of the underlying SharePoint site. |
| Get-PnPMicrosoft365Group | IncludeClassification | - | The parameter was obsolete and hence removed. The site classification is now included by default. |
| Get-PnPMicrosoft365Group | IncludeHasTeam | - | The parameter was obsolete and hence removed. The `HasTeam` is now included by default. |
| New-PnPGroup | OnlyAllowMembersViewMembership | - | The parameter was obsolete and hence removed. It is now done by default. Use `DisallowMembersViewMembership` to disallow group members viewing membership |
| New-PnPGroup | SetAssociatedGroup | - | The parameter was obsolete and hence removed. Use `Set-PnPGroup` cmdlet instead. |
| Convert-PnPSiteTemplate | - | - | We have removed support for `2019-03` provisioning schema. Use `2019-09` or later versions |
| Get-PnPSiteTemplate | NoBaseTemplate | - | The parameter was obsolete and hence removed. Use of this parameter is generally not required/recommended. It will use the connected web template |
| New-PnPSiteTemplateFromFolder | NoBaseTemplate | - | We have removed support for `2019-03` provisioning schema. Use `2019-09` or later versions |
| Add-PnPTeamsChannel | Private | ChannelType | The parameter was obsolete and hence removed. Use `-ChannelType` instead |
| New-PnPTeamsTeam | Owner | Owners | The parameter was obsolete and hence removed. Use `-Owners` instead which supports setting multiple owner of a Teams team |
| Export-PnPTaxonomy | - | - | The cmdlet does not support export of taxonomy using `UTF-7` encoding. If `UTF-7` is specified, it will switch to `UTF-8` encoding |
| Get-PnPField | ReturnTyped | - | The cmdlet will always return the typed object of the field. |
| Get-PnPUserProfileProperty | - | - | Additional user profile properties are no longer returned under UserProfileProperties but instead will be directly under the returned instance |
| Get-PnPFlowEnvironment | - | - | The alias on the cmdlet has been removed. Use `PnPPowerPlatformEnvironment` instead. |

## Other notable changes

- We have removed support for `2019-03` version of the PnP Provisioning schema from all provisioning related cmdlets. You should be using atleast `2019-09` or a later version of the schema.
- The `-Web` parameter, which was marked obsolete, was used in certain Web level cmdlets. We have removed that. You should use `Connect-PnPOnline -Url [subweburl]` instead to connect to a Web.

## Changes to output type

- When using `Add-PnPTeamsTab` cmdlet, if you specify the `-Type SharePointPageAndList`, then `-WebSiteUrl` is mandatory.
- When using `Add-PnPTeamsTab` cmdlet, if you specify the `-Type Planner`, then `-ContentUrl` is mandatory.
- The output type of `Get-PnPAzureADGroupOwner` has changed to `PnP.PowerShell.Commands.Model.Microsoft365User`
- The output type of `Get-PnPAzureADGroupMember` has changed to `PnP.PowerShell.Commands.Model.Microsoft365User`
- The output type of `Get-PnPAzureADGroup` has changed to `PnP.PowerShell.Commands.Model.Graph.Group`
- The output type of `New-PnPAzureADGroup` has changed to `PnP.PowerShell.Commands.Model.Graph.Group`
- The output type of `Get-PnPUserProfileProperty` has changed to `SortedDictionary<string, object>`