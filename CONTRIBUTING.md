# Contribution guidance

Please have a look at [our documentation](https://pnp.github.io/powershell/articles/buildingsource.html) to learn how you can contribute to PnP PowerShell!

# Contribution guidance

All PnP repositories are following up on the standard PnP process on getting started and contribute. 

See following PnP wiki page from the main repository for additional details. 

- For getting started guidance, see [Setting up your environment](https://github.com/PnP/PnP/wiki/Setting-up-your-environment). 

*Notice that you'll need to update the URLs based on used repository. All community contributions are also more than welcome. 
Please see following page for additional insights on the model.

- For contributing to PnP, see [Contributing to Microsoft 365 Developer Patterns and Practices](https://github.com/PnP/PnP/wiki/contributing-to-Office-365-developer-patterns-and-practices)
---

## Building the source code ##
Once you have downloaded the code, in the folder with the PnP PowerShell source code, open the solution file PnP.PowerShell.sln.

When you build the solution a post build script will generate and copy the required files to a folder in your users folder called 
`C:\Users\[YourUserName]\Documents\PowerShell\Modules\PnP.PowerShell`. During build also the help and document files will be generated. If you have a session of PowerShell open in which you have used the PnP Cmdlets, make sure to close this PowerShell session first before you build. You will receive a build error otherwise because it tries to overwrite files that are in use.

To debug the cmdlets: launch PowerShell and attach Visual Studio to the powershell.exe process. In case you want to debug methods in PnP Sites Core, make sure that you open the PnP Sites Core project instead, and then attach Visual Studio to the powershell.exe. In case you see strange debug behavior, like it wants to debug PSReadLine.ps1, uninstall the PowerShell extension from Visual Studio.

## Keeping your fork up to date
Before starting on any new submissions, please ensure your fork is up to date with the upstream repository. This avoids frustration and challenges for us to validate and test your submission. Steps on how to easily keep your fork up to date can be found [on this wiki page](https://github.com/pnp/PnP-PowerShell/wiki/Update-your-fork-with-the-latest-code).

## Code contributions
In order to successfully compile the PnP PowerShell solution you will _also_ have to download *and build in Visual Studio* the [PnP-Framework](https://github.com/pnp/pnpframework) repository and make the dev branch available. **The PowerShell solution depends on it**. In order to successfully 
compile it, make sure that PnP-Sites-Core is located at the same level as PnP-PowerShell and you open the solution file `pnpframework.sln` located in the Core subfolder of the sourcecode.

So:
```
c:\[YOUR REPO FOLDER]\pnpframework
c:\[YOUR REPO FOLDER]\powershell
```

The reason for this is that the PnP-PowerShell library will have references to the release and debug builds of the PnP Framework library.

A few notes:

### Most cmdlets will extend PnPCmdlet or PnPWebCmdlet which provides a few helper objects for you to use, like SelectedWeb and ClientContext
As most cmdlets are 'web sensitive' (e.g. you can specify a -Web parameter to point to a different subweb), make sure that you use the correct ClientContext. When a user specifies the -Web parameter
in a cmdlet that extends PnPWebCmdlet, the cmdlet will switch it's internal context to that web, reusing credentials. It is important to use the right context, and the easiest way to do that is to use

```csharp
var context = ClientContext
```

alternatively 

```csharp
var context = SelectedWeb.Context;
```

### Cmdlets will have to use common verbs
 
The verb of a cmdlet (get-, add-, etc.) should follow acceptable cmdlet standards and should be part of one of the built in verbs classes (VerbsCommon, VerbsData, etc.):

## Documentation contributions
The PowerShell documentation is located on https://learn.microsoft.com/powershell/sharepoint/sharepoint-pnp/sharepoint-pnp-cmdlets?view=sharepoint-ps
