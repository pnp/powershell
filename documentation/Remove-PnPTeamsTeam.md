---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPTeamsTeam.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPTeamsTeam
---

# Remove-PnPTeamsTeam

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Removes a Microsoft Teams Team instance and its corresponding Microsoft 365 Group

## SYNTAX

### Default (Default)

```
Remove-PnPTeamsTeam -Identity <TeamsTeamPipeBind> [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Removes a Microsoft Teams Team. This also removes the associated Microsoft 365 Group, and is functionally identical to `Remove-PnPMicrosoft365Group`

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPTeamsTeam -Identity 5beb63c5-0571-499e-94d5-3279fdd9b6b5
```

Removes the specified Team

### EXAMPLE 2

```powershell
Remove-PnPTeamsTeam -Identity testteam
```

Removes the specified Team. If there are multiple teams with the same display name it will not proceed deleting the team.

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

Specify the group id, mailNickname or display name of the team to remove.

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
