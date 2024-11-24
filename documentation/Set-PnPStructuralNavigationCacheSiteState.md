---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPStructuralNavigationCacheSiteState.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPStructuralNavigationCacheSiteState
---

# Set-PnPStructuralNavigationCacheSiteState

## SYNOPSIS

Enable or disable caching for all webs in a site collection.

## SYNTAX

### Default (Default)

```
Set-PnPStructuralNavigationCacheSiteState -IsEnabled <Boolean> [-SiteUrl <String>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

The Set-PnPStructuralNavigationCacheSiteState cmdlet can be used to enable or disable caching for all webs in a site collection. If the SiteUrl parameter has not been specified the currently connected to site will be used.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPStructuralNavigationCacheSiteState -IsEnabled $true -SiteUrl "https://contoso.sharepoint.com/sites/product/"
```

This example enables caching for all webs in the site collection https://contoso.sharepoint.com/sites/product/.

### EXAMPLE 2

```powershell
Set-PnPStructuralNavigationCacheSiteState -IsEnabled $false -SiteUrl "https://contoso.sharepoint.com/sites/product/"
```

This example disables caching for all webs in the site collection https://contoso.sharepoint.com/sites/product/.

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

### -IsEnabled

$true to enable caching, $false to disable caching.

```yaml
Type: Boolean
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

### -SiteUrl

Specifies the absolute URL for the site collection's root web that needs its caching state to be set.

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
