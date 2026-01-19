---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPUnifiedGroup.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPUnifiedGroup
---
  
# Set-PnPUnifiedGroup

## SYNOPSIS
Updates properties of a Microsoft 365 Group (Unified Group) in a SharePoint Multi-Geo tenant.

## DESCRIPTION
Sets the preferred data location for a Microsoft 365 Group (Unified Group) in a SharePoint Multi-Geo tenant. This cmdlet allows you to specify where the group's content should be stored geographically.

## Syntax

```powershell
Set-PnPUnifiedGroup -GroupAlias <String> -PreferredDataLocation <String> [-Connection <PnPConnection>]
```

## Description
This cmdlet updates the preferred data location for a Microsoft 365 Group (also known as Unified Group) in a SharePoint Multi-Geo environment. The preferred data location determines where the group's content, including the associated SharePoint site, will be stored geographically.

## Examples

### Example 1: Set preferred data location for a group
```powershell
Set-PnPUnifiedGroup -GroupAlias "marketing" -PreferredDataLocation "EUR"
```
Sets the preferred data location for the marketing group to Europe.

### Example 2: Move group to Asia-Pacific region
```powershell
Set-PnPUnifiedGroup -GroupAlias "sales-apac" -PreferredDataLocation "APC"
```
Sets the preferred data location for the sales-apac group to Asia-Pacific.

### Example 3: Set data location for multiple groups
```powershell
"hr-europe", "finance-eu", "legal-emea" | ForEach-Object { Set-PnPUnifiedGroup -GroupAlias $_ -PreferredDataLocation "EUR" }
```
Sets the preferred data location to Europe for multiple groups using pipeline input.

### Example 4: Set data location with verbose output
```powershell
Set-PnPUnifiedGroup -GroupAlias "research" -PreferredDataLocation "NAM" -Verbose
```
Sets the preferred data location for the research group to North America with detailed logging.

### Example 5: Update based on existing group information
```powershell
$group = Get-PnPUnifiedGroup -GroupAlias "projectteam"
Set-PnPUnifiedGroup -GroupAlias $group.GroupAlias -PreferredDataLocation "APC"
```
Gets group information first, then updates its preferred data location.

## Parameters

### -GroupAlias
The alias (mail nickname) of the Microsoft 365 Group to update.

```yaml
Type: String
Parameter Sets: (All)
Aliases:
Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -PreferredDataLocation
The preferred geo location code where the group's content should be stored. Common values include:
- NAM (North America)
- EUR (Europe)
- APC (Asia-Pacific)
- JPN (Japan)
- AUS (Australia)
- IND (India)
- CAN (Canada)
- GBR (United Kingdom)
- FRA (France)
- DEU (Germany)

```yaml
Type: String
Parameter Sets: (All)
Aliases:
Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## Outputs

### System.String
Returns a confirmation message indicating the preferred data location has been updated.

## Notes
- This cmdlet requires SharePoint Online admin permissions
- The cmdlet works with SharePoint Multi-Geo tenants
- Setting a preferred data location does not immediately move existing content
- The preferred data location affects where new content will be created
- Use Get-PnPGeoMoveCrossCompatibilityStatus to check compatibility before setting locations
- Actual content moves may require separate move operations for existing data

### Data Location Codes
The preferred data location should be specified using the appropriate geo location code:
- **NAM**: North America
- **EUR**: Europe  
- **APC**: Asia-Pacific
- **JPN**: Japan
- **AUS**: Australia
- **IND**: India
- **CAN**: Canada
- **GBR**: United Kingdom
- **FRA**: France
- **DEU**: Germany

Contact your Microsoft 365 administrator to confirm which geo locations are available in your tenant.

## Related Links

[Get-PnPUnifiedGroup](Get-PnPUnifiedGroup.md)
[Start-PnPSiteContentMove](Start-PnPSiteContentMove.md)
[Get-PnPGeoMoveCrossCompatibilityStatus](Get-PnPGeoMoveCrossCompatibilityStatus.md)
[Microsoft 365 Groups documentation](https://docs.microsoft.com/en-us/microsoft-365/admin/create-groups/)
[SharePoint Multi-Geo documentation](https://docs.microsoft.com/en-us/microsoft-365/enterprise/multi-geo-capabilities-in-onedrive-and-sharepoint-online-in-microsoft-365)