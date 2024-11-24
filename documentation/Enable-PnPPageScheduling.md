---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Enable-PnPPageScheduling.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Enable-PnPPageScheduling
---

# Enable-PnPPageScheduling

## SYNOPSIS

Enables the modern page schedule feature

## SYNTAX

### Default (Default)

```
Enable-PnPPageScheduling [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This will enable page publishing scheduling on modern sites

## EXAMPLES

### EXAMPLE 1

```powershell
Enable-PnPPageScheduling
```

This will enable page publishing scheduling on the current site

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
