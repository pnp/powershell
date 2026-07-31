---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPlannerUserPolicy.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPPlannerUserPolicy
---
  
# Get-PnPPlannerUserPolicy

## SYNOPSIS

**Required Permissions**

* Azure: tasks.office.com

Returns the Microsoft Planner user policy for a specific user

## SYNTAX

```powershell
Get-PnPPlannerUserPolicy -Identity <string> [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet returns the Microsoft Planner user policy for the provided user. If a Microsoft Planner user policy has never been set yet on a tenant, this cmdlet may return a '403 Forbidden: Access is denied' error. Set a policy once first to enable the background configuration to be done so this cmdlet can succeed from thereon.

## EXAMPLES

### Example 1
```powershell
Get-PnPPlannerUserPolicy -Identity "johndoe@contoso.onmicrosoft.com"
```
Returns the Microsoft Planner user policy for the provided user

## PARAMETERS

### -Identity
Azure Active Directory user identifier or user principal name of the user to retrieve the Microsoft Planner policy for

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