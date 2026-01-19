---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Start-PnPUnifiedGroupMove.html
external help file: PnP.PowerShell.dll-Help.xml
title: Start-PnPUnifiedGroupMove
---
  
# Start-PnPUnifiedGroupMove

## SYNOPSIS
Starts a Microsoft 365 Group move job in a SharePoint Multi-Geo tenant.

## DESCRIPTION
Starts a Microsoft 365 Group (Unified Group) move job in a SharePoint Multi-Geo tenant. This cmdlet allows you to move a Microsoft 365 Group and its associated content from one geo location to another within a multi-geo tenant.

## Syntax

```powershell
Start-PnPUnifiedGroupMove -GroupAlias <String> -DestinationDataLocation <String> 
[-PreferredMoveBeginDate <DateTime>] [-PreferredMoveEndDate <DateTime>] [-Reserved <String>] 
[-ValidationOnly] [-Force] [-SuppressMarketplaceAppCheck] [-SuppressWorkflow2013Check] 
[-SuppressAllWarnings] [-SuppressBcsCheck] [-Connection <PnPConnection>]
```

## Description
This cmdlet initiates a Microsoft 365 Group move operation in a SharePoint Multi-Geo environment. It moves a Microsoft 365 Group, including its mailbox, SharePoint site, and associated content from its current geo location to a specified destination geo location. The operation is asynchronous and can be monitored using Get-PnPUnifiedGroupMoveState.

## Examples

### Example 1: Basic group move to geo location
```powershell
Start-PnPUnifiedGroupMove -GroupAlias "marketing" -DestinationDataLocation "EUR"
```
Starts a move job for the marketing group to the European geo location.

### Example 2: Scheduled group move
```powershell
Start-PnPUnifiedGroupMove -GroupAlias "sales" -DestinationDataLocation "APC" -PreferredMoveBeginDate (Get-Date).AddDays(7) -PreferredMoveEndDate (Get-Date).AddDays(14)
```
Schedules a move job for the sales group to Asia-Pacific, with preferred execution window between 7 and 14 days from now.

### Example 3: Validation only
```powershell
Start-PnPUnifiedGroupMove -GroupAlias "hr" -DestinationDataLocation "NAM" -ValidationOnly
```
Performs validation checks for moving the HR group without actually executing the move.

### Example 4: Force move with warning suppression
```powershell
Start-PnPUnifiedGroupMove -GroupAlias "legacy" -DestinationDataLocation "EUR" -Force -SuppressWorkflow2013Check -SuppressMarketplaceAppCheck
```
Forces a group move while suppressing specific compatibility warnings.

### Example 5: Move with all warnings suppressed
```powershell
Start-PnPUnifiedGroupMove -GroupAlias "project" -DestinationDataLocation "APC" -SuppressAllWarnings
```
Moves a group while suppressing all warning checks.

### Example 6: Move multiple groups from pipeline
```powershell
"team1", "team2", "team3" | ForEach-Object { Start-PnPUnifiedGroupMove -GroupAlias $_ -DestinationDataLocation "EUR" -ValidationOnly }
```
Validates the move for multiple groups to Europe using pipeline input.

## Parameters

### -GroupAlias
The alias (mail nickname) of the Microsoft 365 Group to be moved.

```yaml
Type: String
Parameter Sets: GroupAliasAndDestinationDataLocation
Aliases:
Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -DestinationDataLocation
The geo location code where the group should be moved to (e.g., EUR, NAM, APC).

```yaml
Type: String
Parameter Sets: GroupAliasAndDestinationDataLocation
Aliases:
Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PreferredMoveBeginDate
The preferred date and time when the move operation should begin.

```yaml
Type: DateTime
Parameter Sets: (All)
Aliases:
Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PreferredMoveEndDate
The preferred date and time by when the move operation should complete.

```yaml
Type: DateTime
Parameter Sets: (All)
Aliases:
Required: False
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Reserved
Reserved parameter for internal use.

```yaml
Type: String
Parameter Sets: (All)
Aliases:
Required: False
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ValidationOnly
When specified, only validates the move operation without actually executing it.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:
Required: False
Position: 5
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Forces the move operation and suppresses all warning checks.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:
Required: False
Position: 6
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -SuppressMarketplaceAppCheck
Suppresses the marketplace app compatibility check.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:
Required: False
Position: 7
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -SuppressWorkflow2013Check
Suppresses the SharePoint 2013 workflow compatibility check.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:
Required: False
Position: 8
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -SuppressAllWarnings
Suppresses all warning and compatibility checks.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:
Required: False
Position: 9
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -SuppressBcsCheck
Suppresses the Business Connectivity Services (BCS) compatibility check.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:
Required: False
Position: 10
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

## Outputs

### PnP.PowerShell.Commands.Model.GroupMoveJob
Returns a GroupMoveJob object containing details about the created group move job, including the job ID, status, and configuration.

## Notes
- This cmdlet requires SharePoint Online admin permissions
- The cmdlet works with SharePoint Multi-Geo tenants
- Group moves include both the Microsoft 365 Group (Exchange) and SharePoint site content
- Group moves can take considerable time depending on the size and complexity of the group
- Use validation mode first to identify potential issues before executing the actual move
- Some features may not be compatible with cross-geo moves and may require suppression flags

### What Gets Moved
A Microsoft 365 Group move includes:
- **Exchange mailbox**: Group conversations, calendar, and mail data
- **SharePoint site**: Document libraries, lists, and site content
- **Teams data**: If the group is Teams-enabled, the Teams data moves with the group
- **OneNote**: Group OneNote notebooks
- **Planner**: If associated, Planner data

### Warning Suppression Guidelines
- **SuppressMarketplaceAppCheck**: Use when the group's SharePoint site contains marketplace apps that may not be available in the destination geo
- **SuppressWorkflow2013Check**: Use when the site contains SharePoint 2013 workflows that may not function in the destination geo
- **SuppressBcsCheck**: Use when the site contains Business Connectivity Services connections that may not work in the destination geo
- **SuppressAllWarnings**: Use with caution - suppresses all compatibility checks and may result in feature loss

## Related Links

[Get-PnPUnifiedGroupMoveState](Get-PnPUnifiedGroupMoveState.md)
[Get-PnPUnifiedGroup](Get-PnPUnifiedGroup.md)
[Set-PnPUnifiedGroup](Set-PnPUnifiedGroup.md)
[Start-PnPSiteContentMove](Start-PnPSiteContentMove.md)
[Get-PnPGeoMoveCrossCompatibilityStatus](Get-PnPGeoMoveCrossCompatibilityStatus.md)
[Microsoft 365 Groups documentation](https://docs.microsoft.com/en-us/microsoft-365/admin/create-groups/)
[SharePoint Multi-Geo documentation](https://docs.microsoft.com/en-us/microsoft-365/enterprise/multi-geo-capabilities-in-onedrive-and-sharepoint-online-in-microsoft-365)