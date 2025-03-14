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
This cmdlet returns the logged messages during the execution of PnP PowerShell cmdlets. It can return the messages from an in memory log stream or from a file. You can use [Start-PnPTraceLog](Start-PnPTraceLog.md) to start logging to a file and/or to an in memory stream.

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

This returns all items from the log file stored at the provided location

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

## PARAMETERS

### -Path
The path to the log file. If not provided, the cmdlet will return the in memory log stream.

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