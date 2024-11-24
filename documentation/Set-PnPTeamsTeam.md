---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPTeamsTeam.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPTeamsTeam
---

# Set-PnPTeamsTeam

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Updates an existing Team.

## SYNTAX

### Default (Default)

```
Set-PnPTeamsTeam -Identity <TeamsTeamPipeBind> [-DisplayName <String>] [-Description <String>]
 [-Visibility <TeamVisibility>] [-AllowAddRemoveApps <Boolean>] [-AllowChannelMentions <Boolean>]
 [-AllowCreateUpdateChannels <Boolean>] [-AllowCreateUpdateRemoveConnectors <Boolean>]
 [-AllowCreateUpdateRemoveTabs <Boolean>] [-AllowCustomMemes <Boolean>]
 [-AllowDeleteChannels <Boolean>] [-AllowGiphy <Boolean>]
 [-AllowGuestCreateUpdateChannels <Boolean>] [-AllowGuestDeleteChannels <Boolean>]
 [-AllowOwnerDeleteMessages <Boolean>] [-AllowStickersAndMemes <Boolean>]
 [-AllowTeamMentions <Boolean>] [-AllowUserDeleteMessages <Boolean>]
 [-AllowUserEditMessages <Boolean>] [-GiphyContentRating <TeamGiphyContentRating>]
 [-ShowInTeamsSearchAndSuggestions <Boolean>] [-Classification <String>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to update team settings.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPTeamsTeam -Identity 'MyTeam' -DisplayName 'My Team'
```

Updates the team called 'MyTeam' to have the display name set to 'My Team'.

### EXAMPLE 2

```powershell
Set-PnPTeamsTeam -Identity "baba9192-55be-488a-9fb7-2e2e76edbef2" -Visibility Public
```

Updates the team by using id to have the visibility Public.

### EXAMPLE 3

```powershell
Set-PnPTeamsTeam -Identity "My Team" -AllowTeamMentions $false -AllowChannelMentions $true -AllowDeleteChannels $false
```

Updates the team 'My Team' to disallow Team @mentions, allow Channel @mentions and disallow members from deleting channels.

### EXAMPLE 4

```powershell
Set-PnPTeamsTeam -Identity "My Team" -GiphyContentRating Moderate
```

Updates the team 'My Team' to have a Moderate level of sensitivity for giphy usage.

## PARAMETERS

### -AllowAddRemoveApps

Boolean value that determines whether or not members (not only owners) are allowed to add apps to the team.

```yaml
Type: Boolean
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

### -AllowChannelMentions

Boolean value that determines whether or not channels in the team can be @ mentioned so that all users who follow the channel are notified.

```yaml
Type: Boolean
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

### -AllowCreateUpdateChannels

Setting that determines whether or not members (and not just owners) are allowed to create channels.

```yaml
Type: Boolean
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

### -AllowCreateUpdateRemoveConnectors

Setting that determines whether or not members (and not only owners) can manage connectors in the team.

```yaml
Type: Boolean
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

### -AllowCreateUpdateRemoveTabs

Setting that determines whether or not members (and not only owners) can manage tabs in channels.

```yaml
Type: Boolean
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

### -AllowCustomMemes

Setting that determines whether or not members can use the custom memes functionality in teams.

```yaml
Type: Boolean
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

### -AllowDeleteChannels

Setting that determines whether or not members (and not only owners) can delete channels in the team.

```yaml
Type: Boolean
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

### -AllowGiphy

Setting that determines whether or not giphy can be used in the team.

```yaml
Type: Boolean
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

### -AllowGuestCreateUpdateChannels

Setting that determines whether or not guests can create channels in the team.

```yaml
Type: Boolean
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

### -AllowGuestDeleteChannels

Setting that determines whether or not guests can delete in the team.

```yaml
Type: Boolean
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

### -AllowOwnerDeleteMessages

Setting that determines whether or not owners can delete messages that they or other members of the team have posted.

```yaml
Type: Boolean
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

### -AllowStickersAndMemes

Setting that determines whether stickers and memes usage is allowed in the team.

```yaml
Type: Boolean
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

### -AllowTeamMentions

Setting that determines whether the entire team can be @ mentioned (which means that all users will be notified)

```yaml
Type: Boolean
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

### -AllowUserDeleteMessages

Setting that determines whether or not members can delete messages that they have posted.

```yaml
Type: Boolean
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

### -AllowUserEditMessages

Setting that determines whether or not users can edit messages that they have posted.

```yaml
Type: Boolean
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

### -Classification



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

### -Description

Changes the description of the specified team.

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

Changes the display name of the specified team.

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

### -GiphyContentRating

Setting that determines the level of sensitivity of giphy usage that is allowed in the team. Accepted values are "Strict" or "Moderate"

```yaml
Type: TeamGiphyContentRating
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
- moderate
- strict
HelpMessage: ''
```

### -Identity

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
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ShowInTeamsSearchAndSuggestions

Setting that determines whether or not private teams should be searchable from Teams clients for users who do not belong to that team. Set to $false to make those teams not discoverable from Teams clients.

```yaml
Type: Boolean
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

### -Visibility

Changes the visibility of the specified team.

```yaml
Type: TeamVisibility
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
- Private
- Public
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
