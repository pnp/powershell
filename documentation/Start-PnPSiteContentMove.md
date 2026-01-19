# Start-PnPSiteContentMove

## Description
Starts a site content move job in a SharePoint Multi-Geo tenant. This cmdlet allows you to move SharePoint sites from one geo location to another within a multi-geo tenant.

## Syntax

### UrlAndDestinationDataLocation (Default)
```powershell
Start-PnPSiteContentMove -SourceSiteUrl <String> -DestinationDataLocation <String> 
[-PreferredMoveBeginDate <DateTime>] [-PreferredMoveEndDate <DateTime>] [-Reserved <String>] 
[-ValidationOnly] [-Force] [-SuppressMarketplaceAppCheck] [-SuppressWorkflow2013Check] 
[-SuppressAllWarnings] [-SuppressBcsCheck] [-Connection <PnPConnection>]
```

### UrlAndDestinationUrl
```powershell
Start-PnPSiteContentMove -SourceSiteUrl <String> -DestinationUrl <String> 
[-PreferredMoveBeginDate <DateTime>] [-PreferredMoveEndDate <DateTime>] [-Reserved <String>] 
[-ValidationOnly] [-Force] [-SuppressMarketplaceAppCheck] [-SuppressWorkflow2013Check] 
[-SuppressAllWarnings] [-SuppressBcsCheck] [-Connection <PnPConnection>]
```

## Description
This cmdlet initiates a site content move operation in a SharePoint Multi-Geo environment. It moves a SharePoint site from its current geo location to a specified destination geo location or URL. The operation is asynchronous and can be monitored using other cmdlets.

## Examples

### Example 1: Basic Site Move to Geo Location
```powershell
Start-PnPSiteContentMove -SourceSiteUrl "https://contoso.sharepoint.com/sites/marketing" -DestinationDataLocation "EUR"
```
Starts a move job for the marketing site to the European geo location.

### Example 2: Site Move to Specific URL
```powershell
Start-PnPSiteContentMove -SourceSiteUrl "https://contoso.sharepoint.com/sites/sales" -DestinationUrl "https://contoso-eur.sharepoint.com/sites/sales-europe"
```
Moves the sales site to a specific URL in the European geo location.

### Example 3: Scheduled Site Move
```powershell
Start-PnPSiteContentMove -SourceSiteUrl "https://contoso.sharepoint.com/sites/hr" -DestinationDataLocation "APC" -PreferredMoveBeginDate (Get-Date).AddDays(7) -PreferredMoveEndDate (Get-Date).AddDays(14)
```
Schedules a move job for the HR site to Asia-Pacific, with preferred execution window between 7 and 14 days from now.

### Example 4: Validation Only
```powershell
Start-PnPSiteContentMove -SourceSiteUrl "https://contoso.sharepoint.com/sites/test" -DestinationDataLocation "NAM" -ValidationOnly
```
Performs validation checks for moving the test site without actually executing the move.

### Example 5: Force Move with Warning Suppression
```powershell
Start-PnPSiteContentMove -SourceSiteUrl "https://contoso.sharepoint.com/sites/legacy" -DestinationDataLocation "EUR" -Force -SuppressWorkflow2013Check -SuppressMarketplaceAppCheck
```
Forces a site move while suppressing specific compatibility warnings.

### Example 6: Move with All Warnings Suppressed
```powershell
Start-PnPSiteContentMove -SourceSiteUrl "https://contoso.sharepoint.com/sites/project" -DestinationDataLocation "APC" -SuppressAllWarnings
```
Moves a site while suppressing all warning checks.

## Parameters

### -SourceSiteUrl
The URL of the source SharePoint site to be moved.

```yaml
Type: String
Parameter Sets: (All)
Aliases:
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DestinationDataLocation
The geo location code where the site should be moved to (e.g., EUR, NAM, APC).

```yaml
Type: String
Parameter Sets: UrlAndDestinationDataLocation
Aliases:
Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DestinationUrl
The specific URL where the site should be moved to.

```yaml
Type: String
Parameter Sets: UrlAndDestinationUrl
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

### PnP.PowerShell.Commands.Model.SiteMoveJob
Returns a SiteMoveJob object containing details about the created site move job, including the job ID, status, and configuration.

## Notes
- This cmdlet requires SharePoint Online admin permissions
- The cmdlet works with SharePoint Multi-Geo tenants
- Site moves can take considerable time depending on the size and complexity of the site
- Use validation mode first to identify potential issues before executing the actual move
- Some features may not be compatible with cross-geo moves and may require suppression flags

### Warning Suppression Guidelines
- **SuppressMarketplaceAppCheck**: Use when the site contains marketplace apps that may not be available in the destination geo
- **SuppressWorkflow2013Check**: Use when the site contains SharePoint 2013 workflows that may not function in the destination geo  
- **SuppressBcsCheck**: Use when the site contains Business Connectivity Services connections that may not work in the destination geo
- **SuppressAllWarnings**: Use with caution - suppresses all compatibility checks and may result in feature loss

## Related Links

[Get-PnPSiteContentMoveState](Get-PnPSiteContentMoveState.md)
[Start-PnPUserAndContentMove](Start-PnPUserAndContentMove.md)
[Get-PnPGeoMoveCrossCompatibilityStatus](Get-PnPGeoMoveCrossCompatibilityStatus.md)
[SharePoint Multi-Geo documentation](https://docs.microsoft.com/en-us/microsoft-365/enterprise/multi-geo-capabilities-in-onedrive-and-sharepoint-online-in-microsoft-365)