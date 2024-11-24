---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPFileInFolder.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPFileInFolder
---

# Get-PnPFileInFolder

## SYNOPSIS

List files in a folder

## SYNTAX

### Folder via url

```
Get-PnPFileInFolder [-FolderSiteRelativeUrl <String>] [-ItemName <String>] [-Recurse]
 [-Includes <String[]>] [-ExcludeSystemFolders] [-Verbose] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### Folder via pipebind

```
Get-PnPFileInFolder [-Identity <FolderPipeBind>] [-ItemName <String>] [-Recurse]
 [-Includes <String[]>] [-ExcludeSystemFolders] [-Verbose] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows listing of all the files in a folder. It can optionally also list all files in the underlying subfolders.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPFileInFolder
```

Returns all the files in the root of the current web

### EXAMPLE 2

```powershell
Get-PnPFileInFolder -Recurse
```

Returns all the files in the entire site. This will take a while to complete and will cause a lot of calls to be made towards SharePoint Online. Use it wisely.

### EXAMPLE 3

```powershell
Get-PnPFileInFolder -Identity "Shared Documents"
```

Returns the files located in the 'Shared Documents' folder located in the root of the current web

### EXAMPLE 4

```powershell
Get-PnPFileInFolder -FolderSiteRelativeUrl "SitePages" -ItemName "Default.aspx"
```

Returns the file 'Default.aspx' which is located in the folder SitePages which is located in the root of the current web

### EXAMPLE 5

```powershell
Get-PnPFolder -Identity "Shared Documents" | Get-PnPFileInFolder
```

Returns all files in the "Shared Documents" folder which is located in the root of the current web

### EXAMPLE 6

```powershell
Get-PnPFileInFolder -FolderSiteRelativeUrl "SitePages" -Recurse
```

Returns all files, including those located in any subfolders, in the folder SitePages which is located in the root of the current web

### EXAMPLE 7

```powershell
Get-PnPFolder -List "Documents" | Get-PnPFileInFolder -Recurse -ExcludeSystemFolders
```

Returns all files, including those located in any subfolders, located in the Documents document library where the files in system folders are excluded

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

### -ExcludeSystemFolders

When provided, all files in system folders will be excluded from the output. This parameter is not supported when not providing a folder through -Identity or -FolderSiteRelativeUrl.

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

### -Includes

Optionally allows properties to be retrieved for the returned files which are not included in the response by default

```yaml
Type: String[]
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

### -ItemName

Name of the file to retrieve (not case sensitive)

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

### -Recurse

A switch parameter to include files of all subfolders in the specified folder

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
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
