---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/remove-pnporgnewssite
schema: 2.0.0
title: Remove-PnPOrgNewsSite
---

# Remove-PnPOrgNewsSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes a given site from the list of organizational news sites.

## SYNTAX

```
Remove-PnPOrgNewsSite -OrgNewsSiteUrl <SitePipeBind> [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Removes a given site from the list of organizational news sites based on its URL in your Sharepoint Online Tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPOrgNewsSite -OrgNewsSiteUrl https://tenant.sharepoint.com/sites/mysite
```

This example removes the specified site from list of organization's news sites.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OrgNewsSiteUrl
The site to be removed from list of organization's news sites

```yaml
Type: SitePipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)