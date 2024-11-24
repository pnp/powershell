---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Update-PnPTeamsUser.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Update-PnPTeamsUser
---

# Update-PnPTeamsUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Updates a user's role in an existing Microsoft Teams instance.

## SYNTAX

### Default (Default)

```
Update-PnPTeamsUser -Team <TeamsTeamPipeBind> -User <String> -Role <String>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet updates the role of the specified user in the selected Teams instance to Member or Owner.

## EXAMPLES

### EXAMPLE 1

```powershell
Update-PnPTeamsUser -Team MyTeam -User john@doe.com -Role Owner
```

Updates a user as an owner of the team.

### EXAMPLE 2

```powershell
Update-PnPTeamsUser -Team MyTeam -User john@doe.com -Role Member
```

Updates a user as a member of the team.

### EXAMPLE 3

```powershell
Update-PnPTeamsUser -Team a0c0a395-4ba6-4fff-958a-000000506d18 -User john@doe.com -Role Member -Force
```

Updates john@doe.com user as a member of the team and skips the confirmation question.

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

### -Role

Specify the role of the user

```yaml
Type: String
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
AcceptedValues:
- Owner
- Member
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

### -User

Specify the UPN (e.g. john@doe.com)

```yaml
Type: String
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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/team-update-members)
