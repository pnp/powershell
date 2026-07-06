---
applicable: SharePoint Online
Module Name: PnP.PowerShell
title: Get-PnPGeoAdministrator
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
tags: Available in the current Nightly Release only.
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPGeoAdministrator.html
---
 
# Get-PnPGeoAdministrator

## SYNOPSIS
Returns SharePoint Online geo administrators.

## SYNTAX

```powershell
Get-PnPGeoAdministrator [-Connection <PnPConnection>]
```

## DESCRIPTION
Returns the SharePoint Online geo administrators configured for the tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPGeoAdministrator
```

Returns all SharePoint Online geo administrators.

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

### PnP.PowerShell.Commands.Model.GeoAdministrator
Returns objects with `DisplayName`, `LoginName`, `MemberType`, `ObjectId`, and `GeoLocation` properties.

## RELATED LINKS

[Get-PnPMultiGeoCompanyAllowedDataLocation](Get-PnPMultiGeoCompanyAllowedDataLocation.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

