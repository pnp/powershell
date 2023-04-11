# PnP PowerShell

**PnP PowerShell** is a .NET 6 based PowerShell Module providing over 650 cmdlets that work with Microsoft 365 environments such as SharePoint Online, Microsoft Teams, Microsoft Project, Security & Compliance, Azure Active Directory, and more.

Last version | Last nightly version
-------------|---------------------
[![PnP.PowerShell](https://img.shields.io/powershellgallery/v/pnp.powershell)](https://www.powershellgallery.com/packages/PnP.PowerShell/) | [![PnP.PowerShell](https://img.shields.io/powershellgallery/v/pnp.powershell?include_prereleases)](https://www.powershellgallery.com/packages/PnP.PowerShell/)

This module is a successor of the [PnP-PowerShell](https://github.com/pnp/pnp-powershell) module. The original cmdlets only work on Windows and Windows PowerShell and supports SharePoint On-Premises (2013, 2016 and 2019) and SharePoint Online. This version of the cmdlets is cross-platform (i.e it works on Windows, MacOS and Linux) but it will only support SharePoint Online. Going forward we will only be **actively maintaining the cross-platform PnP PowerShell** module.

For more information about installing or upgrading to this module, please refer to the documentation at https://pnp.github.io/powershell/articles/index.html

## IMPORTANT - New PnP PowerShell 2.x

As the technologies behind PowerShell evolve, so will the PnP PowerShell module. 
Since Microsoft is not supporting .NET 3.1 since December 2022, and .NET Framework not actively developed (and not cross platform), we are currently working on PnP PowerShell 2.x. 

Therefore, **this new version going forward will not support PowerShell 5.1 nor the ISE**. 

PnP PowerShell 2.x is already available should you wish to update now, and we have created a [guide](https://github.com/pnp/powershell/blob/dev/MIGRATE-1.0-to-2.0.md) for that purpose. If you are still using PowerShell 5.1 or the ISE, and want to use the latest major or nightly release, you will need to specify the required version like below: 

`Install-Module PnP.PowerShell -RequiredVersion 1.12.0 -Force`

Both (1.x and 2.x) can be installed in the same machine with no conflict.

## Supportability and SLA

This library is open-source and community provided library with active community providing support for it. This is not Microsoft provided module so there's no SLA or direct support for this open-source component from Microsoft. Please report any issues using the [issues list](https://github.com/pnp/powershell/issues).

---
This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.


<img src="https://m365-visitor-stats.azurewebsites.net/pnp-powershell/readme" />


## Updating from 1.x to 2.x

Please refer to [this page](MIGRATE-1.0-to-2.0.md) while performing an update from your 1.x version to 2.x version of PnP PowerShell.

