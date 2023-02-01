---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPRoleDefinition.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPRoleDefinition
---
  
# Add-PnPRoleDefinition

## SYNOPSIS
Adds a Role Defintion (Permission Level) to the site collection in the current context

## SYNTAX

```powershell
Add-PnPRoleDefinition -RoleName <String> [-Clone <RoleDefinitionPipeBind>] [-Include <PermissionKind[]>]
 [-Exclude <PermissionKind[]>] [-Description <String>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
This command allows adding a custom Role Defintion (Permission Level) to the site collection in the current context. It does not replace or remove existing Role Definitions.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPRoleDefinition -RoleName "CustomPerm"
```

Creates additional permission level with no permission flags enabled.

### EXAMPLE 2
```powershell
Add-PnPRoleDefinition -RoleName "NoDelete" -Clone "Contribute" -Exclude DeleteListItems
```

Creates additional permission level by cloning "Contribute" and removes flags DeleteListItems

### EXAMPLE 3
```powershell
Add-PnPRoleDefinition -RoleName "AddOnly" -Clone "Contribute" -Exclude DeleteListItems, EditListItems
```

Creates additional permission level by cloning "Contribute" and removes flags DeleteListItems and EditListItems

### EXAMPLE 4
```powershell
$roleDefinition = Get-PnPRoleDefinition -Identity "Contribute"
Add-PnPRoleDefinition -RoleName "AddOnly" -Clone $roleDefinition -Exclude DeleteListItems, EditListItems
```

Creates additional permission level by cloning "Contribute" and removes flags DeleteListItems and EditListItems

## PARAMETERS

### -Clone
An existing permission level or the name of an permission level to clone as base template.

```yaml
Type: RoleDefinitionPipeBind
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
Optional description for the new permission level.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Exclude
Specifies permission flag(s) to disable. Please visit https://learn.microsoft.com/previous-versions/office/sharepoint-csom/ee536458(v%3Doffice.15) for the PermissionKind enum

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

### -Include
Specifies permission flag(s) to enable. Please visit https://learn.microsoft.com/previous-versions/office/sharepoint-csom/ee536458(v%3Doffice.15) for the PermissionKind enum

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

### -RoleName
Name of new permission level.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


