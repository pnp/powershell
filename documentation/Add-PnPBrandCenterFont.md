---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPBrandCenterFont.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPBrandCenterFont
---
  
# Add-PnPBrandCenterFont

## SYNOPSIS
Allows a font to be uploaded to the tenant Brand Center

## SYNTAX

```powershell
Add-PnPBrandCenterFont -Path <String> [-Visible <Boolean>] [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
This cmdlet allows a font to be uploaded to the tenant Brand Center. The font will be available for use in the tenant and site collection Brand Center.

Use [Use-PnPBrandCenterFont](Use-PnPBrandCenterFont.md) to apply the font to the current site.
Use [Get-PnPBrandCenterFont](Get-PnPBrandCenterFont.md) to retrieve the available fonts.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPBrandCenterFont -Path c:\temp\MyAwesomeFont.ttf
```

This will upload the font MyAwesomeFont.ttf to the tenant Brand Center and will make it visible

### EXAMPLE 2
```powershell
Add-PnPBrandCenterFont -Path c:\temp\MyAwesomeFont.ttf -Visible:$false
```

This will upload the font MyAwesomeFont.ttf to the tenant Brand Center and will hide it from being used

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

### -Path
The local file path to where the font is stored that needs to be uploaded

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Visible
Indicates if the font should be visible in the Brand Center. The default is true.

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