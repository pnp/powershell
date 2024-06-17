---
Module Name: PnP.PowerShell
title: Get-PnPPlannerPlan
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPlannerPlan.html
---
 
# Get-PnPPlannerPlan

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All
  * Microsoft Graph API: Group.Read.All
  
Returns all or a specific Planner plan for a Microsoft 365 Group.

## SYNTAX

### By Group
```powershell
Get-PnPPlannerPlan -Group <PlannerGroupPipeBind> [-Identity <PlannerPlanPipeBind>] [-ResolveIdentities]
  
```

### By Plan Id
```powershell
Get-PnPPlannerPlan -Id <String> [-ResolveIdentities]
  
```

## DESCRIPTION
This cmdlet returns all or a specific Planner plan for a Microsoft 365 Group.

## EXAMPLES

### Example 1
```powershell
Get-PnPPlannerPlan -Group "Marketing"
```

Returns all plans for the Marketing group.

### Example 2
```powershell
Get-PnPPlannerPlan -Group "Marketing" -Identity "Conference Plan"
```

Returns the specified plan for the Marketing group.

### Example 3
```powershell
Get-PnPPlannerPlan -Id "gndWOTSK60GfPQfiDDj43JgACDCb" -ResolveIdentities
```

Rerturns the plan with specified ID with resolved identities.

## PARAMETERS

### -Group
Specify the group containing the plans

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

### -Identity
If specified the plan with this ID or Name will be returned.

```yaml
Type: PlannerPlanPipeBind
Parameter Sets: By Group
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Id
If specified the plan with this ID will be returned.

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

### -ResolveIdentities
Show user display names instead of user IDs.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

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

