---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPPlannerTask.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPPlannerTask
---

# Remove-PnPPlannerTask

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of Tasks.ReadWrite, Tasks.ReadWrite.All, Group.ReadWrite.All

Removes a Planner task.

## SYNTAX

### Default (Default)

```
Remove-PnPPlannerTask -Task <PlannerTaskPipeBind>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet removes a specific Planner task.

## EXAMPLES

### Example 1

```powershell
Remove-PnPPlannerTask -Task _LIqnL4lZUqurT71i2-iY5YALFLk
```

Removes the task with the specified id.

## PARAMETERS

### -Task

Specify the id or Task object to delete.

```yaml
Type: PlannerTaskPipeBind
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
