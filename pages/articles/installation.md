# Installing PnP PowerShell

You can run the following commands to install the latest stable PowerShell cmdlets for the current user:

```powershell
Install-Module PnP.PowerShell -Scope CurrentUser
```

If you want to install or update to the latest nightly built prerelease of PnP PowerShell for the current user, run:

```powershell
Install-Module PnP.PowerShell -Scope CurrentUser -AllowPrerelease
```

This will work on Windows / Linux / MacOS.

# Uninstalling PnP PowerShell

In case you would like to remove PnP PowerShell, you can run:

```powershell
Uninstall-Module PnP.PowerShell
```
