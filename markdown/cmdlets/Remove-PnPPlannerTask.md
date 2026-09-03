---
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPPlannerTask.html
tags: Available in the current Nightly Release only.
title: Remove-PnPPlannerTask
---
  
# Remove-PnPPlannerTask

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of Tasks.ReadWrite or Group.ReadWrite.All (delegated), or Tasks.ReadWrite.All (application)

Removes a Planner task.

## SYNTAX

```powershell
Remove-PnPPlannerTask -Task <PlannerTaskPipeBind> 
```

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
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


