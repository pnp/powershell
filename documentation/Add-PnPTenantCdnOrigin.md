---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPTenantCdnOrigin.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPTenantCdnOrigin
---

# Add-PnPTenantCdnOrigin

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Adds a new origin to the public or private content delivery network (CDN).

## SYNTAX

### Default (Default)

```
Add-PnPTenantCdnOrigin -OriginUrl <String> -CdnType <SPOTenantCdnType> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Add a new origin to the public or private CDN, on either Tenant level or on a single Site level. Effectively, a tenant admin points out to a document library, or a folder in the document library and requests that content in that library should be retrievable by using a CDN.

You must be a SharePoint Online Administrator and a site collection administrator to run the cmdlet.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPTenantCdnOrigin -OriginUrl /sites/site/subfolder -CdnType Public
```

This example configures a public CDN on site level.

## PARAMETERS

### -CdnType

Specifies the CDN type. The valid values are: public or private.

```yaml
Type: SPOTenantCdnType
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
AcceptedValues:
- Public
- Private
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

### -OriginUrl

Specifies a path to the doc library to be configured. It can be provided in two ways: relative path, or a mask.

Relative path depends on the OriginScope. If the originScope is Tenant, a path must be a relative path under the tenant root. If the originScope is Site, a path must be a relative path under the given Site. The path must point to the valid Document Library or a folder with a document library.

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
