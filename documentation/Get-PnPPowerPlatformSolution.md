---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPowerPlatformSolution.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPPowerPlatformSolution
---
  
# Get-PnPPowerPlatformSolution

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Returns the Power Platform Solution/s for a given environment

## SYNTAX

```powershell
Get-PnPPowerPlatformSolution [-Environment <PowerPlatformEnvironmentPipeBind>] [-Name <PowerPlatformConnectorPipeBind>] [-Verbose]
```

## DESCRIPTION
This cmdlet returns the PowerPlatform solution on a given enviroment.

## EXAMPLES

### Example 1
```powershell
Get-PnPPowerPlatformSolution -Environment (Get-PnPPowerPlatformEnvironment)
```
This returns all the solutions for a given Power Platform environment

### Example 2
```powershell
Get-PnPPowerPlatformSolution -Environment (Get-PnPPowerPlatformEnvironment -IsDefault) -Name 'My Solution Name'
```
This returns a specific solution on the default Power Platform environment

## PARAMETERS

### -Environment
The name of the Power Platform environment or an Environment instance to retrieve the available solutions for. If omitted, the default environment will be used.

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

### -Name
The Name of the solution to retrieve. If not provided, all the solutions will be returned.

```yaml
Type: PowerPlatformSolutionPipeBind
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