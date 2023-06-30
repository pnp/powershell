---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPDocumentSet.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPDocumentSet
---
  
# Add-PnPDocumentSet

## SYNOPSIS
Creates a new document set in a library.

## SYNTAX

```powershell
Add-PnPDocumentSet -List <ListPipeBind> -Name <String> -ContentType <ContentTypePipeBind> 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Allows to add new document set to library.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPDocumentSet -List "Documents" -ContentType "Test Document Set" -Name "Test"
```

This will add a new document set based upon the 'Test Document Set' content type to a list called 'Documents'. The document set will be named 'Test'

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
The name of the content type, its ID or an actual content object referencing to the document set

```yaml
Type: ContentTypePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The name of the list, its ID or an actual list object from where the document set needs to be added

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The name of the document set

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


