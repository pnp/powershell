---
title: Clear-PnPTraceLog
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
online version: https://pnp.github.io/powershell/cmdlets/Clear-PnPTraceLog.html
Module Name: PnP.PowerShell
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
---
  
# Clear-PnPTraceLog

## SYNOPSIS
Clears the log stream in memory

## SYNTAX

```powershell
Clear-PnPTraceLog [-Verbose]
```

## DESCRIPTION
This clears the in memory stored log stream which was started with the [Start-PnPTraceLog -WriteToLogstream](Start-PnPTraceLog.md) cmdlet. It will not clear the log file if one was specified.

## EXAMPLES

### EXAMPLE 1
```powershell
Clear-PnPTraceLog
```

This clears the in memory stored log stream

## PARAMETERS

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

