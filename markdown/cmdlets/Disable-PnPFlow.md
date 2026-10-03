---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Disable-PnPFlow.html
external help file: PnP.PowerShell.dll-Help.xml
title: Disable-PnPFlow
---
  
# Disable-PnPFlow

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Disables a specific flow

## SYNTAX

```powershell
Disable-PnPFlow [-Environment <PowerAutomateEnvironmentPipeBind>] -Identity <PowerAutomateFlowPipeBind> [-AsAdmin] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet disables a specific flow

## EXAMPLES

### Example 1
```powershell
Disable-PnPFlow -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```

Disables the specified flow in the default environment

### Example 2
```powershell
Disable-PnPFlow -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```

Disables the specified flow in the specified environment

## PARAMETERS

### -AsAdmin
Disable the flow as an administrator.

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
Identity of the flow to disable.

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


