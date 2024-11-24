---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSyntexModel.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSyntexModel
---

# Get-PnPSyntexModel

## SYNOPSIS

Returns Microsoft Syntex models from a Syntex Content Center.

This cmdlet only works when you've connected to a Syntex Content Center site.

## SYNTAX

### Default (Default)

```
Get-PnPSyntexModel [-Identity] <SyntexModelPipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command allows the retrieval of a Syntex document processing models defined in the connected Syntex Content Center site.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSyntexModel
```

Lists all the document processing models in the connected Syntex Content Center site.

### EXAMPLE 2

```powershell
Get-PnPSyntexModel -Identity 1
```

Gets the document processing model with the id 1.

### EXAMPLE 3

```powershell
Get-PnPSyntexModel -Identity "Invoice model"
```

Gets the document processing model named "Invoice model".

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
