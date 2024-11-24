---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPFlowOwner.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPFlowOwner
---

# Get-PnPFlowOwner

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Returns the owners of a Power Automate flow

## SYNTAX

### Default (Default)

```
Get-PnPFlowOwner -Identity <PowerAutomateFlowPipeBind>
 [-Environment <PowerAutomateEnvironmentPipeBind>] [-AsAdmin]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet returns the Power Automate flow owners for a given Power Automate Flow in a Power Platform environment.

## EXAMPLES

### Example 1

```powershell
Get-PnPFlowOwner -Identity 33f78dac-7e93-45de-ab85-67cad0f6ee30
```
Returns all the owners of the Power Automate Flow with the provided identifier on the default Power Platform environment

### Example 2

```powershell
Get-PnPFlowOwner -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Identity 33f78dac-7e93-45de-ab85-67cad0f6ee30
```
Returns all the owners of the Power Automate Flow with the provided identifier on the specified Power Platform environment

## PARAMETERS

### -AsAdmin

If specified returns the owners of the given flow as admin. If not specified only the flows for the current user will be targeted, and returns the owners of the targeted flow.

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

### -Identity

The Name, Id or instance of the Power Automate Flow to retrieve the permissions of.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
