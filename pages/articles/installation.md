---
uid: pnp.powershell.installation
title: Installing PnP PowerShell
description: Install, update, and uninstall the PnP PowerShell module.
---

# Installing PnP PowerShell

You need PowerShell 7.4.0 or later to use PnP PowerShell. It is available for Windows, Linux and Mac and can be [installed through here](https://learn.microsoft.com/powershell/scripting/install/installing-powershell).

Starting with version 3.1.379-nightly, all releases, including the nightly releases, will be fully digitally signed and will therefore work on machines restricted with the AllSigned PowerShell execution policy set. Major thanks to the [.NET Foundation](https://dotnetfoundation.org) for providing us with the signing certificate to be able to do so.

## Stable build

You can run the following commands to install the latest stable PowerShell cmdlets:

```powershell
Install-Module PnP.PowerShell
```

## Nightly build

If you want to install the latest nightly build prerelease of PnP PowerShell, run:

```powershell
Install-Module PnP.PowerShell -AllowPrerelease
```

## v4 Preview build

If you want to try the latest v4 Preview build of PnP PowerShell, ensure you're running PowerShell 7.6 or later, as it is built on top of .NET 10. 

1. Download the latest ZIP package from where it states Release 4.0.0 Preview:
   https://github.com/pnp/powershell/releases

2. Extract the files from the downloaded ZIP package to a location of your choice, i.e. c:\temp\PnP.PowerShell

3. Open PowerShell and run, adjusting the location according to where you extracted the files from the ZIP file:

   ```powershell
   Import-Module C:\temp\PnP.PowerShell\PnP.PowerShell.psd1
   ```

   Within the PowerShell session, you can now use the PnP PowerShell cmdlets as usual utilizing the v4 Preview build.

## Use in Docker

To use PnP.PowerShell in a Docker container:

```powershell
docker run -it m365pnp/powershell
```

More information on it, [here](docker.md)

# Updating PnP PowerShell

If you already have PnP PowerShell installed and just want to update to the latest version you can follow these steps. If you're not sure if your version is already up to date, it does no harm to run it anyway. If there's no new version available, it will not do anything. You need PowerShell 7.4.0 or later to use PnP PowerShell. It is available for Windows, Linux and Mac and can be [installed through here](https://learn.microsoft.com/powershell/scripting/install/installing-powershell).

## Stable build

You can run the following commands to update to the latest stable PowerShell cmdlets:

```powershell
Update-Module PnP.PowerShell
```

## Nightly build

If you want to update to the latest nightly built prerelease of PnP PowerShell, run:

```powershell
Update-Module PnP.PowerShell -AllowPrerelease -Force
```

# Uninstalling PnP PowerShell

In case you would like to remove PnP PowerShell, you can run:

```powershell
Uninstall-Module PnP.PowerShell -AllVersions
```
