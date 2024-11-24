---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsChannel.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTeamsChannel
---

# Get-PnPTeamsChannel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Group.Read.All, Group.ReadWrite.All

Gets the channels for a specified Team.

## SYNTAX

### Default (Default)

```
Get-PnPTeamsChannel -Team <TeamsTeamPipeBind> [-Identity <TeamsChannelPipeBind>]
 [-IncludeModerationSettings <SwitchParameter>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve list of channels for a specified team.

Note that the ModerationSettings are only being returned when providing the channel Id of a specific channel through -Identity and by providing -IncludeModerationSettings (Example 4). They will not be returned when retrieving all channels for a team or when omitting -IncludeModerationSettings. This is because of a design choice in Microsoft Graph and the moderationsettings currently only being available through its beta endpoint, which will be used when -IncludeModerationSettings is provided.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTeamsChannel -Team a6c1e0d7-f579-4993-81ab-4b666f8edea8
```

Retrieves all channels for the specified team

### EXAMPLE 2

```powershell
Get-PnPTeamsChannel -Team a6c1e0d7-f579-4993-81ab-4b666f8edea8 -Identity "Test Channel"
```

Retrieves the channel called 'Test Channel'

### EXAMPLE 3

```powershell
Get-PnPTeamsChannel -Team a6c1e0d7-f579-4993-81ab-4b666f8edea8 -Identity "19:796d063b63e34497aeaf092c8fb9b44e@thread.skype"
```

Retrieves the channel specified by its channel id

### EXAMPLE 4

```powershell
Get-PnPTeamsChannel -Team a6c1e0d7-f579-4993-81ab-4b666f8edea8 -Identity "19:796d063b63e34497aeaf092c8fb9b44e@thread.skype" -IncludeModerationSettings
```

Retrieves the channel specified by its channel id which will include the ModerationSettings

## PARAMETERS

### -Identity

The id or name of the channel to retrieve.

```yaml
Type: TeamsChannelPipeBind
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

### -IncludeModerationSettings

When provided, it will use the beta endpoint of Microsoft Graph to retrieve the information. This will include the ModerationSettings if used in combination with -Identity <channelId>.

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
