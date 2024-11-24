---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPPlannerRosterMember.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPPlannerRosterMember
---

# Get-PnPPlannerRosterMember

## SYNOPSIS

**Required Permissions**

* Azure: tasks.office.com

Returns the current members of a Microsoft Planner Roster

## SYNTAX

### Default (Default)

```
Get-PnPPlannerRosterMember -Identity <string> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet will return the current members of a Microsoft Planner Roster

## EXAMPLES

### Example 1

```powershell
Get-PnPPlannerRosterMember -Identity "6519868f-868f-6519-8f86-19658f861965"
```
Returns the current members of a Microsoft Planner Roster with the provided Id

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

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

Id of the Microsoft Planner Roster plan of which to return its current members

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
