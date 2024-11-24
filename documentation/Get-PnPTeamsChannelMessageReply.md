---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsChannelMessageReply.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTeamsChannelMessageReply
---

# Get-PnPTeamsChannelMessageReply

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: ChannelMessage.Read.All

Returns replies from the specified Microsoft Teams channel message.

## SYNTAX

### Default (Default)

```
Get-PnPTeamsChannelMessageReply -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind>
 -Message <TeamsChannelMessagePipeBind> [-Identity <TeamsChannelMessageReplyPipeBind>]
 [-IncludeDeleted]
```

## ALIASES

This cmdlet has no aliases.

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

### -Channel

Specify id or name of the channel to use.

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

### -Identity

Specify the id of the message reply to use.

```yaml
Type: TeamsChannelMessageReplyPipeBind
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

### -IncludeDeleted

Specify to include deleted messages

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

Specify the id of the message to use.

```yaml
Type: TeamsChannelMessagePipeBind
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
