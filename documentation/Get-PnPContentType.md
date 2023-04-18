---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPContentType.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPContentType
---
  
# Get-PnPContentType

## SYNOPSIS
Retrieves a content type

## SYNTAX

```powershell
Get-PnPContentType [[-Identity] <ContentTypePipeBind>] [-List <ListPipeBind>] [-InSiteHierarchy]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to get single or list of content types from site or list. Use the `Identity` option to specify the exact content type.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPContentType
```

This will get a listing of all available content types within the current web

### EXAMPLE 2
```powershell
Get-PnPContentType -InSiteHierarchy
```

This will get a listing of all available content types within the site collection

### EXAMPLE 3
```powershell
Get-PnPContentType -Identity "Project Document"
```

This will get the content type with the name "Project Document" within the current context

### EXAMPLE 4
```powershell
Get-PnPContentType -List "Documents"
```

This will get a listing of all available content types within the list "Documents"

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

### -Identity
Name or ID of the content type to retrieve

```yaml
Type: ContentTypePipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -InSiteHierarchy
Search site hierarchy for content types

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
List to query

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


