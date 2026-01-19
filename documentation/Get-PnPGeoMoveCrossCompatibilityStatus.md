---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPGeoMoveCrossCompatibilityStatus.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPGeoMoveCrossCompatibilityStatus
---
  
# Get-PnPGeoMoveCrossCompatibilityStatus

## SYNOPSIS
Gets the geo move cross-compatibility status for geo locations in a SharePoint Multi-Geo tenant.

## DESCRIPTION
Gets the cross-compatibility status for geo move operations between different data locations in a SharePoint Multi-Geo tenant. This cmdlet helps determine which geo locations are compatible for user and content moves.

## Syntax

```powershell
Get-PnPGeoMoveCrossCompatibilityStatus [-Connection <PnPConnection>]
```

## Description
This cmdlet retrieves the compatibility matrix for user and content moves between different geo locations within a SharePoint Multi-Geo tenant. It shows which source and destination data location combinations are supported for move operations.

## Examples

### Example 1: Get all geo move compatibility statuses
```powershell
Get-PnPGeoMoveCrossCompatibilityStatus
```
Retrieves the compatibility status for moves between all available geo locations in the tenant.

### Example 2: Filter compatible moves only
```powershell
Get-PnPGeoMoveCrossCompatibilityStatus | Where-Object { $_.CompatibilityStatus -eq "Compatible" }
```
Gets only the geo location pairs that are fully compatible for user moves.

### Example 3: Check specific source location compatibility
```powershell
Get-PnPGeoMoveCrossCompatibilityStatus | Where-Object { $_.SourceDataLocation -eq "NAM" }
```
Shows compatibility status for moves originating from the North American data location.

### Example 4: Export compatibility matrix to CSV
```powershell
Get-PnPGeoMoveCrossCompatibilityStatus | Export-Csv -Path "GeoMoveCompatibility.csv" -NoTypeInformation
```
Exports the complete compatibility matrix to a CSV file for analysis and documentation.

## Parameters

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases:
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## Outputs

### System.Management.Automation.PSObject
Returns PSObjects with the following properties:
- **SourceDataLocation**: The source geo location code
- **DestinationDataLocation**: The destination geo location code  
- **CompatibilityStatus**: The compatibility status (Compatible, Incompatible, PartiallyCompatible, Unknown)

## Notes
- This cmdlet requires SharePoint Online admin permissions
- The cmdlet works only with SharePoint Multi-Geo tenants
- Compatibility status may change based on tenant configuration and feature availability
- Use this information to plan user and content moves in advance

### Compatibility Status Values
- **Compatible**: Full support for moves between these locations
- **Incompatible**: Moves are not supported between these locations
- **PartiallyCompatible**: Some limitations may apply to moves between these locations
- **Unknown**: Compatibility status could not be determined

## Related Links

[Start-PnPUserAndContentMove](Start-PnPUserAndContentMove.md)
[Get-PnPUserAndContentMoveState](Get-PnPUserAndContentMoveState.md)
[SharePoint Multi-Geo documentation](https://docs.microsoft.com/en-us/microsoft-365/enterprise/multi-geo-capabilities-in-onedrive-and-sharepoint-online-in-microsoft-365)