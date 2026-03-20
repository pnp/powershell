---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPUnifiedGroupMoveState.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPUnifiedGroupMoveState
---
  
# Get-PnPUnifiedGroupMoveState

## SYNOPSIS
Gets the status of a Microsoft 365 Group move job in a SharePoint Multi-Geo tenant.

## DESCRIPTION
Gets the status and details of a Microsoft 365 Group (Unified Group) move operation in a SharePoint Multi-Geo tenant. This cmdlet allows you to monitor the progress of group move jobs and check their current state.

## Syntax

```powershell
Get-PnPUnifiedGroupMoveState -GroupAlias <String> [-Connection <PnPConnection>]
```

## Description
This cmdlet retrieves information about a Microsoft 365 Group move operation in a SharePoint Multi-Geo environment. You can monitor the progress of group moves, check their current status, and retrieve detailed information about the move operation.

## Examples

### Example 1: Get move state for a specific group
```powershell
Get-PnPUnifiedGroupMoveState -GroupAlias "marketing"
```
Retrieves the move job status for the Microsoft 365 Group with alias "marketing".

### Example 2: Get detailed move information with verbose output
```powershell
Get-PnPUnifiedGroupMoveState -GroupAlias "sales" -Verbose
```
Gets the move state for the sales group with detailed verbose information including source/destination URLs and group size.

### Example 3: Monitor move progress
```powershell
do {
    $moveState = Get-PnPUnifiedGroupMoveState -GroupAlias "hr"
    Write-Host "Move progress: $($moveState.ProgressPercentage)% - Status: $($moveState.Status)"
    Start-Sleep -Seconds 30
} while ($moveState.Status -eq "InProgress")
```
Continuously monitors the progress of an HR group move until completion.

### Example 4: Get move state from pipeline
```powershell
"marketing", "sales", "hr" | ForEach-Object { 
    Get-PnPUnifiedGroupMoveState -GroupAlias $_ | Select-Object GroupAlias, Status, ProgressPercentage 
}
```
Gets move states for multiple groups and displays key information.

### Example 5: Check for failed moves
```powershell
$moveState = Get-PnPUnifiedGroupMoveState -GroupAlias "finance"
if ($moveState.Status -eq "Failed") {
    Write-Warning "Move failed: $($moveState.ErrorMessage)"
}
```
Checks if a group move failed and displays the error message.

## Parameters

### -GroupAlias
The alias (mail nickname) of the Microsoft 365 Group whose move state you want to retrieve.

```yaml
Type: String
Parameter Sets: GroupAlias
Aliases:
Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## Outputs

### System.Management.Automation.PSObject
Returns a PSObject containing details about the group move operation, including:

**Standard Properties (always included):**
- **JobId**: Unique identifier of the move job
- **GroupAlias**: Alias of the group being moved
- **GroupDisplayName**: Display name of the group
- **SourceDataLocation**: Original geo location of the group
- **DestinationDataLocation**: Target geo location for the move
- **Status**: Current status of the move operation
- **CreatedDate**: When the move job was created
- **LastModified**: When the move job was last updated
- **ProgressPercentage**: Completion percentage of the move

**Verbose Properties (included with -Verbose):**
- **CompletedDate**: When the move completed (if applicable)
- **PreferredMoveBeginDateInUtc**: Scheduled start date for the move
- **PreferredMoveEndDateInUtc**: Scheduled completion date for the move
- **ErrorMessage**: Detailed error information (if move failed)
- **ValidationOnly**: Whether this was a validation-only operation
- **SourceSiteUrl**: URL of the source SharePoint site
- **DestinationSiteUrl**: URL of the destination SharePoint site
- **GroupSize**: Size of the group content in bytes

## Notes
- This cmdlet requires SharePoint Online admin permissions
- The cmdlet works with SharePoint Multi-Geo tenants
- Group move operations can take considerable time depending on the size and complexity of the group
- Use this cmdlet to monitor active moves or check the history of completed moves
- The move state includes both the Microsoft 365 Group and its associated SharePoint site

### Common Move States
- **NotStarted**: Move job created but not yet begun
- **InProgress**: Move operation is currently running
- **Completed**: Move operation finished successfully
- **Failed**: Move operation encountered an error
- **Cancelled**: Move operation was cancelled
- **ValidationOnly**: Validation check completed (no actual move)

## Related Links

[Get-PnPUnifiedGroup](Get-PnPUnifiedGroup.md)
[Set-PnPUnifiedGroup](Set-PnPUnifiedGroup.md)
[Start-PnPSiteContentMove](Start-PnPSiteContentMove.md)
[Get-PnPSiteContentMoveState](Get-PnPSiteContentMoveState.md)
[Get-PnPGeoMoveCrossCompatibilityStatus](Get-PnPGeoMoveCrossCompatibilityStatus.md)
[Microsoft 365 Groups documentation](https://docs.microsoft.com/en-us/microsoft-365/admin/create-groups/)
[SharePoint Multi-Geo documentation](https://docs.microsoft.com/en-us/microsoft-365/enterprise/multi-geo-capabilities-in-onedrive-and-sharepoint-online-in-microsoft-365)