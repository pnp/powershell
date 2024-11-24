---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPTeamsUser.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPTeamsUser
---

# Remove-PnPTeamsUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Removes a user from a team.

## SYNTAX

### Default (Default)

```
Remove-PnPTeamsUser -Team <TeamsTeamPipeBind> -User <String> [-Role <String>] [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove user from a team.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPTeamsUser -Team MyTeam -User john@doe.com
```

Removes the user specified from both owners and members of the team.

### EXAMPLE 2

```powershell
Remove-PnPTeamsUser -Team MyTeam -User john@doe.com -Role Owner
```

Removes the user john@doe.com from the owners of the team, but retains the user as a member.

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

Specify the role of the user you are removing from the team. Accepts "Owner" and "Member" as possible values.
        If specified as "Member" then the specified user is removed from the Team completely even if they were the owner of the Team. If "Owner" is specified in the -Role parameter then the
        specified user is removed as an owner of the team but stays as a team member. Defaults to "Member". Note: The last owner cannot be removed from the team.

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
