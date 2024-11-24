---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Read-PnPTenantTemplate.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Read-PnPTenantTemplate
---

# Read-PnPTenantTemplate

## SYNOPSIS

Loads/Reads a PnP tenant template from the file system and returns an in-memory instance of this template.

## SYNTAX

### By Path (default) (Default)

```
Read-PnPTenantTemplate -Path <String>
```

### By Stream

```
Read-PnPTenantTemplate -Stream <Stream>
```

### By XML

```
Read-PnPTenantTemplate -Xml <String>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to load a PnP tenant template from the file system, from a stream or from a string to memory and return its instance object.

## EXAMPLES

### EXAMPLE 1

```powershell
Read-PnPTenantTemplate -Path template.pnp
```

Reads a PnP tenant template file from the file system and returns an in-memory instance

### EXAMPLE 2

```powershell
$template = Get-PnPFile "/sites/config/Templates/Default.xml" -AsMemoryStream
Read-PnPTenantTemplate -Stream $template
```

Downloads a PnP Tenant template from the provided location into memory and parses its contents into a TenantTemplate instance which can then be modified and passed on to the Apply-PnPTenantTemplate cmdlet without needing to write anything to disk

## PARAMETERS

### -Path

Filename to read from, optionally including full path.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Path
  Position: 0
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Stream

A stream containing the PnP tenant template.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Stream
  Position: 0
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Xml

A string containing the XML of the PnP tenant template.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By XML
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
