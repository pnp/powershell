---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Convert-PnPSiteTemplateToMarkdown.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Convert-PnPSiteTemplateToMarkdown
---

# Convert-PnPSiteTemplateToMarkdown

## SYNOPSIS

Converts an existing PnP Site Template to a markdown report

## SYNTAX

### Default (Default)

```
Convert-PnPSiteTemplateToMarkdown -TemplatePath <String> [-Out <String>] [-Force <SwitchParameter>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Converts an existing PnP Site Template to markdown report. Notice that this cmdlet is work in work progress, and the completeness of the report will increase in the future.

## EXAMPLES

### EXAMPLE 1

```powershell
Convert-PnPSiteTemplateToMarkdown -TemplatePath ./mytemplate.xml
```

This will convert the site template to a markdown file and outputs the result to the console.

### EXAMPLE 2

```powershell
Convert-PnPSiteTemplateToMarkdown -TemplatePath ./mytemplate.xml -Out ./myreport.md
```

This will convert the site template to a markdown file and writes the result to the specified myreport.md file.

## PARAMETERS

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

The output file name to write the report to in markdown format.

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

### -TemplatePath

The path to an existing PnP Site Template

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
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
