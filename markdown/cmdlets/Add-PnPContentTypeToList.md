---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPContentTypeToList.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPContentTypeToList
---
  
# Add-PnPContentTypeToList

## SYNOPSIS
Adds a new content type to a list

## SYNTAX

```powershell
Add-PnPContentTypeToList -List <ListPipeBind> -ContentType <ContentTypePipeBind> [-DefaultContentType]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add content type to list. By specifying `-DefaultContentType` option it is possible set the newly added content type as default.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPContentTypeToList -List "Documents" -ContentType "Project Document" -DefaultContentType
```

This will add an existing content type to a list and sets it as the default content type

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
Specifies the content type that needs to be added to the list

```yaml
Type: ContentTypePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultContentType
Specify if the content type needs to be the default content type or not

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
Specifies the list to which the content type needs to be added

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


