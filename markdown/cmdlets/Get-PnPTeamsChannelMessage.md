---
Module Name: PnP.PowerShell
title: Get-PnPTeamsChannelMessage
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsChannelMessage.html
---
 
# Get-PnPTeamsChannelMessage

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Returns messages from the specified Microsoft Teams Channel.

## SYNTAX

```powershell
Get-PnPTeamsChannelMessage -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> [-Identity <TeamsChannelMessagePipeBind>] [-IncludeDeleted]
  
```

## DESCRIPTION

Allows to retrieve messages from the specified channel.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTeamsChannelMessage -Team MyTestTeam -Channel "My Channel"
```

Gets all messages of the specified channel

### EXAMPLE 2

```powershell
Get-PnPTeamsChannelMessage -Team MyTestTeam -Channel "My Channel" -Identity 1653089769293
```

Gets a specific message of the specified channel

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

### -Identity
Specify the id of the message to use.

```yaml
Type: TeamsChannelMessagePipeBind
Parameter Sets: (All)
Required: False

Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeDeleted
Specify to include deleted messages.

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
