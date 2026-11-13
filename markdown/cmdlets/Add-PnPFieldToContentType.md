---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPFieldToContentType.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPFieldToContentType
---
  
# Add-PnPFieldToContentType

## SYNOPSIS
Adds an existing site column to a content type

## SYNTAX

```powershell
Add-PnPFieldToContentType -Field <FieldPipeBind> -ContentType <ContentTypePipeBind> [-Required] [-Hidden]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add a field from site columns to an existing content type.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPFieldToContentType -Field "Project_Name" -ContentType "Project Document"
```

This will add an existing site column with an internal name of "Project_Name" to a content type called "Project Document"

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
Specifies which content type a field needs to be added to

```yaml
Type: ContentTypePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Field
Specifies the field that needs to be added to the content type

```yaml
Type: FieldPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Hidden
Specifies whether the field should be hidden or not

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Required
Specifies whether the field is required or not

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UpdateChildren
Specifies whether the field needs to be pushed to child content types. Default value is true.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: True
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


