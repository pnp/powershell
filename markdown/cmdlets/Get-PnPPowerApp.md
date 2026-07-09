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
This cmdlet returns the Power Apps for a given environment.

### Prerequisites

Your Entra app registration must have the `user_impersonation` delegated permission from the Azure Service Management API. To add this permission using Azure CLI:

```bash
az ad app permission add --id <your-app-id> --api 797f4846-ba00-4fd7-ba43-dac1f8f63013 --api-permissions 41094075-9dad-400e-a0bd-54e686782033=Scope
az ad app permission admin-consent --id <your-app-id>
```

For full Power Platform access, you may also need to add permissions from the PowerApps Service API (found under "APIs my organization uses" in Azure Portal).

## EXAMPLES

### Example 1
```powershell
Get-PnPPowerApp
```
This returns all the apps for the default environment

### Example 2
```powershell
Get-PnPPowerApp -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```
This returns a specific app from a specific environment

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