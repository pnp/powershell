---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPDocumentSetTemplate.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPDocumentSetTemplate
---

# Get-PnPDocumentSetTemplate

## SYNOPSIS

Retrieves a document set template

## SYNTAX

### Default (Default)

```
Get-PnPDocumentSetTemplate [-Identity] <DocumentSetPipeBind> [-Connection <PnPConnection>]
 [-Includes <String[]>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve a document set template.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPDocumentSetTemplate -Identity "Test Document Set"
```

This will get the document set template with the name "Test Document Set"

### EXAMPLE 2

```powershell
Get-PnPDocumentSetTemplate -Identity "0x0120D520005DB65D094035A241BAC9AF083F825F3B"
```

This will get the document set template with the id "0x0120D520005DB65D094035A241BAC9AF083F825F3B"

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

Either specify a name, an id, a document set template object or a content type object

```yaml
Type: DocumentSetPipeBind
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

### -Includes

Optionally allows properties to be retrieved for the returned document set template which are not included in the response by default

```yaml
Type: String[]
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
