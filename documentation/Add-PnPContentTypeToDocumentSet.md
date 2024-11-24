---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPContentTypeToDocumentSet.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPContentTypeToDocumentSet
---

# Add-PnPContentTypeToDocumentSet

## SYNOPSIS

Adds a content type to a document set.

## SYNTAX

### Default (Default)

```
Add-PnPContentTypeToDocumentSet -ContentType <ContentTypePipeBind[]>
 -DocumentSet <DocumentSetPipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add a content type to a document set.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPContentTypeToDocumentSet -ContentType "Test CT" -DocumentSet "Test Document Set"
```

This will add the content type called 'Test CT' to the document set called 'Test Document Set'.

### EXAMPLE 2

```powershell
$docset = Get-PnPDocumentSetTemplate -Identity "Test Document Set"
$ct = Get-PnPContentType -Identity "Test CT"
Add-PnPContentTypeToDocumentSet -ContentType $ct -DocumentSet $docset
```

This will add the content type called 'Test CT' to the document set called 'Test Document Set'.

### EXAMPLE 3

```powershell
Add-PnPContentTypeToDocumentSet -ContentType 0x0101001F1CEFF1D4126E4CAD10F00B6137E969 -DocumentSet 0x0120D520005DB65D094035A241BAC9AF083F825F3B
```

This will add the content type called 'Test CT' to the document set called 'Test Document Set'.

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

### -ContentType

The content type object, name or id to add. Either specify name, an id, or a content type object.

```yaml
Type: ContentTypePipeBind[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DocumentSet

The document set object or id to add the content type to. Either specify a name, a document set template object, an id, or a content type object.

```yaml
Type: DocumentSetPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
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
