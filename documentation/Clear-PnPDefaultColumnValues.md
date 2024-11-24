---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Clear-PnPDefaultColumnValues.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Clear-PnPDefaultColumnValues
---

# Clear-PnPDefaultColumnValues

## SYNOPSIS

Clear default column values for a document library

## SYNTAX

### Default (Default)

```
Clear-PnPDefaultColumnValues [-List] <ListPipeBind> -Field <FieldPipeBind> [-Folder <String>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

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

### -Field

The internal name, id or a reference to a field

```yaml
Type: FieldPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Folder

A library relative folder path, if not specified it will set the default column values on the root folder of the library ('/')

```yaml
Type: String
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

### -List

The ID, Name or Url of the list.

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
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
