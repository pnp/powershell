---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPOrgNewsSite.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPOrgNewsSite
---
  
# Add-PnPOrgNewsSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Adds the site as an organization news source in your tenant

## SYNTAX

```powershell
Add-PnPOrgNewsSite -OrgNewsSiteUrl <SitePipeBind> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add site as an organization news source in your tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPOrgNewsSite -OrgNewsSiteUrl "https://yourtenant.sharepoint.com/sites/news"
```

Adds the site as one of multiple possible tenant's organizational news sites

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

### -OrgNewsSiteUrl
The url of the site to be marked as one of organization's news sites

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


