---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPPlannerBucket.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPPlannerBucket
---

# Remove-PnPPlannerBucket

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of Tasks.ReadWrite, Tasks.ReadWrite.All, Group.ReadWrite.All

Removes a planner bucket

## SYNTAX

### By Name

```
Remove-PnPPlannerBucket -Group <PlannerGroupPipeBind> -Plan <PlannerPlanPipeBind>
 -Identity <PlannerBucketPipeBind> [-Confirm]
```

### By Bucket Id

```
Remove-PnPPlannerBucket -BucketId <String> -Identity <PlannerBucketPipeBind> [-Confirm]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet removes a Planner bucket.

## EXAMPLES

### Example 1

```powershell
Remove-PnPPlannerBucket -Group "Marketing" -Plan "Conference" -Identity "Pre-conference Todos"
```

This removes the "Pre-conference Todos" bucket from the specified plan.

## PARAMETERS

### -BucketId

The id of a bucket to remove

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Bucket Id
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Confirm

Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- cf
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
- Name: By Name
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

Specify the id or name of the bucket.

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

### -Plan

Specify the id or name of the plan owning the bucket.

```yaml
Type: PlannerPlanPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Name
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
