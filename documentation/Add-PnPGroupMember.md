---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPGroupMember.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPGroupMember
---
  
# Add-PnPGroupMember

## SYNOPSIS
Adds a user to a SharePoint group

## SYNTAX

### Internal
```powershell
Add-PnPGroupMember -LoginName <String> -Group <GroupPipeBind> 
 [-Connection <PnPConnection>] 
```

### External
```powershell
Add-PnPGroupMember -Group <GroupPipeBind> -EmailAddress <String> [-SendEmail] [-EmailBody <String>]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add new user to SharePoint group. The SharePoint group may be specified either by id, name or related object.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPGroupMember -LoginName user@company.com -Group 'Marketing Site Members'
```

Add the specified user to the SharePoint group "Marketing Site Members"

### EXAMPLE 2
```powershell
Add-PnPGroupMember -LoginName user@company.com -Group 5
```

Add the specified user to the SharePoint group with Id 5

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

### -EmailAddress
The email address of the user

```yaml
Type: String
Parameter Sets: External

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EmailBody

```yaml
Type: String
Parameter Sets: External

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Group
The SharePoint group id, SharePoint group name or SharePoint group object to add the user to

```yaml
Type: GroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -LoginName
The login name of the user

```yaml
Type: String
Parameter Sets: Internal

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SendEmail

```yaml
Type: SwitchParameter
Parameter Sets: External

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


