---
Module Name: PnP.PowerShell
title: Get-PnPTenantCdnOrigin
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantCdnOrigin.html
---
 
# Get-PnPTenantCdnOrigin

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns the current registered origins from the public or private content delivery network (CDN).

## SYNTAX

```powershell
Get-PnPTenantCdnOrigin -CdnType <SPOTenantCdnType> [-Connection <PnPConnection>] 
```

## DESCRIPTION
Returns the current registered origins from the public or private content delivery network (CDN).

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantCdnOrigin -CdnType Public
```

Returns the configured CDN origins for the specified CDN type

## PARAMETERS

### -CdnType
The type of cdn to retrieve the origins from

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

