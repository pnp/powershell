---
Module Name: PnP.PowerShell
title: Set-PnPStorageEntity
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPStorageEntity.html
---
 
# Set-PnPStorageEntity

## SYNOPSIS
Set Storage Entities / Farm Properties in either the tenant scoped app catalog or the site collection app catalog.

## SYNTAX

```powershell
Set-PnPStorageEntity -Key <String> -Value <String> [-Comment <String>] [-Description <String>]
 [-Scope <StorageEntityScope>] [-Connection <PnPConnection>]   
```

## DESCRIPTION

Allows to set Storage Entities / Farm Properties in either the tenant scoped app catalog or the site collection app catalog.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPStorageEntity -Key MyKey -Value "MyValue" -Comment "My Comment" -Description "My Description"
```

Sets an existing or adds a new storage entity / farm property at tenant level.

### EXAMPLE 2
```powershell
Set-PnPStorageEntity -Scope Site -Key MyKey -Value "MyValue" -Comment "My Comment" -Description "My Description"
```

Sets an existing or adds a new storage entity site collection level.

## PARAMETERS

### -Comment
The comment to set.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Description
The description to set.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Key
The key of the value to set.

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

### -Value
The value to set.

```yaml
Type: String
Parameter Sets: (All)

Required: True
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

