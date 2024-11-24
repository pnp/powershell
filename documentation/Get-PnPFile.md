---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPFile.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPFile
---

# Get-PnPFile

## SYNOPSIS

Downloads a file

## SYNTAX

### Return as file object (Default)

```
Get-PnPFile -Url <String> -AsFileObject [-Connection <PnPConnection>]
```

### Return as list item

```
Get-PnPFile -Url <String> -AsListItem [-ThrowExceptionIfFileNotFound] [-Connection <PnPConnection>]
```

### Save to local path

```
Get-PnPFile -Url <String> -AsFile -Path <String> -Filename <String> [-Force]
 [-Connection <PnPConnection>]
```

### Return as string

```
Get-PnPFile -Url <String> -AsString [-Connection <PnPConnection>]
```

### Return as memorystream

```
Get-PnPFile -Url <String> -AsMemoryStream [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows downloading of a file from SharePoint Online. The file contents can either be read directly into memory as text, directly saved to local disk or stored in memory for further processing.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPFile -Url "/sites/project/Shared Documents/Document.docx"
```

Retrieves the file and downloads it to the current folder

### EXAMPLE 2

```powershell
Get-PnPFile -Url "https://contoso.sharepoint.com/sites/project/Shared Documents/Document.docx"
```

Retrieves the file and downloads it to the current folder

### EXAMPLE 3

```powershell
Get-PnPFile -Url /sites/project/SiteAssets/image.jpg -Path c:\temp -FileName image.jpg -AsFile
```

Retrieves the file and downloads it to c:\temp\image.jpg

### EXAMPLE 4

```powershell
Get-PnPFile -Url /sites/project/_catalogs/themes/15/company.spcolor -AsString
```

Retrieves the contents of the file as text and outputs its contents to the console

### EXAMPLE 5

```powershell
Get-PnPFile -Url /sites/project/Shared Documents/Folder/Presentation.pptx -AsFileObject
```

Retrieves the file and returns it as a File object

### EXAMPLE 6

```powershell
Get-PnPFile -Url /sites/project/_catalogs/themes/15/company.spcolor -AsListItem
```

Retrieves the file and returns it as a ListItem object

### EXAMPLE 7

```powershell
Get-PnPFile -Url /personal/john_tenant_onmicrosoft_com/Documents/Sample.xlsx -Path c:\temp -FileName Project.xlsx -AsFile
```

Retrieves the file Sample.xlsx by its site relative URL from a OneDrive for Business site and downloads it to c:\temp\Project.xlsx

### EXAMPLE 8

```powershell
Get-PnPFile -Url "/sites/templates/Shared Documents/HR Site.pnp" -AsMemoryStream
```

Retrieves the file in memory for further processing

## PARAMETERS

### -AsFile



```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Save to local path
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -AsFileObject

Retrieve the file contents as a file object.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Return as file object
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -AsListItem

Returns the file as a listitem showing all its properties

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Return as list item
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -AsMemoryStream



```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Download the content of the file to memory
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -AsString

Retrieve the file contents as a string

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Return as string
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

### -Filename

Name for the local file

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Save to local path
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Force

Overwrites the file if it exists.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Save to local path
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

Local path where the file should be saved

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Save to local path
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ThrowExceptionIfFileNotFound

If provided in combination with -AsListItem, a System.ArgumentException will be thrown if the file specified in the -Url argument does not exist. Otherwise it will return nothing instead.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Return as list item
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Url

The URL (server or site relative) to the file

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- ServerRelativeUrl
- SiteRelativeUrl
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
