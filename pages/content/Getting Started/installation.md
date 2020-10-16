---
title: Updating PnP PowerShell
---

When using `Connect-PnPOnline` we will check if a new version is available and notify you of that. Notice we will only notify you if a new major or minor version is available. We will not notify you if a new nightly build is available.

To update the current installation:

```PowerShell
Update-Module -Name "PnP.PowerShell" -AllowPrerelease
```

You can check the currently installed version of PnP PowerShell with the following command:

```PowerShell
Get-Module -Name "PnP.PowerShell" -ListAvailable | Select-Object Name, Version | Sort-Object Version -Descending
```
