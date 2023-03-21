---
Module Name: PnP.PowerShell
title: Get-PnPPlannerRosterPlan
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPlannerRosterPlan.html
---
 
# Get-PnPPlannerRosterPlan

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Tasks.ReadWrite
  * Microsoft Graph API: Tasks.Read
  
Returns Microsft Planner roster plans for a specific Microsoft Planner Roster or a specific user

## SYNTAX

```powershell
Get-PnPPlannerRosterPlan [-Identity <PlannerRosterPipeBind>] [-User <string>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet returns Microsoft Planner roster plans for a specific Microsoft Planner Roster or a specific user

## EXAMPLES

### Example 1
```powershell
Get-PnPPlannerRosterPlan -Identity "abcdefgh"
```

Returns all plans inside the roster with the provided identifier

### Example 2
```powershell
Get-PnPPlannerRosterPlan -User "johndoe@contoso.onmicrosoft.com"
```

Returns all roster plans for the provided user

## PARAMETERS

### -Identity
A Microsoft Planner Roster Id or instance. Documentation: https://learn.microsoft.com/graph/api/plannerroster-list-plans?view=graph-rest-beta&tabs=http

```yaml
Type: PlannerGroupPipeBind
Parameter Sets: BYROSTER
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
The user principal name to query for. Documentation: https://learn.microsoft.com/graph/api/planneruser-list-rosterplans?view=graph-rest-beta&tabs=http

```yaml
Type: String
Parameter Sets: BYUSER
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