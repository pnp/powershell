---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPFolder.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPFolder
---

# Get-PnPFolder

## SYNOPSIS

Returns a folder object

## SYNTAX

### Folders in current Web (Default)

```
Get-PnPFolder [-Includes <String[]>] [-Connection <PnPConnection>] [-Verbose] [<CommonParameters>]
```

### Root folder of the current Web

```
Get-PnPFolder -CurrentWebRootFolder [-Includes <String[]>] [-Connection <PnPConnection>] [-Verbose]
 [<CommonParameters>]
```

### Folder by url

```
Get-PnPFolder -Url <String> [-Includes <String[]>] [-AsListItem <SwitchParameter>]
 [-Connection <PnPConnection>] [-Verbose] [<CommonParameters>]
```

### Root folder of a list

```
Get-PnPFolder -ListRootFolder <ListPipeBind> [-Includes <String[]>] [-AsListItem <SwitchParameter>]
 [-Connection <PnPConnection>] [-Verbose] [<CommonParameters>]
```

### Folders In List

```
Get-PnPFolder -List <ListPipeBind> [-Includes <String[]>] [-Connection <PnPConnection>] [-Verbose]
 [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Retrieves the folder instance of the specified location.

Use [Resolve-PnPFolder](Resolve-PnPFolder.md) to create the folder if it does not exist.
Use [Get-PnPFolderInFolder](Get-PnPFolderInFolder.md) to retrieve subfolders.
Use [Get-PnPFileInFolder](Get-PnPFileInFolder.md) to retrieve files in a folder.
Use [Get-PnPFolderItem](Get-PnPFolderItem.md) to retrieve files and subfolders.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPFolder
```

Returns all the folders located in the root of the current web

### EXAMPLE 2

```powershell
Get-PnPFolder -CurrentWebRootFolder
```

Returns the folder instance representing the root of the current web

### EXAMPLE 3

```powershell
Get-PnPFolder -Url "Shared Documents"
```

Returns the folder called 'Shared Documents' which is located in the root of the current web

### EXAMPLE 4

```powershell
Get-PnPFolder -Url "/sites/demo/Shared Documents"
```

Returns the folder called 'Shared Documents' which is located in the root of the site collection located at '/sites/demo'

### EXAMPLE 5

```powershell
Get-PnPFolder -ListRootFolder "Shared Documents"
```

Returns the root folder of the list called 'Shared Documents'

### EXAMPLE 6

```powershell
Get-PnPFolder -List "Shared Documents"
```

Returns the folders inside the root folder of the list called 'Shared Documents'. Please use Get-PnPFolder -ListRootFolder \<folder\> | Get-PnPFolderInFolder instead.

### EXAMPLE 7

```powershell
Get-PnPFolder -Url "/sites/demo/Shared Documents/Test" -AsListItem
```

Returns the folder called 'Test' which is located in the root of the site collection located at '/sites/demo' inside 'Shared Documents' document library as a SharePoint list item.

## PARAMETERS

### -AsListItem

Returns the folder as a listitem showing all its properties

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Folder by url
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Root folder of a list
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

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

### -CurrentWebRootFolder

If provided, the folder representing the root of the current web will be returned

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Root folder of the current Web
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Includes

Optionally allows properties to be retrieved for the returned folders which are not included in the response by default

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

### -List

Name, ID or instance of a list or document library to retrieve the folders residing in it for. Please use Get-PnPFolder -ListRootFolder <folder> | Get-PnPFolderInFolder instead.

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Folders In List
  Position: 0
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ListRootFolder

Name, ID or instance of a list or document library to retrieve the rootfolder of.

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Root folder of a list
  Position: 0
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Url

Site or server relative URL of the folder to retrieve. In the case of a server relative url, make sure that the url starts with the managed path as the current web.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- RelativeUrl
ParameterSets:
- Name: Folder By Url
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
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
