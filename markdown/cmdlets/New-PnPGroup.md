---
Module Name: PnP.PowerShell
title: New-PnPGroup
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPGroup.html
---
 
# New-PnPGroup

## SYNOPSIS
Adds group to the Site Groups List and returns a group object

## SYNTAX

```powershell
New-PnPGroup -Title <String> [-Description <String>] [-Owner <String>] [-AllowRequestToJoinLeave]
 [-AutoAcceptRequestToJoinLeave] [-AllowMembersEditMembership] [-DisallowMembersViewMembership]
[-RequestToJoinEmail <String>] [-SetAssociatedGroup <AssociatedGroupType>]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

This command creates a SharePoint group in the current Site Collection connected to. 

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPGroup -Title "My Site Users"
```

## PARAMETERS

### -AllowMembersEditMembership
A switch parameter that specifies whether group members can modify membership in the group

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowRequestToJoinLeave
A switch parameter that specifies whether to allow users to request membership in the group and to allow users to request to leave the group

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AutoAcceptRequestToJoinLeave
A switch parameter that specifies whether users are automatically added or removed when they make a request

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
The description for the group

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisallowMembersViewMembership
A switch parameter that disallows group members to view membership.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Owner
The owner for the group, which can be a user or another group

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RequestToJoinEmail
The e-mail address to which membership requests are sent

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SetAssociatedGroup
{{ Fill SetAssociatedGroup Description }}

```yaml
Type: AssociatedGroupType
Parameter Sets: (All)
Accepted values: None, Visitors, Members, Owners

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The Title of the group

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

