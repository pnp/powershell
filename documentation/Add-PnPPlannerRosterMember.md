---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPPlannerRosterMember.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPPlannerRosterMember
---

# Add-PnPPlannerRosterMember

## SYNOPSIS

Adds a user to an existing Microsoft Planner Roster

## SYNTAX

### Default (Default)

```
Add-PnPPlannerRosterMember -Identity <PlannerRosterPipeBind> -User <String>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Adds a user to an existing Microsoft Planner Roster

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPPlannerRosterMember -Identity "6519868f-868f-6519-8f86-19658f861965" -User "johndoe@contoso.onmicrosoft.com"
```

Creates a new Microsoft Planner Roster

## PARAMETERS

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

Identity of the Microsoft Planner Roster to add the member to

```yaml
Type: PlannerRosterPipeBind
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

User principal name of the user to add as a member

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
