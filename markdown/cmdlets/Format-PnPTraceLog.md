---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Format-PnPTraceLog.html
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
title: Format-PnPTraceLog
Module Name: PnP.PowerShell
---
  
# Format-PnPTraceLog

## SYNOPSIS
Converts a raw line from a PnP PowerShell trace log file into an object

## SYNTAX

```powershell
Format-PnPTraceLog [[-LogLine] <String>] 
```

## DESCRIPTION
This cmdlet takes a single tab separated line as written to a trace log file by [Start-PnPTraceLog](Start-PnPTraceLog.md) and returns it as an object with the time stamp, source, thread id, level, message, elapsed milliseconds and correlation id available as separate properties. This allows a log file to be filtered, sorted and grouped in the same way as the in memory log stream returned by [Get-PnPTraceLog](Get-PnPTraceLog.md).

Use this cmdlet when reading a log file that is still in use for writing, which [Get-PnPTraceLog](Get-PnPTraceLog.md) cannot open. Log lines are read from the pipeline, so an entire file can be piped through it in one go.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-Content -Path "C:\temp\log.txt" | Format-PnPTraceLog
```

Returns every line of the log file at the provided location as an object.

### EXAMPLE 2
```powershell
Get-Content -Path "C:\temp\log.txt" | Format-PnPTraceLog | Where-Object { $_.Level -eq "Error" }
```

Returns only the lines from the log file that have a level of "Error".

### EXAMPLE 3
```powershell
Get-Content -Path "C:\temp\log.txt" -Wait | Format-PnPTraceLog | Select-Object TimeStamp, Source, Message
```

Follows a log file that is currently being written to and shows the time stamp, the cmdlet that logged the entry and the logged message for each new line.

## PARAMETERS

### -LogLine
A single line from a trace log file. Typically provided through the pipeline.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

