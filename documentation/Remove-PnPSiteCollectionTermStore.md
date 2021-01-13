---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/remove-pnpsitecollectiontermstore
schema: 2.0.0
title: Remove-PnPSiteCollectionTermStore
---

# Remove-PnPSiteCollectionTermStore

## SYNOPSIS
Removes the site collection term store if it exists or else nothing will happen

## SYNTAX

```powershell
Remove-PnPSiteCollectionTermStore [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

Removes the site collection scoped term store for the currently connected to site collection. If it does not exist yet, it will not do anything.

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