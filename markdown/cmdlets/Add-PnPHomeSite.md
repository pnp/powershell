---
Module Name: PnP.PowerShell
title: Add-PnPHomeSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPHomeSite.html
---
 
# Add-PnPHomeSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Adds the home site to your tenant. The home site needs to be a communication site.

## SYNTAX

```powershell
Add-PnPHomeSite -HomeSiteUrl <String> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Adds a home site to the current tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPHomeSite -HomeSiteUrl "https://yourtenant.sharepoint.com/sites/myhome"
```

Adds a home site with the provided site collection url

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

### -HomeSiteUrl
The url of the site to set as the home site

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Set up a home site for your organization](https://learn.microsoft.com/sharepoint/home-site)
[Customize and edit the Viva Connections home experience](https://learn.microsoft.com/en-us/viva/connections/edit-viva-home)
