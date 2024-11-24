---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPFileToSiteTemplate.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPFileToSiteTemplate
---

# Add-PnPFileToSiteTemplate

## SYNOPSIS

Adds a file to a PnP Provisioning Template package

## SYNTAX

### Local File

```
Add-PnPFileToSiteTemplate [-Path] <String> [-Source] <String> [-Folder] <String>
 [[-Container] <String>] [[-FileLevel] <FileLevel>]
 [[-TemplateProviderExtensions] <ITemplateProviderExtension[]>] [-FileOverwrite]
 [-Connection <PnPConnection>]
```

### Remove File

```
Add-PnPFileToSiteTemplate [-Path] <String> [-SourceUrl] <String> [[-Container] <String>]
 [[-FileLevel] <FileLevel>] [[-TemplateProviderExtensions] <ITemplateProviderExtension[]>]
 [-FileOverwrite] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows adding a file to a PnP Provisioning Template package (.pnp) so that the file will get uploaded to the SharePoint Online site to which the template is being invoked. This allows the file to be referenced in i.e. a document template, site logo or any other component that references a file.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPFileToSiteTemplate -Path template.pnp -Source "Instructions.docx" -Folder "Shared Documents"
```

Embeds a file named "Instructions.docx" located in the current folder on the local machine into the PnP Site Template file "template.pnp" located in the current folder on the local machine, instructing it to be uploaded to the default document library when the template is applied to a site.

### EXAMPLE 2

```powershell
Add-PnPFileToSiteTemplate -Path c:\temp\template.pnp -Source "c:\temp\Sample.pptx" -Folder "Shared Documents\Samples"
```

Embeds a file named "Sample.pptx" located in the c:\temp on the local machine into the PnP Site Template file located at "c:\temp\template.pnp" on the local machine, instructing it to be uploaded to the folder Samples located in the default document library when the template is applied to a site.

### EXAMPLE 3

```powershell
Add-PnPFileToSiteTemplate -Path template.pnp -Source "./myfile.png" -Folder "folderinsite" -FileLevel Published -FileOverwrite:$false
```

Adds a file to a PnP Site Template, specifies the level as Published and defines to not overwrite the file if it exists in the site.

### EXAMPLE 4

```powershell
Add-PnPFileToSiteTemplate -Path template.pnp -Source $sourceFilePath -Folder $targetFolder -Container $container
```

Adds a file to a PnP Site Template with a custom container for the file

### EXAMPLE 5

```powershell
Add-PnPFileToSiteTemplate -Path template.pnp -SourceUrl "Shared%20Documents/ProjectStatus.docx"
```

Adds a file to a PnP Provisioning Template retrieved from the currently connected site. The url can be server relative or web relative. If specifying a server relative url has to start with the current site url.

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

### -Container

The target Container for the file to add to the in-memory template, optional argument.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 3
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -FileLevel

The level of the files to add, defaults to Published.

```yaml
Type: FileLevel
DefaultValue: Published
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

### -FileOverwrite

Set to overwrite in site, defaults to true.

```yaml
Type: SwitchParameter
DefaultValue: $true
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 5
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Folder

The target Folder for the file to add to the in-memory template.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Local File
  Position: 2
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Path

Filename of the .PNP Open XML site template to read from, optionally including full path.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Source

The file to add to the PnP Provisioning Template, optionally including full path.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Local File
  Position: 1
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SourceUrl

The file to add to the PnP Provisioning Template, specifying its url in the current connected Web.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Remove File
  Position: 1
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TemplateProviderExtensions

Allows you to specify ITemplateProviderExtension to execute while loading the template.

```yaml
Type: ITemplateProviderExtension[]
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
