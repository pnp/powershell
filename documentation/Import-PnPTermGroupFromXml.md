---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Import-PnPTermGroupFromXml.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Import-PnPTermGroupFromXml
---

# Import-PnPTermGroupFromXml

## SYNOPSIS

Imports a taxonomy TermGroup from either the input or from an XML file.

## SYNTAX

### XML

```
Import-PnPTermGroupFromXml [[-Xml] <String>] [-Connection <PnPConnection>]
```

### File

```
Import-PnPTermGroupFromXml [-Path <String>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

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
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Path

The XML File to import the data from

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: File
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Xml

The XML to process

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: XML
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
