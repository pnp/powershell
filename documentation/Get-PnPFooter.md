---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPFooter.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPFooter
---

# Get-PnPFooter

## SYNOPSIS

Gets the configuration regarding the footer of the current web

## SYNTAX

### Default (Default)

```
Get-PnPFooter [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows the current configuration of the footer in the current web to be retrieved. The footer currently only works on Modern Communication sites.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPFooter
```

Returns the current footer configuration of the current web

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
