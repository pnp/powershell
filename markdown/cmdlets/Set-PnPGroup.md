---
Module Name: PnP.PowerShell
title: Set-PnPGroup
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPGroup.html
---
 
# Set-PnPGroup

## SYNOPSIS
Updates a group.

## SYNTAX

```powershell
Set-PnPGroup -Identity <GroupPipeBind> [-SetAssociatedGroup <AssociatedGroupType>] [-AddRole <String>]
 [-RemoveRole <String>] [-Title <String>] [-Owner <String>] [-Description <String>]
 [-AllowRequestToJoinLeave <Boolean>] [-AutoAcceptRequestToJoinLeave <Boolean>]
 [-AllowMembersEditMembership <Boolean>] [-OnlyAllowMembersViewMembership <Boolean>]
 [-RequestToJoinEmail <String>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet updates the roles and settings of the specified group.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPGroup -Identity 'My Site Members' -SetAssociatedGroup Members
```

Sets the SharePoint group with the name 'My Site Members' as the associated members group.

### EXAMPLE 2
```powershell
Set-PnPGroup -Identity 'My Site Members' -Owner 'site owners'
```

Sets the SharePoint group with the name 'site owners' as the owner of the SharePoint group with the name 'My Site Members'.

## PARAMETERS

### -AddRole
Name of the role (permission level) to add to the SharePoint group.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowMembersEditMembership
Specifies whether group members can modify membership in the group.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowRequestToJoinLeave
Specifies whether users are allowed to request membership in the group or to request to leave the group.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AutoAcceptRequestToJoinLeave
Specifies whether users are automatically added or removed when they make a request.

```yaml
Type: Boolean
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
The description of the group.

```yaml
Type: String
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

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OnlyAllowMembersViewMembership
Specifies whether only group members are allowed to view the list of members in the group.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Owner
The owner of the group. It can be a user or another group.

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
Name of the role (permission level) to remove from the SharePoint group.

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
The e-mail address to which membership requests are sent.

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
One of the associated group types (Visitors, Members, Owners).

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
The title of the group.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

