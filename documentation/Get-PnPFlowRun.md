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


### Example 3
```powershell
$environment = Get-PnPPowerPlatformEnvironment
$flowrun = Get-PnPFlowRun -Environment $environment -Flow fba63225-baf9-4d76-86a1-1b42c917a182 -Identity 08585531682024670884771461819CU230
$flowrun.Properties.trigger
```
This returns a specific flow run trigger information as below

```powershell
Name              : Recurrence
StartTime         : 2024-02-02 06:00:00
EndTime           : 2024-02-02 06:00:00
ScheduledTime     : 2024-02-02 06:00:00
OriginHistoryName : 08584947532854535568834568113CU171
Code              : OK
Status            : Succeeded
```


### Example 4
```powershell
$environment = Get-PnPPowerPlatformEnvironment
$flowruns = Get-PnPFlowRun -Environment $environment -Flow fba63225-baf9-4d76-86a1-1b42c917a182
$flowruns.Properties.trigger
```
This returns a specific flows trigger information as below

```powershell
Name              : Recurrence
StartTime         : 2024-02-02 06:00:00
EndTime           : 2024-02-02 06:00:00
ScheduledTime     : 2024-02-02 06:00:00
OriginHistoryName : 08584947532854535568834568113CU171
Code              : OK
Status            : Succeeded

Name              : Recurrence
StartTime         : 2024-02-01 06:00:00
EndTime           : 2024-02-01 06:00:00
ScheduledTime     : 2024-02-01 06:00:00
OriginHistoryName : 08584948396849679000001446214CU251
Code              : OK
Status            : Succeeded

Name              : Recurrence
StartTime         : 2024-01-31 06:00:00
EndTime           : 2024-01-31 06:00:00
ScheduledTime     : 2024-01-31 06:00:00
OriginHistoryName : 08584949260853628013416159080CU185
Code              : OK
Status            : Succeeded
```


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
