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
Sets list permissions

## SYNTAX

### Group
```powershell
Set-PnPListPermission -Identity <ListPipeBind> -Group <GroupPipeBind> [-AddRole <String>]
 [-RemoveRole <String>] [-Connection <PnPConnection>] [<CommonParameters>]
```

### User
```powershell
Set-PnPListPermission -Identity <ListPipeBind> -User <String> [-AddRole <String>] [-RemoveRole <String>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPListPermission -Identity 'Documents' -User 'user@contoso.com' -AddRole 'Contribute'
```

Adds the 'Contribute' permission to the user 'user@contoso.com' for the list 'Documents'

### EXAMPLE 2
```powershell
Set-PnPListPermission -Identity 'Documents' -User 'user@contoso.com' -RemoveRole 'Contribute'
```

Removes the 'Contribute' permission to the user 'user@contoso.com' for the list 'Documents'

## PARAMETERS

### -AddRole
The role that must be assigned to the group or user

```yaml
Type: String
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

### -Group

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
The ID or Title of the list.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveRole
The role that must be removed from the group or user

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User

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

