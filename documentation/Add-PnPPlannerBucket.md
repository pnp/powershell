---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPPlannerBucket.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPPlannerBucket
---

# Add-PnPPlannerBucket

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of Tasks.ReadWrite, Tasks.ReadWrite.All, Group.ReadWrite.All,

Adds a new bucket to a Planner plan

## SYNTAX

### By Group

```
Add-PnPPlannerBucket -Group <PlannerGroupPipeBind> -Plan <PlannerPlanPipeBind> -Name <String>
```

### By Plan Id

```
Add-PnPPlannerBucket -PlanId <String> -Name <String>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlets creates a new bucket for tasks in a Planner plan.

## EXAMPLES

### Example 1

```powershell
Add-PnPPlannerBucket -Group "My Group" -Plan "My Plan" -Name "Project Todos"
```

Adds a new bucket called "Project Todos" to the specified plans

### Example 2

```powershell
Add-PnPPlannerBucket -PlanId "QvfkTd1mc02gwxHjHC_43JYABhAy" -Name "Project Todos"
```

Adds a new bucket called "Project Todos" to the plan with the specified id.

## PARAMETERS

### -Group

Specify the group id or group name of the group owning the plan.

```yaml
Type: PlannerGroupPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Group
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Name

The name of the bucket to add

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

### -Plan

Specify the id or name of the plan to add the tasks to.

```yaml
Type: PlannerPlanPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Group
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -PlanId

A plan id to add the bucket to.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Plan Id
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
