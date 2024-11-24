---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPTeamsTab.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPTeamsTab
---

# Remove-PnPTeamsTab

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Removes a Microsoft Teams tab in a channel.

## SYNTAX

### Default (Default)

```
Remove-PnPTeamsTab -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind>
 -Identity <TeamsTabPipeBind> [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove a tab from channel.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPTeamsTab -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel "General" -Identity Wiki
```
Removes the tab with the display name 'Wiki' from the General channel using display name.

### EXAMPLE 2

```powershell
Remove-PnPTeamsTab -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel 19:796d063b63e34497aeaf092c8fb9b44e@thread.skype -Identity Wiki
```

Removes the tab with the display name 'Wiki' from the channel using id.

### EXAMPLE 3

```powershell
Remove-PnPTeamsTab -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel 19:796d063b63e34497aeaf092c8fb9b44e@thread.skype -Identity fcef815d-2e8e-47a5-b06b-9bebba5c7852
```

Removes a tab with the specified id from the channel

## PARAMETERS

### -Channel

Specify the channel id or display name of the channel to use.

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

### -Force

Specifying the Force parameter will skip the confirmation question.

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

### -Identity

Specify the id of the tab

```yaml
Type: TeamsTabPipeBind
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
