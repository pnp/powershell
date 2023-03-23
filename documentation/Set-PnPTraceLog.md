---
Module Name: PnP.PowerShell
title: Set-PnPTraceLog
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTraceLog.html
---
 
# Set-PnPTraceLog

## SYNOPSIS
Turn log tracing on or off

## SYNTAX

### On
```powershell
Set-PnPTraceLog [-On] [-LogFile <String>] [-WriteToConsole] [-Level <LogLevel>] [-Delimiter <String>]
 [-IndentSize <Int32>] [-AutoFlush <Boolean>] [<CommonParameters>]
```

### Off
```powershell
Set-PnPTraceLog [-Off] [<CommonParameters>]
```

## DESCRIPTION
Defines if tracing should be turned on. PnP Core, which is the foundation of these cmdlets, uses the standard Trace functionality of .NET. With this cmdlet you can turn capturing of this trace to a log file on or off. Notice that basically only the Provisioning Engine writes to the trace log which means that cmdlets related to the engine will produce output.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTraceLog -On -LogFile traceoutput.txt
```

This turns on trace logging to the file 'traceoutput.txt' and will capture events of at least 'Information' level.

### EXAMPLE 2
```powershell
Set-PnPTraceLog -On -LogFile traceoutput.txt -Level Debug
```

This turns on trace logging to the file 'traceoutput.txt' and will capture debug events.

### EXAMPLE 3
```powershell
Set-PnPTraceLog -On -LogFile traceoutput.txt -Level Debug -Delimiter ","
```

This turns on trace logging to the file 'traceoutput.txt' and will write the entries as comma separated. Debug events are captured.

### EXAMPLE 4
```powershell
Set-PnPTraceLog -Off
```

This turns off trace logging. It will flush any remaining messages to the log file.

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

### -Delimiter
If specified the trace log entries will be delimited with this value.

```yaml
Type: String
Parameter Sets: On

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IndentSize
Indents in the trace log will be with this amount of characters. Defaults to 4.

```yaml
Type: Int32
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

### -LogFile
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

### -Off
Turn off tracing to log file.

```yaml
Type: SwitchParameter
Parameter Sets: Off

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -On
Turn on tracing to log file

```yaml
Type: SwitchParameter
Parameter Sets: On

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WriteToConsole
Turn on console trace output.

```yaml
Type: SwitchParameter
Parameter Sets: On

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

