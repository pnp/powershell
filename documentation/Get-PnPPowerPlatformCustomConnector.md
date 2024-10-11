---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPowerPlatformCustomConnector.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPPowerPlatformCustomConnector
---
  
# Get-PnPPowerPlatformCustomConnector

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Returns the custom Power Platform Connectors for a given environment

## SYNTAX

```powershell
Get-PnPPowerPlatformCustomConnector [-Environment <PowerPlatformEnvironmentPipeBind>] [-Identity <PowerPlatformConnectorPipeBind>] [-AsAdmin] [-Verbose]
```

## DESCRIPTION
This cmdlet returns the custom connectors on a given enviroment.

## EXAMPLES

### Example 1
```powershell
Get-PnPPowerPlatformCustomConnector -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment")
```
This returns all the custom connectors for a given Power Platform environment

### Example 2
```powershell
Get-PowerPlatformConnectorPipeBind -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```
This returns a specific custom connector on the default Power Platform environment

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

### -Identity
The Id of the connector to retrieve. If not provided, all custom connectors will be returned.

```yaml
Type: PowerPlatformConnectorPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AsAdmin
If specified returns all the custom connectors as admin. If not specified only the custom connectors for the current user will be returned.

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