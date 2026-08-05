---
Module Name: PnP.PowerShell
title: Remove-PnPPlannerRoster
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPPlannerRoster.html
---
 
# Remove-PnPPlannerRoster

## SYNOPSIS
Removes a Microsoft Planner Roster

## SYNTAX

```powershell
Remove-PnPPlannerRoster -Identity <PlannerRosterPipeBind> [-Connection <PnPConnection>] 
```

## DESCRIPTION
Removes a Microsoft Planner Roster

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPPlannerRoster -Identity "6519868f-868f-6519-8f86-19658f861965"
```

Removes the Microsoft Planner Roster with the provided identifier

## PARAMETERS

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

### -Identity
The name or ID of the Microsoft Planner Roster to remove

```yaml
Type: PlannerRosterPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)