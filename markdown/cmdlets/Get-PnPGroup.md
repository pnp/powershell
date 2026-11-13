---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPGroup.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPGroup
---
  
# Get-PnPGroup

## SYNOPSIS
Returns a specific SharePoint group or all SharePoint groups in the current site

## SYNTAX

### All (Default)
```powershell
Get-PnPGroup [-Connection <PnPConnection>] [-Includes <String[]>] 
```

### ByName
```powershell
Get-PnPGroup -Identity <GroupPipeBind> [-Connection <PnPConnection>] [-Includes <String[]>] 
```

### Members
```powershell
Get-PnPGroup -AssociatedMemberGroup [-Connection <PnPConnection>] [-Includes <String[]>]
 
```

### Visitors
```powershell
Get-PnPGroup -AssociatedVisitorGroup [-Connection <PnPConnection>]
 [-Includes <String[]>] 
```

### Owners
```powershell
Get-PnPGroup -AssociatedOwnerGroup [-Connection <PnPConnection>] [-Includes <String[]>]
 
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPGroup
```

Returns all SharePoint groups in the current site

### EXAMPLE 2
```powershell
Get-PnPGroup -Identity 'My Site Users'
```

This will return the group called 'My Site Users' if available in the current site. The name is case sensitive, so a group called 'My site users' would not be found.

### EXAMPLE 3
```powershell
Get-PnPGroup -AssociatedMemberGroup
```

This will return the current members group for the site

## PARAMETERS

### -AssociatedMemberGroup
Retrieve the associated member group

```yaml
Type: SwitchParameter
Parameter Sets: Members

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AssociatedOwnerGroup
Retrieve the associated owner group

```yaml
Type: SwitchParameter
Parameter Sets: Owners

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AssociatedVisitorGroup
Retrieve the associated visitor group

```yaml
Type: SwitchParameter
Parameter Sets: Visitors

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
Get a specific group by its name or id. The name case sensitive.

```yaml
Type: GroupPipeBind
Parameter Sets: ByName
Aliases: Name

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Includes
Optionally allows properties to be retrieved for the returned SharePoint security group which are not included in the response by default

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
