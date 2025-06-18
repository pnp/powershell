# Installing PnP PowerShell

You need PowerShell 7.4.0 or later to use PnP PowerShell. It is available for Windows, Linux and Mac and can be [installed through here](https://learn.microsoft.com/powershell/scripting/install/installing-powershell).

## Stable build

You can run the following commands to install the latest stable PowerShell cmdlets for the current user:

```powershell
Install-Module PnP.PowerShell -Scope CurrentUser
```

## Nightly build

If you want to install the latest nightly build prerelease of PnP PowerShell for the current user, run:

```powershell
Install-Module PnP.PowerShell -Scope CurrentUser -AllowPrerelease -SkipPublisherCheck
```

## Use in Docker

To use PnP.PowerShell in a Windows container:

```powershell
docker run -it m365pnp/powershell:2.12.0-nanoserver-1809
```

To use PnP.PowerShell in a Linux container:

```powershell
docker run -it m365pnp/powershell
```

# Updating PnP PowerShell

If you already have PnP PowerShell installed and just want to update to the latest version you can follow these steps. If you're not sure if your version is already up to date, it does no harm to run it anyway. If there's no new version available, it will not do anything. You need PowerShell 7.4.0 or later to use PnP PowerShell. It is available for Windows, Linux and Mac and can be [installed through here](https://learn.microsoft.com/powershell/scripting/install/installing-powershell).

## Stable build

You can run the following commands to update to the latest stable PowerShell cmdlets for the current user:

```powershell
Update-Module PnP.PowerShell -Scope CurrentUser
```

## Nightly build

If you want to update to the latest nightly built prerelease of PnP PowerShell for the current user, run:

```powershell
Update-Module PnP.PowerShell -Scope CurrentUser -AllowPrerelease -Force
```

# Uninstalling PnP PowerShell

In case you would like to remove PnP PowerShell, you can run:

```powershell
Uninstall-Module PnP.PowerShell -AllVersions
```
