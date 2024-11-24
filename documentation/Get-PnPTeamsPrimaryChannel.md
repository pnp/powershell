---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsPrimaryChannel.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTeamsPrimaryChannel
---

# Get-PnPTeamsPrimaryChannel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Channel.ReadBasic.All, ChannelSettings.Read.All, ChannelSettings.ReadWrite.All

Gets the default channel, General, of a team.

## SYNTAX

### Default (Default)

```
Get-PnPTeamsPrimaryChannel -Team <TeamsTeamPipeBind> [-Identity <TeamsChannelPipeBind>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Gets the default channel, General, of a team.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTeamsPrimaryChannel -Team ee0f40fc-b2f7-45c7-b62d-11b90dd2ea8e
```

Gets the default channel of the Team with the provided Id

### EXAMPLE 2

```powershell
Get-PnPTeamsPrimaryChannel -Team Sales
```

Gets the default channel of the Sales Team

## PARAMETERS

### -Team

The group id, mailNickname or display name of the team to use.

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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/team-get-primarychannel)
