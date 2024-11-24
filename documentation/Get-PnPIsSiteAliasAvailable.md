---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPIsSiteAliasAvailable.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPIsSiteAliasAvailable
---

# Get-PnPIsSiteAliasAvailable

## SYNOPSIS

Validates if a certain alias is still available to be used to create a new site collection for. If it is not, it will propose an alternative alias and URL which is still available.

## SYNTAX

### Default (Default)

```
Get-PnPIsSiteAliasAvailable [-Identity <String>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPIsSiteAliasAvailable -Identity "HR"
```

Validates if the alias "HR" is still available to be used

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPIsSiteAliasAvailable -Identity "HR"
```

Validates if the alias "HR" is still available to be used

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

Alias you want to check for if it is still available to create a new site collection for

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Alias
ParameterSets:
- Name: (All)
  Position: Named
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
