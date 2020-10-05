---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/remove-pnphubsiteassociation
schema: 2.0.0
title: Remove-PnPHubSiteAssociation
---

# Remove-PnPHubSiteAssociation

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Disconnects a site from a hubsite.

## SYNTAX

```
Remove-PnPHubSiteAssociation -Site <SitePipeBind> [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Disconnects an site from a hubsite

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPHubSiteAssociation -Site https://tenant.sharepoint.com/sites/mysite
```

This example adds the specified site to the hubsite.

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

### -Site
The site to disconnect from its hubsite

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