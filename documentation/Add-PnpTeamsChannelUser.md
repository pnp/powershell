---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPTeamsChannelUser.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPTeamsChannelUser
---

# Add-PnPTeamsChannelUser

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: ChannelMember.ReadWrite.All

Adds a user to an existing Microsoft Teams private channel.

## SYNTAX

### Default (Default)

```
Add-PnPTeamsChannelUser -Team <TeamsTeamPipeBind> -Channel <TeamsChannelPipeBind> -User <String>
 -Role <String>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add a user to a private channel in Microsoft Teams.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPTeamsChannelUser -Team 4efdf392-8225-4763-9e7f-4edeb7f721aa -Channel "19:796d063b63e34497aeaf092c8fb9b44e@thread.skype" -User john@doe.com -Role Owner
```

Adds user as an owner to the private channel.

### EXAMPLE 2

```powershell
Add-PnPTeamsChannelUser -Team "My Team" -Channel "My Private Channel" -User john@doe.com -Role Member
```

Adds user as a member to the private channel.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
