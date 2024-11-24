---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsTag.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTeamsTag
---

# Get-PnPTeamsTag

## SYNOPSIS

**Required Permissions**

* Microsoft Graph API : TeamWorkTag.Read, Group.Read.All

Gets one or all tags in a team.

## SYNTAX

### Default (Default)

```
Get-PnPTeamsTag -Team <TeamsTeamPipeBind> [-Identity <string>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTeamsTag -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5
```

Retrieves all the tags for the specified Microsoft Teams instance.

### EXAMPLE 2

```powershell
Get-PnPTeamsTag -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Identity "ZmY1ZjdmMjctZDhiNy00MWRkLTk2ZDQtYzcyYmVhMWIwOGYxIyM3ZTVhNGRmZS1kNWNlLTRkOTAtODM4MC04ZDIxM2FkYzYzOGIjI3RiVlVpR01rcg=="
```

Retrieves a tag with the specified Id from the specified team.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTeamsTag -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5
```

Retrieves all the tags for the specified Microsoft Teams instance.

### EXAMPLE 2

```powershell
Get-PnPTeamsTag -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Identity "ZmY1ZjdmMjctZDhiNy00MWRkLTk2ZDQtYzcyYmVhMWIwOGYxIyM3ZTVhNGRmZS1kNWNlLTRkOTAtODM4MC04ZDIxM2FkYzYzOGIjI3RiVlVpR01rcg=="
```

Retrieves a tag with the specified Id from the specified team.

## PARAMETERS

### -Identity

Specify the id of the tag

```yaml
Type: TeamsTagPipeBind
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
