---
Module Name: PnP.PowerShell
title: Submit-PnPTeamsChannelMessage
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Submit-PnPTeamsChannelMessage.html
---
 
# Submit-PnPTeamsChannelMessage

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: API required one of 'Teamwork.Migrate.All, ChannelMessage.ReadWrite.All'.

Sends a message to a Microsoft Teams Channel.

## SYNTAX

```powershell
Submit-PnPTeamsChannelMessage -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> -Message <String>
 [-ContentType <TeamChannelMessageContentType>] [-Important]  
```

## DESCRIPTION

Allows to send a message to a Microsoft Teams Channel.

## EXAMPLES

### EXAMPLE 1
```powershell
Submit-PnPTeamsChannelMessage -Team MyTestTeam -Channel "My Channel" -Message "A new message"
```

Sends "A new message" to the specified channel

### EXAMPLE 2
```powershell
Submit-PnPTeamsChannelMessage -Team MyTestTeam -Channel "My Channel" -Message "<strong>A bold new message</strong>" -ContentType Html
```

Sends the message, formatted as html to the specified channel

## PARAMETERS

### -Channel
Specify the group id, mailNickname or display name of the team to use.

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ContentType
Specify to set the content type of the message, either Text or Html.

```yaml
Type: TeamChannelMessageContentType
Parameter Sets: (All)
Accepted values: Text, Html

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Important
Specify to make this an important message.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Message
The message to send to the channel.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

