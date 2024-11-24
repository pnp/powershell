---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Save-PnPTenantTemplate.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Save-PnPTenantTemplate
---

# Save-PnPTenantTemplate

## SYNOPSIS

Saves a PnP provisioning hierarchy to the file system.

## SYNTAX

### Default (Default)

```
Save-PnPTenantTemplate [-Out] <String> -Template <ProvisioningHierarchyPipeBind>
 [-Schema <XMLPnPSchemaVersion>] [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet saves the tenant template for provisioning hierarchy to the file system in the latest or specified schema.

## EXAMPLES

### EXAMPLE 1

```powershell
Save-PnPTenantTemplate -Template template.xml -Out .\tenanttemplate.pnp
```

Saves a PnP tenant template to the file system.

### EXAMPLE 2

```powershell
$template = Read-PnPTenantTemplate -Path template.xml
Save-PnPTenantTemplate -Template $template -Out .\template.pnp
```

Saves a PnP tenant template to the file system as a PnP file. The schema used will be the latest released schema when creating the PnP file regardless of the original schema.

### EXAMPLE 3

```powershell
$template = Read-PnPTenantTemplate -Path template.xml
Save-PnPTenantTemplate -Template $template -Out .\template.pnp -Schema V202002
```

Saves a PnP tenant template to the file system as a PnP file and converts the template in the PnP file to the specified schema.

### EXAMPLE 4

```powershell
Read-PnPTenantTemplate -Path template.xml | Save-PnPTenantTemplate -Out .\template.pnp
```

Saves a PnP tenant template to the file system as a PnP file.

## PARAMETERS

### -Force

Specifying the Force parameter will skip the confirmation question.

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

### -Schema

The optional schema to use when creating the PnP file. Always defaults to the latest schema.

```yaml
Type: XMLPnPSchemaVersion
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

### -Template

Allows you to provide an in-memory instance of a Tenant Template or a filename of a template file in XML format. When using this parameter, the -Out parameter refers to the path for saving the template and storing any supporting file for the template.

```yaml
Type: ProvisioningHierarchyPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
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
