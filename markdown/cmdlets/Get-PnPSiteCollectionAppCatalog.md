---
Module Name: PnP.PowerShell
title: Get-PnPSiteCollectionAppCatalog
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteCollectionAppCatalog.html
---
 
# Get-PnPSiteCollectionAppCatalog

## SYNOPSIS
Returns all site collection scoped app catalogs that exist on the tenant

## SYNTAX

```powershell
Get-PnPSiteCollectionAppCatalog [-CurrentSite <SwitchParameter>] [-ExcludeDeletedSites <SwitchParameter>] [-SkipUrlValidation <SwitchParameter>] [-Connection <PnPConnection>] [-Verbose] 
```

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
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CurrentSite
When provided, it will check if the currently connected to site has a site collection App Catalog and will return information on it. If the current site holds no site collection App Catalog, an empty response will be returned.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludeDeletedSites
When provided, all site collections having site collection App Catalogs but residing in the tenant recycle bin, will be excluded

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SkipUrlValidation
When provided, the site collection app catalog Urls will not be validated for if they have been renamed since their creation. This makes the cmdlet a lot faster, but it could also lead to URLs being returned that no longer exist. If not provided, for each site collection app catalog, it will look up the actual URL of the site collection app catalog and return that instead of the URL that was used when the site collection app catalog was created.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)