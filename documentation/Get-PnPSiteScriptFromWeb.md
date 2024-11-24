---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteScriptFromWeb.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSiteScriptFromWeb
---

# Get-PnPSiteScriptFromWeb

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Generates a Site Script from an existing site

## SYNTAX

### All components

```
Get-PnPSiteScriptFromWeb -Url <String> [-Lists <String[]>] [-IncludeAll]
 [-Connection <PnPConnection>]
```

### Specific components

```
Get-PnPSiteScriptFromWeb [-Url <String>] [-Lists <String[]>] [-IncludeBranding]
 [-IncludeLinksToExportedItems] [-IncludeRegionalSettings] [-IncludeSiteExternalSharingCapability]
 [-IncludeTheme] [-Connection <PnPConnection>]
```

### All lists

```
Get-PnPSiteScriptFromWeb [-Url <String>] [-IncludeAllLists] [-IncludeBranding]
 [-IncludeLinksToExportedItems] [-IncludeRegionalSettings] [-IncludeSiteExternalSharingCapability]
 [-IncludeTheme] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command allows a Site Script to be generated off of an existing site on your tenant. You need to provide at least one of the optional Include or Lists arguments. If you omit the URL, the Site Script will be created from the site to which you are connected.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSiteScriptFromWeb -IncludeAll
```

Returns the generated Site Script JSON containing all supported components from the currently connected to site

### EXAMPLE 2

```powershell
Get-PnPSiteScriptFromWeb -Url "https://contoso.sharepoint.com/sites/teamsite" -IncludeAll
```

Returns the generated Site Script JSON containing all supported components from the site at the provided Url

### EXAMPLE 3

```powershell
Get-PnPSiteScriptFromWeb -Url "https://contoso.sharepoint.com/sites/teamsite" -IncludeAll -Lists "Shared Documents","Lists\MyList"
```

Returns the generated Site Script JSON containing all supported components from the site at the provided Url including the lists "Shared Documents" and "MyList"

### EXAMPLE 4

```powershell
Get-PnPSiteScriptFromWeb -Url "https://contoso.sharepoint.com/sites/teamsite" -IncludeBranding -IncludeLinksToExportedItems
```

Returns the generated Site Script JSON containing the branding and navigation links from the site at the provided Url

### EXAMPLE 5

```powershell
Get-PnPSiteScriptFromWeb -IncludeAllLists
```

Returns the generated Site Script JSON containing all lists from the currently connected to site

### EXAMPLE 5

```powershell
Get-PnPSiteScriptFromWeb -IncludeAllLists | Add-PnPSiteScript -Title "My Site Script" | Add-PnPSiteDesign -Title "My Site Design" -WebTemplate TeamSite
```

Creates a new site script and site design based on the currently connected to site

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

### -IncludeAll

If specified will include all supported components into the Site Script including all self lists, branding, navigation links, regional settings, external sharing capability and theme.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All components
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IncludeAllLists

If specified, all lists that are not hidden, private, internal or catalogs will be included into the Site Script. It cannot be combined with the -Lists nor the -IncludeAll parameters as both will already include all lists.

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All lists
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IncludeBranding

If specified will include the branding of the site into the Site Script

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Specific components
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: All lists
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IncludeLinksToExportedItems

If specified will include navigation links into the Site Script

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Specific components
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: All lists
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IncludeRegionalSettings

If specified will include the regional settings into the Site Script

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Specific components
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: All lists
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IncludeSiteExternalSharingCapability

If specified will include the external sharing configuration into the Site Script

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Specific components
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: All lists
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IncludeTheme

If specified will include the branding of the site into the Site Script

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Specific components
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: All lists
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Lists

Allows specifying one or more site relative URLs of lists that should be included into the Site Script, i.e. "Shared Documents","Lists\MyList"

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Basic components
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: All components
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Specific components
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Url

Specifies the URL of the site to generate a Site Script from. If omitted, the currently connected to site will be used.

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
