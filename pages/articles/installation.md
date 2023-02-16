# Installing PnP PowerShell

You can run the following commands to install the latest stable PowerShell cmdlets:

```powershell
Install-Module -Name "PnP.PowerShell" -Scope CurrentUser
```

If you want to install or update to the latest nightly built prerelease of PnP PowerShell, run:

```powershell
Install-Module -Name "PnP.PowerShell" -AllowPrerelease -Scope CurrentUser
```

This will work on Windows / Linux / MacOS.

# Uninstalling PnP PowerShell

In case you would like to remove PnP PowerShell again, you can run:

```powershell
Uninstall-Module -Name "PnP.PowerShell"
```

## Using PnP PowerShell in the Azure Cloud Shell

Open the Azure Cloud Shell at https://shell.azure.com

Select PowerShell as your shell and enter

```powershell
Install-Module -Name "PnP.PowerShell"
```

As the Azure Cloud Shell retains its settings and installed modules, the next time you open the Azure Cloud Shell PnP PowerShell will be available for you to use.
