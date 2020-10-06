---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpconnection
schema: 2.0.0
title: Get-PnPConnection
---

# Get-PnPConnection

## SYNOPSIS
Returns the current context

## SYNTAX

```
Get-PnPConnection [<CommonParameters>]
```

## DESCRIPTION
Returns a PnP PowerShell Connection for use with the -Connection parameter on other cmdlets.

## EXAMPLES

### EXAMPLE 1
```powershell
$ctx = Get-PnPConnection
```

This will put the current connection for use with the -Connection parameter on other cmdlets.

## PARAMETERS

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)