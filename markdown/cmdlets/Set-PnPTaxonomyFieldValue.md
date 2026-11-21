---
Module Name: PnP.PowerShell
title: Set-PnPTaxonomyFieldValue
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTaxonomyFieldValue.html
---
 
# Set-PnPTaxonomyFieldValue

## SYNOPSIS
Sets a taxonomy term value in a listitem field

## SYNTAX

### ITEM (Default)
```powershell
Set-PnPTaxonomyFieldValue -ListItem <ListItem> -InternalFieldName <String> -TermId <Guid>
 [-Label <String>] [-Connection <PnPConnection>] 
```

### PATH
```powershell
Set-PnPTaxonomyFieldValue -ListItem <ListItem> -InternalFieldName <String> -TermPath <String>
 [-Connection <PnPConnection>] 
```

### ITEMS
```powershell
Set-PnPTaxonomyFieldValue -ListItem <ListItem> -InternalFieldName <String> [-Terms <Hashtable>]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to update taxonomy term value in a listitem field.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTaxonomyFieldValue -ListItem $item -InternalFieldName 'Department' -TermId 863b832b-6818-4e6a-966d-2d3ee057931c
```

Sets the field called 'Department' to the value of the term with the ID specified

### EXAMPLE 2
```powershell
Set-PnPTaxonomyFieldValue -ListItem $item -InternalFieldName 'Department' -TermPath 'CORPORATE|DEPARTMENTS|HR'
```

Sets the field called 'Department' to the term called HR which is located in the DEPARTMENTS termset, which in turn is located in the CORPORATE termgroup.

### EXAMPLE 3
```powershell
Set-PnPTaxonomyFieldValue -ListItem $item -InternalFieldName 'Department' -Terms @{"TermId1"="Label1";"TermId2"="Label2"}
```

Sets the field called 'Department' with multiple terms by ID and label. You can refer to those terms with the {ID:label} token.

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

### -InternalFieldName
The internal name of the field

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Label
The Label value of the term

```yaml
Type: String
Parameter Sets: ITEM

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListItem
The list item to set the field value to

```yaml
Type: ListItem
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TermId
The Id of the Term

```yaml
Type: Guid
Parameter Sets: ITEM

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TermPath
A path in the form of GROUPLABEL|TERMSETLABEL|TERMLABEL

```yaml
Type: String
Parameter Sets: PATH

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Terms
Allows you to specify terms with key value pairs that can be referred to in the template by means of the {id:label} token. See examples on how to use this parameter.

```yaml
Type: Hashtable
Parameter Sets: ITEMS

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

