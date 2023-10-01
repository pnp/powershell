---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Disconnect-PnPOnline.html
external help file: PnP.PowerShell.dll-Help.xml
title: Disconnect-PnPOnline
---
  
# Disconnect-PnPOnline

## SYNOPSIS
Disconnects the current connection and clears its token cache.

## SYNTAX

```powershell
Disconnect-PnPOnline 
```

## DESCRIPTION

Disconnects the current connection and clears its token cache. It will require you to build up a new connection again using [Connect-PnPOnline](Connect-PnPOnline.md) in order to use any of the PnP PowerShell cmdlets. You will have to reauthenticate. If instead you simply want to connect to another site collection within the same tenant using the same credentials you used previously, do not use this cmdlet but instead use `Connect-PnPOnline -Url https://tenant.sharepoint.com/sites/othersite` instead without disconnecting. It will try to reuse the existing authentication method and cached credentials.

Note that this cmdlet does not support passing in a specific connection to disconnect. If you wish to dispose a specific connection you have set up in a variable using `$variable = Connect-PnPOnline -ReturnConnection`, just dispose that variable using `$variable = $null` and it will be cleared from memory.

## EXAMPLES

### EXAMPLE 1
```powershell
Disconnect-PnPOnline
```

This will clear out all active tokens from the current connection

## PARAMETERS

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)