---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPPlannerTask.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPPlannerTask
---
  
# Add-PnPPlannerTask

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of Tasks.ReadWrite, Tasks.ReadWrite.All, Group.ReadWrite.All

Adds a new task to a planner bucket

## SYNTAX

### By Group
```powershell
Add-PnPPlannerTask -Group <PlannerGroupPipeBind> -Plan <PlannerPlanPipeBind> -Bucket <PlannerBucketPipeBind> -Title <String> 
[-PercentComplete <Int32>] [-DueDateTime <DateTime>] [-StartDateTime <DateTime>]
 [-AssignedTo <String[]] [-Priority <Int32>] [-Description <String>] [-OutputTask]
 
```

### By Plan Id
```powershell
Add-PnPPlannerTask -Bucket <PlannerBucketPipeBind> -PlanId <String> -Title <String> 
[-PercentComplete <Int32>] [-DueDateTime <DateTime>] [-StartDateTime <DateTime>]
 [-AssignedTo <String[]] [-Priority <Int32>] [-Description <String>] [-OutputTask]
 
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

### -AssignedTo
Specify the email(s) of the user to assign the task to.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StartDateTime
Defines the start date of the task.

```yaml
Type: DateTime
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DueDateTime
Specify the due date.

```yaml
Type: DateTime
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PercentComplete
Defines the percentage of completeness of the task.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Priority
Sets the priority of the task. Value should be a number between 0 and 10.
- values 0 and 1 are interpreted as _Urgent_
- values 2, 3 and 4 are interpreted as _Important_
- values 5, 6 and 7 are interpreted as _Medium_
- values 8, 9 and 10 are interpreted as _Low_

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
Sets the description (notes) of the task.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutputTask
Returns the just created task as an object to inspect values

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


