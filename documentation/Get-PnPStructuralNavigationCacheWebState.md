---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPStructuralNavigationCacheWebState.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPStructuralNavigationCacheWebState
---

# Get-PnPStructuralNavigationCacheWebState

## SYNOPSIS

Get the structural navigation caching state for a web.

## SYNTAX

### Default (Default)

```
Get-PnPStructuralNavigationCacheWebState [-WebUrl <String>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

The Get-PnPStructuralNavigationCacheWebState cmdlet can be used to determine if structural navigation caching is enabled or disabled for a web in a site collection. If the WebUrl parameter has not been specified the currently connected to web will be used.

## EXAMPLES

### Example 1

```powershell
Get-PnPStructuralNavigationCacheWebState -WebUrl "https://contoso.sharepoint.com/sites/product/electronics"
```

This example checks if structural navigation caching is enabled for the web https://contoso.sharepoint.com/sites/product/electronics. If caching is enabled, then it will return True. If caching is disabled, then it will return False.

## PARAMETERS

### -WebUrl

Specifies the absolute URL for the web being checked for its caching state.

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
