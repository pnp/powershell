---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPStructuralNavigationCacheWebState.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPStructuralNavigationCacheWebState
---

# Set-PnPStructuralNavigationCacheWebState

## SYNOPSIS

Enable or disable caching for a web.

## SYNTAX

### Default (Default)

```
Set-PnPStructuralNavigationCacheWebState -IsEnabled <Boolean> [-WebUrl <String>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

The Set-PnPStructuralNavigationCacheWebState cmdlet can be used to enable or disable caching for a web. If the WebUrl parameter has not been specified the currently connected to web will be used.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPStructuralNavigationCacheWebState -IsEnabled $true -WebUrl "https://contoso.sharepoint.com/sites/product/electronics"
```

This example enables caching for the web https://contoso.sharepoint.com/sites/product/electronics.

### EXAMPLE 2

```powershell
Set-PnPStructuralNavigationCacheWebState -IsEnabled $false -WebUrl "https://contoso.sharepoint.com/sites/product/electronics"
```

This example disables caching for the web https://contoso.sharepoint.com/sites/product/electronics.

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

### -WebUrl

Specifies the absolute URL for the web that needs its caching state set.

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
