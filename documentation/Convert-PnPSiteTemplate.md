---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Convert-PnPSiteTemplate.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Convert-PnPSiteTemplate
---

# Convert-PnPSiteTemplate

## SYNOPSIS

Converts a provisioning template to an other schema version

## SYNTAX

### Default (Default)

```
Convert-PnPSiteTemplate [-Path] <String> [[-ToSchema] <XMLPnPSchemaVersion>] [-Out <String>]
 [-Encoding <Encoding>] [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to convert a provisioning template to an other schema version.

## EXAMPLES

### EXAMPLE 1

```powershell
Convert-PnPSiteTemplate -Path template.xml
```

Converts a provisioning template to the latest schema and outputs the result to current console.

### EXAMPLE 2

```powershell
Convert-PnPSiteTemplate -Path template.xml -Out newtemplate.xml
```

Converts a provisioning template to the latest schema and outputs the result the newtemplate.xml file.

### EXAMPLE 3

```powershell
Convert-PnPSiteTemplate -Path template.xml -Out newtemplate.xml -ToSchema V201512
```

Converts a provisioning template to the latest schema using the 201512 schema and outputs the result the newtemplate.xml file.

## PARAMETERS

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

### -Force

Overwrites the output file if it exists

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

Filename to write to, optionally including full path

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

### -Path

Path to the xml file containing the site template

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
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ToSchema

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
- [Encoding documentation](https://learn.microsoft.com/dotnet/api/system.text.encoding?view=net-8.0)
