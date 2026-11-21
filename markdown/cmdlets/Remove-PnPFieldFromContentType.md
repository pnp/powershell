---
Module Name: PnP.PowerShell
title: Remove-PnPFieldFromContentType
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPFieldFromContentType.html
---
 
# Remove-PnPFieldFromContentType

## SYNOPSIS
Removes a site column from a content type

## SYNTAX

```powershell
Remove-PnPFieldFromContentType -Field <FieldPipeBind> -ContentType <ContentTypePipeBind> [-DoNotUpdateChildren]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to remove a field from a content type.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPFieldFromContentType -Field "Project_Name" -ContentType "Project Document"
```

This will remove the site column with an internal name of "Project_Name" from a content type called "Project Document"

### EXAMPLE 2
```powershell
Remove-PnPFieldFromContentType -Field "Project_Name" -ContentType "Project Document" -DoNotUpdateChildren
```

This will remove the site column with an internal name of "Project_Name" from a content type called "Project Document". It will not update content types that inherit from the "Project Document" content type.

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
The content type where the field is to be removed from

```yaml
Type: ContentTypePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DoNotUpdateChildren
If specified, inherited content types will not be updated

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Field
The field to remove

```yaml
Type: FieldPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

