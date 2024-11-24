---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPPlannerRoster.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPPlannerRoster
---

# Add-PnPPlannerRoster

## SYNOPSIS

Creates a new Microsoft Planner Roster

## SYNTAX

### Default (Default)

```
Add-PnPPlannerRoster [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Creates a new Microsoft Planner Roster

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPPlannerRoster
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
