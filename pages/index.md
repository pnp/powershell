# PnP PowerShell
PnP PowerShell is a **cross-platform** PowerShell Module providing over 600 cmdlets that work with Microsoft 365 environments and more specifically SharePoint Online, Microsoft Teams, Microsoft Planner and Microsoft Flow.

> [!NOTE]
> This module is a successor of the [SharePointPnPPowerShellOnline](https://github.com/pnp/pnp-powershell) module, which is now regarded legacy and not maintained anymore. This newer version of the PowerShell module is cross-platform (e.g. it works on Windows, MacOS and Linux) however will only support SharePoint Online. Going forward will only be actively maintaining the cross-platform PnP PowerShell module.

# Getting up and running

It is recommended to use PowerShell 7.x, which can be installed by following [these instructions](https://learn.microsoft.com/powershell/scripting/install/installing-powershell-on-windows). Most of the cmdlets should work under PowerShell 5.x as well, though.

To install PnP PowerShell:

```powershell
Install-Module -Name PnP.PowerShell
```

To install a nightly build of PnP PowerShell:
```powershell
Install-Module -Name PnP.PowerShell -AllowPrerelease -SkipPublisherCheck -AllowClobber
```

To use PnP.PowerShell in a Windows container:

```
docker run -it m365pnp/powershell:1.10.0-nanoserver-1809
```

To use PnP.PowerShell in a Linux container:

```
docker run -it m365pnp/powershell
```

See the [articles](/powershell/articles) section for more information on authentication and configuration. All [cmdlets](/powershell/cmdlets/Add-PnPAlert.html) have been documented too.

See [Using PnP PowerShell in Docker containers](/powershell/articles/docker.html) article for information how to run PnP.PowerShell without installing it.

# I've found a bug, where do I need to log an issue or create a PR

You can create an issue at https://github.com/pnp/powershell/issues, but please consider first if asking a question at https://github.com/pnp/powershell/discussions is more appropriate. We would like to focus the issues on actual bugs whereas PnP PowerShell related questions can be asked in the discussions section.

> [!NOTE]
> As some of the code being PnP PowerShell is actually coming from other PnP repositories, we can move your issue over to that specific repository. You will be notified by email about that.

Before you start to work on code change consider starting a discussion in the repository first. It could potentially save you a lot of time if someone is about to submit a change with exactly the same functionality. It will also allow you to discuss a possible change with the maintainers of the repository before you start to work on it.


## PnP PowerShell roadmap status

We have shipped the version now both PnP PowerShell for classic PowerShell and PnP PowerShell for PowerShell 7

![PnP PowerShell RoadMap](images/PnP_PowerShell_Roadmap.png)


## Building the source code

Make a clone of the repository, navigate to the build folder in the repository and run Build-Debug.ps1. See more details [here](articles/buildingsource.md).

## Supportability and SLA

This library is open-source and community provided library with active community providing support for it. This is not Microsoft provided module so there's no SLA or direct support for this open-source component from Microsoft. Please report any issues using the [issues list](https://github.com/pnp/powershell/issues).

## Updating the documentation

All cmdlet documentation has been moved to the https://github.com/pnp/powershell/tree/dev/documentation folder. If you want to make changes, make sure to follow the format as used in the other files present there. These files follow a specific schema which allows us to generate the correct files. You can also make changes directly to the documention at https://learn.microsoft.com/powershell/sharepoint/sharepoint-pnp/sharepoint-pnp-cmdlets?view=sharepoint-ps. Notice that the documentation there is currently NOT reflecting this library: the documentation applies to the Windows Only version of PnP PowerShell.

-------
This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

<img src="https://telemetry.sharepointpnp.com/pnp-powershell/readme" /> 
