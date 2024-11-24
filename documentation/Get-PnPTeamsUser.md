---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsUser.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTeamsUser
---

# Get-PnPTeamsUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Group.Read.All, Group.ReadWrite.All
  * Microsoft Graph API : Directory.Read.All

Returns owners, members or guests from a team.

## SYNTAX

### Default (Default)

```
Get-PnPTeamsUser -Team <TeamsTeamPipeBind> [-Channel <TeamsChannelPipeBind>] [-Role <String>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve list of owners, members or guests from a team.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTeamsUser -Team MyTeam
```

Returns all owners, members or guests from the specified team.

### EXAMPLE 2

```powershell
Get-PnPTeamsUser -Team MyTeam -Role Owner
```

Returns all owners from the specified team.

### EXAMPLE 3

```powershell
Get-PnPTeamsUser -Team MyTeam -Role Member
```

Returns all members from the specified team.

### EXAMPLE 4

```powershell
Get-PnPTeamsUser -Team MyTeam -Role Guest
```

Returns all guests from the specified team.

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
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Role

Specify to filter on the role of the user

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
AcceptedValues:
- Owner
- Member
- Guest
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
