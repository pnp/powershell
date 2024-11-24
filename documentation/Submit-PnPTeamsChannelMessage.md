---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Submit-PnPTeamsChannelMessage.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Submit-PnPTeamsChannelMessage
---

# Submit-PnPTeamsChannelMessage

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: API required one of: `Teamwork.Migrate.All, ChannelMessage.Send or Group.ReadWrite.All`.

Sends a message to a Microsoft Teams Channel.

## SYNTAX

### Default (Default)

```
Submit-PnPTeamsChannelMessage -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind>
 -Message <String> [-ContentType <TeamChannelMessageContentType>] [-Important]
```

## ALIASES

This cmdlet has no aliases.

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
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ContentType

Specify to set the content type of the message, either Text or Html.

```yaml
Type: TeamChannelMessageContentType
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Text
- Html
HelpMessage: ''
```

### -Important

Specify to make this an important message.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Message

The message to send to the channel.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Team

Specify the group id, mailNickname or display name of the team to use.

```yaml
Type: TeamsTeamPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
