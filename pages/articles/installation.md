# Installing PnP PowerShell

You need PowerShell 7.2 or later to use PnP PowerShell. It is available for Windows, Linux and Mac and can be [installed through here](https://learn.microsoft.com/powershell/scripting/install/installing-powershell).

## Stable build

You can run the following commands to install the latest stable PowerShell cmdlets for the current user:

```powershell
Install-Module PnP.PowerShell -Scope CurrentUser
```

## Nightly build

If you want to install or update to the latest nightly built prerelease of PnP PowerShell for the current user, run:

```powershell
Install-Module PnP.PowerShell -Scope CurrentUser -AllowPrerelease
```

## Use in Docker

To use PnP.PowerShell in a Windows container:

```
docker run -it m365pnp/powershell:1.10.0-nanoserver-1809
```

To use PnP.PowerShell in a Linux container:

```
docker run -it m365pnp/powershell
```

# Uninstalling PnP PowerShell

In case you would like to remove PnP PowerShell, you can run:

```powershell
Uninstall-Module PnP.PowerShell
```
