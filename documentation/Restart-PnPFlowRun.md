---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Restart-PnPFlowRun.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Restart-PnPFlowRun
---

# Restart-PnPFlowRun

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Restarts/resubmits a specific flow run for the specified Microsoft Power Automate flow.

## SYNTAX

### Default (Default)

```
Restart-PnPFlowRun -Flow <PowerAutomateFlowPipeBind> -Identity <PowerAutomateFlowRunPipeBind>
 [-Environment <PowerAutomateEnvironmentPipeBind>] [-Force]
```

## ALIASES

This cmdlet has no aliases.

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

The Name/Id of the flow run to restart.

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
