---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPFileFromSiteTemplate.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPFileFromSiteTemplate
---

# Remove-PnPFileFromSiteTemplate

## SYNOPSIS

Removes a file from a PnP Provisioning Template

## SYNTAX

### Default (Default)

```
Remove-PnPFileFromSiteTemplate [-Path] <String> [-FilePath] <String>
 [[-TemplateProviderExtensions] <ITemplateProviderExtension[]>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove a file from a PnP Provisioning Template.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPFileFromSiteTemplate -Path template.pnp -FilePath filePath
```

Removes a file from an in-memory PnP Provisioning Template

## PARAMETERS

### -FilePath

The relative File Path of the file to remove from the in-memory template

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

### -Path

Filename to read the template from, optionally including full path.

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

### -TemplateProviderExtensions

Allows you to specify ITemplateProviderExtension to execute while saving the template.

```yaml
Type: ITemplateProviderExtension[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 2
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
