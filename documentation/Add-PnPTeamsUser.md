---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPTeamsUser.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPTeamsUser
---

# Add-PnPTeamsUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Adds a user to an existing Microsoft Teams instance.

## SYNTAX

### User

```
Add-PnPTeamsUser -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> -User <String>
 -Role <String>
```

### Users

```
Add-PnPTeamsUser -Team <TeamsTeamPipeBind> -Users <String[]> -Role <String>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet adds one or more users to an existing Team.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPTeamsUser -Team MyTeam -User john@doe.com -Role Owner
```

Adds a user as an owner to the team.

### EXAMPLE 2

```powershell
Add-PnPTeamsUser -Team MyTeam -User john@doe.com -Role Member
```

Adds a user as a member to the team.

### EXAMPLE 3

```powershell
Add-PnPTeamsUser -Team MyTeam -Users "john@doe.com","jane@doe.com" -Role Member
```

Adds multiple users as members to the team.

### EXAMPLE 4

```powershell
Add-PnPTeamsUser -Team MyTeam -User "jane@doe.com" -Role Member -Channel Private
```

Adds user as a member to a private channel named Private in MyTeam team.

## PARAMETERS

### -Channel

Specify the channel id or name of the team to retrieve.

```yaml
Type: TeamsChannelPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (User)
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
- Name: (User)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Users

Specify the users UPN (e.g. john@doe.com, jane@doe.com)

```yaml
Type: String array
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (Users)
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
