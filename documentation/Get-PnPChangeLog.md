---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPChangeLog.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPChangeLog
---
  
# Get-PnPChangeLog

## SYNOPSIS
Returns the changelog for PnP PowerShell

## SYNTAX

```powershell
Get-PnPChangeLog [-Nightly]
```

## DESCRIPTION
This cmdlets returns the changelog in markdown format. It is retrieved dynamically from GitHub.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPChangeLog
```

Returns the changelog for the currently released version.

### EXAMPLE 2
```powershell
Get-PnPChangeLog -Nightly
```

Returns the changelog for the current nightly build.

## PARAMETERS

### -Nightly
Return the changelog for the nightly build

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


