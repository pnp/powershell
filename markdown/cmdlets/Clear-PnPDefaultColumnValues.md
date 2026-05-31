---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Clear-PnPDefaultColumnValues.html
external help file: PnP.PowerShell.dll-Help.xml
title: Clear-PnPDefaultColumnValues
---
  
# Clear-PnPDefaultColumnValues

## SYNOPSIS
Clear default column values for a document library

## SYNTAX

```powershell
Clear-PnPDefaultColumnValues [-List] <ListPipeBind> -Field <FieldPipeBind> [-Folder <String>]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Clear default column values for a document library, per folder, or for the root folder if the folder parameter has not been specified.

## EXAMPLES

### EXAMPLE 1
```powershell
Clear-PnPDefaultColumnValues -List Documents -Field MyField
```

Clears the default value for the field MyField on a library

### EXAMPLE 2
```powershell
Clear-PnPDefaultColumnValues -List Documents -Field MyField -Folder A
```

Clears the default value for the field MyField on the folder A on a library

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

### -Field
The internal name, id or a reference to a field

```yaml
Type: FieldPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Folder
A library relative folder path, if not specified it will set the default column values on the root folder of the library ('/')

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The ID, Name or Url of the list.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


