---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSyntexModelPublication.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSyntexModelPublication
---

# Get-PnPSyntexModelPublication

## SYNOPSIS

Returns the libraries to which a Microsoft Syntex model was published.

This cmdlet only works when you've connected to a Syntex Content Center site.

## SYNTAX

### Default (Default)

```
Get-PnPSyntexModelPublications -Model <SyntexModelPipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command returns the libraries to which a Syntex document processing model defined in the connected Syntex Content Center site was published.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSyntexModelPublication -Identity "Invoice model"
```

Gets the libraries to which the document processing model named "Invoice model" was published.

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

### -Model

The name or id of the Syntex model.

```yaml
Type: SyntexModelPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
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
