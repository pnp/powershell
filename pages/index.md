# PnP PowerShell
PnP PowerShell is a **cross-platform** PowerShell Module providing over 500 cmdlets that work with Microsoft 365 environments and more specifically SharePoint Online and Microsoft Teams.

This module is a successor of the [PnP-PowerShell](https://github.com/pnp/pnp-powershell) module. The original cmdlets only work on Windows and Windows PowerShell and supports SharePoint On-Premises (2013, 2016 and 2019) and SharePoint Online. This version of the cmdlets is cross-platform (e.g. it works on Windows, MacOS and Linux) however will only support SharePoint Online. Going forward will only be **actively maintaining the cross-platform PnP PowerShell** module.

# Documentation

For documentation, navigate the [articles](/powershell/articles) or the [cmdlets](/powershell/cmdlets/Add-PnPAlert.html) documentation.

# PnP PowerShell roadmap status

We have shipped the version now both PnP PowerShell for classic PowerShell and PnP PowerShell for PowerShell 7

![PnP PowerShell RoadMap](images/PnP_PowerShell_Roadmap.png)

# I've found a bug, where do I need to log an issue or create a PR

Between now and the end of 2020 both [PnP-PowerShell](https://github.com/pnp/pnp-powershell) and [PnP.PowerShell](https://github.com/pnp/powershell) are actively maintained. Once the new PnP PowerShell GA's we will stop mainting the old repository.

Given that the cross-platform PnP PowerShell is our future going forward we would prefer issues and PRs being created in the new https://github.com/pnp/powershell repository. If you want your PR to apply to both then it is recommend to create the PR in both repositories for the time being.

## Building the source code

Make a clone of the repository, navigate to the build folder in the repository and run Build-Debug.ps1. See more details [here](articles/buildingsource.md).

## Updating the documentation

All cmdlet documentation has been moved to the https://github.com/pnp/powershell/tree/dev/documentation folder. If you want to make changes, make sure to follow the format as used in the other files present there. These files follow a specific schema which allows us to generate the correct files. You can also make changes directly to the documention at https://docs.microsoft.com/en-us/powershell/sharepoint/sharepoint-pnp/sharepoint-pnp-cmdlets?view=sharepoint-ps. Notice that the documentation there is currently NOT reflecting this library: the documentation applies to the Windows Only version of PnP PowerShell.

-------
This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

<img src="https://telemetry.sharepointpnp.com/pnp-powershell/readme" /> 
