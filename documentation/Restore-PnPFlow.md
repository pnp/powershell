---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Restore-PnPFlow.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Restore-PnPFlow
---

# Restore-PnPFlow

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Restores a specific flow

## SYNTAX

### Default (Default)

```
Restore-PnPFlow -Identity <PowerAutomateFlowPipeBind>
 [-Environment <PowerAutomateEnvironmentPipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet Restores a specific flow

## EXAMPLES

### Example 1

```powershell
Restore-PnPFlow -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```

Restores the specified flow located in the default environment.

### Example 2

```powershell
Restore-PnPFlow -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment") -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```

Restores the specified flow located in the specified environment

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

### -Identity

Identity of the flow to Restore.

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
