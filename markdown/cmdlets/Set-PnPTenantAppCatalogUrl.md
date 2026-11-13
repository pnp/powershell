---
Module Name: PnP.PowerShell
title: Set-PnPTenantAppCatalogUrl
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTenantAppCatalogUrl.html
---
 
# Set-PnPTenantAppCatalogUrl

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Sets the url of the tenant scoped app catalog

## SYNTAX

```powershell
Set-PnPTenantAppCatalogUrl -Url <String> [-Connection <PnPConnection>]  
 
```

## DESCRIPTION
This cmdlet sets the tenant scoped app catalog to the specified url.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTenantAppCatalogUrl -Url "https://yourtenant.sharepoint.com/sites/appcatalog"
```

Sets the tenant scoped app catalog to the provided site collection url

## PARAMETERS

### -Url
The url of the site to set as the tenant scoped app catalog

```yaml
Type: String
Parameter Sets: (All)

Required: True
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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

