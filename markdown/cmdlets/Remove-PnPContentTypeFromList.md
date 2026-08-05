---
Module Name: PnP.PowerShell
title: Remove-PnPContentTypeFromList
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPContentTypeFromList.html
---
 
# Remove-PnPContentTypeFromList

## SYNOPSIS
Removes a content type from a list.

## SYNTAX

```powershell
Remove-PnPContentTypeFromList -List <ListPipeBind> -ContentType <ContentTypePipeBind> [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to remove a content type from a list.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPContentTypeFromList -List "Documents" -ContentType "Project Document"
```

This will remove a content type called "Project Document" from the "Documents" list.

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
The name of a content type, its ID or an actual content type object that needs to be removed from the specified list.

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
The name of the list, its ID or an actual list object the content type needs to be removed from.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

