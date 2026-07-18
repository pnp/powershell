---
title: Get-PnPMultiGeoExperience
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPMultiGeoExperience.html
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
---
  
# Get-PnPMultiGeoExperience

## SYNOPSIS
Returns the SharePoint Online multi-geo experience mode.

## SYNTAX

```powershell
Get-PnPMultiGeoExperience [-Connection <PnPConnection>]
```

## DESCRIPTION
Returns the SharePoint Online multi-geo experience mode for the current geo location.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPMultiGeoExperience
```

Returns the SharePoint Online multi-geo experience mode for the current geo location.

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

## OUTPUTS

### PnP.PowerShell.Commands.Model.MultiGeoExperience
Returns an object with `GeoLocation` and `MultiGeoExperienceMode` properties.

## RELATED LINKS

[Set-PnPMultiGeoExperience](Set-PnPMultiGeoExperience.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

