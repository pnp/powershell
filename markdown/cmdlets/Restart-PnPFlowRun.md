---
Module Name: PnP.PowerShell
title: Restart-PnPFlowRun
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Restart-PnPFlowRun.html
---
 
# Restart-PnPFlowRun

## SYNOPSIS
**Required Permissions**

* Azure: management.azure.com

Restarts/resubmits a specific flow run for the specified Microsoft Power Automate flow.

## SYNTAX

```powershell
Restart-PnPFlowRun [-Environment <PowerAutomateEnvironmentPipeBind>] -Flow <PowerAutomateFlowPipeBind> -Identity <PowerAutomateFlowRunPipeBind> [-Force] 
```

## DESCRIPTION
This cmdlet restarts/resubmits a specific Power Automate flow run.

## EXAMPLES

### Example 1
```powershell
Restart-PnPFlowRun -Flow fba63225-baf9-4d76-86a1-1b42c917a182 -Identity 08585531682024670884771461819CU230
```
This restarts the specified flow run of the specified flow located in the default environment


### Example 2
```powershell
$environment = 
Restart-PnPFlowRun -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Flow fba63225-baf9-4d76-86a1-1b42c917a182 -Identity 08585531682024670884771461819CU230 -Force
```
This restarts the specified flow run of the specified flow without confirmation located in the specified environment

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

### -Flow
The Name/Id of the flow to retrieve the available flow runs for.

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

### -Identity
The Name/Id of the flow run to restart.

```yaml
Type: PowerAutomateFlowRunPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Specifying the Force parameter will skip the confirmation question.

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
