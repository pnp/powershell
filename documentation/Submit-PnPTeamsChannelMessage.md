---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/submit-pnpteamschannelmessage
schema: 2.0.0
title: Submit-PnPTeamsChannelMessage
---

# Submit-PnPTeamsChannelMessage

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Sends a message to a Microsoft Teams Channel.

## SYNTAX

```
Submit-PnPTeamsChannelMessage -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> -Message <String>
 [-ContentType <TeamChannelMessageContentType>] [-Important] [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

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

### -ByPassPermissionCheck
Allows the check for required permissions in the access token to be bypassed when set to $true

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Channel
Specify the group id, mailNickname or display name of the team to use.

```yaml
Type: TeamsChannelPipeBind
Parameter Sets: (All)
Aliases:

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
Aliases:
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
Aliases:

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
Aliases:

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
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)