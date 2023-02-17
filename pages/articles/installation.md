# Installing PnP PowerShell

You can run the following commands to install the latest stable PowerShell cmdlets for the current user:

```powershell
Install-Module PnP.PowerShell -Scope CurrentUser
```

If you want to install or update to the latest nightly built prerelease of PnP PowerShell for the current user, run:

```powershell
Install-ModulePnP.PowerShell -Scope CurrentUser -AllowPrerelease
```

This will work on Windows / Linux / MacOS.

# Uninstalling PnP PowerShell

In case you would like to remove PnP PowerShell again, you can run:

```powershell
Uninstall-Module PnP.PowerShell
```

## Using PnP PowerShell in the Azure Cloud Shell

Open the Azure Cloud Shell at https://shell.azure.com

Select PowerShell as your shell and enter

```powershell
Install-Module PnP.PowerShell
```

As the Azure Cloud Shell retains its settings and installed modules, the next time you open the Azure Cloud Shell PnP PowerShell will be available for you to use.
