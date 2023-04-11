---
Module Name: PnP.PowerShell
title: Set-PnPTheme
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTheme.html
---
 
# Set-PnPTheme

## SYNOPSIS
Sets the theme of the current web.

## SYNTAX

```powershell
Set-PnPTheme [-ColorPaletteUrl <String>] [-FontSchemeUrl <String>] [-BackgroundImageUrl <String>]
 [-ResetSubwebsToInherit] [-UpdateRootWebOnly] [-Connection <PnPConnection>]
 
```

## DESCRIPTION
 Sets the theme of the current web, if any of the attributes is not set, that value will be set to null

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTheme
```

Removes the current theme and resets it to the default.

### EXAMPLE 2
```powershell
Set-PnPTheme -ColorPaletteUrl _catalogs/theme/15/company.spcolor
```

### EXAMPLE 3
```powershell
Set-PnPTheme -ColorPaletteUrl _catalogs/theme/15/company.spcolor -BackgroundImageUrl 'style library/background.png'
```

### EXAMPLE 4
```powershell
Set-PnPTheme -ColorPaletteUrl _catalogs/theme/15/company.spcolor -BackgroundImageUrl 'style library/background.png' -ResetSubwebsToInherit
```

Sets the theme to the web, and updates all subwebs to inherit the theme from this web.

## PARAMETERS

### -BackgroundImageUrl
Specifies the Background Image Url based on the site or server relative url

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColorPaletteUrl
Specifies the Color Palette Url based on the site or server relative url

```yaml
Type: String
Parameter Sets: (All)

Required: False
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

### -FontSchemeUrl
Specifies the Font Scheme Url based on the site or server relative url

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResetSubwebsToInherit
Resets subwebs to inherit the theme from the rootweb

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UpdateRootWebOnly
Updates only the rootweb, even if subwebs are set to inherit the theme.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

