---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPSearchSettings.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPSearchSettings
---

# Set-PnPSearchSettings

## SYNOPSIS

Sets search settings for a site.

## SYNTAX

### Default (Default)

```
Set-PnPSearchSettings [-SearchBoxInNavBar <SearchBoxInNavBarType>] [-SearchPageUrl <String>]
 [-SearchBoxPlaceholderText <String>] [-SearchScope <SearchScopeType>]
 [-Scope <SearchSettingsScope>] [-Force] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to modify search settings for a site.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPSearchSettings -SearchBoxInNavBar Hidden -Scope Site
```

This example hides the suite bar search box on all pages and sites in the site collection.

### EXAMPLE 2

```powershell
Set-PnPSearchSettings -SearchBoxInNavBar Hidden -Scope Web
```

Example 2 hides the suite bar search box on all pages in the current site.

### EXAMPLE 3

```powershell
Set-PnPSearchSettings -SearchPageUrl "https://contoso.sharepoint.com/sites/mysearch/SitePages/search.aspx"
```

Redirects the suite bar search box in the site to a custom URL

### EXAMPLE 4

```powershell
Set-PnPSearchSettings -SearchPageUrl ""
```

This example clears the suite bar search box redirect URL and reverts to the default behavior.

### EXAMPLE 5

```powershell
Set-PnPSearchSettings -SearchPageUrl "https://contoso.sharepoint.com/sites/mysearch/SitePages/search.aspx" -Scope Site
```

Redirects classic search to a custom URL.

### EXAMPLE 6

```powershell
Set-PnPSearchSettings -SearchScope Tenant
```

Example 6 sets default behavior of the suite bar search box to show tenant wide results instead of site or hub scoped results.

### EXAMPLE 7

```powershell
Set-PnPSearchSettings -SearchScope Hub
```

Sets default behavior of the suite bar search box to show hub results instead of site results on an associated hub site.

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

Do not ask for confirmation.

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

### -Scope

Scope to apply the setting to. Possible values: Web (default), Site. For a root site, the scope does not matter.

```yaml
Type: SearchSettingsScope
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
- Site
- Web
HelpMessage: ''
```

### -SearchBoxInNavBar

Set the scope of which the suite bar search box shows. Possible values: Inherit, AllPages, ModernOnly, Hidden.

```yaml
Type: SearchBoxInNavBarType
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
- Inherit
- AllPages
- ModernOnly
- Hidden
HelpMessage: ''
```

### -SearchBoxPlaceholderText

Set the placeholder text displayed in the search box.

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

### -SearchPageUrl

Set the URL where the search box should redirect to.

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

### -SearchScope

Set the search scope of the suite bar search box. Possible values: DefaultScope, Tenant, Hub, Site.

```yaml
Type: SearchScopeType
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
- DefaultScope
- Tenant
- Hub
- Site
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
