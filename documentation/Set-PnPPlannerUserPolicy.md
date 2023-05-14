---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPPlannerUserPolicy.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPPlannerUserPolicy
---
  
# Set-PnPPlannerUserPolicy

## SYNOPSIS

**Required Permissions**

* Azure: tasks.office.com

Allows a Microsoft Planner user policy to be set for a specific user

## SYNTAX

```powershell
Set-PnPPlannerUserPolicy -Identity <string> [-BlockDeleteTasksNotCreatedBySelf <boolean>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet allows a Microsoft Planner user policy to be set for the provided user

## EXAMPLES

### Example 1
```powershell
Set-PnPPlannerUserPolicy -Identity "johndoe@contoso.onmicrosoft.com"
```
Sets the Microsoft Planner user policy for the provided user

## PARAMETERS

### -Identity
Azure Active Directory user identifier or user principal name of the user to create the Microsoft Planner policy for

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

### -BlockDeleteTasksNotCreatedBySelf
Allows the user for which the policy gets created to be blocked from deleting tasks that have not been created by the user itself

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
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