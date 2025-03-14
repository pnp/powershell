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
Stops log tracing and flushes the log buffer if any items in there.

## SYNTAX

```powershell
Stop-PnPTraceLog
```


## DESCRIPTION
Stops PnP PowerShell tracelogging. Turn on the trace log with [Start-PnPTraceLog](Start-PnPTraceLog.md). Look at the logged data using [Get-PnPTraceLog](Get-PnPTraceLog.md).

## EXAMPLES

### EXAMPLE 1
```powershell
Stop-PnPTraceLog
```

This turns off trace logging 

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)