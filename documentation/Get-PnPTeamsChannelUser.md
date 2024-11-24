---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsChannelUser.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTeamsChannelUser
---

# Get-PnPTeamsChannelUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: ChannelMember.Read.All

Returns members from the specified Microsoft Teams private Channel.

## SYNTAX

### Default (Default)

```
Get-PnPTeamsChannelUser -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind>
 [-Identity <TeamsChannelMemberPipeBind>] [-Role <String>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve list of members of the specified private channel.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTeamsChannelUser -Team "My Team" -Channel "My Channel"
```

Returns all owners, members and guests from the specified channel.

### EXAMPLE 2

```powershell
Get-PnPTeamsChannelUser -Team "My Team" -Channel "My Channel" -Role Member
```

Returns all members from the specified channel.

### EXAMPLE 3

```powershell
Get-PnPTeamsChannelUser -Team "My Team" -Channel "My Channel" -Identity john.doe@contoso.com
```

Returns membership of the user "john.doe@contoso.com" for the specified channel.

### EXAMPLE 4

```powershell
Get-PnPTeamsChannelUser -Team "My Team" -Channel "My Channel" -Identity 00000000-0000-0000-0000-000000000000
```

Returns membership of the user with ID "00000000-0000-0000-0000-000000000000" for the specified channel.

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
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Role

Specify to filter on the role of the user.

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
AcceptedValues:
- Owner
- Member
- Guest
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
