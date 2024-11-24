---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Register-PnPAppCatalogSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Register-PnPAppCatalogSite
---

# Register-PnPAppCatalogSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Creates a new App Catalog Site and sets this site as the Tenant App Catalog

## SYNTAX

### Default (Default)

```
Register-PnPAppCatalogSite -Url <String> -Owner <String> -TimeZoneId <Int32> [-Force]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to create a new App Catalog Site and sets this site as the Tenant App Catalog.

## EXAMPLES

### EXAMPLE 1

```powershell
Register-PnPAppCatalogSite -Url "https://yourtenant.sharepoint.com/sites/appcatalog" -Owner admin@domain.com -TimeZoneId 4
```

This will create a new appcatalog site if no app catalog is already present. Use -Force to create a new appcatalog site if one has already been registered. If using the same URL as an existing one and Force is present, the current/existing appcatalog site will be deleted.

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

If specified, and an app catalog is already present, a new app catalog site will be created. If the same URL is used the existing/current app catalog site will be deleted first.

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

### -Owner

The login account of the user designated to be the admin for the site, e.g. user@domain.com

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

### -TimeZoneId

Use Get-PnPTimeZoneId to retrieve possible timezone values

```yaml
Type: Int32
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

### -Url

The full url of the app catalog site to be created, e.g. https://yourtenant.sharepoint.com/sites/appcatalog

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
