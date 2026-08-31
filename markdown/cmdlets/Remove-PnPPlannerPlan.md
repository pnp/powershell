---
Module Name: PnP.PowerShell
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
applicable: SharePoint Online
title: Remove-PnPPlannerPlan
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPPlannerPlan.html
tags: Available in the current Nightly Release only.
---
  
# Remove-PnPPlannerPlan

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of Tasks.ReadWrite or Group.ReadWrite.All (delegated), or Tasks.ReadWrite.All (application). Additionally Group.Read.All when -Group is supplied as a mail nickname or display name rather than as a group id, as resolving those reads the group from Microsoft Graph.

Removes a Planner plan.

## SYNTAX

```powershell
Remove-PnPPlannerPlan -Group <PlannerGroupPipeBind> -Identity <PlannerPlanPipeBind>  [-Connection <PnPConnection>] [-Confirm]
```

## DESCRIPTION
This cmdlet removes a Planner plan.

## EXAMPLES

### Example 1
```powershell
Remove-PnPPlannerPlan -Group "Marketing" -Identity "Conference Planning"
```

This removes the plan identified.

## PARAMETERS

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Group
Specify the id or name of the group owning the plan.

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

### -Identity
Specify the name or id of the plan.

```yaml
Type: PlannerPlanPipeBind
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

