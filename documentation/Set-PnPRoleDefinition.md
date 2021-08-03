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
Sets an existing Role Definition (Permission Level) in the site collection in the current context

## SYNTAX

```powershell
Set-PnPRoleDefinition -Identity <RoleDefinitionPipeBind> [-NewRoleName <String>] [-Description <String>] [-Order <Int32>] [-SelectAll] [-ClearAll] [-Select <PermissionKind[]>] [-Clear <PermissionKind[]>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Allows setting an existing Role Definition (Permission Level) in the site collection in the current context.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPRoleDefinition -Identity "CustomPerm" -Clear EditListItems
```
Removes the EditListItems flag from an existing permission level

### EXAMPLE 2
```powershell
Set-PnPRoleDefinition -Identity "NoDelete" -SelectAll -Clear DeleteListItems
```

Select all flags for an existing permission level except DeleteListItems

### EXAMPLE 3
```powershell
Set-PnPRoleDefinition -Identity "CustomPerm" -NewRoleName "NoDelete" -Description "Contribute without delete"
```

Change the name and description of an existing permission level

### EXAMPLE 4
```powershell
Set-PnPRoleDefinition -Identity "CustomPerm" -Order 500
```

Change the order in which the permission level is displayed

## PARAMETERS

### -Identity
The identity of the role definition, either a RoleDefinition object or a the name of roledefinition.

```yaml
Type: RoleDefinitionPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -RoleName
The new name for the permission level.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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

### -Select
Specifies permission flags(s) to enable. Please visit https://docs.microsoft.com/previous-versions/office/sharepoint-csom/ee536458(v%3Doffice.15) for the PermissionKind enum

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

### -Clear
Specifies permission flags(s) to disable. Please visit https://docs.microsoft.com/previous-versions/office/sharepoint-csom/ee536458(v%3Doffice.15) for the PermissionKind enum

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


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
