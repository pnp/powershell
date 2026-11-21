---
Module Name: PnP.PowerShell
title: Get-PnPSiteCollectionAdmin
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteCollectionAdmin.html
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

