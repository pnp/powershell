---
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteCollectionTermStore.html
title: Get-PnPSiteCollectionTermStore
applicable: SharePoint Online
schema: 2.0.0
tags: Available in the current Nightly Release only.
Module Name: PnP.PowerShell
---
  
# Get-PnPSiteCollectionTermStore

## SYNOPSIS
Returns the site collection term store if it exists or else it will not return anything

## SYNTAX

```powershell
Get-PnPSiteCollectionTermStore [-Connection <PnPConnection>] 
```

## DESCRIPTION

Returns the site collection scoped term store for the currently connected to site collection. If it does not exist yet, it will return a null value.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteCollectionTermStore
```

Returns the site collection term store.

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


