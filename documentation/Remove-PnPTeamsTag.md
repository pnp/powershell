---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPTeamsTag.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPTeamsTag
---

# Remove-PnPTeamsTag

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: TeamworkTag.ReadWrite, Group.Read.All

Removes a Microsoft Teams Tag in a Team.

## SYNTAX

### Default (Default)

```
Remove-PnPTeamsTag -Team <TeamsTeamPipeBind> -Identity <TeamsTagPipeBind> [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTeamsTag -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Identity "ZmY1ZjdmMjctZDhiNy00MWRkLTk2ZDQtYzcyYmVhMWIwOGYxIyM3ZTVhNGRmZS1kNWNlLTRkOTAtODM4MC04ZDIxM2FkYzYzOGIjI3RiVlVpR01rcg=="
```
Removes the Tag with the specified Id from the Teams team.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPTeamsTag -Team 5beb63c5-0571-499e-94d5-3279fdd9b6b5 -Identity "ZmY1ZjdmMjctZDhiNy00MWRkLTk2ZDQtYzcyYmVhMWIwOGYxIyM3ZTVhNGRmZS1kNWNlLTRkOTAtODM4MC04ZDIxM2FkYzYzOGIjI3RiVlVpR01rcg=="
```
Removes the Tag with the specified Id from the Teams team.

## PARAMETERS

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

Specify the id of the Tag

```yaml
Type: TeamsTagPipeBind
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
