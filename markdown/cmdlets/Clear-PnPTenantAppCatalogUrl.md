---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Clear-PnPTenantAppCatalogUrl.html
external help file: PnP.PowerShell.dll-Help.xml
title: Clear-PnPTenantAppCatalogUrl
---
  
# Clear-PnPTenantAppCatalogUrl

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes the url of the tenant scoped app catalog. It will not delete the site collection itself.

## SYNTAX

```powershell
Clear-PnPTenantAppCatalogUrl [-Connection <PnPConnection>]   
```

## DESCRIPTION

Allows to remove the url of the tenant scoped app catalog. The app catalog site collection will not be removed.

## EXAMPLES

### EXAMPLE 1
```powershell
Clear-PnPTenantAppCatalogUrl
```

Removes the url of the tenant scoped app catalog

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)