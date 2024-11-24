---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Disable-PnPPageScheduling.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Disable-PnPPageScheduling
---

# Disable-PnPPageScheduling

## SYNOPSIS

Disables the modern page schedule feature

## SYNTAX

### Default (Default)

```
Disable-PnPPageScheduling [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This will disable page publishing scheduling on modern sites

## EXAMPLES

### EXAMPLE 1

```powershell
Disable-PnPPageScheduling
```

This will disable page publishing scheduling on the current site

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
