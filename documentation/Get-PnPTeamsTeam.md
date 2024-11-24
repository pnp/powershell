---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsTeam.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTeamsTeam
---

# Get-PnPTeamsTeam

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Group.Read.All, Group.ReadWrite.All

Gets one Microsoft Teams Team or a list of Teams.

## SYNTAX

### Default (Default)

```
Get-PnPTeamsTeam [-Identity <TeamsTeamPipeBind>] [-Filter <String>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve list of Microsoft Teams teams. By using `Identity` it is possible to retrieve a specific team, and by using `Filter` you can supply any filter queries supported by the Graph API.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTeamsTeam
```

Retrieves all the Microsoft Teams instances

### EXAMPLE 2

```powershell
Get-PnPTeamsTeam -Identity "PnP PowerShell"
```

Retrieves a specific Microsoft Teams instance using display name.

### EXAMPLE 3

```powershell
Get-PnPTeamsTeam -Identity "baba9192-55be-488a-9fb7-2e2e76edbef2"
```

Retrieves a specific Microsoft Teams instance using group id.

### EXAMPLE 4

```powershell
Get-PnPTeamsTeam -Filter "startswith(mailNickName, 'contoso')"
```

Retrieves all Microsoft Teams instances with MailNickName starting with "contoso".

### EXAMPLE 5

```powershell
Get-PnPTeamsTeam -Filter "startswith(description, 'contoso')"
```

Retrieves all Microsoft Teams instances with Description starting with "contoso". This example demonstrates using Advanced Query capabilities (see: https://learn.microsoft.com/en-us/graph/aad-advanced-queries?tabs=http#group-properties).

## PARAMETERS

### -Filter

Specify the query to pass to Graph API in $filter.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Filter
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

Specify the group id, mailNickname or display name of the team to use.

```yaml
Type: TeamsTeamPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Identity
  Position: Named
  IsRequired: false
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
