---
Module Name: PnP.PowerShell
title: Set-PnPDefaultContentTypeToList
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPDefaultContentTypeToList.html
---
 
# Set-PnPDefaultContentTypeToList

## SYNOPSIS
Sets the default content type for a list

## SYNTAX

```powershell
Set-PnPDefaultContentTypeToList -List <ListPipeBind> -ContentType <ContentTypePipeBind> 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to modify the default content type for a list.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPDefaultContentTypeToList -List "Project Documents" -ContentType "Project"
```

This will set the Project content type (which has already been added to a list) as the default content type

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
The content type object that needs to be set as the default content type on the list. Content Type needs to be present on the list.

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
The name of a list, an ID or the actual list object to update

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

