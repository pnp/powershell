---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPContext.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPContext
---
  
# Get-PnPContext

## SYNOPSIS
Returns the current context

## SYNTAX

```powershell
Get-PnPContext [<CommonParameters>]
```

## DESCRIPTION
Returns a Client Side Object Model context

## EXAMPLES

### EXAMPLE 1
```powershell
$ctx = Get-PnPContext
```

This will put the current context in the $ctx variable.

### EXAMPLE 2
```powershell
Connect-PnPOnline -Url $siteAurl -Credentials $credentials
$ctx = Get-PnPContext
Get-PnPList # returns the lists from site specified with $siteAurl
Connect-PnPOnline -Url $siteBurl -Credentials $credentials
Get-PnPList # returns the lists from the site specified with $siteBurl
Set-PnPContext -Context $ctx # switch back to site A
Get-PnPList # returns the lists from site A
```

## PARAMETERS

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


