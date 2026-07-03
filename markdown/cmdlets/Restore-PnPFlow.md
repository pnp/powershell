---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Restore-PnPFlow.html
external help file: PnP.PowerShell.dll-Help.xml
title: Restore-PnPFlow
---
  
# Restore-PnPFlow

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Restores a specific flow

## SYNTAX

```powershell
Restore-PnPFlow [-Environment <PowerAutomateEnvironmentPipeBind>] -Identity <PowerAutomateFlowPipeBind> [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet Restores a specific flow

## EXAMPLES

### Example 1
```powershell
Restore-PnPFlow -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```

Restores the specified flow located in the default environment.

### Example 2
```powershell
Restore-PnPFlow -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```

Restores the specified flow located in the specified environment

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
Identity of the flow to Restore.

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


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


