---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPPlannerPlan.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPPlannerPlan
---

# New-PnPPlannerPlan

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Creates a new Planner plan.

## SYNTAX

### Default (Default)

```
New-PnPPlannerPlan -Group <PlannerGroupPipeBind> -Title <String>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet creates a new Planner plan.

## EXAMPLES

### Example 1

```powershell
New-PnPPlannerPlan -Group "Marketing" -Title "Conference Plan"
```

This example will add a new plan called "Conference Plan" to the "Marketing" group.

## PARAMETERS

### -Group

Specify the group name or id owning the plan.

```yaml
Type: PlannerGroupPipeBind
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

### -Title

Specify the name of the new plan.

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

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable,
-InformationAction, -InformationVariable, -OutBuffer, -OutVariable, -PipelineVariable,
-ProgressAction, -Verbose, -WarningAction, and -WarningVariable. For more information, see
[about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
