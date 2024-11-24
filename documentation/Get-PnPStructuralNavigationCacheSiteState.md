---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPStructuralNavigationCacheSiteState.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPStructuralNavigationCacheSiteState
---

# Get-PnPStructuralNavigationCacheSiteState

## SYNOPSIS

Get the structural navigation caching state for a site collection.

## SYNTAX

### Default (Default)

```
Get-PnPStructuralNavigationCacheSiteState [-SiteUrl <String>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

The Get-PnPStructuralNavigationCacheSiteState cmdlet can be used to determine if structural navigation caching is enabled or disabled for a site collection. If the SiteUrl parameter has not been specified the currently connected to site will be used.

## EXAMPLES

### Example 1

```powershell
Get-PnPStructuralNavigationCacheSiteState -SiteUrl "https://contoso.sharepoint.com/sites/product/"
```

This example checks if structural navigation caching is enabled for the entire site collection https://contoso.sharepoint.com/sites/product/. If caching is enabled, then it will return True. If caching is disabled, then it will return False.

## PARAMETERS

### -SiteUrl

Specifies the absolute URL for the site collection's root web being checked for its caching state.

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
