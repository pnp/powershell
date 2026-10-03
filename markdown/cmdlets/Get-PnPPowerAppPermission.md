---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPowerAppPermission.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPPowerAppPermission
---

# Get-PnPPowerAppPermission

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com  
* PowerApps: service.powerapps.com

Returns the list of permissions assigned to a specified Power App.

## SYNTAX

```powershell
Get-PnPPowerAppPermission [-Environment <PowerPlatformEnvironmentPipeBind>] [-AsAdmin] -Identity <PowerAppPipeBind> 
[-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION

This cmdlet returns the list of user or group permissions granted to a specific Power App.

## EXAMPLES

### Example 1

```powershell
Get-PnPPowerAppPermission -Identity "bde2239e-fabc-42ad-9c9e-72323413b1b0"
```

Returns the list of permissions for the specified Power App in the default environment.

### Example 2

```powershell
Get-PnPPowerAppPermission -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Identity "bde2239e-fabc-42ad-9c9e-72323413b1b0" -AsAdmin
```

Returns the list of permissions for the specified Power App in the given environment using admin context.

## PARAMETERS

### -Environment

The name of the Power Platform environment or an Environment instance. If omitted, the default environment will be used.

```yaml
Type: PowerPlatformEnvironmentPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: The default environment
Accept pipeline input: True
Accept wildcard characters: False
```

### -AsAdmin

If specified, returns permissions using admin privileges. If not specified, only permissions for the current user will be returned.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity

The ID or instance of the Power App to retrieve permissions for.

```yaml
Type: PowerAppPipeBind
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
Retrieve the value for this parameter by either specifying `-ReturnConnection` on `Connect-PnPOnline` or by executing `Get-PnPConnection`.

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