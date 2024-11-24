---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPPlannerTask.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPPlannerTask
---

# Add-PnPPlannerTask

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of Tasks.ReadWrite, Tasks.ReadWrite.All, Group.ReadWrite.All

Adds a new task to a planner bucket

## SYNTAX

### By Group

```
Add-PnPPlannerTask -Group <PlannerGroupPipeBind> -Plan <PlannerPlanPipeBind>
 -Bucket <PlannerBucketPipeBind> -Title <String> [-PercentComplete <Int32>]
 [-DueDateTime <DateTime>] [-StartDateTime <DateTime>] [-AssignedTo <<String[]]>]
 [-Priority <Int32>] [-Description <String>] [-OutputTask]
```

### By Plan Id

```
Add-PnPPlannerTask -Bucket <PlannerBucketPipeBind> -PlanId <String> -Title <String>
 [-PercentComplete <Int32>] [-DueDateTime <DateTime>] [-StartDateTime <DateTime>]
 [-AssignedTo <<String[]]>] [-Priority <Int32>] [-Description <String>] [-OutputTask]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet adds a new task to Planner bucket

## EXAMPLES

### Example 1

```powershell
Add-PnPPlannerTask -Group "Marketing" -Plan "Conference Plan" -Bucket "Todos" -Title "Design booth layout"
```

This cmdlet adds a new task.

### Example 2

```powershell
Add-PnPPlannerTask -PlanId "QvfkTd1mc02gwxHjHC_43JYABhAy" -Bucket "Todos" -Title "Design booth layout"
```

This cmdlet adds a new task.

### Example 3

```powershell
Add-PnPPlannerTask -Group "Marketing" -Plan "Conference Plan" -Bucket "Todos" -Title "Design booth layout" -AssignedTo "user@contoso.com","manager@contoso.com"
```

This cmdlet adds a new task and assigns to user@contoso.com and manager@contoso.com

### Example 4

```powershell
$task = Add-PnPPlannerTask -Group "Marketing" -Plan "Conference Plan" -Bucket "Todos" -Title "Design booth layout" -AssignedTo "user@contoso.com","manager@contoso.com" -OutputTask
```

This returns the task as an object to inspect specific values

## PARAMETERS

### -AssignedTo

Specify the email(s) of the user to assign the task to.

```yaml
Type: String[]
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

### -Bucket

The bucket to add the task too

```yaml
Type: PlannerBucketPipeBind
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

### -Description

Sets the description (notes) of the task.

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

### -DueDateTime

Specify the due date.

```yaml
Type: DateTime
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

### -Group

Specify the group id or name of the group owning the plan.

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

### -OutputTask

Returns the just created task as an object to inspect values

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

### -PercentComplete

Defines the percentage of completeness of the task.

```yaml
Type: Int32
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

Specify the id the plan to add the tasks to.

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

### -Priority

Sets the priority of the task. Value should be a number between 0 and 10.
- values 0 and 1 are interpreted as _Urgent_
- values 2, 3 and 4 are interpreted as _Important_
- values 5, 6 and 7 are interpreted as _Medium_
- values 8, 9 and 10 are interpreted as _Low_

```yaml
Type: Int32
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

### -StartDateTime

Defines the start date of the task.

```yaml
Type: DateTime
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

### -Title

Specify the title of the task

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
