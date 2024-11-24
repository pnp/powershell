---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPPowerPlatformCustomConnector.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPPowerPlatformCustomConnector
---

# Get-PnPPowerPlatformCustomConnector

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Returns the custom Power Platform Connectors for a given environment

## SYNTAX

### Default (Default)

```
Get-PnPPowerPlatformCustomConnector [-Environment <PowerPlatformEnvironmentPipeBind>]
 [-Identity <PowerPlatformConnectorPipeBind>] [-AsAdmin] [-Verbose] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet returns the custom connectors on a given enviroment.

## EXAMPLES

### Example 1

```powershell
Get-PnPPowerPlatformCustomConnector -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment")
```
This returns all the custom connectors for a given Power Platform environment

### Example 2

```powershell
Get-PowerPlatformConnectorPipeBind -Identity fba63225-baf9-4d76-86a1-1b42c917a182
```
This returns a specific custom connector on the default Power Platform environment

## PARAMETERS

### -AsAdmin

If specified returns all the custom connectors as admin. If not specified only the custom connectors for the current user will be returned.

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

The Id of the connector to retrieve. If not provided, all custom connectors will be returned.

```yaml
Type: PowerPlatformConnectorPipeBind
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

### -Verbose

When provided, additional debug statements will be shown while executing the cmdlet.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
