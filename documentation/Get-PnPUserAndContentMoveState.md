# Get-PnPUserAndContentMoveState

## Description
Gets the status and details of user and content move operations in a SharePoint Multi-Geo tenant. This cmdlet allows you to monitor the progress of move jobs, check their current state, and retrieve historical move operation data.

## Syntax

### MoveReport (Default)
```powershell
Get-PnPUserAndContentMoveState [-MoveState <MoveState>] [-MoveDirection <MoveDirection>] 
[-Limit <UInt32>] [-MoveStartTime <DateTime>] [-MoveEndTime <DateTime>] [-Connection <PnPConnection>]
```

### UserPrincipalName
```powershell
Get-PnPUserAndContentMoveState -UserPrincipalName <String> [-Connection <PnPConnection>]
```

### OdbMoveId
```powershell
Get-PnPUserAndContentMoveState -OdbMoveId <Guid> [-Connection <PnPConnection>]
```

## Description
This cmdlet retrieves information about user and content move operations in a SharePoint Multi-Geo environment. You can get information about specific moves by user or move ID, or retrieve a filtered list of all move operations within your tenant.

## Examples

### Example 1: Get all move jobs
```powershell
Get-PnPUserAndContentMoveState
```
Retrieves all user move jobs with default filters (last 200 operations).

### Example 2: Get move state for specific user
```powershell
Get-PnPUserAndContentMoveState -UserPrincipalName "john.doe@contoso.com"
```
Gets the move job status for the specified user.

### Example 3: Get move job by ID
```powershell
Get-PnPUserAndContentMoveState -OdbMoveId "12345678-1234-1234-1234-123456789012"
```
Retrieves details for a specific move job using its unique identifier.

### Example 4: Get failed move operations
```powershell
Get-PnPUserAndContentMoveState -MoveState Failed -Limit 50
```
Gets the last 50 failed move operations.

### Example 5: Get move operations within date range
```powershell
Get-PnPUserAndContentMoveState -MoveStartTime (Get-Date).AddDays(-30) -MoveEndTime (Get-Date) -MoveState InProgress
```
Retrieves in-progress move operations that started within the last 30 days.

### Example 6: Get outbound moves to other geo locations
```powershell
Get-PnPUserAndContentMoveState -MoveDirection MoveOut -MoveState Success -Limit 100
```
Gets the last 100 successful moves from the current geo location to other geo locations.

## Parameters

### -MoveState
Specifies the state of move operations to retrieve.

```yaml
Type: MoveState
Parameter Sets: MoveReport
Aliases:
Accepted values: All, NotStarted, InProgress, Success, Failed, ReadyToTrigger, MovedByOtherMeans
Required: False
Position: Named
Default value: All
Accept pipeline input: False
Accept wildcard characters: False
```

### -MoveDirection
Specifies the direction of move operations to retrieve.

```yaml
Type: MoveDirection
Parameter Sets: MoveReport
Aliases:
Accepted values: All, MoveIn, MoveOut
Required: False
Position: Named
Default value: All
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserPrincipalName
The User Principal Name (UPN) of the user whose move job status you want to retrieve.

```yaml
Type: String
Parameter Sets: UserPrincipalName
Aliases:
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OdbMoveId
The unique identifier of the move job to retrieve.

```yaml
Type: Guid
Parameter Sets: OdbMoveId
Aliases:
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Limit
The maximum number of move jobs to retrieve. Valid range is 1-1000.

```yaml
Type: UInt32
Parameter Sets: MoveReport
Aliases:
Required: False
Position: Named
Default value: 200
Accept pipeline input: False
Accept wildcard characters: False
```

### -MoveStartTime
Filters move jobs that started on or after the specified date and time.

```yaml
Type: DateTime
Parameter Sets: MoveReport
Aliases:
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MoveEndTime
Filters move jobs that started on or before the specified date and time.

```yaml
Type: DateTime
Parameter Sets: MoveReport
Aliases:
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## Outputs

### PnP.PowerShell.Commands.Model.UserMoveJob[]
Returns an array of UserMoveJob objects containing details about the move operations, including job ID, status, progress, dates, and any error messages.

## Notes
- This cmdlet requires SharePoint Online admin permissions
- The cmdlet works with SharePoint Multi-Geo tenants
- Move operations are sorted by last modified date in descending order (newest first)

## Related Links

[Start-PnPUserAndContentMove](Start-PnPUserAndContentMove.md)
[SharePoint Multi-Geo documentation](https://docs.microsoft.com/en-us/microsoft-365/enterprise/multi-geo-capabilities-in-onedrive-and-sharepoint-online-in-microsoft-365)