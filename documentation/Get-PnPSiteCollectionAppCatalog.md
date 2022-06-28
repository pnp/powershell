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
Get-PnPSiteCollectionAppCatalog [-ExcludeDeletedSites <SwitchParameter>] [-Connection <PnPConnection>] [-Verbose] [<CommonParameters>]
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
Get-PnPSiteCollectionAppCatalog -ExcludeDeletedSites
```
Will return all the site collection app catalogs that exist on the tenant excluding the site collections having App Catalogs that are in the tenant recycle bin

## PARAMETERS

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