---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsTab.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTeamsTab
---

# Get-PnPTeamsTab

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Group.Read.All, Group.ReadWrite.All

Gets one or all tabs in a channel.

## SYNTAX

### Default (Default)

```
Get-PnPTeamsTab -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind>
 [-Identity <TeamsTabPipeBind>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve tabs in channel. By using `Identity` it is possible to retrieve a specific single tab.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTeamsTab -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel 19:796d063b63e34497aeaf092c8fb9b44e@thread.skype
```

Retrieves the tabs for the specified Microsoft Teams instance and channel

### EXAMPLE 2

```powershell
Get-PnPTeamsTab -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel 19:796d063b63e34497aeaf092c8fb9b44e@thread.skype -Identity "Wiki"
```

Retrieves a tab with the display name 'Wiki' from the specified team and channel

### EXAMPLE 3

```powershell
Get-PnPTeamsTab -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Channel 19:796d063b63e34497aeaf092c8fb9b44e@thread.skype -Identity d8740a7a-e44e-46c5-8f13-e699f964fc25
```

Retrieves a tab with an id from the specified team and channel

### EXAMPLE 4

```powershell
Get-PnPTeamsTab -Team "My Team" -Channel "My Channel"
```

Retrieves the tabs for the specified Microsoft Teams instance and channel

### EXAMPLE 5

```powershell
Get-PnPTeamsTab -Team "My Team" -Channel "My Channel" -Identity "Wiki"
```

Retrieves a tab with the display name 'Wiki' from the specified team and channel

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
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Identity

Specify the id or display name of the tab

```yaml
Type: TeamsTabPipeBind
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
  ValueFromPipeline: true
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
