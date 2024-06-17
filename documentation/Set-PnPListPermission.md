---
Module Name: PnP.PowerShell
title: Set-PnPListPermission
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPListPermission.html
---
 
# Set-PnPListPermission

## SYNOPSIS
Allows permissions on a SharePoint list to be changed.

## SYNTAX

### Group
```powershell
Set-PnPListPermission -Identity <ListPipeBind> -Group <GroupPipeBind> [-AddRole <String>]
 [-RemoveRole <String>] [-Connection <PnPConnection>] 
```

### User
```powershell
Set-PnPListPermission -Identity <ListPipeBind> -User <String> [-AddRole <String>] [-RemoveRole <String>]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Allows changing permissions on a SharePoint list. In case you would like to break the permission inheritance on a list from its parent, you can use [Set-PnPList -BreakRoleInheritance](Set-PnPList.md#-breakroleinheritance).

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPListPermission -Identity 'Documents' -User 'user@contoso.com' -AddRole 'Contribute'
```

Adds the 'Contribute' permission to the user 'user@contoso.com' for the list 'Documents'.

### EXAMPLE 2
```powershell
Set-PnPListPermission -Identity 'Documents' -User 'user@contoso.com' -RemoveRole 'Contribute'
```

Removes the 'Contribute' permission to the user 'user@contoso.com' for the list 'Documents'.

## PARAMETERS

### -Identity
The Id, title or an instance of the list.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AddRole
The name of the role that must be assigned to the group or user. The name of the role is localized and depends on the language in which the site has been created, so i.e. for an English site you would use `Full Control`, but for a Dutch site you would use `Volledig beheer`.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveRole
The name of the role that must be removed from the group or user. The name of the role is localized and depends on the language in which the site has been created, so i.e. for an English site you would use `Full Control`, but for a Dutch site you would use `Volledig beheer`.

```yaml
Type: String
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
