---
Module Name: PnP.PowerShell
title: Remove-PnPGroup
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPGroup.html
---
 
# Remove-PnPGroup

## SYNOPSIS
Removes a group from a web.

## SYNTAX

```powershell
Remove-PnPGroup [[-Identity] <GroupPipeBind>] [-Force] [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to remove a group from web.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPGroup -Identity "My Users"
```

Removes the group "My Users"

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

### -Force
Specifying the Force parameter will skip the confirmation question.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
A group object, an ID or a name of a group to remove

```yaml
Type: GroupPipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

