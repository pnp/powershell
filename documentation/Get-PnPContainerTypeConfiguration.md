---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPContainerTypeConfiguration.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPContainerTypeConfiguration
---

# Get-PnPContainerTypeConfiguration

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns container type configuration of a SharePoint repository services application.

## SYNTAX

### Default (Default)

```
Get-PnPContainerTypeConfiguration [-Identity <GUID>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPContainerTypeConfiguration -Identity a187e399-0c36-4b98-8f04-1edc167a0996
```

Returns a container type configuration data of the application created under the specified SharePoint repository services application.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPContainerTypeConfiguration -Identity a187e399-0c36-4b98-8f04-1edc167a0996
```

Returns a container type configuration data of the application created under the specified SharePoint repository services application.

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

### -Identity

Specify container type GUID

```yaml
Type: ContainerPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
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
