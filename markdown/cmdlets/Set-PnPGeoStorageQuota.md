---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPGeoStorageQuota.html
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPGeoStorageQuota
Module Name: PnP.PowerShell
---
  
# Set-PnPGeoStorageQuota

## SYNOPSIS
Sets the allocated storage quota for a SharePoint Online multi-geo location.

## SYNTAX

```powershell
Set-PnPGeoStorageQuota -GeoLocation <String> -StorageQuotaMB <Int64> [-Connection <PnPConnection>]
```

## DESCRIPTION
Sets the allocated storage quota, in megabytes, for a SharePoint Online multi-geo location.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPGeoStorageQuota -GeoLocation EUR -StorageQuotaMB 1048576
```

Sets the allocated storage quota for the EUR geo location to 1,048,576 MB.

## PARAMETERS

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

### -GeoLocation
The multi-geo location code for which to set the allocated storage quota.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StorageQuotaMB
The allocated storage quota for the geo location, in megabytes.

```yaml
Type: Int64
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## OUTPUTS

### None

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

