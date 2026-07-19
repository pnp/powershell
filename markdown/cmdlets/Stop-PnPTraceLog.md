---
Module Name: PnP.PowerShell
title: Stop-PnPTraceLog
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTraceLog.html
---
 
# Stops-PnPTraceLog

## SYNOPSIS
Stops all log tracing and flushes the log buffer if any items in there.

## SYNTAX

```powershell
Stop-PnPTraceLog [-StopFileLogging <SwitchParameter>] [-StopConsoleLogging <SwitchParameter>] [-StopLogStreamLogging <SwitchParameter>] [-Verbose]
```

## DESCRIPTION
Stops PnP PowerShell tracelogging to specific targets. By default, all logging is stopped. You can use the parameters to stop specific logging targets only.

You can turn on the trace log with [Start-PnPTraceLog](Start-PnPTraceLog.md).
You can look at the logged data using [Get-PnPTraceLog](Get-PnPTraceLog.md).

## EXAMPLES

### EXAMPLE 1
```powershell
Stop-PnPTraceLog
```

This turns off all trace logging

## EXAMPLES

### EXAMPLE 2
```powershell
Stop-PnPTraceLog -StopFileLogging -StopConsoleLogging
```

This turns off trace logging to file and console, but keeps the other logging options active.

## PARAMETERS

### -StopConsoleLogging
Allows you to specifically stop logging to the console while keeping the other logging options active.

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
Allows you to specifically stop logging to a file while keeping the other logging options active.

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
Allows you to specifically stop logging to the in memory log stream while keeping the other logging options active.

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