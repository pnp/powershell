---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/set-pnpcontext
schema: 2.0.0
title: Set-PnPContext
---

# Set-PnPContext

## SYNOPSIS
Set the ClientContext

## SYNTAX

```powershell
Set-PnPContext [-Context] <ClientContext> [<CommonParameters>]
```

## DESCRIPTION
Sets the Client Context to use by the cmdlets, which allows easy context switching. See examples for details.

## EXAMPLES

### EXAMPLE 1
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

### -Context
The ClientContext to set

```yaml
Type: ClientContext
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)