---
Module Name: PnP.PowerShell
title: Remove-PnPHubSiteAssociation
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPHubSiteAssociation.html
---
 
# Remove-PnPHubSiteAssociation

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes a hub site association from a site.

## SYNTAX

```powershell
Remove-PnPHubSiteAssociation -Site <SitePipeBind> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to remove a hub site association from a site.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPHubSiteAssociation -Site "https://tenant.sharepoint.com/sites/mysite"
```

This example removes the hub site association from the specified site.

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
The site to disconnect from its hubsite

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

