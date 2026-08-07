---
Module Name: PnP.PowerShell
title: Remove-PnPContentTypeFromDocumentSet
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPContentTypeFromDocumentSet.html
---
 
# Remove-PnPContentTypeFromDocumentSet

## SYNOPSIS
Removes a content type from a document set.

## SYNTAX

```powershell
Remove-PnPContentTypeFromDocumentSet -ContentType <ContentTypePipeBind> -DocumentSet <DocumentSetPipeBind> [-Connection <PnPConnection>]
```

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
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ContentType
The content type to remove. Either specify name, an id, or a content type object.

```yaml
Type: ContentTypePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DocumentSet
The document set to remove the content type from. Either specify a name, a document set template object, an id, or a content type object.

```yaml
Type: DocumentSetPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

