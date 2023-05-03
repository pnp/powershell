---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPPlannerBucket.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPPlannerBucket
---
  
# Add-PnPPlannerBucket

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All
  
Adds a new bucket to a Planner plan

## SYNTAX

### By Group
```powershell
Add-PnPPlannerBucket -Group <PlannerGroupPipeBind> -Plan <PlannerPlanPipeBind> -Name <String> 
```

### By Plan Id
```powershell
Add-PnPPlannerBucket -PlanId <String> -Name <String> 
```

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
Parameter Sets: By Group
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The name of the bucket to add

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
A plan id to add the bucket to.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


