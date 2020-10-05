# PnP PowerShell  #

### Summary ###
This solution contains a library of PowerShell commands that allows you to perform complex provisioning and artifact management actions towards SharePoint. The commands use a combination of CSOM and REST behind the scenes, and can work against both SharePoint Online as SharePoint On-Premises.

![Microsoft 365 Patterns and Practices](https://devofficecdn.azureedge.net/media/Default/PnP/sppnp.png)
  
### Applies to ###
-  Sharepoint Online

# Commands included #
[Navigate here for an overview of all cmdlets and their parameters](https://docs.microsoft.com/powershell/sharepoint/sharepoint-pnp/sharepoint-pnp-cmdlets?view=sharepoint-ps)

# Installation using the [PowerShell Gallery](https://www.powershellgallery.com)

You can run the following commands to install the PowerShell cmdlets:

```PowerShell
Install-Module PnP.PowerShell
```

## How to Update the Cmdlets 
When using Connect-PnPOnline we will check if a new version is available (only one time during a single PowerShell session).

To update the current installation:

```powershell
Update-Module PnP.PowerShell
``` 

You can check the installed PnP.PowerShell version with the following command:

```powershell
Get-Module PnP.PowerShell -ListAvailable | Select-Object Name,Version | Sort-Object Version -Descending
```

# Getting started #

To use the library you first need to connect to your tenant:

```powershell
Connect-PnPOnline –Url https://yoursite.sharepoint.com –Credentials (Get-Credential)
```

To view all cmdlets, enter:

```powershell
Get-Command -Module PnP.PowerShell
```

## Setting up credentials ##
See this [wiki page](https://github.com/pnp/PnP-PowerShell/wiki/How-to-use-the-Windows-Credential-Manager-to-ease-authentication-with-PnP-PowerShell) for more information on how to use the Windows Credential Manager to setup credentials that you can use in unattended scripts.

# Contributing #

If you want to contribute to this SharePoint Patterns and Practices PowerShell library, please [proceed here](CONTRIBUTING.md)

### Solution/Authors ###
Solution | Author(s)
---------|----------
PnP.PowerShell | Erwin van Hunen and countless community contributors

### Disclaimer ###
**THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.**


## Building the source code ##

If you have set up the projects and you are ready to build the source code, make sure to build the SharePointPnP.PowerShellModuleFilesGenerator project first. This project will be executed after every build and it will generate the required PSD1 and XML files with cmdlet documentation in them.

When you build the solution a postbuild script will copy the required files to a folder in your users folder called 
*C:\Users\\[YourUserName]\Documents\PowerShell\Modules\PnP.PowerShell*. During build also the help and document files will be generated. If you have a session of PowerShell open in which you have used the PnP Cmdlets, make sure to close this PowerShell session first before you build. You will receive a build error otherwise because it tries to overwrite files that are in use.

To debug the cmdlets: launch PowerShell and attach Visual Studio to the `pwsh.exe` process. In case you want to debug methods in the PnP Framework project, make sure that you open the PnP Framework project instead, and then attach Visual Studio to the pwsh.exe. In case you see strange debug behavior, like it wants to debug PSReadLine.ps1, uninstall the PowerShell extension from Visual Studio.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

<img src="https://telemetry.sharepointpnp.com/pnp-powershell/readme" /> 
