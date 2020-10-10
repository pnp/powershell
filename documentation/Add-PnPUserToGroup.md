---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/add-pnpusertogroup
schema: 2.0.0
title: Add-PnPUserToGroup
---

# Add-PnPUserToGroup

## SYNOPSIS
Adds a user to a SharePoint group

## SYNTAX

### Internal
```powershell
Add-PnPUserToGroup -LoginName <String> -Identity <GroupPipeBind> [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

### External
```powershell
Add-PnPUserToGroup -Identity <GroupPipeBind> -EmailAddress <String> [-SendEmail] [-EmailBody <String>]
 [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPUserToGroup -LoginName user@company.com -Identity 'Marketing Site Members'
```

Add the specified user to the SharePoint group "Marketing Site Members"

### EXAMPLE 2
```powershell
Add-PnPUserToGroup -LoginName user@company.com -Identity 5
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

### -Identity
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

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)