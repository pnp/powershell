---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPTeamsChannelUser.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPTeamsChannelUser
---

# Remove-PnPTeamsChannelUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: ChannelMember.ReadWrite.All

Removes a specified user of a specified Microsoft Teams private Channel.

## SYNTAX

### Default (Default)

```
Remove-PnPTeamsChannelUser -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind>
 -Identity <TeamsChannelMemberPipeBind> [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove a user from specified private channel.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPTeamsChannelUser -Team "My Team" -Channel "My Channel" -Identity MCMjMiMjMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAwIyMxOTowMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMEB0aHJlYWQuc2t5cGUjIzAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMA==
```

Removes the user with specific membership ID from the specified Teams channel.

### EXAMPLE 2

```powershell
Remove-PnPTeamsChannelUser -Team "My Team" -Channel "My Channel" -Identity 00000000-0000-0000-0000-000000000000
```

Removes the user with ID "00000000-0000-0000-0000-000000000000" from the specified Teams channel.

### EXAMPLE 3

```powershell
Remove-PnPTeamsChannelUser -Team "My Team" -Channel "My Channel" -Identity john.doe@contoso.com -Force
```

Removes the user "john.doe@contoso.com" from the specified Teams channel without confirmation prompt.

## PARAMETERS

### -Channel

Specify id or name of the channel to use.

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

Specify membership id, UPN or user ID of the channel member.

```yaml
Type: TeamsChannelMemberPipeBind
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
