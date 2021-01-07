---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/add-pnpplannertask
schema: 2.0.0
title: add-pnpplannertask
---

# Add-PnPPlannerTask

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Adds a new task to a planner bucket

## SYNTAX

```powershell
Add-PnPPlannerTask -Group <PlannerGroupPipeBind> -Plan <PlannerPlanPipeBind> -Bucket <PlannerBucketPipeBind>
 -Title <String> [<CommonParameters>]
```

## DESCRIPTION
This cmdlet adds a new task to Planner bucket

## EXAMPLES

### Example 1
```powershell
Add-PnPPlannerTask -Group "Marketing" -Plan "Conference Plan" -Bucket "Todos" -Title "Design booth layout"
```

This cmdlet adds a new task called "Design booth layout" in the planner bucket "Todos" in the plan "Conference Plan" in the "Marketing Group".

### Example 2
```powershell
Add-PnPPlannerTask -Group "baba9192-55be-488a-9fb7-2e2e76edbef2" -Plan "odmeGVFLm02Xb0C9rWMtg5cACLXR" -Bucket "AuSMGhJKL0qLad8axOdHdZcAPuWa" -Title "Complete Project"
```

This cmdlet adds a new task called "Complete Project" using group id, plan id and bucket id.


## PARAMETERS

### -Bucket
Specify the name or id of planner bucket to add the task too

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
Specify the group id, mailNickname or display name of the group to use.

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
