---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Save-PnPPageConversionLog.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Save-PnPPageConversionLog
---

# Save-PnPPageConversionLog

## SYNOPSIS

Persists the current client side page conversion log data to the loggers linked to the last used page transformation run. Needs to be used in conjunction with the -LogSkipFlush flag on the ConvertTo-PnPPage cmdlet.

## SYNTAX

### Default (Default)

```
Save-PnPPageConversionLog [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION



## EXAMPLES

### EXAMPLE 1

```powershell
Save-PnPPageConversionLog
```

Persists the current client side page conversion log data to the loggers linked to the last used page transformation run. Needs to be used in conjunction with the -LogSkipFlush flag on the ConvertTo-PnPPage cmdlet.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
DefaultValue: ''
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
