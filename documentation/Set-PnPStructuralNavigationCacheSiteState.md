---
Module Name: PnP.PowerShell
title: Set-PnPStructuralNavigationCacheSiteState
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPStructuralNavigationCacheSiteState.html
---
 
# Set-PnPStructuralNavigationCacheSiteState

## SYNOPSIS
Enable or disable caching for all webs in a site collection.

## SYNTAX

```powershell
Set-PnPStructuralNavigationCacheSiteState -IsEnabled <Boolean> [-SiteUrl <String>] [-Connection <PnPConnection>]
```

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
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsEnabled
$true to enable caching, $false to disable caching.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteUrl
Specifies the absolute URL for the site collection's root web that needs its caching state to be set.

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

