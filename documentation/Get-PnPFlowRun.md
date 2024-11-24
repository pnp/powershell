---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPFlowRun.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPFlowRun
---

# Get-PnPFlowRun

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Returns the flows runs for a given flow.

## SYNTAX

### Default (Default)

```
Get-PnPFlowRun -Flow <PowerAutomateFlowPipeBind> [-Environment <PowerAutomateEnvironmentPipeBind>]
 [-Identity <PowerAutomateFlowRunPipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet returns the flow runs for a given flow.

## EXAMPLES

### Example 1

```powershell
Get-PnPFlowRun -Flow fba63225-baf9-4d76-86a1-1b42c917a182
```
This returns all the flow runs for a given flow in the default environment

### Example 2

```powershell
Get-PnPFlowRun -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Flow fba63225-baf9-4d76-86a1-1b42c917a182 -Identity 08585531682024670884771461819CU230
```
This returns a specific flow run for a given flow in a specific environment

### Example 3

```powershell
$flowrun = Get-PnPFlowRun -Flow fba63225-baf9-4d76-86a1-1b42c917a182 -Identity 08585531682024670884771461819CU230
$flowrun.Properties.trigger
```
This returns the trigger information of a run of a specific flow located in the default environment as shown below

### Output

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
$flowruns = Get-PnPFlowRun -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Flow fba63225-baf9-4d76-86a1-1b42c917a182
$flowruns.Properties.trigger
```
This returns the trigger information of a run of a specific flow located in the specified environment as shown below

### Output

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

### -Connection

Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Environment

The name of the Power Platform environment or an Environment instance. If omitted, the default environment will be used.

```yaml
Type: PowerPlatformEnvironmentPipeBind
DefaultValue: The default environment
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Flow

The Name/Id of the flow to retrieve the available runs for.

```yaml
Type: PowerAutomateFlowPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Identity

The Name/Id of the flow run to retrieve.

```yaml
Type: PowerAutomateFlowRunPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
