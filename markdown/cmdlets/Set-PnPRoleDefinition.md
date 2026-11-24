---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPRoleDefinition.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPRoleDefinition
---
  
# Set-PnPRoleDefinition

## SYNOPSIS
Updates an existing Role Definition (Permission Level) in the site collection in the current context.

## SYNTAX

```powershell
Set-PnPRoleDefinition -Identity <RoleDefinitionPipeBind> [-NewRoleName <String>] [-Description <String>] [-Order <Int32>] [-SelectAll] [-ClearAll] [-Select <PermissionKind[]>] [-Clear <PermissionKind[]>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Allows updating an existing Role Definition (Permission Level) in the site collection in the current context.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPRoleDefinition -Identity "CustomPerm" -Clear EditListItems
```
Removes the EditListItems flag from an existing permission level.

### EXAMPLE 2
```powershell
Set-PnPRoleDefinition -Identity "NoDelete" -SelectAll -Clear DeleteListItems
```

Selects all flags for an existing permission level except DeleteListItems.

### EXAMPLE 3
```powershell
Set-PnPRoleDefinition -Identity "CustomPerm" -NewRoleName "NoDelete" -Description "Contribute without delete"
```

Changes the name and description of an existing permission level.

### EXAMPLE 4
```powershell
Set-PnPRoleDefinition -Identity "CustomPerm" -Order 500
```

Changes the order in which the permission level is displayed.

## PARAMETERS

### -Clear
Specifies permission flag(s) to disable. Please visit https://learn.microsoft.com/previous-versions/office/sharepoint-csom/ee536458(v%3Doffice.15) for the PermissionKind enum.

```yaml
Type: PermissionKind[]
Parameter Sets: (All)
Accepted values: EmptyMask, ViewListItems, AddListItems, EditListItems, DeleteListItems, ApproveItems, OpenItems, ViewVersions, DeleteVersions, CancelCheckout, ManagePersonalViews, ManageLists, ViewFormPages, AnonymousSearchAccessList, Open, ViewPages, AddAndCustomizePages, ApplyThemeAndBorder, ApplyStyleSheets, ViewUsageData, CreateSSCSite, ManageSubwebs, CreateGroups, ManagePermissions, BrowseDirectories, BrowseUserInfo, AddDelPrivateWebParts, UpdatePersonalWebParts, ManageWeb, AnonymousSearchAccessWebLists, UseClientIntegration, UseRemoteAPIs, ManageAlerts, CreateAlerts, EditMyUserInfo, EnumeratePermissions, FullMask

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClearAll
Clears ​all permission flags.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
The new description for the permission level.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The identity of the role definition, either a RoleDefinition object or the name of the RoleDefinition.

```yaml
Type: RoleDefinitionPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -NewRoleName
The new name for the permission level.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Order
Sets the order of the permission level.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Select
Specifies permission flag(s) to enable. Please visit https://learn.microsoft.com/previous-versions/office/sharepoint-csom/ee536458(v%3Doffice.15) for the PermissionKind enum.

```yaml
Type: PermissionKind[]
Parameter Sets: (All)
Accepted values: EmptyMask, ViewListItems, AddListItems, EditListItems, DeleteListItems, ApproveItems, OpenItems, ViewVersions, DeleteVersions, CancelCheckout, ManagePersonalViews, ManageLists, ViewFormPages, AnonymousSearchAccessList, Open, ViewPages, AddAndCustomizePages, ApplyThemeAndBorder, ApplyStyleSheets, ViewUsageData, CreateSSCSite, ManageSubwebs, CreateGroups, ManagePermissions, BrowseDirectories, BrowseUserInfo, AddDelPrivateWebParts, UpdatePersonalWebParts, ManageWeb, AnonymousSearchAccessWebLists, UseClientIntegration, UseRemoteAPIs, ManageAlerts, CreateAlerts, EditMyUserInfo, EnumeratePermissions, FullMask

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SelectAll
Sets all permission flags.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
