---
Module Name: PnP.PowerShell
title: Set-PnPFooter
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPFooter.html
---
 
# Set-PnPFooter

## SYNOPSIS
Configures the footer of the current web.

## SYNTAX

```powershell
Set-PnPFooter [-Enabled] [-Layout <FooterLayoutType>] [-BackgroundTheme <FooterVariantThemeType>]
 [-Title <String>] [-LogoUrl <String>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Allows the footer to be enabled or disabled and fine tuned in the current web. For modifying the navigation links shown in the footer, use Add-PnPNavigationNode -Location Footer.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPFooter -Enabled:$true
```

Enables the footer to be shown on the current web.

### EXAMPLE 2
```powershell
Set-PnPFooter -Enabled:$true -Layout Extended -BackgroundTheme Neutral
```

Enables the footer to be shown on the current web with the extended layout using a neutral background.

### EXAMPLE 3
```powershell
Set-PnPFooter -Title "Contoso Inc." -LogoUrl "/sites/communication/Shared Documents/logo.png"
```

Sets the title and logo shown in the footer.

### EXAMPLE 4
```powershell
Set-PnPFooter -LogoUrl ""
```

Removes the current logo shown in the footer.

## PARAMETERS

### -BackgroundTheme
Defines the background emphasis of the content in the footer.

```yaml
Type: FooterVariantThemeType
Parameter Sets: (All)
Accepted values: Strong, Neutral, Soft, None

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

### -Enabled
Indicates if the footer should be shown on the current web ($true) or if it should be hidden ($false).

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Layout
Defines how the footer should look like.

```yaml
Type: FooterLayoutType
Parameter Sets: (All)
Accepted values: Simple, Extended, Stacked

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LogoUrl
Defines the server relative URL to the logo to be displayed in the footer. Provide an empty string to remove the current logo.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
Defines the title displayed in the footer.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

