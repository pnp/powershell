# Updating from PnP PowerShell 1.x to 2.x

The 2.x version of PnP PowerShell is based exclusively on .NET 6.0, which means that it will not work on older PowerShell editions like PowerShell 5.1 or ISE.

- The 1.x version of PnP PowerShell was based on .NET Framework 4.6.2 and .NET 3.1.

- We had to update the module to .NET 6.0 because Microsoft removed support for .NET 3.1 in early December 2022.

- We decided to drop support for .NET Framework it is not actively developed, doesn't work across-platforms and only receives maintainence and security updates. So, it would add additional code complexity and maintainabilty issues for us going forward in the future.

- The 2.x version of PnP PowerShell will work only on PowerShell 7.2.x or later versions.

## Steps to update from 1.x to 2.x

- Download and install the PowerShell version from [this link](https://aka.ms/powershell-release?tag=lts)

Or

- For Windows environments, please use [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-windows?view=powershell-7.2) as a guide

- For Linux based environments, please use [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-linux?view=powershell-7.2) as a guide

- For Mac OS envoronments, please use [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-macos?view=powershell-7.2) as a guide

Once PowerShell is downloaded and installed in the environment, you can install the PnP PowerShell module like you normally do.

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

If you are using PnP PowerShell in Azure functions, you might be required to change the PnP PowerShell version used.

- If the Azure function is based on PowerShell 7.0 runtime, then you should keep using PnP PowerShell 1.x versions. The latest version is 1.12.0. The PowerShell 7.0 based runtime is deprecated and not supported anymore by Microsoft. You should consider updating to the 7.2 based runtime.

- If the Azure function is based on PowerShell 7.2 runtime, you should update the PnP PowerShell to the 2.x version. It is currently only available as a nightly build, so you need to explicitly specify the version like `2.0.1-nightly`. This will fix the error that you might be currently facing:  [Could not load type 'Microsoft.Extensions.Logging.Abstractions.Internal.NullScope' from assembly 'Microsoft.Extensions.Logging.Abstractions'](https://github.com/pnp/powershell/issues/2136)

## Breaking changes

- `Export-PnPTaxonomy` cmdlet does not support export of taxonomy using `UTF-7` encoding. If `UTF-7` is specified, it will switch to `UTF-8` encoding.
- There are no other breaking changes.
