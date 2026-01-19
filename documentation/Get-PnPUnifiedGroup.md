# Get-PnPUnifiedGroup

## Description
Gets information about a Microsoft 365 Group (Unified Group) in a SharePoint Multi-Geo tenant. This cmdlet retrieves detailed information about a specific group including its geo location, members, and configuration.

## Syntax

```powershell
Get-PnPUnifiedGroup -GroupAlias <String> [-Connection <PnPConnection>]
```

## Description
This cmdlet retrieves detailed information about a Microsoft 365 Group (also known as Unified Group) in a SharePoint Multi-Geo environment. It provides information about the group's location, members, settings, and associated SharePoint site.

## Examples

### Example 1: Get unified group by alias
```powershell
Get-PnPUnifiedGroup -GroupAlias "marketing"
```
Retrieves information about the Microsoft 365 Group with the alias "marketing".

### Example 2: Get group and display specific properties
```powershell
Get-PnPUnifiedGroup -GroupAlias "sales" | Select-Object DisplayName, DataLocation, SiteUrl, Visibility
```
Gets the sales group and displays only the specified properties.

### Example 3: Get group with verbose output
```powershell
Get-PnPUnifiedGroup -GroupAlias "hr" -Verbose
```
Retrieves the HR group with detailed verbose logging information.

### Example 4: Check group data location
```powershell
$group = Get-PnPUnifiedGroup -GroupAlias "finance"
Write-Host "Group '$($group.DisplayName)' is located in: $($group.DataLocation)"
```
Gets the finance group and displays its current geo data location.

### Example 5: Get group from pipeline
```powershell
"marketing", "sales", "hr" | ForEach-Object { Get-PnPUnifiedGroup -GroupAlias $_ }
```
Retrieves information for multiple groups using pipeline input.

## Parameters

### -GroupAlias
The alias (mail nickname) of the Microsoft 365 Group to retrieve.

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

## Outputs

### PnP.PowerShell.Commands.Model.UnifiedGroup
Returns a UnifiedGroup object containing detailed information about the Microsoft 365 Group, including:
- **Id**: Unique identifier of the group
- **GroupAlias**: The group's alias (mail nickname)
- **DisplayName**: Display name of the group
- **Description**: Description of the group
- **Mail**: Primary email address of the group
- **MailNickname**: Email alias of the group
- **SiteUrl**: URL of the associated SharePoint site
- **DataLocation**: Current geo location where the group is stored
- **CreatedDateTime**: When the group was created
- **LastModifiedDateTime**: When the group was last modified
- **Owners**: Array of group owners
- **Members**: Array of group members
- **Visibility**: Privacy setting (Public/Private)
- **Classification**: Data classification label
- **MailEnabled**: Whether the group is mail-enabled
- **SecurityEnabled**: Whether the group is security-enabled
- **GroupType**: Type of group (typically "Unified")

## Notes
- This cmdlet requires SharePoint Online admin permissions
- The cmdlet works with SharePoint Multi-Geo tenants
- The GroupAlias parameter corresponds to the mail nickname of the Microsoft 365 Group
- Use this cmdlet to check the current geo location of groups before planning moves

## Related Links

[Start-PnPUserAndContentMove](Start-PnPUserAndContentMove.md)
[Start-PnPSiteContentMove](Start-PnPSiteContentMove.md)
[Get-PnPGeoMoveCrossCompatibilityStatus](Get-PnPGeoMoveCrossCompatibilityStatus.md)
[Microsoft 365 Groups documentation](https://docs.microsoft.com/en-us/microsoft-365/admin/create-groups/)
[SharePoint Multi-Geo documentation](https://docs.microsoft.com/en-us/microsoft-365/enterprise/multi-geo-capabilities-in-onedrive-and-sharepoint-online-in-microsoft-365)