# Start-PnPUserAndContentMove

## Description
Starts a user and content move job in a SharePoint Multi-Geo tenant. This cmdlet allows you to move a user's OneDrive for Business content and associated data from one geo location to another within a multi-geo tenant.

## Syntax

### Default (Default)
```powershell
Start-PnPUserAndContentMove -UserPrincipalName <String> -DestinationDataLocation <String> 
[-PreferredMoveBeginDate <DateTime>] [-PreferredMoveEndDate <DateTime>] [-Notify <String>] 
[-Reserved <String>] [-ValidationOnly] [-Connection <PnPConnection>]
```

## Description
This cmdlet initiates a user and content move operation in a SharePoint Multi-Geo environment. It moves a user's OneDrive for Business content and associated data from their current geo location to a specified destination geo location. The operation is asynchronous and can be monitored using other cmdlets.

## Examples

### Example 1: Basic User Move
```powershell
Start-PnPUserAndContentMove -UserPrincipalName "john.doe@contoso.com" -DestinationDataLocation "EUR"
```
Starts a move job for user john.doe@contoso.com to the European geo location.

### Example 2: Scheduled User Move
```powershell
Start-PnPUserAndContentMove -UserPrincipalName "jane.smith@contoso.com" -DestinationDataLocation "APC" -PreferredMoveBeginDate (Get-Date).AddDays(7) -PreferredMoveEndDate (Get-Date).AddDays(14)
```
Schedules a move job for user jane.smith@contoso.com to the Asia-Pacific geo location, with preferred execution window between 7 and 14 days from now.

### Example 3: Validation Only
```powershell
Start-PnPUserAndContentMove -UserPrincipalName "test.user@contoso.com" -DestinationDataLocation "NAM" -ValidationOnly
```
Performs validation checks for moving test.user@contoso.com to the North American geo location without actually executing the move.

### Example 4: User Move with Notifications
```powershell
Start-PnPUserAndContentMove -UserPrincipalName "admin@contoso.com" -DestinationDataLocation "EUR" -Notify "admin@contoso.com,helpdesk@contoso.com"
```
Starts a move job and sends notifications to specified email addresses when the move completes.

## Parameters

### -UserPrincipalName
The User Principal Name (UPN) of the user whose content should be moved.

```yaml
Type: String
Parameter Sets: (All)
Aliases:
Required: True
Position: 0
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -DestinationDataLocation
The geo location code where the user's content should be moved to (e.g., EUR, NAM, APC).

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

### -Notify
Comma-separated list of email addresses to notify when the move operation completes.

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

### -Reserved
Reserved parameter for internal use.

```yaml
Type: String
Parameter Sets: (All)
Aliases:
Required: False
Position: 5
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
Position: 6
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

## Outputs

### PnP.PowerShell.Commands.Model.UserMoveJob
Returns a UserMoveJob object containing details about the created move job, including the job ID, status, and configuration.

## Related Links

[SharePoint Multi-Geo documentation](https://docs.microsoft.com/en-us/microsoft-365/enterprise/multi-geo-capabilities-in-onedrive-and-sharepoint-online-in-microsoft-365)