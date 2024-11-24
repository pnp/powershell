---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Stop-PnPFlowRun.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Stop-PnPFlowRun
---

# Stop-PnPFlowRun

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Stops/cancels a specific run of a Microsoft flow.

## SYNTAX

### Default (Default)

```
Stop-PnPFlowRun -Flow <PowerAutomateFlowPipeBind> -Identity <PowerAutomateFlowRunPipeBind>
 [-Environment <PowerAutomateEnvironmentPipeBind>] [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet cancels a running Power Automate flow run.

## EXAMPLES

### Example 1

```powershell
Stop-PnPFlowRun -Flow fba63225-baf9-4d76-86a1-1b42c917a182 -Identity 08585531682024670884771461819CU230
```
This cancels the specified flow run of the specified flow located in the default environment.

### Example 2

```powershell
Stop-PnPFlowRun -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Flow fba63225-baf9-4d76-86a1-1b42c917a182 -Identity 08585531682024670884771461819CU230 -Force
```
This cancels the specified flow run located in the specified environment of the specified flow without confirmation.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

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

The Name/Id of the flow to retrieve the available flow runs for.

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

### -Force

Specifying the Force parameter will skip the confirmation question.

```yaml
Type: SwitchParameter
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

### -Identity

The Name/Id of the flow run to cancel.

```yaml
Type: PowerAutomateFlowRunPipeBind
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
