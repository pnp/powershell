---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPTeamsChannelUser.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPTeamsChannelUser
---

# Set-PnPTeamsChannelUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: ChannelMember.ReadWrite.All

Updates the role of a user in an existing Microsoft Teams private channel.

## SYNTAX

### Default (Default)

```
Set-PnPTeamsChannelUser -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind>
 -Identity <TeamsChannelMemberPipeBind> -Role <String> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to update the role of a user in an existing Microsoft Teams private channel.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPTeamsChannelUser -Team 4efdf392-8225-4763-9e7f-4edeb7f721aa -Channel "19:796d063b63e34497aeaf092c8fb9b44e@thread.skype" -Identity MCMjMiMjMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAwIyMxOTowMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMEB0aHJlYWQuc2t5cGUjIzAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMA== -Role Owner
```
Updates the user with specific membership ID as owner of the specified Teams private channel.

### EXAMPLE 2

```powershell
Set-PnPTeamsChannelUser -Team "My Team" -Channel "My Private Channel" -Identity john@doe.com -Role Member
```
Updates the user john@doe.com as member of the specified Teams private channel.

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

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
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

### -Role

Specify the role of the user.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
