---
Module Name: PnP.PowerShell
title: Register-PnPHubSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Register-PnPHubSite.html
---
 
# Register-PnPHubSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Registers a site as a hub site.

## SYNTAX

```powershell
Register-PnPHubSite -Site <SitePipeBind> [-Principals <String[][]>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Registers a site as a hub site.

## EXAMPLES

### EXAMPLE 1
```powershell
Register-PnPHubSite -Site "https://tenant.sharepoint.com/sites/myhubsite"
```

This example registers the specified site as a hub site.

### EXAMPLE 2
```powershell
Register-PnPHubSite -Site "https://tenant.sharepoint.com/sites/myhubsite" -Principals "user@contoso.com"
```

This example registers the specified site as a hub site and specifies that 'user@contoso.com' be granted rights to the hub site.

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
The site to register as a hub site.

```yaml
Type: SitePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Principals
Specifies one or more principals (user or group) to be granted rights to the specified hub site. Can be used to filter who can associate sites to this hub site.

```yaml
Type: String[]
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

