---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPSiteTemplateFromFolder.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPSiteTemplateFromFolder
---

# New-PnPSiteTemplateFromFolder

## SYNOPSIS

Generates a provisioning template from a given folder, including only files that are present in that folder

## SYNTAX

### Default (Default)

```
New-PnPSiteTemplateFromFolder [[-Out] <String>] [[-Folder] <String>] [[-TargetFolder] <String>]
 [[-Schema] <XMLPnPSchemaVersion>] [-Match <String>] [-ContentType <ContentTypePipeBind>]
 [-Properties <Hashtable>] [-AsIncludeFile] [-Force] [-Encoding <Encoding>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to create a new provisioning site template based on a given folder, including files present in it.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPSiteTemplateFromFolder -Out template.xml
```

Creates an empty provisioning template, and includes all files in the current folder.

### EXAMPLE 2

```powershell
New-PnPSiteTemplateFromFolder -Out template.xml -Folder c:\temp
```

Creates an empty provisioning template, and includes all files in the c:\temp folder.

### EXAMPLE 3

```powershell
New-PnPSiteTemplateFromFolder -Out template.xml -Folder c:\temp -Match *.js
```

Creates an empty provisioning template, and includes all files with a JS extension in the c:\temp folder.

### EXAMPLE 4

```powershell
New-PnPSiteTemplateFromFolder -Out template.xml -Folder c:\temp -Match *.js -TargetFolder "Shared Documents"
```

Creates an empty provisioning template, and includes all files with a JS extension in the c:\temp folder and marks the files in the template to be added to the 'Shared Documents' folder

### EXAMPLE 5

```powershell
New-PnPSiteTemplateFromFolder -Out template.xml -Folder c:\temp -Match *.js -TargetFolder "Shared Documents" -ContentType "Test Content Type"
```

Creates an empty provisioning template, and includes all files with a JS extension in the c:\temp folder and marks the files in the template to be added to the 'Shared Documents' folder. It will add a property to the item for the content type.

### EXAMPLE 6

```powershell
New-PnPSiteTemplateFromFolder -Out template.xml -Folder c:\temp -Match *.js -TargetFolder "Shared Documents" -Properties @{"Title" = "Test Title"; "Category"="Test Category"}
```

Creates an empty provisioning template, and includes all files with a JS extension in the c:\temp folder and marks the files in the template to be added to the 'Shared Documents' folder. It will add the specified properties to the file entries.

### EXAMPLE 7

```powershell
New-PnPSiteTemplateFromFolder -Out template.pnp
```

Creates an empty provisioning template as a pnp package file, and includes all files in the current folder

### EXAMPLE 8

```powershell
New-PnPSiteTemplateFromFolder -Out template.pnp -Folder c:\temp
```

Creates an empty provisioning template as a pnp package file, and includes all files in the c:\temp folder

## PARAMETERS

### -AsIncludeFile

If specified, the output will only contain the &lt;pnp:Files&gt; element. This allows the output to be included in another template.

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

### -ContentType

An optional content type to use.

```yaml
Type: ContentTypePipeBind
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

### -Encoding

The encoding type of the XML file, Unicode is default

```yaml
Type: Encoding
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

Folder to process. If not specified the current folder will be used.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Force

Overwrites the output file if it exists.

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

### -Match

Optional wildcard pattern to match filenames against. If empty all files will be included.

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

### -Out

Filename to write to, optionally including full path.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Properties

Additional properties to set for every file entry in the generated template.

```yaml
Type: Hashtable
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

### -Schema

The schema of the output to use, defaults to the latest schema

```yaml
Type: XMLPnPSchemaVersion
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 1
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- LATEST
- V201503
- V201505
- V201508
- V201512
- V201605
- V201705
- V201801
- V201805
- V201807
- V201903
- V201909
- V202002
- V202103
- V202209
HelpMessage: ''
```

### -TargetFolder

Target folder to provision to files to. If not specified, the current folder name will be used.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 1
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
- [Encoding](https://learn.microsoft.com/dotnet/api/system.text.encoding)
