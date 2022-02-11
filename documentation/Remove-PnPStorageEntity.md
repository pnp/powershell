---
Module Name: PnP.PowerShell
title: Remove-PnPStorageEntity
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPStorageEntity.html
---
 
# Remove-PnPStorageEntity

## SYNOPSIS
Remove Storage Entities / Farm Properties from either the tenant scoped app catalog or the current site collection if the site has a site collection scoped app catalog

## SYNTAX

```powershell
Remove-PnPStorageEntity -Key <String> [-Scope <StorageEntityScope>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPStorageEntity -Key MyKey
```

Removes an existing storage entity / farm property

### EXAMPLE 2
```powershell
Remove-PnPStorageEntity -Key MyKey -Scope Site
```

Removes an existing storage entity from the current site collection

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

### -Key
The key of the value to remove.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scope
Defines the scope of the storage entity. Defaults to Tenant.

```yaml
Type: StorageEntityScope
Parameter Sets: (All)
Accepted values: Site, Tenant

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