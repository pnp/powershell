---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPHubSiteChild.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPHubSiteChild
---
  
# Get-PnPHubSiteChild

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Retrieves all sites associated to a specific hub site

## SYNTAX

```powershell
Get-PnPHubSiteChild [-Identity <HubSitePipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Retrieves all sites associated to a specific hub site

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPHubSiteChild
```

Returns the sites which are associated to the currently connected to hub site

### EXAMPLE 2
```powershell
Get-PnPHubSiteChild -Identity "https://contoso.sharepoint.com/sites/myhubsite"
```

Returns the sites which are associated with the provided hub site as their hub site

### EXAMPLE 3
```powershell
Get-PnPHubSite | Get-PnPHubSiteChild
```

Returns all sites that are associated to a hub site

## PARAMETERS

### -Identity
The URL, Id or instance of the hubsite for which to receive the sites referring to it. If not provided, the currently connected to site will be used.

```yaml
Type: HubSitePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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