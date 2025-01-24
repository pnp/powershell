---
Module Name: PnP.PowerShell
title: Start-PnPTraceLog
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Start-PnPTraceLog.html
---
 
# Start-PnPTraceLog

## SYNOPSIS
Starts log tracing

## SYNTAX

```powershell
Start-PnPTraceLog [-Path <String>] [-Level <LogLevel>] [-AutoFlush <Boolean>] 
```


## DESCRIPTION
Starts .NET tracelogging. Many cmdlets output detailed trace information when executed. Turn on the trace log with this cmdlet, optionally specify the level. By default the level is set to 'Information', but you will receive more detail by setting the level to 'Debug'.

## EXAMPLES

### EXAMPLE 1
```powershell
Start-PnPTraceLog -Path ./TraceOutput.txt
```

This turns on trace logging to the file 'TraceOutput.txt' and will capture events of at least 'Information' level.

### EXAMPLE 2
```powershell
Start-PnPTraceLog -Path ./TraceOutput.txt -Level Debug
```

This turns on trace logging to the file 'TraceOutput.txt' and will capture debug events.

## PARAMETERS

### -AutoFlush
Auto flush the trace log. Defaults to true.

```yaml
Type: Boolean
Parameter Sets: On

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Level
The level of events to capture. Possible values are 'Debug', 'Error', 'Warning', 'Information'. Defaults to 'Information'.

```yaml
Type: LogLevel
Parameter Sets: On
Accepted values: Debug, Error, Warning, Information

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
The path and filename of the file to write the trace log to.

```yaml
Type: String
Parameter Sets: On

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

