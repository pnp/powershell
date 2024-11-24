---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Copy-PnPTeamsTeam.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Copy-PnPTeamsTeam
---

# Copy-PnPTeamsTeam

## SYNOPSIS

Creates a copy of a Microsoft Teams team

## SYNTAX

### Clone a team

```
Copy-PnPTeamsTeam -DisplayName <String> -Identity <TeamsTeamPipeBind>
 [-PartsToClone <ClonableTeamParts[]>] [-Description <String>] [-Visibility <TeamVisibility>]
 [-Classification <String>] [-Verbose] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Using this command, global admins and Microsoft Teams service admins can access teams that they are not a member of to make a copy of them.

Creates a copy of a team. This operation also creates a copy of the corresponding group. You can specify which parts of the team to clone:

apps - Copies Microsoft Teams apps that are installed in the team.
channels – Copies the channel structure (but not the messages in the channel).
members – Copies the members and owners of the group.
settings – Copies all settings within the team, along with key group settings.
tabs – Copies the tabs within channels.

When tabs are cloned, they are put into an un configured state -- they are displayed on the tab bar in Microsoft Teams, and the first time you open them, you'll go through the configuration screen. If the person opening the tab does not have permission to configure apps, they will see a message explaining that the tab hasn't been configured.

## EXAMPLES

### EXAMPLE 1

```powershell
Copy-PnPTeamsTeam -Identity ee0f40fc-b2f7-45c7-b62d-11b90dd2ea8e -DisplayName "Library Assist" -PartsToClone apps,tabs,settings,channels,members
```
Creates a clone of a Microsoft Teams team named "Library Assist" from Microsoft Teams team ID ee0f40fc-b2f7-45c7-b62d-11b90dd2ea8e with the apps,tabs,settings,channels and members

### EXAMPLE 2

```powershell
Copy-PnPTeamsTeam -Identity "Team 12" -DisplayName "Library Assist"
```
Creates a clone of a Microsoft Teams team named "Library Assist" from Microsoft Teams team "Team 12" with the all the available parts

### EXAMPLE 3

```powershell
Copy-PnPTeamsTeam -Identity "Team 12" -DisplayName "Library Assist" -PartsToClone apps,tabs,settings,channels,members -Description "Self help community for library" -Classification "Library" -Visibility public
```
Creates a clone of a Microsoft Teams team named "Library Assist" from Microsoft Teams team  "Team 12" with the apps,tabs,settings,channels and members setting the classification to "Library", Visibility to public and Description to "Self help community for library"

### EXAMPLE 4

```powershell
Copy-PnPTeamsTeam -Identity "Team 12" -DisplayName "Library Assist" -PartsToClone settings,channels -Description "Self help community for library" -Classification "Library" -Visibility public
```
Creates a clone of a Microsoft Teams team named "Library Assist" from Microsoft Teams team  "Team 12" with the settings and channels setting the classification to "Library", Visibility to public and Description to "Self help community for library"

## PARAMETERS

### -Classification

Describes a classification for the group (such as low, medium or high business impact). If classification is not specified, the classification will be copied from the original team/group.

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

### -DisplayName

The display name for the group. This property is required when a group is created and it cannot be cleared during updates. Supports $filter and $orderby.

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

### -Identity

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

### -PartsToClone

A comma-separated list of the parts to clone. Allowed values are apps,channels,members,settings,tabs. When not provided, all available parts will be cloned.

```yaml
Type: ClonableTeamParts
DefaultValue: apps,channels,members,settings,tabs
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

### -Visibility

Specifies the visibility of the group. Possible values are: Private, Public. If visibility is not specified, the visibility will be copied from the original team/group. If the team being cloned is an educationClass team, the visibility parameter is ignored, and the new group's visibility will be set to HiddenMembership.

```yaml
Type: teamVisibilityType
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
