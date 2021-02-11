---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPPlannerTask.html
external help file: PnP.PowerShell.dll-Help.xml
title: add-pnpplannertask
---
  
# Add-PnPPlannerTask

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Adds a new task to a planner bucket

## SYNTAX

### By Group
```powershell
Add-PnPPlannerTask -Group <PlannerGroupPipeBind> -Plan <PlannerPlanPipeBind> -Bucket <PlannerBucketPipeBind>
 -Title <String> [<CommonParameters>]
```

### By Plan Id
```powershell
Add-PnPPlannerTask -Bucket <PlannerBucketPipeBind> -PlanId <String> -Title <String> [<CommonParameters>]
```

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


## PARAMETERS

### -Bucket
The bucket to add the task too

```yaml
Type: PlannerBucketPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Group
Specify the group id or name of the group owning the plan.

```yaml
Type: PlannerGroupPipeBind
Parameter Sets: By Group
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Plan
Specify the id or name of the plan to add the tasks to.

```yaml
Type: PlannerPlanPipeBind
Parameter Sets: By Group
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PlanId
Specify the id the plan to add the tasks to.

```yaml
Type: String
Parameter Sets: By Plan Id
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
Specify the title of the task

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


