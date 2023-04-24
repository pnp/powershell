---
Module Name: PnP.PowerShell
title: Get-PnPStorageEntity
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPStorageEntity.html
---
 
# Get-PnPStorageEntity

## SYNOPSIS
Retrieve Storage Entities / Farm Properties from either the Tenant App Catalog or from the current site if it has a site scope app catalog.

## SYNTAX

```powershell
Get-PnPStorageEntity [-Key <String>] [-Scope <StorageEntityScope>] [-Connection <PnPConnection>] 
  
```

## DESCRIPTION

Allows to retrieve storage entities from either tenant app catalog or current site app catalog.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPStorageEntity
```

Returns all site storage entities/farm properties

### EXAMPLE 2
```powershell
Get-PnPStorageEntity -Key MyKey
```

Returns the storage entity/farm property with the given key.

### EXAMPLE 3
```powershell
Get-PnPStorageEntity -Scope Site
```

Returns all site collection scoped storage entities

### EXAMPLE 4
```powershell
Get-PnPStorageEntity -Key MyKey -Scope Site
```

Returns the storage entity from the site collection with the given key

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
The key of the value to retrieve.

```yaml
Type: String
Parameter Sets: (All)

Required: False
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