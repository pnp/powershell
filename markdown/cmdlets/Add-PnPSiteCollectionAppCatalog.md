---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPSiteCollectionAppCatalog.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPSiteCollectionAppCatalog
---
  
# Add-PnPSiteCollectionAppCatalog

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Adds a Site Collection scoped App Catalog to a site

## SYNTAX

```powershell
Add-PnPSiteCollectionAppCatalog [-Site <SitePipeBind>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add Site Collection scoped App Catalog.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPSiteCollectionAppCatalog
```

This will add a SiteCollection app catalog to the currently connected to site

### EXAMPLE 2
```powershell
Add-PnPSiteCollectionAppCatalog -Site "https://contoso.sharepoint.com/sites/FinanceTeamsite"
```

This will add a SiteCollection app catalog to the specified site

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

### -Site
Url of the site to add the app catalog to.

```yaml
Type: SitePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/sharepoint/dev/general-development/site-collection-app-catalog#configure-and-manage-site-collection-app-catalogs)