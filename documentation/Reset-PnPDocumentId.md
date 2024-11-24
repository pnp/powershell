---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Reset-PnPDocumentId.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Reset-PnPDocumentId
---

# Reset-PnPDocumentId

## SYNOPSIS

Requests the unique document ID of a file to be recalculated and reassigned.

## SYNTAX

### Default (Default)

```
Reset-PnPDocumentId -Identity <FilePipeBind> [-Verbose] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows requesting SharePoint Online to recalculate and reassign the unique document ID of a file. This can be useful if the document ID of a file has been lost, has gotten corrupted or duplicated. The unique document ID will be calculated based on an internal predictable algorithm and will contain parts of the site collection, web, list and listitem. It should only take seconds for it to recalculate and reassign the document ID. If it remains the same after running this cmdlet, it means the assigned document ID is correct. There's no use of running it multiple times on the same file.

You need to be connected to the same site collection in which the file on which you wish to perform the operation resides.

## EXAMPLES

### EXAMPLE 1

```powershell
Reset-PnPDocumentId -Identity "/sites/demo/Shared Documents/MyDocument.docx"
```

This will request SharePoint Online to recalculate and reassign the unique document ID of the file MyDocument.docx in the Shared Documents library of the demo site collection.

### EXAMPLE 2

```powershell
Get-PnPFileInFolder -Recurse -FolderSiteRelativeUrl "Shared Documents" -ItemName "MyDocument.docx" | Reset-PnPDocumentId
```

This will request SharePoint Online to recalculate and reassign the unique document ID of the file MyDocument.docx in the Shared Documents library of the current site collection.

## PARAMETERS

### -Identity

The ID, listitem instance, File instance or server relative path of the file for which you want to request a document id reset.

```yaml
Type: FilePipeBind
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
