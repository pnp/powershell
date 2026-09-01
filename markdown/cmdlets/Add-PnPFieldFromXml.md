---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPFieldFromXml.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPFieldFromXml
---
  
# Add-PnPFieldFromXml

## SYNOPSIS
Adds a field to a list or as a site column based upon a CAML/XML field definition

## SYNTAX

```powershell
Add-PnPFieldFromXml [-List <ListPipeBind>] [-FieldXml] <String> 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add new field by specifying its definition in CAML/XML format to list or site columns.

## EXAMPLES

### EXAMPLE 1
```powershell
$xml = '<Field Type="Text" Name="PSCmdletTest" DisplayName="PSCmdletTest" ID="{27d81055-f208-41c9-a976-61c5473eed4a}" Group="Test" Required="FALSE" StaticName="PSCmdletTest" />'
Add-PnPFieldFromXml -FieldXml $xml
```

Adds a field with the specified field CAML code to the site.

### EXAMPLE 2
```powershell
$xml = '<Field Type="Text" Name="PSCmdletTest" DisplayName="PSCmdletTest" ID="{27d81055-f208-41c9-a976-61c5473eed4a}" Group="Test" Required="FALSE" StaticName="PSCmdletTest" />'
Add-PnPFieldFromXml -List "Demo List" -FieldXml $xml
```

Adds a field with the specified field CAML code to the list "Demo List".

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

### -FieldXml
CAML snippet containing the field definition. See https://learn.microsoft.com/sharepoint/dev/schema/field-element-list

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The name of the list, its ID or an actual list object where this field needs to be added

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
[Field CAML documentation](https://learn.microsoft.com/sharepoint/dev/schema/field-element-list)