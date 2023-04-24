---
Module Name: PnP.PowerShell
title: Set-PnPListItemPermission
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPListItemPermission.html
---
 
# Set-PnPListItemPermission

## SYNOPSIS
Sets list item permissions. 
Use Get-PnPRoleDefinition to retrieve all available roles you can add or remove using this cmdlet.
Use Get-PnPGroup to retrieve all groups available that you can add or remove using this cmdlet when using the -group parameter.


## SYNTAX

### User (Default)
```powershell
Set-PnPListItemPermission [-List] <ListPipeBind> -Identity <ListItemPipeBind> -User <String>
 [-AddRole <String>] [-RemoveRole <String>] [-ClearExisting] [-SystemUpdate] 
 [-Connection <PnPConnection>] 
```

### Group
```powershell
Set-PnPListItemPermission [-List] <ListPipeBind> -Identity <ListItemPipeBind> -Group <GroupPipeBind>
 [-AddRole <String>] [-RemoveRole <String>] [-ClearExisting] [-SystemUpdate] 
 [-Connection <PnPConnection>] 
```

### Inherit
```powershell
Set-PnPListItemPermission [-List] <ListPipeBind> -Identity <ListItemPipeBind> [-InheritPermissions]
 [-SystemUpdate] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to modify list item permissions.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPListItemPermission -List 'Documents' -Identity 1 -User 'user@contoso.com' -AddRole 'Contribute'
```

Adds the 'Contribute' permission to the user 'user@contoso.com' for listitem with id 1 in the list 'Documents'

### EXAMPLE 2
```powershell
Set-PnPListItemPermission -List 'Documents' -Identity 1 -User 'user@contoso.com' -RemoveRole 'Contribute'
```

Removes the 'Contribute' permission to the user 'user@contoso.com' for listitem with id 1 in the list 'Documents'

### EXAMPLE 3
```powershell
Set-PnPListItemPermission -List 'Documents' -Identity 1 -User 'user@contoso.com' -AddRole 'Contribute' -ClearExisting
```

Adds the 'Contribute' permission to the user 'user@contoso.com' for listitem with id 1 in the list 'Documents' and removes all other permissions

### EXAMPLE 4
```powershell
Set-PnPListItemPermission -List 'Documents' -Identity 1 -InheritPermissions
```

Resets permissions for listitem with id 1 to inherit permissions from the list 'Documents'

### EXAMPLE 5
```powershell
Set-PnPListItemPermission -List 'Documents' -Identity 1 -AddRole 'Read' -RemoveRole 'Contribute' -Group "Site collection Visitors"
```

Adds the 'Read' permission to the group of Site collection Visitors while removing the 'Contribute' permissions


## PARAMETERS

### -AddRole
The role that must be assigned to the group or user

```yaml
Type: RoleDefinitionPipeBind
Parameter Sets: User, Group

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClearExisting
Clear all existing permissions

```yaml
Type: SwitchParameter
Parameter Sets: User, Group

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

### -Group
A group object, an ID or a name of a group.

```yaml
Type: GroupPipeBind
Parameter Sets: Group

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The ID of the listitem, or actual ListItem object

```yaml
Type: ListItemPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -InheritPermissions
Inherit permissions from the list, removing unique permissions

```yaml
Type: SwitchParameter
Parameter Sets: Inherit

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The ID, Title or Url of the list.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -RemoveRole
The role that must be removed from the group or user

```yaml
Type: RoleDefinitionPipeBind
Parameter Sets: User, Group

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SystemUpdate
Update the item permissions without creating a new version or triggering MS Flow.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
A valid login name of a user (e.g. john@doe.com).

```yaml
Type: String
Parameter Sets: User

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
