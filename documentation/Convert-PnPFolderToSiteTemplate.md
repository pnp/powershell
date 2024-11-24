---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Convert-PnPFolderToSiteTemplate.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Convert-PnPFolderToSiteTemplate
---

# Convert-PnPFolderToSiteTemplate

## SYNOPSIS

Creates a pnp package file of an existing template xml, and includes all files in the current folder

## SYNTAX

### Default (Default)

```
Convert-PnPFolderToSiteTemplate [-Out] <String> [[-Folder] <String>] [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to convert the current folder together with all files, to a pnp package file of and existing template xml.

## EXAMPLES

### EXAMPLE 1

```powershell
Convert-PnPFolderToSiteTemplate -Out template.pnp
```

Creates a pnp package file of an existing template xml, and includes all files in the current folder

### EXAMPLE 2

```powershell
Convert-PnPFolderToSiteTemplate -Out template.pnp -Folder c:\temp
```

Creates a pnp package file of an existing template xml, and includes all files in the c:\temp folder

## PARAMETERS

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
  Position: 1
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
