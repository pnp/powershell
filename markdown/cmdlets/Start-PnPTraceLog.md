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
Start-PnPTraceLog [-Path <String>] [-Level <LogLevel>] [-AutoFlush <Boolean>] [-WriteToConsole <SwitchParameter>] [-WriteToLogStream <SwitchParameter>]
```

## DESCRIPTION
Starts .NET tracelogging. Many cmdlets output detailed trace information when executed. Turn on the trace log with this cmdlet, optionally specify the level. By default the level is set to 'Information', but you will receive more detail by setting the level to 'Debug'.

You can look at the logged data using [Get-PnPTraceLog](Get-PnPTraceLog.md).

The logged data contains the following information in the following order:

- Timestamp
- Source
- Thread ID
- Log level
- Message
- Elapsed time in milliseconds since the last log entry for the same cmdlet execution
- Correlation ID which is an unique identifier per executed cmdlet so you can filter the log for everything logged during a specific cmdlet execution

Beware that the logged data can be quite verbose, especially when the level is set to 'Debug'. When logging in memory, it can take up a lot of memory. When logging to a file, it can take up a lot of disk space. So be careful when using this in production environments and only use it when you need to troubleshoot something or are aware of the consequences.

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

This turns on trace logging to the file 'TraceOutput.txt' and will capture all events.

### EXAMPLE 3
```powershell
Start-PnPTraceLog -WriteToConsole -WriteToLogStream -Level Debug
```

This turns on trace logging to the console and in memory stream in which you are running your PowerShell script and will capture all events.

### EXAMPLE 3
```powershell
Start-PnPTraceLog -WriteToConsole -Level Debug
```

This turns on trace logging to the console in which you are running your PowerShell script and will capture all events.

## PARAMETERS

### -AutoFlush
Auto flush the trace log. Defaults to true.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: True
Accept pipeline input: False
Accept wildcard characters: False
```

### -Level
The level of events to capture. Possible values are 'Debug', 'Error', 'Warning', 'Information'. Defaults to 'Information'.

```yaml
Type: LogLevel
Parameter Sets: (All)
Accepted values: Debug, Error, Warning, Information

Required: False
Position: Named
Default value: Information
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
The path and filename of the file to write the trace log to.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WriteToConsole
Write the trace log to the console.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -WriteToLogStream
Write the trace log to the in memory stream. Use [Get-PnPTraceLog](Get-PnPTraceLog.md) to read the log stream.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)