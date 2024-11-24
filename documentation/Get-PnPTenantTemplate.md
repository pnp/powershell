---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantTemplate.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTenantTemplate
---

# Get-PnPTenantTemplate

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Generates a provisioning tenant template from a site. If the site is a hubsite any connected site will be included.

## SYNTAX

### Extract a template to a file

```
Get-PnPTenantTemplate [-SiteUrl <String>] [-Out <String>] [-Force]
 [-Configuration <ExtractConfigurationPipeBind>] [-Connection <PnPConnection>]
```

### Extract a template as an object

```
Get-PnPTenantTemplate [-SiteUrl <String>] [-AsInstance]
 [-Configuration <ExtractConfigurationPipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to generate a provisioning tenant template from a site. If the site is a hubsite any connected site will be included.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTenantTemplate -Out tenanttemplate.xml
```

Extracts a tenant template of the currently connected to site.

### EXAMPLE 2

```powershell
Get-PnPTenantTemplate -Out tenanttemplate.xml -SiteUrl https://m365x123456.sharepoint.com/sites/HomeSite
```

Extracts a tenant template for the site https://m365x123456.sharepoint.com/sites/HomeSite and places the schema XML into the file "tenanttemplate.xml".

### EXAMPLE 3

```powershell
Get-PnPTenantTemplate -Out tenanttemplate.xml -SiteUrl https://m365x123456.sharepoint.com/sites/HomeSite -Force
```

Extracts a tenant template for the site https://m365x123456.sharepoint.com/sites/HomeSite and places the schema XML into the file "tenanttemplate.xml". The xml file will be overwritten if it already exists.

## PARAMETERS

### -AsInstance

Returns the template as an in-memory object, which is an instance of the ProvisioningHierarchy type of the PnP Core Component. It cannot be used together with the -Out parameter.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Extract a template as an object
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Configuration

Specify a JSON configuration file to configure the extraction progress.

```yaml
Type: ExtractConfigurationPipeBind
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

### -Force

Overwrites the output file if it exists.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Extract a template to a file
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
- Name: Extract a template to a file
  Position: 0
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SiteUrl

The URL of the site collection to create a tenant template out of. If omitted, the currently connected to site will be used.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
