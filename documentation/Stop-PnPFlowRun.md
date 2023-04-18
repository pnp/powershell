---
Module Name: PnP.PowerShell
title: Stop-PnPFlowRun
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Stop-PnPFlowRun.html
---
 
# Stop-PnPFlowRun

## SYNOPSIS
**Required Permissions**

* Azure: management.azure.com

Stops/cancels a specific run of a Microsoft flow.

## SYNTAX

```powershell
Stop-PnPFlowRun -Environment <PowerAutomateEnvironmentPipeBind> -Flow <PowerAutomateFlowPipeBind> -Identity <PowerAutomateFlowRunPipeBind> [-Force] 
```

## DESCRIPTION
This cmdlet cancels a running Power Automate flow run.

## EXAMPLES

### Example 1
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Stop-PnPFlowRun -Environment $environment -Flow fba63225-baf9-4d76-86a1-1b42c917a182 -Identity 08585531682024670884771461819CU230
```
This cancels the specified flow run of the specified flow


### Example 2
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Stop-PnPFlowRun -Environment $environment -Flow fba63225-baf9-4d76-86a1-1b42c917a182 -Identity 08585531682024670884771461819CU230 -Force
```
This cancels the specified flow run of the specified flow without confirmation

## PARAMETERS

### -Environment
The name of the Power Platform environment or an Environment object to retrieve the available flows for.

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
The Name/Id of the flow run to cancel.

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

