---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPowerApp.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPPowerApp
---
  
# Get-PnPPowerApp

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Returns the Power Apps for a given environment

## SYNTAX

```powershell
Get-PnPPowerApp [-Environment <PowerPlatformEnvironmentPipeBind>] [-AsAdmin] [-Identity <PowerAppPipeBind>] 
[-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
This cmdlet returns the Power Apps for a given enviroment.

## EXAMPLES

### Example 1
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Get-PnPPowerApp -Environment $environment
```
This returns all the apps for a given Power Platform environment

### Example 2
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Get-PnPPowerApp -Environment $environment -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```
This returns a specific app

## PARAMETERS

### -Environment
The name of the Power Platform environment or an Environment object to retrieve the available Power Apps for.

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

### -Identity
The Id of the app to retrieve.

```yaml
Type: PowerAppPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AsAdmin
If specified returns all the Power Apps as admin. If not specified only the apps for the current user will be returned.

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