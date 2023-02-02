---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPlannerRosterMember.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPPlannerRosterMember
---
  
# Get-PnPPlannerRosterMember

## SYNOPSIS

**Required Permissions**

* Azure: tasks.office.com

Returns the current members of a Microsoft Planner Roster

## SYNTAX

```powershell
Get-PnPPlannerRosterMember -Identity <string> [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet will return the current members of a Microsoft Planner Roster

## EXAMPLES

### Example 1
```powershell
Get-PnPPlannerRosterMember -Identity "6519868f-868f-6519-8f86-19658f861965"
```
Returns the current members of a Microsoft Planner Roster with the provided Id

## PARAMETERS

### -Identity
Id of the Microsoft Planner Roster plan of which to return its current members

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

### -Connection
Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)