---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPowerPlatformEnvironment.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPPowerPlatformEnvironment
---
  
# Get-PnPPowerPlatformEnvironment

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Retrieves the Microsoft Power Platform environments for the current tenant.

## SYNTAX

### Default (Default)

```powershell
Get-PnPPowerPlatformEnvironment [-IsDefault] [-Connection <PnPConnection>] [-Verbose]
```

### By Identity

```powershell
Get-PnPPowerPlatformEnvironment -Identity <PowerPlatformEnvironmentPipeBind> [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
This cmdlet retrieves all of the Microsoft Power Platform environments for the current tenant

## EXAMPLES

### Example 1
```powershell
Get-PnPPowerPlatformEnvironment
```

This cmdlets returns all of the Power Platform environments for the current tenant.

### Example 2
```powershell
Get-PnPPowerPlatformEnvironment -IsDefault $true
```

This cmdlets returns the default Power Platform environment for the current tenant.

### Example 3
```powershell
Get-PnPPowerPlatformEnvironment -Identity "MyOrganization (default)"
```

This cmdlets returns the Power Platform environment with the provided display name for the current tenant.

## PARAMETERS

### -Identity
Allows specifying an environment display name or internal name to retrieve a specific environment.

```yaml
Type: bool
Parameter Sets: By Identity
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsDefault
Allows retrieval of the default Power Platform environment by passing in `-IsDefault $true`. When passing in `-IsDefault $false` you will get all non default environments. If not provided at all, all available environments, both default and non-default, will be returned.

```yaml
Type: bool
Parameter Sets: Default
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

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)