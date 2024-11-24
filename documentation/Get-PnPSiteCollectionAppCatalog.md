---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteCollectionAppCatalog.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSiteCollectionAppCatalog
---

# Get-PnPSiteCollectionAppCatalog

## SYNOPSIS

Returns all site collection scoped app catalogs that exist on the tenant

## SYNTAX

### Default (Default)

```
Get-PnPSiteCollectionAppCatalog [-CurrentSite <SwitchParameter>]
 [-ExcludeDeletedSites <SwitchParameter>] [-SkipUrlValidation <SwitchParameter>]
 [-Connection <PnPConnection>] [-Verbose] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Returns all the site collection scoped app catalogs that exist on the tenant

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSiteCollectionAppCatalog
```
Will return all the site collection app catalogs that exist on the tenant, including those that may be in the tenant recycle bin

### EXAMPLE 2

```powershell
Get-PnPSiteCollectionAppCatalog -CurrentSite
```
Will return the site collection app catalog for the currently connected to site, if it has one. Otherwise it will yield no result.

### EXAMPLE 3

```powershell
Get-PnPSiteCollectionAppCatalog -ExcludeDeletedSites
```
Will return all the site collection app catalogs that exist on the tenant excluding the site collections having App Catalogs that are in the tenant recycle bin

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

### -CurrentSite

When provided, it will check if the currently connected to site has a site collection App Catalog and will return information on it. If the current site holds no site collection App Catalog, an empty response will be returned.

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

### -ExcludeDeletedSites

When provided, all site collections having site collection App Catalogs but residing in the tenant recycle bin, will be excluded

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

### -SkipUrlValidation

When provided, the site collection app catalog Urls will not be validated for if they have been renamed since their creation. This makes the cmdlet a lot faster, but it could also lead to URLs being returned that no longer exist. If not provided, for each site collection app catalog, it will look up the actual URL of the site collection app catalog and return that instead of the URL that was used when the site collection app catalog was created.

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

### -Verbose

When provided, additional debug statements will be shown while executing the cmdlet.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
