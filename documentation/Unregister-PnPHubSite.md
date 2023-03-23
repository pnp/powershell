---
Module Name: PnP.PowerShell
title: Unregister-PnPHubSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Unregister-PnPHubSite.html
---
 
# Unregister-PnPHubSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Unregister a site as a hubsite

## SYNTAX

```powershell
Unregister-PnPHubSite -Site <SitePipeBind> [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

Allows to unregister a site as a hubsite.

## EXAMPLES

### EXAMPLE 1
```powershell
Unregister-PnPHubSite -Site "https://tenant.sharepoint.com/sites/myhubsite"
```

This example unregister the specified site as a hubsite

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
The site to unregister as a hubsite

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

