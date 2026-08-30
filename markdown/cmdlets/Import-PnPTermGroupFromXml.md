---
Module Name: PnP.PowerShell
title: Import-PnPTermGroupFromXml
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Import-PnPTermGroupFromXml.html
---
 
# Import-PnPTermGroupFromXml

## SYNOPSIS
Imports a taxonomy TermGroup from either the input or from an XML file.

## SYNTAX

### XML
```powershell
Import-PnPTermGroupFromXml [[-Xml] <String>] [-Connection <PnPConnection>]  
 
```

### File
```powershell
Import-PnPTermGroupFromXml [-Path <String>] [-Connection <PnPConnection>]  
 
```

## DESCRIPTION

Allows to import taxonomy term group from xml.

## EXAMPLES

### EXAMPLE 1
```powershell
Import-PnPTermGroupFromXml -Xml $xml
```

Imports the XML based termgroup information located in the $xml variable

### EXAMPLE 2
```powershell
Import-PnPTermGroupFromXml -Path input.xml
```

Imports the XML file specified by the path.

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

### -Path
The XML File to import the data from

```yaml
Type: String
Parameter Sets: File

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Xml
The XML to process

```yaml
Type: String
Parameter Sets: XML

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

