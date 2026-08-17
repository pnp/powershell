---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPGeoMoveCrossCompatibilityStatus.html
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPGeoMoveCrossCompatibilityStatus
Module Name: PnP.PowerShell
---
 
# Get-PnPGeoMoveCrossCompatibilityStatus

## SYNOPSIS
Returns compatibility statuses between SharePoint Online multi-geo locations.

## SYNTAX

```powershell
Get-PnPGeoMoveCrossCompatibilityStatus [-Connection <PnPConnection>]
```

## DESCRIPTION
Returns the compatibility between source and destination data locations for site moves in a multi-geo SharePoint Online tenant.

The returned status can be `Compatible`, `Incompatible`, `Warning`, or `Error`.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPGeoMoveCrossCompatibilityStatus
```

Returns the compatibility status for all source and destination geo location combinations.

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

### System.Management.Automation.PSObject
Returns objects with `SourceDataLocation`, `DestinationDataLocation`, and `CompatibilityStatus` properties.

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

