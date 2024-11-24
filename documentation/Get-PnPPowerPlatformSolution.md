---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPPowerPlatformSolution.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPPowerPlatformSolution
---

# Get-PnPPowerPlatformSolution

## SYNOPSIS

**Required Permissions**

* Azure: management.azure.com

Returns the Power Platform Solution/s for a given environment

## SYNTAX

### Default (Default)

```
Get-PnPPowerPlatformSolution [-Environment <PowerPlatformEnvironmentPipeBind>]
 [-Name <PowerPlatformConnectorPipeBind>] [-Verbose] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet returns the PowerPlatform solution on a given enviroment.

## EXAMPLES

### Example 1

```powershell
Get-PnPPowerPlatformSolution -Environment (Get-PnPPowerPlatformEnvironment -Identity "myenvironment")
```
This returns all the solutions for a given Power Platform environment

### Example 2

```powershell
Get-PnPPowerPlatformSolution -Name 'My Solution Name'
```
This returns a specific solution on the default Power Platform environment

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

### -Name

The Name of the solution to retrieve. If not provided, all the solutions will be returned.

```yaml
Type: PowerPlatformSolutionPipeBind
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
