---
title: Installing PnP PowerShell
---

Open PowerShell, either the legacy Windows PowerShell (the one with the blue background) or install PowerShell Core on either Windows/MacOS/Linux: https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-7

Open your preferred PowerShell and enter

```powershell
Install-Module -Name "PnP.PowerShell" -AllowPrerelease
```

# Using PnP PowerShell in the Azure Cloud Shell

Open your Azure Cloud Shell select "PowerShell" as your shell. Enter:

```powershell
Install-Module -Name "PnP.PowerShell" -AllowPrerelease
```

This is a one time action. The Azure Cloud Shell will remember your installation.