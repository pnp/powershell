---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPHubSiteAssociation.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPHubSiteAssociation
---
  
# Add-PnPHubSiteAssociation

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Connects a site to a hubsite.

## SYNTAX

```powershell
Add-PnPHubSiteAssociation -Site <SitePipeBind> -HubSite <SitePipeBind> [-Connection <PnPConnection>]
 
```

## DESCRIPTION
Connects an existing site to a hubsite

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPHubSiteAssociation -Site "https://tenant.sharepoint.com/sites/mysite" -HubSite "https://tenant.sharepoint.com/sites/hubsite"
```

This example adds the specified site to the hubsite.

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

### -HubSite
The hubsite to connect the site to

```yaml
Type: SitePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
The site to connect to the hubsite

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


