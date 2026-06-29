---
title: Get-PnPGeoStorageQuota
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPGeoStorageQuota.html
Module Name: PnP.PowerShell
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
---
 
# Get-PnPGeoStorageQuota

## SYNOPSIS
Returns SharePoint Online storage quota details for multi-geo locations.

## SYNTAX

```powershell
Get-PnPGeoStorageQuota [-AllLocations] [-Connection <PnPConnection>]
```

## DESCRIPTION
Returns SharePoint Online storage quota details for the local geo location by default. Use `-AllLocations` to return storage quota details for all multi-geo locations.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPGeoStorageQuota
```

Returns the storage quota details for the local geo location.

### EXAMPLE 2

```powershell
Get-PnPGeoStorageQuota -AllLocations
```

Returns the storage quota details for all multi-geo locations.

## PARAMETERS

### -AllLocations
Returns storage quota details for all multi-geo locations instead of only the local geo location.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by specifying `-ReturnConnection` on `Connect-PnPOnline` or by executing `Get-PnPConnection`.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## OUTPUTS

### PnP.PowerShell.Commands.Model.StorageQuota
Returns objects with `GeoLocation`, `GeoUsedStorageMB`, `GeoAvailableStorageMB`, `GeoAllocatedStorageMB`, `TenantStorageMB`, and `QuotaType` properties.

## RELATED LINKS

[Get-PnPMultiGeoCompanyAllowedDataLocation](Get-PnPMultiGeoCompanyAllowedDataLocation.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

