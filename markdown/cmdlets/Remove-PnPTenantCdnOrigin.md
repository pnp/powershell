---
Module Name: PnP.PowerShell
title: Remove-PnPTenantCdnOrigin
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTenantCdnOrigin.html
---
 
# Remove-PnPTenantCdnOrigin

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes an origin from the Public or Private content delivery network (CDN).

## SYNTAX

```powershell
Remove-PnPTenantCdnOrigin -OriginUrl <String> -CdnType <SPOTenantCdnType> [-Connection <PnPConnection>]
 
```

## DESCRIPTION
Removes an origin from the Public or Private content delivery network (CDN).

You must be a SharePoint Online Administrator to run the cmdlet.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTenantCdnOrigin -OriginUrl /sites/site/subfolder -CdnType Public
```

This example removes the specified origin from the public CDN.

## PARAMETERS

### -CdnType
The cdn type to remove the origin from.

```yaml
Type: SPOTenantCdnType
Parameter Sets: (All)
Accepted values: Public, Private

Required: True
Position: Named
Default value: None
Accept pipeline input: False
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

### -OriginUrl
The origin to remove.

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

