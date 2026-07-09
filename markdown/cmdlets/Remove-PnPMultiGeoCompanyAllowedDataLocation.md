---
Module Name: PnP.PowerShell
title: Remove-PnPMultiGeoCompanyAllowedDataLocation
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPMultiGeoCompanyAllowedDataLocation.html
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
---
  
# Remove-PnPMultiGeoCompanyAllowedDataLocation

## SYNOPSIS
Deletes a SharePoint Online multi-geo data location from the tenant.

## SYNTAX

```powershell
Remove-PnPMultiGeoCompanyAllowedDataLocation [-Location] <String> [-Connection <PnPConnection>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
Deletes an allowed SharePoint Online multi-geo data location from the tenant. Any remaining user data in the location will be permanently deleted.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPMultiGeoCompanyAllowedDataLocation -Location EUR
```

Deletes the EUR multi-geo data location from the tenant.

## PARAMETERS

### -Confirm
Prompts you for confirmation before running the cmdlet.

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

### -Location
Specifies the multi-geo location code to delete.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## OUTPUTS

### System.Object
Writes the SharePoint Online Management Shell cancellation message if the delete operation is canceled. On success, writes a warning that deleting the location might take a few hours.

## RELATED LINKS

[Get-PnPMultiGeoCompanyAllowedDataLocation](Get-PnPMultiGeoCompanyAllowedDataLocation.md)

[Set-PnPMultiGeoCompanyAllowedDataLocation](Set-PnPMultiGeoCompanyAllowedDataLocation.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

