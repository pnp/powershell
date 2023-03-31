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
Remove-PnPFlow -Environment <PowerAutomateEnvironmentPipeBind> -Identity <PowerAutomateFlowPipeBind> [-AsAdmin]
 [-Force] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet removes the specified flow.

## EXAMPLES

### Example 1
```powershell
$environment = Get-PnPFlowEnvironment
Remove-PnPFlow -Environment $environment -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```

This removes the specified flow.

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
The name of the environment which contains the flow.

```yaml
Type: PowerAutomateEnvironmentPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
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

