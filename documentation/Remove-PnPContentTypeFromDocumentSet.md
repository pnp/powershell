---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPContentTypeFromDocumentSet.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPContentTypeFromDocumentSet
---

# Remove-PnPContentTypeFromDocumentSet

## SYNOPSIS

Removes a content type from a document set.

## SYNTAX

### Default (Default)

```
Remove-PnPContentTypeFromDocumentSet -ContentType <ContentTypePipeBind>
 -DocumentSet <DocumentSetPipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows to remove a content type from a document set.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPContentTypeFromDocumentSet -ContentType "Test CT" -DocumentSet "Test Document Set"
```

This will remove the content type called 'Test CT' from the document set called 'Test Document Set'.

### EXAMPLE 2

```powershell
Remove-PnPContentTypeFromDocumentSet -ContentType 0x0101001F1CEFF1D4126E4CAD10F00B6137E969 -DocumentSet 0x0120D520005DB65D094035A241BAC9AF083F825F3B
```

This will remove the content type with ID '0x0101001F1CEFF1D4126E4CAD10F00B6137E969' from the document set with ID '0x0120D520005DB65D094035A241BAC9AF083F825F3B'.

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

The content type to remove. Either specify name, an id, or a content type object.

```yaml
Type: ContentTypePipeBind
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

The document set to remove the content type from. Either specify a name, a document set template object, an id, or a content type object.

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
