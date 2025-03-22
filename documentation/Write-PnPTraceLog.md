---
Module Name: PnP.PowerShell
title: Write-PnPTraceLog
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Write-PnPTraceLog.html
---
 
# Write-PnPTraceLog

## SYNOPSIS
Allows logging your own messages during the execution of PnP PowerShell cmdlets

## SYNTAX

### Log from file

```powershell
Write-PnPTraceLog -Path <string> [-Verbose]
```
### Log from log stream

```powershell
Write-PnPTraceLog [-Verbose]
```

## DESCRIPTION
This cmdlet allows logging of your own messages in line with the PnPTraceLog cmdlets that allow logging what happens behind the scenes of the execution of PnP PowerShell cmdlets. This allows you to inject your own custom logging along with the built in logging to get a complete and chronoligal log of the execution of the cmdlets in your scripts.

Look at [Start-PnPTraceLog](Start-PnPTraceLog.md) to see how to start logging to a file and/or to an in memory stream.
You can use [Get-PnPTraceLog](Get-PnPTraceLog.md) to read from the log file or the in memory stream.

## EXAMPLES

### EXAMPLE 1
```powershell
Write-PnPTraceLog "Hello World"
```

This logs the message "Hello World" as an informational message to the built in logging.

### EXAMPLE 2
```powershell
Write-PnPTraceLog "Hello World" -Level Warning
```

This logs the message "Hello World" as an warning message to the built in logging.

### EXAMPLE 3
```powershell
Write-PnPTraceLog "Hello World" -Level Error -Source "MyScript"
```

This logs the message "Hello World" as an error message to the built in logging with a source of "MyScript". The source is used to identify the cmdlet that was executed when the log was created. This is useful to identify which part of your script was executing when the log was created.	

### EXAMPLE 4
```powershell
Write-PnPTraceLog "Hello World" -Level Debug -Source "MyScript" -CorrelationId "5a6206a0-6c83-4446-9d1b-38c14f93cb60" -EllapsedMilliseconds 1000
```

This is the most complete example. It logs the message "Hello World" as a debug message to the built in logging with a source of "MyScript", a correlation id of "5a6206a0-6c83-4446-9d1b-38c14f93cb60" and an ellapsed time of 1000 milliseconds. The correlation id is used to identify the set of operations that were executed when the log was created. The ellapsed time is used to identify how long the operation took to complete. You can provide your own measurements to define this value. If you omit the ellapsed time, the cmdlet will try to define the execution time based on the last logged entry.

## PARAMETERS

### -CorrelationId
An optional GUID as an unique identifier for the cmdlet execution. This is useful to identify which part of your script was executing when the log was created. If not provided, it will leave the value empty.

```yaml
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EllapsedMilliseconds
You can optionaly provide the ellapsed time in milliseconds. This is the time that was taken to execute the operation. If you omit this parameter, the cmdlet will try to define the execution time based on the last logged entry.

```yaml
Type: Long
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Level
The level to log your message under. Options are: Verbose, Information, Warning, Error and Debug. It will log it both to the default PowerShell its logging equivallent such as Write-Warning, Write-Error, Write-Verbose, and to the PnPTraceLog logging. If not provided, it will default to Information.

```yaml
Type: Framework.Diagnostics.LogLevel
Parameter Sets: (All)

Required: False
Position: Named
Default value: Information
Accept pipeline input: False
Accept wildcard characters: False
```

### -Message
The message to log. This is the message that will be logged to the log file or the in memory stream.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Source
The source of the log. This is the source that will be logged to the log file or the in memory stream. This is useful to identify which part of your script was executing when the log was created.

```yaml
Type: String
Parameter Sets: (All)

Required: False
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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)