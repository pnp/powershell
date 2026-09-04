---
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPFlow.html
Module Name: PnP.PowerShell
title: Remove-PnPFlow
schema: 2.0.0
---
 
# Remove-PnPFlow

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Removes the specified flow.

## SYNTAX

```powershell
Remove-PnPFlow [-Environment <PowerAutomateEnvironmentPipeBind>] -Identity <PowerAutomateFlowPipeBind> [-AsAdmin]
 [-Force] [-ThrowExceptionIfPowerAutomateNotFound] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet removes the specified flow.

## EXAMPLES

### Example 1
```powershell
Remove-PnPFlow -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```

This removes the specified flow from the default environment.

### Example 2
```powershell
Remove-PnPFlow -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Identity fba63225-baf9-4d76-86a1-1b42c917a182 -ThrowExceptionIfPowerAutomateNotFound
```

This removes the specified flow located in the specified environment and throws an exception if the specified flow is not present.

## PARAMETERS

### -AsAdmin
If specified removes the flow as an administrator.

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
The name of, or a flow object to remove.

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

### -ThrowExceptionIfPowerAutomateNotFound
Switch parameter if an exception should be thrown if the requested flow does not exist (true) or if omitted, nothing will be returned in case the flow does not exist

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
If specified, no confirmation question will be asked.

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


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

