---
Module Name: PnP.PowerShell
title: Get-PnPTraceLog
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTraceLog.html
---
 
# Get-PnPTraceLog

## SYNOPSIS
Returns logged messages during the execution of PnP PowerShell cmdlets

## SYNTAX

### Log from file

```powershell
Get-PnPTraceLog -Path <string> [-Verbose]
```
### Log from log stream

```powershell
Get-PnPTraceLog [-Verbose]
```

## DESCRIPTION
This cmdlet returns the logged messages during the execution of PnP PowerShell cmdlets. It can return the messages from an in memory log stream or from a file. Note that you cannot read from a log file if it is currently in use to write to. In this case, you would first have to stop logging to it using [Stop-PnPTraceLog](Stop-PnPTraceLog.md) and then read the log file. The in memory log stream is always available.

You can use [Start-PnPTraceLog](Start-PnPTraceLog.md) to start logging to a file and/or to an in memory stream.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTraceLog
```

This returns all items in the in memory stored log stream

### EXAMPLE 2
```powershell
Get-PnPTraceLog -Path "C:\temp\log.txt"
```

This returns all items from the log file stored at the provided location. Note that you cannot read from a log file if it is currently in use to write to. In this case, you would first have to stop logging to it using [Stop-PnPTraceLog](Stop-PnPTraceLog.md) and then read the log file.

### EXAMPLE 3
```powershell
Get-PnPTraceLog | Where-Object { $_.Level -eq "Error" }
```

This returns only logged items from the in memory stored log stream that have a level of "Error"

### EXAMPLE 4
```powershell
Get-PnPTraceLog | Where-Object { $_.CorrelationId -eq "5a6206a0-6c83-4446-9d1b-38c14f93cb60" }
```

This returns only logged items from the in memory stored log stream that happened during the execution of a PnP PowerShell cmdlet with the provided correlation id. This is useful to find out what happened during the execution of a specific cmdlet. Mind that the correlation id is an unique identifier for the cmdlet execution assigned by PnP PowerShell and is not the same as the correlation id of a SharePoint operation.

### EXAMPLE 5
```powershell
Get-PnPTraceLog | Sort-Object -Property EllapsedMilliseconds -Descending -Top 10 | Select EllapsedMilliseconds, Source, Message
```

Returns the 10 longest running operations from the in memory stored log stream. An operation is an action within the execution of a cmdlet. The output is sorted by the time it took to complete the operation with the longest execution at the top. The output shows the ellapsed time in milliseconds taken by a single operation, the cmdlet that was executed and the message that was logged.

### EXAMPLE 6
```powershell
Get-PnPTraceLog | Group-Object -Property CorrelationId | ForEach-Object { [pscustomobject]@{ Started = ($_.Group | Select -First 1).TimeStamp; Ended = ($_.Group | Select -Last 1).TimeStamp; Cmdlet = $_.Group[0].Source; TimeTaken = ($_.Group | Measure-Object -Property EllapsedMilliseconds -Sum).Sum; Logs = $_.Group }} | Sort-Object -Property TimeTaken -Descending -Top 5 | Select Started, Cmdlet, TimeTaken
```

Returns the top 5 longest running cmdlets from the in memory stored log stream. The output is sorted by the TimeTaken property in descending order which shows the total execution time of a single cmdlet. The output contains the time the cmdlet started executing, the cmdlet that was executed and the total time it took to execute the cmdlet. From there it is easy to examine all the individual logs collected during the execution of that single cmdlet.

## PARAMETERS

### -Path
The path to the log file. If not provided, the cmdlet will return the in memory log stream.

Note that you cannot read from a log file if it is currently in use to write to. In this case, you would first have to stop logging to it using [Stop-PnPTraceLog](Stop-PnPTraceLog.md) and then read the log file.

```yaml
Type: String
Parameter Sets: Log from file

Required: True
Position: Named
Default value: None
Accept pipeline input: True
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