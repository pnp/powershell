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

Returns the Custom Power Platform Connectors for a given environment

## SYNTAX

```powershell
Get-PnPPowerPlatformCustomConnector [-Environment <PowerPlatformEnvironmentPipeBind>] [-Identity <PowerPlatformConnectorPipeBind>] 
[-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
This cmdlet returns the custom connectors on a given enviroment.

## EXAMPLES

### Example 1
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Get-PnPPowerPlatformCustomConnector -Environment $environment
```
This returns all the custom connectors for a given Power Platform environment

### Example 2
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Get-PowerPlatformCustomConnector -Environment $environment -Identity 'Tikit Connector'
```
This returns a specific custom connector based on connector display name

## PARAMETERS

### -Environment
The name of the Power Platform environment or an Environment object to retrieve the available custom connectors for.

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
The Display Name of the connector to retrieve.

```yaml
Type: PowerPlatformCustomConnectorPipeBind
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