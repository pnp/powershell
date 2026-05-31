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

### Current nightly
```powershell
Get-PnPChangeLog -Nightly [-Verbose]
```

### Specific version
```powershell
Get-PnPChangeLog [-Version <String>] [-Verbose]
```

## DESCRIPTION
This cmdlets returns what has changed in PnP PowerShell in a specific version in markdown format. It is retrieved dynamically from GitHub.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPChangeLog
```

Returns the changelog for the latest released stable version.

### EXAMPLE 2
```powershell
Get-PnPChangeLog -Nightly
```

Returns the changelog for the current nightly build.

### EXAMPLE 3
```powershell
Get-PnPChangeLog -Version 2.12.0
```

Returns the changelog for the 2.12.0 release.

## PARAMETERS

### -Nightly
Return the changelog for the nightly build

```yaml
Type: SwitchParameter
Parameter Sets: Current nightly

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Version
Return the changelog for a specific version. Be sure to use the correct version number in the format <#>.<#>.<#>, i.e. 2.12.0, otherwise the cmdlet will fail. If omitted, the latest stable version will be returned.

```yaml
Type: SwitchParameter
Parameter Sets: Specific version

Required: False
Position: Named
Default value: Latest stable version
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)