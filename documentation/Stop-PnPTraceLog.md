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
Stops .NET tracelogging. Many cmdlets output detailed trace information when executed. Turn on the trace log with Start-PnPTraceLog, optionally specify the level. By default the level is set to 'Information', but you will receive more detail by setting the level to 'Debug'.

## EXAMPLES

### EXAMPLE 1
```powershell
Stop-PnPTraceLog
```

This turns off trace logging 

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

