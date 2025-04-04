---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Use-PnPBrandCenterFont.html
external help file: PnP.PowerShell.dll-Help.xml
title: Use-PnPBrandCenterFont
---
  
# Use-PnPBrandCenterFont

## SYNOPSIS
Applies the specified font from the Brand Center to the current site.

## SYNTAX

```powershell
Use-PnPBrandCenterFont -Identity <BrandCenterFontPipeBind> [-Store <Tenant|OutOfBox|Site|All>] [-Connection <PnPConnection>]
```

## DESCRIPTION
Applies the specified font from the Brand Center to the current site.

## EXAMPLES

### EXAMPLE 1
```powershell
Use-PnPBrandCenterFont -Identity "2812cbd8-7176-4e45-8911-6a063f89a1f1"
```

Looks up and applies the font with the identity "2812cbd8-7176-4e45-8911-6a063f89a1f1" from any of the Brand Centers to the current site

### EXAMPLE 2
```powershell
Use-PnPBrandCenterFont -Identity "My awesome font" -Store Tenant
```

Looks up and applies the font with the title "My awesome font" from the tenant Brand Center

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
Unique identifier of the font to be applied. This can be its guid, name or a BrandCenterFont object.

```yaml
Type: BrandCenterFontPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Store
Indicates the source of the fonts to be looked in to try to locate the font to apply. The following values are available:
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