---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/new-pnpplannerplan
schema: 2.0.0
title: new-pnpplannerplan
---

# New-PnPPlannerPlan

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All
Creates a new Planner plan.

## SYNTAX

```powershell
New-PnPPlannerPlan -Group <PlannerGroupPipeBind> -Title <String>  [<CommonParameters>]
```

## DESCRIPTION
This cmdlet creates a new Planner plan.

## EXAMPLES

### Example 1
```powershell
New-PnPPlannerPlan -Group "Marketing" -Title "Conference Plan"
```

This example will add a new plan called "Conference Plan" to the "Marketing" group

### Example 2
```powershell
New-PnPPlannerPlan -Group 'baba9192-55be-488a-9fb7-2e2e76edbef2' -Title "Master Plan"
```

This example will add a new plan called "Master Plan" to group using the group id.

## PARAMETERS


### -Group
Specify the group name or id owning the plan.

```yaml
Type: PlannerGroupPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
Specify the name of the new plan.

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
