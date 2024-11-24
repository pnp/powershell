---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPTeamsChannel.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPTeamsChannel
---

# Add-PnPTeamsChannel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Adds a channel to an existing Microsoft Teams team.

## SYNTAX

### Standard channel

```
Add-PnPTeamsChannel -Team <TeamsTeamPipeBind> -DisplayName <String> [-Description <String>]
 [-IsFavoriteByDefault <Boolean>]
```

### Private channel

```
Add-PnPTeamsChannel -Team <TeamsTeamPipeBind> -DisplayName <String> -ChannelType -OwnerUPN <String>
 [-Description <String>]
```

### Shared channel

```
Add-PnPTeamsChannel -Team <TeamsTeamPipeBind> -DisplayName <String> -ChannelType -OwnerUPN <String>
 [-Description <String>] [-IsFavoriteByDefault <Boolean>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add channel to an existing team in Microsoft Teams. By using the `IsFavoriteByDefault` it is possible to specify if the channel will be visible for members by default.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPTeamsChannel -Team 4efdf392-8225-4763-9e7f-4edeb7f721aa -DisplayName "My Channel" -IsFavoriteByDefault $true
```

Adds a new standard channel to the Team specified by its identifier and marks the channel as by default visible for members.

### EXAMPLE 2

```powershell
Add-PnPTeamsChannel -Team "My Team" -DisplayName "My standard channel"
```

Adds a new standard channel to the Team specified by its name.

### EXAMPLE 3

```powershell
Add-PnPTeamsChannel -Team "HR" -DisplayName "My private channel" -ChannelType Private -OwnerUPN user1@domain.com
```

Adds a new private channel to the Team specified by its name and sets the provided user as the owner of the channel.

### EXAMPLE 4

```powershell
Add-PnPTeamsChannel -Team "Logistical Department" -DisplayName "My shared channel" -ChannelType Shared -OwnerUPN user1@domain.com
```

Adds a new shared channel to the Team specified by its name and sets the provided user as the owner of the channel.

## PARAMETERS

### -ChannelType

Allows specifying the type of channel to be created. Possible values are Standard, Private, and Shared.

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

### -Description

An optional description of the channel.

```yaml
Type: String
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

### -DisplayName

The display name of the new channel. Letters, numbers, and spaces are allowed.

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

### -IsFavoriteByDefault

Allows you to specify if the channel is by default visible for members.
**This parameter is obsolete. [Microsoft Graph API docs](https://learn.microsoft.com/en-us/graph/api/resources/channel?view=graph-rest-1.0#properties) mention that it only works when you create a channel in Teams creation request. It will be removed in a future version.**

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Standard channel
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Shared channel
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -OwnerUPN

The User Principal Name (email) of the owner of the channel.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Private channel
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Shared channel
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
