# Updating from PnP PowerShell 1.x to 2.x

The 2.x version of PnP PowerShell is based exclusively on .NET 6.0, which means that it may not work on older PowerShell versions like PowerShell 5.1 or ISE.

- We had to update the module to .NET 6.0 because Microsoft removed support for .NET 3.1 in early December 2022.  So, we had to bump our .NET version.

- The 1.x version of PnP PowerShell was based on .NET Framework 4.6.2 and .NET 3.1.
We decided to drop support for .NET Framework it is not actively developed, doesn't work across-platforms and only receives maintainence and security updates. So, it would add quite a lot of code complexity and maintainabilty issues for us going forward in the future.

The 2.x version of PnP PowerShell will work only on PowerShell 7.2.x or later versions.

## Steps to update from 1.x to 2.x

- Download the PowerShell version from [this link](https://aka.ms/powershell-release?tag=lts)

- For Windows machines, please use [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-windows?view=powershell-7.2)

- For other operating systems, please refer to [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-7.2)

Once downloaded, please install it in your environment. After that, you can install the PnP PowerShell module like you used to do.

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

## Breaking changes

- `Export-PnPTaxonomy` cmdlet does not support export of taxonomy using `UTF-7` encoding. If `UTF-7` is specified, it will switch to `UTF-8` encoding.
- There are no other breaking changes.
