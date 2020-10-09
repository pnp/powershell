---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnphubsitechild
schema: 2.0.0
title: Get-PnPHubSiteChild
---

# Get-PnPHubSiteChild

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Retrieves all sites linked to a specific hub site

## SYNTAX

```powershell
Get-PnPHubSiteChild -Identity <HubSitePipeBind> [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Retrieves all sites linked to a specific hub site

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPHubSiteChild -Identity https://contoso.sharepoint.com/sites/myhubsite
```

Returns the sites having configured the provided hub site as their hub site

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

### -Identity
The URL of the hubsite for which to receive the sites refering to it

```yaml
Type: HubSitePipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)