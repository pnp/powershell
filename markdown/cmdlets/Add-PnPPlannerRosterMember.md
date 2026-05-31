---
Module Name: PnP.PowerShell
title: Add-PnPPlannerRosterMember
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPPlannerRosterMember.html
---
 
# Add-PnPPlannerRosterMember

## SYNOPSIS
Adds a user to an existing Microsoft Planner Roster

## SYNTAX

```powershell
Add-PnPPlannerRosterMember -Identity <PlannerRosterPipeBind> -User <String> [-Connection <PnPConnection>] 
```

## DESCRIPTION
Adds a user to an existing Microsoft Planner Roster

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPPlannerRosterMember -Identity "6519868f-868f-6519-8f86-19658f861965" -User "johndoe@contoso.onmicrosoft.com"
```

Creates a new Microsoft Planner Roster

## PARAMETERS

### -Identity
Identity of the Microsoft Planner Roster to add the member to

```yaml
Type: PlannerRosterPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
User principal name of the user to add as a member

```yaml
Type: String
Parameter Sets: (All)

Required: True
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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)