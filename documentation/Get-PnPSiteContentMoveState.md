# Get-PnPSiteContentMoveState

## Description
Gets the status and details of site content move operations in a SharePoint Multi-Geo tenant. This cmdlet allows you to monitor the progress of site move jobs, check their current state, and retrieve historical site move operation data.

## Syntax

### MoveReport (Default)
```powershell
Get-PnPSiteContentMoveState [-MoveState <MoveState>] [-MoveDirection <MoveDirection>] 
[-Limit <UInt32>] [-MoveStartTime <DateTime>] [-MoveEndTime <DateTime>] [-Connection <PnPConnection>]
```

### SourceSiteUrl
```powershell
Get-PnPSiteContentMoveState -SourceSiteUrl <String> [-Connection <PnPConnection>]
```

### SiteMoveId
```powershell
Get-PnPSiteContentMoveState -SiteMoveId <Guid> [-Connection <PnPConnection>]
```

## Description
This cmdlet retrieves information about site content move operations in a SharePoint Multi-Geo environment. You can get information about specific site moves by URL or move ID, or retrieve a filtered list of all site move operations within your tenant.

## Examples

### Example 1: Get all site move jobs
```powershell
Get-PnPSiteContentMoveState
```
Retrieves all site move jobs with default filters (last 200 operations).

### Example 2: Get move state for specific site
```powershell
Get-PnPSiteContentMoveState -SourceSiteUrl "https://contoso.sharepoint.com/sites/marketing"
```
Gets the move job status for the specified site URL.

### Example 3: Get move job by ID
```powershell
Get-PnPSiteContentMoveState -SiteMoveId "12345678-1234-1234-1234-123456789012"
```
Retrieves details for a specific site move job using its unique identifier.

### Example 4: Get failed site move operations
```powershell
Get-PnPSiteContentMoveState -MoveState Failed -Limit 50
```
Gets the last 50 failed site move operations.

### Example 5: Get site moves within date range
```powershell
Get-PnPSiteContentMoveState -MoveStartTime (Get-Date).AddDays(-30) -MoveEndTime (Get-Date) -MoveState InProgress
```
Retrieves in-progress site move operations that started within the last 30 days.

### Example 6: Get outbound site moves to other geo locations
```powershell
Get-PnPSiteContentMoveState -MoveDirection MoveOut -MoveState Success -Limit 100
```
Gets the last 100 successful site moves from the current geo location to other geo locations.

### Example 7: Export site move data to CSV
```powershell
Get-PnPSiteContentMoveState -Limit 500 | Export-Csv -Path "SiteMoveHistory.csv" -NoTypeInformation
```
Exports site move history to a CSV file for analysis and reporting.

## Parameters

### -MoveState
Specifies the state of site move operations to retrieve.

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
Specifies the direction of site move operations to retrieve.

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

### -SourceSiteUrl
The URL of the source site whose move job status you want to retrieve.

```yaml
Type: String
Parameter Sets: SourceSiteUrl
Aliases:
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteMoveId
The unique identifier of the site move job to retrieve.

```yaml
Type: Guid
Parameter Sets: SiteMoveId
Aliases:
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Limit
The maximum number of site move jobs to retrieve. Valid range is 1-1000.

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
Filters site move jobs that started on or after the specified date and time.

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
Filters site move jobs that started on or before the specified date and time.

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

### System.Management.Automation.PSObject
Returns PSObjects containing details about the site move operations, including:
- **JobId**: Unique identifier for the move job
- **SourceSiteUrl**: Original URL of the site being moved
- **DestinationDataLocation**: Target geo location code
- **DestinationUrl**: New URL after the move
- **Status**: Current status of the move operation
- **CreatedDate**: When the move job was created
- **CompletedDate**: When the move job completed (if applicable)
- **LastModified**: Last time the job status was updated
- **ProgressPercentage**: Completion percentage of the move
- **SiteSize**: Size of the site being moved (in bytes)
- **ErrorMessage**: Error details (if the move failed)

## Notes
- This cmdlet requires SharePoint Online admin permissions
- The cmdlet works with SharePoint Multi-Geo tenants
- Site move operations are sorted by last modified date in descending order (newest first)
- Site moves can take considerable time depending on the size and complexity of the site

## Related Links

[Start-PnPUserAndContentMove](Start-PnPUserAndContentMove.md)
[Get-PnPUserAndContentMoveState](Get-PnPUserAndContentMoveState.md)
[Get-PnPGeoMoveCrossCompatibilityStatus](Get-PnPGeoMoveCrossCompatibilityStatus.md)
[SharePoint Multi-Geo documentation](https://docs.microsoft.com/en-us/microsoft-365/enterprise/multi-geo-capabilities-in-onedrive-and-sharepoint-online-in-microsoft-365)