---
Module Name: PnP.PowerShell
title: Remove-PnPGroupMember
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPGroupMember.html
---
 
# Remove-PnPGroupMember

## SYNOPSIS
Removes a user from a group

## SYNTAX

```powershell
Remove-PnPGroupMember -LoginName <String> -Group <GroupPipeBind> 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to remove a user from group.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPGroupMember -LoginName user@company.com -Group 'Marketing Site Members'
```

Removes the user user@company.com from the Group 'Marketing Site Members'

## PARAMETERS

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
A group object, an ID or a name of a group

```yaml
Type: GroupPipeBind
Parameter Sets: (All)
Aliases: GroupName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LoginName
A valid login name of a user (user@company.com)

```yaml
Type: String
Parameter Sets: (All)
Aliases: LogonName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

