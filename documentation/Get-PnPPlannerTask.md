---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPPlannerTask.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPPlannerTask
---

# Get-PnPPlannerTask

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of Tasks.Read, Tasks.ReadWrite, Tasks.Read.All, Tasks.ReadWrite.All, Group.Read.All, Group.ReadWrite.All

Returns Planner tasks

## SYNTAX

### By Group

```
Get-PnPPlannerTask -Group <PlannerGroupPipeBind> -Plan <PlannerPlanPipeBind>
 [-ResolveUserDisplayNames]
```

### By Bucket

```
Get-PnPPlannerTask -Bucket <PlannerBucketPipeBind> [-ResolveUserDisplayNames]
```

### By Plan Id

```
Get-PnPPlannerTask -PlanId <String> [-ResolveUserDisplayNames]
```

### By Task Id

```
Get-PnPPlannerTask -TaskId <String> [-ResolveUserDisplayNames]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet returns Planner tasks.

## EXAMPLES

### Example 1

```powershell
Get-PnPPlannerTask -Group "Marketing" -Plan "Conference Plan"
```

This returns all tasks for the specific plan.

### Example 2

```powershell
$tasks = Get-PnPPlannerTask -Group "Marketing" -Plan "Conference Plan" -ResolveUserDiplayNames
$task = $tasks | Select-Object -First 1
$task.CreatedBy.DisplayName
```

This retrieves all tasks for a specific plan, takes the first task and prints the display name of the user that created the task.

### Example 3

```powershell
Get-PnPPlannerTask -PlanId "QvfkTd1mc02gwxHjHC_43JYABhAy"
```

This returns all tasks for the specified plan.

### Example 4

```powershell
Get-PnPPlannerTask -TaskId "QvfkTd1mc02gwxHjHC_43JYABhAy"
```

This returns a specific task.

## PARAMETERS

### -Bucket

Specify the bucket or bucket id to retrieve the tasks for.

```yaml
Type: PlannerBucketPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Bucket
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Group

Specify the group id or name of group owning the plan.

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

### -IncludeDetails

Includes checklist and description details

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Task Id
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

Specify the id or name of the plan to retrieve the tasks for.

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

Specify the id of the plan to retrieve the tasks for.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Plan Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ResolveUserDisplayNames

Will resolve user id's to usernames

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

### -TaskId

Specify the id of the task to retrieve.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Task Id
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
