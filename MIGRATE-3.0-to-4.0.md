# Updating from PnP PowerShell 3.x to 4.x

The 4.x version of PnP PowerShell is based exclusively on .NET 10.0, which means that it will not work on older PowerShell editions like PowerShell 5.1, ISE or PowerShell 7.5 or older. It will work only on **PowerShell 7.6.0 or later editions.**

## Steps to update from 3.x to 4.x

- Download and install the latest PowerShell version from [this GitHub releases link](https://aka.ms/powershell-release?tag=lts)

Or

- For Windows environments, please use [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-windows)

- For Linux based environments, please use [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-linux)

- For Mac OS environments, please use [this link](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-macos)

Once PowerShell 7.6.0 or later is downloaded and installed in your environment, you can install the PnP PowerShell module like you normally do.

```powershell
Install-Module -Name "PnP.PowerShell"
```

If you want to install or update to the latest nightly built prerelease of PnP PowerShell, run:

```powershell
Install-Module -Name "PnP.PowerShell" -AllowPrerelease
```

## Changes needed in Azure DevOps/GitHub Actions/Pipelines

If you are using PnP PowerShell in Azure Devops, GitHub Actions or other pipeline infrastructure, you will have to update your PowerShell version from v5 or v7.4.x to v7.6.0 or later.

Recommend reading these 2 links:

- [DevOps Snack: Change PowerShell version in YAML](https://microsoft-bitools.blogspot.com/2021/02/devops-snack-change-powershell-version.html)
- [How to enable PowerShell core in Azure Pipeline?](https://theautomationcode.com/how-to-enable-powershell-core-in-azure-pipeline/)

## Breaking changes in 4.0

| **Cmdlet** | **Comment** |
| ----------- | ---------------------- |

## Other notable changes

## Changes to output type

| **Cmdlet** | **Comment** |
| ----------- | ---------------------- |
