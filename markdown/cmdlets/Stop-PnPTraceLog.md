---
tags: Available in the current Nightly Release only.
online version: https://pnp.github.io/powershell/cmdlets/Stop-PnPTraceLog.html
Module Name: PnP.PowerShell
schema: 2.0.0
title: Stop-PnPTraceLog
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
---
  
# Stop-PnPTraceLog

## SYNOPSIS
Stops all log tracing and flushes the log buffer if any items in there.

## SYNTAX

```powershell
Stop-PnPTraceLog [-StopFileLogging <SwitchParameter>] [-StopConsoleLogging <SwitchParameter>] [-StopLogStreamLogging <SwitchParameter>] [-Verbose]
```

## DESCRIPTION
Stops PnP PowerShell tracelogging to specific targets. By default, all logging is stopped, also when you only provide one of the parameters, as each of them defaults to `$true`. To keep logging to a target, set its parameter to `$false`, i.e. `-StopLogStreamLogging:$false`.

You can turn on the trace log with [Start-PnPTraceLog](Start-PnPTraceLog.md).
You can look at the logged data using [Get-PnPTraceLog](Get-PnPTraceLog.md).

## EXAMPLES

### EXAMPLE 1
```powershell
Stop-PnPTraceLog
```

This turns off all trace logging

### EXAMPLE 2
```powershell
Stop-PnPTraceLog -StopLogStreamLogging:$false
```

This turns off trace logging to file and console, but keeps logging to the in memory log stream active.

## PARAMETERS

### -StopConsoleLogging
Stops logging to the console. Set it to `$false` to keep logging to the console.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: True
Accept pipeline input: False
Accept wildcard characters: False
```

### -StopFileLogging
Stops logging to a file. Set it to `$false` to keep logging to a file.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: True
Accept pipeline input: False
Accept wildcard characters: False
```

### -StopLogStreamLogging
Stops logging to the in memory log stream. Set it to `$false` to keep logging to the in memory log stream.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: True
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

