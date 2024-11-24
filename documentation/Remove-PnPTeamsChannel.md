---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPTeamsChannel.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPTeamsChannel
---

# Remove-PnPTeamsChannel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Removes a channel from a Microsoft Teams instance.

## SYNTAX

### Default (Default)

```
Remove-PnPTeamsChannel -Team <TeamsTeamPipeBind> -Identity <TeamsChannelPipeBind> [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove a channel from specified team.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPTeamsChannel -Team 4efdf392-8225-4763-9e7f-4edeb7f721aa -Identity "My Channel"
```

Removes the channel specified from the team specified

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

Specify the channel id or display name of the channel to delete.

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
