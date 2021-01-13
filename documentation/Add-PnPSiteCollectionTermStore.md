---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/add-pnpsitecollectiontermstore
schema: 2.0.0
title: Add-PnPSiteCollectionTermStore
---

# Add-PnPSiteCollectionTermStore

## SYNOPSIS
Creates the site collection term store if it doesn't exist yet or if it does it will return the already existing site collection term store

## SYNTAX

```powershell
Add-PnPSiteCollectionTermStore [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

The site collection scoped term store will be created if it does not exist yet. If it does already exist for the currently connected to site collection, it will return the existing instance.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPSiteCollectionTermStore
```

Returns the site collection term store by creating it if it doesn't exist yet or returning the existing instance if it does

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

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)