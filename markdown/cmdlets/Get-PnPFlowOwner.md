---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFlowOwner.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFlowOwner
---
  
# Get-PnPFlowOwner

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Returns the owners of a Power Automate flow

## SYNTAX

```powershell
Get-PnPFlowOwner [-Environment <PowerAutomateEnvironmentPipeBind>] -Identity <PowerAutomateFlowPipeBind> [-AsAdmin]
```

## DESCRIPTION
This cmdlet returns the Power Automate flow owners for a given Power Automate Flow in a Power Platform environment.

## EXAMPLES

### Example 1
```powershell
Get-PnPFlowOwner -Identity 33f78dac-7e93-45de-ab85-67cad0f6ee30
```
Returns all the owners of the Power Automate Flow with the provided identifier on the default Power Platform environment

### Example 2
```powershell
Get-PnPFlowOwner -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Identity 33f78dac-7e93-45de-ab85-67cad0f6ee30
```
Returns all the owners of the Power Automate Flow with the provided identifier on the specified Power Platform environment

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
The Name, Id or instance of the Power Automate Flow to retrieve the permissions of.

```yaml
Type: PowerAutomateFlowPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AsAdmin
If specified returns the owners of the given flow as admin. If not specified only the flows for the current user will be targeted, and returns the owners of the targeted flow.

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
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

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