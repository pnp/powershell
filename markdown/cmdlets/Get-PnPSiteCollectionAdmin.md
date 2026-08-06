---
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteCollectionAdmin.html
title: Get-PnPSiteCollectionAdmin
Module Name: PnP.PowerShell
applicable: SharePoint Online
---
  
# Get-PnPSiteCollectionAdmin

## SYNOPSIS
Returns the current site collection administrators of the site collection in the current context

## SYNTAX

```powershell
Get-PnPSiteCollectionAdmin [-Connection <PnPConnection>] 
```

## DESCRIPTION
This command will return all current site collection administrators of the site collection in the current context

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteCollectionAdmin
```

This will return all the current site collection administrators of the site collection in the current context

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


