---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPlannerConfiguration.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPPlannerConfiguration
---
  
# Get-PnPPlannerConfiguration

## SYNOPSIS

**Required Permissions**

* Azure: tasks.office.com

Returns the Microsoft Planner configuration of the tenant

## SYNTAX

```powershell
Get-PnPPlannerConfiguration [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet returns the Microsoft Planner admin configuration of the tenant. Note that after changing the configuration using `Set-PnPPlannerTenantConfiguration`, this cmdlet may return varying results which could deviate from your desired configuration while the new configuration is being propagated across the tenant.

## EXAMPLES

### Example 1
```powershell
Get-PnPPlannerConfiguration
```
Returns the Microsoft Planner configuration of the tenant

## PARAMETERS

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
