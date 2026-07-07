---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPMultiGeoExperience
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPMultiGeoExperience.html
Module Name: PnP.PowerShell
schema: 2.0.0
---
  
# Set-PnPMultiGeoExperience

## SYNOPSIS
Upgrades the tenant multi-geo experience to include SharePoint Online Multi-Geo.

## SYNTAX

```powershell
Set-PnPMultiGeoExperience [-AllInstances] [-Connection <PnPConnection>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
Upgrades the tenant multi-geo experience to include SharePoint Online Multi-Geo. This operation is not reversible and prompts for confirmation before it starts.

The upgrade operation takes some time to take effect.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPMultiGeoExperience
```

Upgrades the current instance's multi-geo experience to include SharePoint Online Multi-Geo.

### EXAMPLE 2

```powershell
Set-PnPMultiGeoExperience -AllInstances
```

Upgrades all instances' multi-geo experience to include SharePoint Online Multi-Geo.

## PARAMETERS

### -AllInstances
Upgrades all instances to the SharePoint Online Multi-Geo experience.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### System.String
Returns the SharePoint Online Management Shell completion message: `This upgrade operation will take some time to take effect. Please run the cmdlet Get-PnPMultiGeoExperience to check the latest mode.`

`Get-PnPMultiGeoExperience` is a SharePoint Online Management Shell cmdlet.

## RELATED LINKS

[Get-PnPMultiGeoExperience](Get-PnPMultiGeoExperience.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

