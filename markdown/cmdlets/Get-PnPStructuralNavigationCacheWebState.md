---
Module Name: PnP.PowerShell
title: Get-PnPStructuralNavigationCacheWebState
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPStructuralNavigationCacheWebState.html
---
 
# Get-PnPStructuralNavigationCacheWebState

## SYNOPSIS
Get the structural navigation caching state for a web.

## SYNTAX

```powershell
Get-PnPStructuralNavigationCacheWebState [-WebUrl <String>]
```

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
Parameter Sets: (All)
Aliases:
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

