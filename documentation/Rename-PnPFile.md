---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Rename-PnPFile.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Rename-PnPFile
---

# Rename-PnPFile

## SYNOPSIS

Renames a file in its current location

## SYNTAX

### SERVER

```
Rename-PnPFile [-ServerRelativeUrl] <String> [-TargetFileName] <String> [-OverwriteIfAlreadyExists]
 [-Force] [-Connection <PnPConnection>]
```

### SITE

```
Rename-PnPFile [-SiteRelativeUrl] <String> [-TargetFileName] <String> [-OverwriteIfAlreadyExists]
 [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to rename a file.

## EXAMPLES

### EXAMPLE 1

```powershell
Rename-PnPFile -ServerRelativeUrl /sites/project/Documents/company.docx -TargetFileName mycompany.docx
```

Renames a file named company.docx located in the document library called Documents located in the projects sitecollection under the managed path sites to mycompany.docx. If a file named mycompany.aspx already exists, it won't perform the rename.

### EXAMPLE 2

```powershell
Rename-PnPFile -SiteRelativeUrl Documents/company.aspx -TargetFileName mycompany.docx
```

Renames a file named company.docx located in the document library called Documents located in the current site to mycompany.aspx. If a file named mycompany.aspx already exists, it won't perform the rename.

### EXAMPLE 3

```powershell
Rename-PnPFile -ServerRelativeUrl /sites/project/Documents/company.docx -TargetFileName mycompany.docx -OverwriteIfAlreadyExists
```

Renames a file named company.docx located in the document library called Documents located in the projects sitecollection under the managed path sites to mycompany.aspx. If a file named mycompany.aspx already exists, it will still perform the rename and replace the original mycompany.aspx file.

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

### -Force

If provided, no confirmation will be requested and the action will be performed

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

### -OverwriteIfAlreadyExists

If provided, if a file already exist with the provided TargetFileName, it will be overwritten. If omitted, the rename operation will be canceled if a file already exists with the TargetFileName file name.

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

### -ServerRelativeUrl

Server relative Url specifying the file to rename. Must include the file name.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: SERVER
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SiteRelativeUrl

Site relative Url specifying the file to rename. Must include the file name.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: SITE
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TargetFileName

File name to rename the file to. Should only be the file name and not include the path to its location. Use Move-PnPFile to move the file to another location.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 1
  IsRequired: true
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
