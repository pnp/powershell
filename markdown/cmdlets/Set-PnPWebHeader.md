---
Module Name: PnP.PowerShell
title: Set-PnPWebHeader
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPWebHeader.html
---
 
# Set-PnPWebHeader

## SYNOPSIS
Allows configuration of the "Change the look" Header

## SYNTAX

```powershell
Set-PnPWebHeader [-SiteLogoUrl <string>] [-SiteThumbnailUrl <string>] [-HeaderLayout <HeaderLayoutType>] [-HeaderEmphasis <SPVariantThemeType>] [-HideTitleInHeader] [-HeaderBackgroundImageUrl <string>] [-HeaderBackgroundImageFocalX <double>] [-HeaderBackgroundImageFocalY <double>] [-LogoAlignment <LogoAlignment>]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Through this cmdlet the various options offered through "Change the look" Header can be configured.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPWebHeader -HeaderBackgroundImageUrl "/sites/hrdepartment/siteassets/background.png" -HeaderLayout Extended
```

Sets the background image of the heading of the site to the provided image

### EXAMPLE 2
```powershell
Set-PnPWebHeader -HeaderEmphasis Strong
```

Sets the site to use a strong colored bar at the top of the site

### EXAMPLE 3
```powershell
Set-PnPWebHeader -LogoAlignment Middle
```

Sets the site title and logo to be displayed in the middle of the screen

## PARAMETERS

### -LogoAlignment
Allows configuring the site title and logo to be shown on the left (default), in the middle or on the right.

```yaml
Type: LogoAlignment
Parameter Sets: (All)
Accepted values: Left, Middle, Right

Required: False
Position: Named
Default value: Left
Accept pipeline input: False
Accept wildcard characters: False
```

### -HeaderBackgroundImageUrl
Allows providing a server relative URL to an image that should be used as the background of the header of the site, i.e. /sites/hrdepartment/siteassets/background.png. HeaderLayout must be set to Extended for the image to show up. Provide "" or $null to remove the current background image.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HeaderBackgroundImageFocalX
When having HeaderLayout set to Extended and when providing a background image to show in the header through HeaderBackgroundImageUrl, this property allows for defining the X coordinate of the image how it should be shown.

```yaml
Type: Double
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HeaderBackgroundImageFocalY
When having HeaderLayout set to Extended and when providing a background image to show in the header through HeaderBackgroundImageUrl, this property allows for defining the Y coordinate of the image how it should be shown.

```yaml
Type: Double
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

### -HeaderEmphasis
Defines the tone of color used for the bar at shown at the top of the site under the site name and logo

```yaml
Type: SPVariantThemeType
Parameter Sets: (All)
Accepted values: None, Neutral, Soft, Strong

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HeaderLayout
Defines how the header of the site should be layed out

```yaml
Type: HeaderLayoutType
Parameter Sets: (All)
Accepted values: None, Standard, Compact, Minimal, Extended

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteLogoUrl
Sets the logo of the site shown at the top left to the provided server relative url, i.e. /sites/hrdepartment/siteassets/logo.png. Provide "" or $null to remove the logo.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteThumbnailUrl
Sets the thumbnail of the site shown at the top left to the provided server relative url, i.e. /sites/hrdepartment/siteassets/thumbnail.png. Provide "" or $null to remove the thumbnail.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HideTitleInHeader
Toggle the title visibility in the header.

Set -HideTitleInHeader:$false to show the header

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
