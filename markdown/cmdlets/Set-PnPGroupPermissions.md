---
Module Name: PnP.PowerShell
title: Set-PnPGroupPermissions
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPGroupPermissions.html
---
 
# Set-PnPGroupPermissions

## SYNOPSIS
Adds and/or removes permissions of a specific SharePoint group.

## SYNTAX

```powershell
Set-PnPGroupPermissions [-Identity] <GroupPipeBind> [-List <ListPipeBind>] [-AddRole <String[]>]
 [-RemoveRole <String[]>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet adds or removes roles (permissions) of a specified group on a site or list level.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPGroupPermissions -Identity 'My Site Members' -AddRole Contribute
```

Adds the 'Contribute' permission to the SharePoint group with the name 'My Site Members'.

### EXAMPLE 2
```powershell
Set-PnPGroupPermissions -Identity 'My Site Members' -RemoveRole 'Full Control' -AddRole 'Read'
```

Removes the 'Full Control' from and adds the 'Read' permissions to the SharePoint group with the name 'My Site Members'.

### EXAMPLE 3
```powershell
Set-PnPGroupPermissions -Identity 'My Site Members' -AddRole @('Contribute', 'Design')
```

Adds the 'Contribute' and 'Design' permissions to the SharePoint group with the name 'My Site Members'.

### EXAMPLE 4
```powershell
Set-PnPGroupPermissions -Identity 'My Site Members' -RemoveRole @('Contribute', 'Design')
```

Removes the 'Contribute' and 'Design' permissions from the SharePoint group with the name 'My Site Members'.

### EXAMPLE 5
```powershell
Set-PnPGroupPermissions -Identity 'My Site Members' -List 'MyList' -RemoveRole @('Contribute')
```

Removes the 'Contribute' permissions from the list 'MyList' for the group with the name 'My Site Members'.

## PARAMETERS

### -AddRole
Name of the role (permission level) to add to the SharePoint group.

```yaml
Type: String[]
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

### -Identity
A group object, an ID or a name of a group.

```yaml
Type: GroupPipeBind
Parameter Sets: (All)
Aliases: Name

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -List
Specify the Id, title or an instance of the list where permissions should be updated.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveRole
Name of the role (permission level) to remove from the SharePoint group.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

