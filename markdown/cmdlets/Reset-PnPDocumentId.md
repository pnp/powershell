---
Module Name: PnP.PowerShell
title: Reset-PnPDocumentId
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Reset-PnPDocumentId.html
---
 
# Reset-PnPDocumentId

## SYNOPSIS
Requests the unique document ID of a specific file or all files with a specific content type in a document library to be recalculated and reassigned.

## SYNTAX

### Reset file

```powershell
Reset-PnPDocumentId -File <FilePipeBind> [-Verbose] [-Connection <PnPConnection>]
```

### Reset library

```powershell
Reset-PnPDocumentId -Library <ListPipeBind> -ContentType <ContentTypePipeBind> [-Verbose] [-Connection <PnPConnection>]
```

## DESCRIPTION
This cmdlet allows requesting SharePoint Online to recalculate and reassign the unique document ID of a specific file or of all files using a specific content type in a document library. This can be useful if the document ID of a file has been lost, has gotten corrupted or duplicated. The unique document ID will be calculated based on an internal predictable algorithm and will contain parts of the site collection, web, list and listitem.

When applying this to a specific file, it should only take seconds for it to recalculate and reassign the document ID. When applying it to the entire library, it may take up to 24 hours to process the request.

If the document ID remains the same after running this cmdlet, it means the assigned document ID is correct. There's no use of running it multiple times on the same file.

You need to be connected to the same site collection in which the file on which you wish to perform the operation resides.

## EXAMPLES

### EXAMPLE 1
```powershell
Reset-PnPDocumentId -File "/sites/demo/Shared Documents/MyDocument.docx"
```

This will request SharePoint Online to recalculate and reassign the unique document ID of the file MyDocument.docx in the Shared Documents library of the demo site collection.

### EXAMPLE 2
```powershell
Get-PnPFileInFolder -Recurse -FolderSiteRelativeUrl "Shared Documents" -ItemName "MyDocument.docx" | Reset-PnPDocumentId
```

This will request SharePoint Online to recalculate and reassign the unique document ID of the file MyDocument.docx in the Shared Documents library of the current site collection.

### EXAMPLE 3
```powershell
 Reset-PnPDocumentId -Library "Documents" -ContentType (Get-PnPContentType -List "Documents" | Where-Object Name -eq "Document"
```

This will request SharePoint Online to recalculate and reassign the unique document ID of all files using the Document content type in the default Documents library of the current site collection.

## PARAMETERS

### -File
The ID, listitem instance, File instance or server relative path of the file for which you want to request a document id reset.

```yaml
Type: FilePipeBind
Parameter Sets: Reset file

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Library
The ID, library instance or server relative URL of the library for which you want to request a document id reset.

```yaml
Type: ListPipeBind
Parameter Sets: Reset library

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -ContentType
The ID, or name of the content type on the library to reset the document ids for.

```yaml
Type: ContentTypePipeBind
Parameter Sets: Reset library

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)