---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFlowRun.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFlowRun
---
  
# Get-PnPFlowRun

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Returns the flows runs for a given flow.

## SYNTAX

```powershell
Get-PnPFlowRun -Environment <PowerAutomateEnvironmentPipeBind> -Flow <PowerAutomateFlowPipeBind> [-Identity <PowerAutomateFlowRunPipeBind>]
[-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet returns the flow runs for a given flow.

## EXAMPLES

### Example 1
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Get-PnPFlowRun -Environment $environment -Flow fba63225-baf9-4d76-86a1-1b42c917a182
```
This returns all the flow runs for a given flow

### Example 2
```powershell
$environment = Get-PnPPowerPlatformEnvironment
Get-PnPFlowRun -Environment $environment -Flow fba63225-baf9-4d76-86a1-1b42c917a182 -Identity 08585531682024670884771461819CU230
```
This returns a specific flow run

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
The Name/Id of the flow to retrieve the available runs for.

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
The Name/Id of the flow run to retrieve.

```yaml
Type: PowerAutomateFlowRunPipeBind
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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
