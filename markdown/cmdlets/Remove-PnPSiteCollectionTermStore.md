---
Module Name: PnP.PowerShell
title: Remove-PnPSiteCollectionTermStore
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPSiteCollectionTermStore.html
---
 
# Remove-PnPSiteCollectionTermStore

## SYNOPSIS
Removes the site collection term store if it exists or else nothing will happen.

## SYNTAX

```powershell
Remove-PnPSiteCollectionTermStore [-Connection <PnPConnection>]
```

## DESCRIPTION

Removes the site collection scoped term store for the currently connected site collection. If it does not exist yet, it will not do anything.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPSiteCollectionTermStore
```

Removes the site collection term store.

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

