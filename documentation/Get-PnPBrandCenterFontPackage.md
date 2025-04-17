---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPBrandCenterFontPackage.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPBrandCenterFontPackage
---
  
# Get-PnPBrandCenterFontPackage

## SYNOPSIS
Returns the available fonts configured through the Brand Center

## SYNTAX

### All
```powershell
Get-PnPBrandCenterFontPackage [-Store <Tenant|OutOfBox|Site|All>] [-Connection <PnPConnection>]
```

### Single
```powershell
Get-PnPBrandCenterFontPackage -Identity <BrandCenterFontPipeBind> [-Store <Tenant|OutOfBox|Site|All>] [-Connection <PnPConnection>]
```

## DESCRIPTION
Allows retrieval of the available fonts from the various Brand Centers.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPBrandCenterFontPackage
```

Returns all the available fonts

### EXAMPLE 2
```powershell
Get-PnPBrandCenterFontPackage -Store Site
```

Returns the available fonts from the site collection Brand Center

### EXAMPLE 3
```powershell
Get-PnPBrandCenterFontPackage -Identity "My awesome font"
```

Looks up and returns the font with the name "My awesome font" from any of the Brand Centers

### EXAMPLE 4
```powershell
Get-PnPBrandCenterFontPackage -Identity "2812cbd8-7176-4e45-8911-6a063f89a1f1"
```

Looks up and returns the font with the Identity "2812cbd8-7176-4e45-8911-6a063f89a1f1" from any of the Brand Centers

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing [Get-PnPConnection](Get-PnPConnection.md).

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
Unique identifier of the font to be retrieved. This can be its guid, name or a BrandCenterFont object. If not specified, all the available fonts will be returned.

```yaml
Type: BrandCenterFontPipeBind
Parameter Sets: Single

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Store
Indicates the source of the fonts to be retrieved. The following values are available:
- Tenant: The fonts configured in the tenant Brand Center
- Site: The fonts configured in the site collection Brand Center
- OutOfBox: The out of box fonts available in the tenant
- All: All the fonts available in the tenant, including the ones configured in the tenant and site collection Brand Center and the out of box fonts.

```yaml
Type: Store
Parameter Sets: (All)

Required: False
Position: Named
Default value: All
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
