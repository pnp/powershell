---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPFolderItem.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPFolderItem
---

# Get-PnPFolderItem

## SYNOPSIS

List files and/or subfolders in a folder

## SYNTAX

### Folder via url

```
Get-PnPFolderItem [-FolderSiteRelativeUrl <String>] [-ItemType <String>] [-ItemName <String>]
 [-Recursive] [-Verbose] [-Connection <PnPConnection>] [<CommonParameters>]
```

### Folder via pipebind

```
Get-PnPFolderItem [-Identity <FolderPipeBind>] [-ItemType <String>] [-ItemName <String>]
 [-Recursive] [-Verbose] [-Connection <PnPConnection>] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows listing of all the content in a folder. It can be used to list all files and folders in a folder and optionally all its subfolders.

Use [Get-PnPFileInFolder](Get-PnPFileInFolder.md) to retrieve only files and [Get-PnPFolderInFolder](Get-PnPFolderInFolder.md) to retrieve only folders allowing additional properties of the returned items to be requested.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPFolderItem
```

Returns all the files and folders in the root of the current web

### EXAMPLE 2

```powershell
Get-PnPFolderItem -Recurse
```

Returns all the files and folders in the entire site. This will take a while to complete and will cause a lot of calls to be made towards SharePoint Online. Use it wisely.

### EXAMPLE 3

```powershell
Get-PnPFolderItem -Identity "Shared Documents"
```

Returns the files and folders located in the 'Shared Documents' folder located in the root of the current web

### EXAMPLE 4

```powershell
Get-PnPFolderItem -FolderSiteRelativeUrl "SitePages" -ItemName "Default.aspx"
```

Returns the file 'Default.aspx' which is located in the folder SitePages which is located in the root of the current web

### EXAMPLE 5

```powershell
Get-PnPFolderItem -FolderSiteRelativeUrl "SitePages" -ItemType Folder
```

Returns all subfolders of the folder SitePages which is located in the root of the current web

### EXAMPLE 6

```powershell
Get-PnPFolder -Url "Shared Documents" | Get-PnPFolderItem -ItemType File
```

Returns all files in the "Shared Documents" folder which is located in the root of the current web

### EXAMPLE 7

```powershell
Get-PnPFolderItem -FolderSiteRelativeUrl "SitePages" -Recursive
```

Returns all files and folders, including contents of any subfolders, in the folder SitePages which is located in the root of the current web

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection

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

### -FolderSiteRelativeUrl

The site relative URL of the folder to retrieve

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Folder via url
  Position: 0
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Identity

A folder instance to the folder to retrieve

```yaml
Type: FolderPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Folder via pipebind
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ItemName

Name of the item to retrieve (not case sensitive)

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

### -ItemType

The type of contents to retrieve, either File, Folder or All (default)

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
AcceptedValues:
- Folder
- File
- All
HelpMessage: ''
```

### -Recursive

A switch parameter to include contents of all subfolders in the specified folder

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Recurse
ParameterSets:
- Name: (All)
  Position: 4
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Verbose

When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
