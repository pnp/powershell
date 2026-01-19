# Set-PnPMultiGeoExperience

## Description
Upgrades the multi-geo experience in a SharePoint Multi-Geo tenant. This cmdlet enables enhanced multi-geo capabilities and features across geo locations.

## Syntax

```powershell
Set-PnPMultiGeoExperience [-AllInstances] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

## Description
This cmdlet upgrades the multi-geo experience in a SharePoint Multi-Geo environment. It enables enhanced capabilities and features across geo locations within the tenant. The upgrade can be applied to the current geo instance or to all geo instances in the tenant.

**Important**: This operation may affect existing functionality and cannot be easily reversed. Use with caution in production environments.

## Examples

### Example 1: Upgrade current geo instance
```powershell
Set-PnPMultiGeoExperience
```
Upgrades the multi-geo experience for the current geo location with user confirmation.

### Example 2: Upgrade all geo instances
```powershell
Set-PnPMultiGeoExperience -AllInstances
```
Upgrades the multi-geo experience for all geo locations in the tenant with user confirmation.

### Example 3: Preview the operation without executing
```powershell
Set-PnPMultiGeoExperience -AllInstances -WhatIf
```
Shows what would happen if the upgrade were executed for all instances without actually performing the operation.

### Example 4: Skip confirmation prompt
```powershell
Set-PnPMultiGeoExperience -AllInstances -Confirm:$false
```
Upgrades the multi-geo experience for all instances without prompting for user confirmation.

### Example 5: Force confirmation for safety
```powershell
Set-PnPMultiGeoExperience -AllInstances -Confirm
```
Explicitly requests user confirmation before proceeding with the upgrade for all instances.

## Parameters

### -AllInstances
When specified, upgrades the multi-geo experience for all geo instances in the tenant. If not specified, only the current geo instance is upgraded.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:
Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs without actually executing the operation.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## Outputs

### System.String
Returns a confirmation message indicating the result of the upgrade operation.

## Notes
- This cmdlet requires SharePoint Online admin permissions
- The cmdlet works with SharePoint Multi-Geo tenants
- The operation requires API version 1.3.7 or later
- **This operation cannot be easily reversed** - use with caution
- The upgrade may temporarily affect multi-geo functionality during the process
- It's recommended to test in a non-production environment first
- The operation includes a confirmation prompt by default for safety

### What the Upgrade Includes
The multi-geo experience upgrade typically includes:
- Enhanced cross-geo search capabilities
- Improved user experience for cross-geo navigation
- Updated APIs and functionality for better geo location management
- Enhanced compatibility with newer SharePoint features
- Improved performance for multi-geo operations

### Version Requirements
- Requires multi-geo REST API version 1.3.7 or later
- May require specific SharePoint Online tenant configuration
- Some features may only be available in specific geo regions

### Safety Considerations
- **Backup recommendation**: Ensure you have recent backups before proceeding
- **Testing**: Test the upgrade in a development/staging environment first
- **Timing**: Consider performing the upgrade during maintenance windows
- **Monitoring**: Monitor the tenant after upgrade for any issues
- **Rollback planning**: Have a rollback plan in case issues arise

## Related Links

[Get-PnPGeoMoveCrossCompatibilityStatus](Get-PnPGeoMoveCrossCompatibilityStatus.md)
[Get-PnPUnifiedGroup](Get-PnPUnifiedGroup.md)
[Set-PnPUnifiedGroup](Set-PnPUnifiedGroup.md)
[SharePoint Multi-Geo documentation](https://docs.microsoft.com/en-us/microsoft-365/enterprise/multi-geo-capabilities-in-onedrive-and-sharepoint-online-in-microsoft-365)