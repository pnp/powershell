---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsChannelFilesFolder.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTeamsChannelFilesFolder
---

# Get-PnPTeamsChannelFilesFolder

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Group.Read.All

Gets the metadata for the location where the files of a channel are stored.

## SYNTAX

### Default (Default)

```
Get-PnPTeamsChannel [-Team <TeamsTeamPipeBind>] [-Channel <TeamsChannelPipeBind>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve folder metadata for specified channel.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTeamsChannelFilesFolder -Team "Sales Team" -Channel "Test Channel"
```

Retrieves the folder metadata for the channel called 'Test Channel' located in the Team named 'Sales Team'

### EXAMPLE 2

```powershell
Get-PnPTeamsChannelFilesFolder -Team a6c1e0d7-f579-4993-81ab-4b666f8edea8 -Channel "19:796d063b63e34497aeaf092c8fb9b44e@thread.skype"
```

Retrieves the folder metadata for the channel specified by its channel id

## PARAMETERS

### -Channel

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
