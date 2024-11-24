---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPSiteTemplate.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPSiteTemplate
---

# Add-PnPSiteTemplate

## SYNOPSIS

Adds a PnP Site Template object to a tenant template

## SYNTAX

### Default (Default)

```
Add-PnPSiteTemplate -SiteTemplate <SiteTemplate> -TenantTemplate <ProvisioningHierarchy>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add PnP Site Template object to a tenant template.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPSiteTemplate -TenantTemplate $tenanttemplate -SiteTemplate $sitetemplate
```

Adds an existing site template to an existing tenant template object

## PARAMETERS

### -SiteTemplate

The template to add to the tenant template

```yaml
Type: SiteTemplate
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

### -TenantTemplate

The tenant template to add the template to

```yaml
Type: ProvisioningHierarchy
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
