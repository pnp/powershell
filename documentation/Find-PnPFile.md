---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Find-PnPFile.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Find-PnPFile
---

# Find-PnPFile

## SYNOPSIS

Finds a file in the virtual file system of the web.

## SYNTAX

### Web (Default)

```
Find-PnPFile [-Match] <String> [-Connection <PnPConnection>]
```

### List

```
Find-PnPFile [-Match] <String> -List <ListPipeBind> [-Connection <PnPConnection>]
```

### Folder

```
Find-PnPFile [-Match] <String> -Folder <FolderPipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to find a file in the virtual file system of the web.

## EXAMPLES

### EXAMPLE 1

```powershell
Find-PnPFile -Match *.master
```

Will return all masterpages located in the current web.

### EXAMPLE 2

```powershell
Find-PnPFile -List "Documents" -Match *.pdf
```

Will return all pdf files located in given list.

### EXAMPLE 3

```powershell
Find-PnPFile -Folder "Shared Documents/Sub Folder" -Match *.docx
```

Will return all docx files located in given folder.

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

### -Folder

Folder object or relative url of a folder to query

```yaml
Type: FolderPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Folder
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -List

List title, url or an actual List object to query

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: List
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Match

Wildcard query using * (any number of charactes) and ? (single character)

```yaml
Type: String
DefaultValue: None
SupportsWildcards: true
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
