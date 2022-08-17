---
Module Name: PnP.PowerShell
title: Remove-PnPSiteCollectionAppCatalog
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPSiteCollectionAppCatalog.html
---
 
# Remove-PnPSiteCollectionAppCatalog

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes a site collection scoped app catalog from a site.

## SYNTAX

```powershell
Remove-PnPSiteCollectionAppCatalog -Site <SitePipeBind> [-Connection <PnPConnection>]
```

## DESCRIPTION
Notice that this will not remove the App Catalog list and its contents from the site.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPSiteCollectionAppCatalog -Site "https://contoso.sharepoint.com/sites/FinanceTeamsite"
```

This will remove a site collection app catalog from the specified site.

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
Url of the site to remove the app catalog from.

```yaml
Type: SitePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

