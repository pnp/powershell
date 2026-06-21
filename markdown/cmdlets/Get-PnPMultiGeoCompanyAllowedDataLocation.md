---
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPMultiGeoCompanyAllowedDataLocation.html
title: Get-PnPMultiGeoCompanyAllowedDataLocation
applicable: SharePoint Online
schema: 2.0.0
tags: Available in the current Nightly Release only.
Module Name: PnP.PowerShell
---
  
# Get-PnPMultiGeoCompanyAllowedDataLocation

## SYNOPSIS
Returns the multi-geo data locations allowed for the SharePoint Online tenant.

## SYNTAX

```powershell
Get-PnPMultiGeoCompanyAllowedDataLocation [-Connection <PnPConnection>]
```

## DESCRIPTION
Returns the SharePoint Online multi-geo data locations configured for the tenant, including each location code, the associated domain, and whether the location is the default location.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPMultiGeoCompanyAllowedDataLocation
```

Returns all allowed multi-geo data locations for the current tenant.

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

### PnP.PowerShell.Commands.Model.MultiGeoCompanyAllowedDataLocation
Returns objects with `Location`, `Domain`, and `IsDefault` properties.

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

