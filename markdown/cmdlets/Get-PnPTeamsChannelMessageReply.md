---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsChannelMessageReply.html
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPTeamsChannelMessageReply
Module Name: PnP.PowerShell
---
  
# Get-PnPTeamsChannelMessageReply

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of ChannelMessage.Read.All, Group.Read.All, or Group.ReadWrite.All (delegated), or one of ChannelMessage.Read.Group, ChannelMessage.Read.All, Group.Read.All, or Group.ReadWrite.All (application). ChannelMessage.Read.Group is a resource specific consent permission which grants access to the team it was consented for only.

Returns replies from the specified Microsoft Teams channel message.

## SYNTAX

```powershell
Get-PnPTeamsChannelMessageReply -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> -Message <TeamsChannelMessagePipeBind> 
[-Identity <TeamsChannelMessageReplyPipeBind>] [-IncludeDeleted]

```

## DESCRIPTION

Allows to retrieve replies from the specified channel message.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTeamsChannelMessageReply -Team MyTestTeam -Channel "My Channel" -Message 1653089769293 -IncludeDeleted
```

Gets all (active and deleted) replies of the specified channel message.

### EXAMPLE 2
```powershell
Get-PnPTeamsChannelMessageReply -Team MyTestTeam -Channel "My Channel" -Message 1653089769293 -Identity 1653086004630
```

Gets a specific reply of the specified channel message.

## PARAMETERS

### -Team
Specify the group id, mailNickname or display name of the team to use.

```yaml
Type: TeamsTeamPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Channel
Specify id or name of the channel to use.

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Message
Specify the id of the message to use.

```yaml
Type: TeamsChannelMessagePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
Specify the id of the message reply to use.

```yaml
Type: TeamsChannelMessageReplyPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeDeleted
Specify to include deleted messages

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

