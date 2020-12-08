# PnP PowerShell

![Nightly Release to PowerShell Gallery](https://github.com/pnp/powershell/workflows/Nightly%20Release%20to%20PowerShell%20Gallery/badge.svg?branch=dev) ![Build External Help](https://github.com/pnp/powershell/workflows/Build%20External%20Help/badge.svg?branch=dev)

**You can find the documentation at https://pnp.github.io/powershell**

**PnP PowerShell** is a .NET Core 3.1 / .NET Framework 4.6.1 based PowerShell Module providing over 400 cmdlets that work with Microsoft 365 environments and more specifically SharePoint Online and Microsoft Teams.

This module is a successor of the [PnP-PowerShell](https://github.com/pnp/pnp-powershell) module. The original cmdlets only work on Windows and Windows PowerShell and supports SharePoint On-Premises (2013, 2016 and 2019) and SharePoint Online. This version of the cmdlets is cross-platform (e.g. it works on Windows, MacOS and Linux) however will only support SharePoint Online. Going forward will only be **actively maintaining the cross-platform PnP PowerShell** and once we declare this module as GA we will retired the [PnP-PowerShell](https://github.com/pnp/pnp-powershell) library.

# Installation using the [PowerShell Gallery](https://www.powershellgallery.com)

You can run the following commands to install the PowerShell cmdlets:

```PowerShell
Install-Module PnP.PowerShell -AllowPrerelease
```

## How to Update the Cmdlets 
When using Connect-PnPOnline we will check if a new version is available (only one time during a single PowerShell session).

To update the current installation:

```powershell
Update-Module PnP.PowerShell -AllowPrerelease
``` 

You can check the installed PnP.PowerShell version with the following command:

```powershell
Get-Module PnP.PowerShell -ListAvailable | Select-Object Name,Version | Sort-Object Version -Descending
```

# Important changes
We renamed all *-PnPProvisioningTemplate cmdlets to *-PnPSiteTemplate. This means that Get-PnPProvisioningTemplate is now for instance called Get-PnPSiteTemplate. Also, we renamed both `Apply-PnPProvisioningTemplate` and `Apply-PnPTenantTemplate` to `Invoke-PnPSiteTemplate` and `Invoke-PnPTenantTemplate`.

## Classic credential based authentication has changed
In order to use credentials to authenticate you will first have to grant consent to the PnP Management Shell application:

```powershell
Register-PnPManagementShellAccess
```

Follow the steps on screen and after you have consented the application access you will be able to use

```powershell
Connect-PnPOnline -Url <yoururl> -Credentials <yourcredentials>
```

## -UseWebLogin is not available anymore
The WebLogin functionality is not available anymore among others due to limitations of the .NET Core framework with launching browsers etc. We propose that you switch to Device Login based Auth instead:

```powershell
Connect-PnPOnline -Url <yoururl> -PnPManagementShell
```

# PnP PowerShell roadmap status

We have shipped the version now both PnP PowerShell for classic PowerShell and PnP PowerShell for PowerShell 7

![PnP PowerShell RoadMap](PnP_PowerShell_Roadmap.png)

# I've found a bug, where do I need to log an issue or create a PR

Between now and the end of 2020 both [PnP-PowerShell](https://github.com/pnp/pnp-powershell) and [PnP.PowerShell](https://github.com/pnp/powershell) are actively maintained. Once the new PnP PowerShell GA's we will stop mainting the old repository.

Given that the cross-platform PnP PowerShell is our future going forward we would prefer issues and PRs being created in the new https://github.com/pnp/powershell repository. If you want your PR to apply to both then it is recommend to create the PR in both repositories for the time being.

## Building the source code

Make a clone of the repository, navigate to the build folder in the repository and run Build-Debug.ps1. This will restore all references, and copy the required files to the correct location on your computer. If you run on Windows both the .NET Framework and the .NET Core version will be build in installed. If you run on MacOS or Linux on the .NET Core version will be build and installed. Unlike the older repository you do not need to have local clone of the PnP Framework repository anymore (we changed the PnP Sites Core library used under the hood to the PnP Framework repository, see for more info about that library here: https://github.com/pnp/pnpframework).

## Updating the documentation

All cmdlet documentation has been moved to the https://github.com/pnp/powershell/tree/dev/documentation folder. If you want to make changes, make sure to follow the format as used in the other files present there. These files follow a specific schema which allows us to generate to correct files. You can also make changes directly to the documention at https://docs.microsoft.com/en-us/powershell/sharepoint/sharepoint-pnp/sharepoint-pnp-cmdlets?view=sharepoint-ps. Notice that the documentation there is currently NOT reflecting this library: the documentation applies to the Windows Only version of PnP PowerShell.

-------
This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

<img src="https://telemetry.sharepointpnp.com/pnp-powershell/readme" /> 
