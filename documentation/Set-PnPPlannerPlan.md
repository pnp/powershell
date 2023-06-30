---
Module Name: PnP.PowerShell
title: Set-PnPPlannerPlan
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPPlannerPlan.html
---
 
# Set-PnPPlannerPlan

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Updates an existing Planner plan.

## SYNTAX

### By Group
```powershell
Set-PnPPlannerPlan -Group <PlannerGroupPipeBind> -Plan <PlannerPlanPipeBind> -Title <String>
  
```

### By Plan Id
```powershell
Set-PnPPlannerPlan -PlanId <String> -Title <String>  
```

## DESCRIPTION
This cmdlet updates an existing planner plan.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPPlannerPlan -Group "Marketing" -Plan "Conference" -Title "Conference 2020"
```

This example renames the "Conference" plan to "Conference 2020".

## PARAMETERS

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
Specify the id or name of the plan to update.

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
Specify the id of the plan to update.

```yaml
Type: String
Parameter Sets: By Plan Id
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The new title of the plan.

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

