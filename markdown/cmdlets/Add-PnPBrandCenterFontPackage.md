---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPBrandCenterFontPackage.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPBrandCenterFontPackage
---
  
# Add-PnPBrandCenterFontPackage

## SYNOPSIS
Allows a font package to be created in the Brand Center

## SYNTAX

```powershell
Add-PnPBrandCenterFontPackage -Name <String> -Store <Site|Tenant> -DisplayFont <BrandCenterFontPipeBind> -ContentFont <String> -TitleFont <BrandCenterFontPipeBind> -TitleFontStyle <String> -HeadlineFont <BrandCenterFontPipeBind> -HeadlineFontStyle <String>> -BodyFont <BrandCenterFontPipeBind> -BodyFontStyle <String>> -InteractiveFont <BrandCenterFontPipeBind> -InteractiveFontStyle <String> [-Visible <Boolean>] [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
This cmdlet allows a font package to be created in the Brand Center. Fonts can be uploaded to the Brand Center using [Add-PnPBrandCenterFond](Add-PnPBrandCenterFond.md). To see the available fonts, as well as the font styles available in each font, use [Get-PnPBrandCenterFont](Get-PnPBrandCenterFont.md).

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPBrandCenterFontPackage -Name "My Font Package" -Store Tenant -DisplayFont "My font" -ContentFont "My other font" -TitleFont "My font" -TitleFontStyle "Normal" -HeadlineFont "My font" -HeadlineFontStyle "Normal" -BodyFont "My other font" -BodyFontStyle "Regular" -InteractiveFont "My other font" -InteractiveFontStyle "Regular"
```

This will create a font package in the Tenant Brand Center with the specified fonts and styles. The package will be available for use in the Brand Center.

## PARAMETERS

### -BodyFont
The name, id or font instance of the font to be used for body text in the Brand Center. 

```yaml
Type: BrandCenterFontPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BodyFontStyle
The name of the font style to be used for body text in the Brand Center. Use the [Get-PnPBrandCenterFont](Get-PnPBrandCenterFont.md) cmdlet to see the available styles for each font.

```yaml
Type: string
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -ContentFont
The name, id or font instance of the font to be used for content text in the Brand Center. 

```yaml
Type: BrandCenterFontPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayFont
The name, id or font instance of the font to be used for display text in the Brand Center. 

```yaml
Type: BrandCenterFontPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HeadlineFont
The name, id or font instance of the font to be used for headline text in the Brand Center. 

```yaml
Type: BrandCenterFontPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HeadlineFontStyle
The name of the font style to be used for headline text in the Brand Center. Use the [Get-PnPBrandCenterFont](Get-PnPBrandCenterFont.md) cmdlet to see the available styles for each font.

```yaml
Type: string
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InteractiveFont
The name, id or font instance of the font to be used for interactive text in the Brand Center.

```yaml
Type: BrandCenterFontPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InteractiveFontStyle
The name of the font style to be used for interactive text in the Brand Center. Use the [Get-PnPBrandCenterFont](Get-PnPBrandCenterFont.md) cmdlet to see the available styles for each font.

```yaml
Type: string
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The name you want to give to the font package. This name will be used to identify the package in the Brand Center.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Store
Indicates the store in which the font package should be created. The following values are available:
- Tenant: The font packages configured in the tenant Brand Center
- Site: The font packages configured in the site collection Brand Center

```yaml
Type: Store
Parameter Sets: (All)

Required: False
Position: Named
Default value: Tenant
Accept pipeline input: False
Accept wildcard characters: False
```

### -TitleFont
The name, id or font instance of the font to be used for title text in the Brand Center. 

```yaml
Type: BrandCenterFontPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TitleFontStyle
The name of the font style to be used for title text in the Brand Center. Use the [Get-PnPBrandCenterFont](Get-PnPBrandCenterFont.md) cmdlet to see the available styles for each font.

```yaml
Type: string
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Visible
Indicates if the font package should be visible in the Brand Center. The default is true.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: True
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
