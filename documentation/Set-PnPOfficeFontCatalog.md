---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPOfficeFontCatalog.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPOfficeFontCatalog
---
  
# Set-PnPOfficeFontCatalog

## SYNOPSIS

**Required Permissions**

* SharePoint Administrator

Publishes the fonts in a local folder to the Office font library, so they show in Office apps.

## SYNTAX

```powershell
Set-PnPOfficeFontCatalog -FontFolder <String> [-LibraryUrl <String>] [-KeepRegistration] [-WhatIf] [-Confirm] [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
Uploads the .ttf and .otf fonts in a folder to the Office font library (the organization assets library with OrgAssetType OfficeFontLibrary), and publishes a new font catalog that lists exactly those fonts. Office desktop and web apps read that catalog to offer the fonts.

It does the same as Set-SPOCustomFontCatalog, but works on a PnP connection (including access tokens), and builds the font catalog itself: it needs neither the SharePoint Online Management Shell module nor Windows.

Like Set-SPOCustomFontCatalog, it replaces the previous upload: afterwards Office offers only the fonts in the folder. To add fonts without listing the existing ones again, use [Sync-PnPBrandCenterOfficeFont](Sync-PnPBrandCenterOfficeFont.md).

The cmdlet also removes and adds the Office font library registration around the upload, as Microsoft documents for changing a font library, and registers the library if it is not registered yet. See [Registration](#registration).

Office apps pick up the new catalog when they next refresh it, which can take up to 24 hours. Publishing again in the meantime restarts the wait.

If publishing fails with "its upload state is broken", the library's previous upload cannot be replaced (Set-SPOCustomFontCatalog fails the same way). Register a new Office font library and publish to it, or ask Microsoft support to reset it.

Other file types (such as .woff, .woff2 and .ttc) are skipped with a warning: Office apps do not use them. The public CDN must be enabled. If another library is already registered as the Office font library, the cmdlet fails.

Fonts and their catalog are stored on the public CDN: anyone who has their URLs can read them, including people outside your organization. Do not publish fonts whose license forbids cloud hosting.

## REGISTRATION
Microsoft documents that to change a font library you remove it with Remove-SPOOrgAssetsLibrary, upload the fonts again, and register it again. Uploading alone replaces the catalog in SharePoint, but Office may keep showing the old fonts. This cmdlet does the three steps for you:

1. If the library is registered as the Office font library, removes the registration. The public CDN origin is kept.
2. Uploads the fonts and the catalog.
3. Registers the library again as OfficeFontLibrary on the public CDN, with its previous thumbnail. This runs even when the upload fails, so the tenant is never left without its font library.

If no Office font library is registered yet, the cmdlet uploads the fonts and then registers the library. Use -KeepRegistration to skip steps 1 and 3 for a library that is already registered.

Office apps can take up to 24 hours to show the fonts after the library is registered again, and they may not show any custom fonts in the meantime. Do not change the font files or catalog folders in SharePoint directly: the catalog would no longer match the files.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPOfficeFontCatalog -FontFolder C:\Fonts -LibraryUrl "https://contoso.sharepoint.com/sites/BrandGuide/Office Fonts"
```

Publishes every .ttf and .otf font in C:\Fonts to the Office font library. Office then offers exactly those fonts.

### EXAMPLE 2
```powershell
Set-PnPOfficeFontCatalog -FontFolder .\fonts -LibraryUrl "/sites/BrandGuide/Office Fonts" -Verbose
```

Same, with a folder relative to the current location and a server-relative library URL. -Verbose shows the catalog version, each uploaded file, and the fonts detected as variable or PostScript.

### EXAMPLE 3
```powershell
Set-PnPOfficeFontCatalog -FontFolder C:\Fonts -WhatIf
```

Shows whether the library would be registered, or its registration removed and added again, without changing anything.

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

### -FontFolder
Folder with every .ttf and .otf font the Office font library should offer. Relative paths start at the current location.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -KeepRegistration
Publishes without removing and adding the Office font library registration again. Use it only when you know Office will pick up the change without it, for example on a test tenant. Has no effect when the library is not registered yet: it is always registered after the upload.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LibraryUrl
URL of the Office font library: absolute ("https://contoso.sharepoint.com/sites/BrandGuide/Office Fonts") or server-relative ("/sites/BrandGuide/Office Fonts"). If a library is registered with OrgAssetType OfficeFontLibrary, it must be that one; any other library fails, because Office never reads it. If none is registered, this library is registered after the upload. When omitted, the library registered as the Office font library (OrgAssetType OfficeFontLibrary) is used. Required when no Office font library is registered yet.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: The registered Office font library
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

[Sync-PnPBrandCenterOfficeFont](Sync-PnPBrandCenterOfficeFont.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
