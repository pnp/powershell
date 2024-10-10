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
Restore-PnPFlow -Environment <PowerAutomateEnvironmentPipeBind> -Identity <PowerAutomateFlowPipeBind> [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet Restores a specific flow

## EXAMPLES

### Example 1
```powershell
$environment = Get-PnPPowerPlatformEnvironment -isdefault
Restore-PnPFlow -Environment $environment -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```

Restores the specified flow.

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
The name of the environment or an Environment object to retrieve the available flows for.

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


