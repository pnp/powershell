---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Sync-PnPBrandCenterOfficeFonts.html
external help file: PnP.PowerShell.dll-Help.xml
title: Sync-PnPBrandCenterOfficeFonts
---
  
# Sync-PnPBrandCenterOfficeFonts

## SYNOPSIS

**Required Permissions**

* SharePoint Administrator

Makes the Office font library offer the Brand Center fonts, or the fonts in a local folder.

## SYNTAX

```powershell
Sync-PnPBrandCenterOfficeFonts [-Source <OfficeFontSource>] [-LocalFolder <String>] [-RemoveMissing] [-KeepRegistration] [-Force]
 [-WhatIf] [-Confirm] [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
Brings the Office font library (the organization assets library with OrgAssetType OfficeFontLibrary) in line with a source: the Brand Center font library, or a local folder. Office desktop and web apps then offer the source's .ttf and .otf fonts.

1. The font file names of the source and of the Office font library are compared. When nothing changed, the cmdlet stops: nothing is downloaded or uploaded.
2. Otherwise the fonts the library keeps and the new ones are all published again, with a new font catalog, replacing the previous upload the way Set-SPOCustomFontCatalog does. The catalog is built by PnP itself: no SharePoint Online Management Shell module and no Windows are needed.
3. Around that upload, the Office font library registration is removed and added again, as Microsoft documents for changing a font library (Remove-SPOOrgAssetsLibrary, upload, Add-SPOOrgAssetsLibrary). The public CDN origin and the thumbnail are kept, and the registration is added again even if the upload fails. Use -KeepRegistration to skip this. See [Set-PnPOfficeFontCatalog](Set-PnPOfficeFontCatalog.md#registration).
4. Office apps pick up the new catalog when they next refresh it, which can take up to 24 hours. Publishing again in the meantime restarts the wait.
5. With -RemoveMissing, the Office fonts that are not in the source are deleted and dropped from the catalog.

Before changing anything, the cmdlet asks for confirmation and lists the fonts it will add and remove. Use -WhatIf to see that list without changing anything, and -Force to skip the confirmation.

The public CDN must be enabled, and an Office font library must be configured.

Fonts and their catalog are stored on the public CDN: anyone who has their URLs can read them, including people outside your organization. Do not publish fonts whose license forbids cloud hosting.

## EXAMPLES

### EXAMPLE 1
```powershell
Sync-PnPBrandCenterOfficeFonts -WhatIf
```

Shows which Brand Center fonts would be added to the Office font library, without changing anything.

### EXAMPLE 2
```powershell
Sync-PnPBrandCenterOfficeFonts -Force
```

Adds the Brand Center fonts that the Office font library does not have yet, without asking for confirmation.

### EXAMPLE 3
```powershell
Sync-PnPBrandCenterOfficeFonts -RemoveMissing
```

Makes the Office font library offer exactly the Brand Center fonts: adds the missing ones, and removes the ones Brand Center no longer has.

### EXAMPLE 4
```powershell
Sync-PnPBrandCenterOfficeFonts -Source Local -LocalFolder C:\Fonts -Verbose
```

Adds the fonts in C:\Fonts that the Office font library does not have. -Verbose lists the fonts found on each side and every file uploaded.

If publishing fails with "its upload state is broken", the library's previous upload cannot be replaced (Set-SPOCustomFontCatalog fails the same way). Register a new Office font library and publish to it, or ask Microsoft support to reset it.

## PARAMETERS

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
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

### -Force
Publishes without asking for confirmation.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -KeepRegistration
Publishes without removing and adding the Office font library registration again. Use it only when you know Office will pick up the change without it, for example on a test tenant.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LocalFolder
Folder with the .ttf and .otf fonts, for -Source Local. Relative paths start at the current location.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveMissing
Also removes the Office fonts that are not in the source. The cmdlet refuses to remove every font.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Source
Where the fonts come from: BrandCenter (the Brand Center font library) or Local (-LocalFolder).

```yaml
Type: OfficeFontSource
Parameter Sets: (All)
Accepted values: BrandCenter, Local

Required: False
Position: Named
Default value: BrandCenter
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Set-PnPOfficeFontCatalog](Set-PnPOfficeFontCatalog.md)

[Get-PnPBrandCenterFont](Get-PnPBrandCenterFont.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
