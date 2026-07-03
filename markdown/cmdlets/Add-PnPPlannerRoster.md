---
Module Name: PnP.PowerShell
title: Add-PnPPlannerRoster
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPPlannerRoster.html
---
 
# Add-PnPPlannerRoster

## SYNOPSIS
Creates a new Microsoft Planner Roster

## SYNTAX

```powershell
Add-PnPPlannerRoster [-Connection <PnPConnection>] 
```

## DESCRIPTION
Creates a new Microsoft Planner Roster

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPPlannerRoster
```

Creates a new Microsoft Planner Roster

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)