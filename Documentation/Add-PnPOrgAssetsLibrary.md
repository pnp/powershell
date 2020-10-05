---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/add-pnporgassetslibrary
schema: 2.0.0
title: Add-PnPOrgAssetsLibrary
---

# Add-PnPOrgAssetsLibrary

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Adds a given document library as a organizational asset source

## SYNTAX

```
Add-PnPOrgAssetsLibrary -LibraryUrl <String> [-ThumbnailUrl <String>] [-CdnType <SPOTenantCdnType>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Adds a given document library as an organizational asset source in your Sharepoint Online Tenant. All organizational asset sources you add must reside in the same site collection. Document libraries specified as organizational asset must be enabled as an Office 365 CDN source, either as private or public. It may take some time before this change will be reflected in the webinterface.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPOrgAssetsLibrary -LibraryUrl https://yourtenant.sharepoint.com/sites/branding/logos
```

Adds the document library with the url "logos" located in the sitecollection at "https://yourtenant.sharepoint.com/sites/branding" as an organizational asset not specifying a thumbnail image for it and enabling the document library as a public Office 365 CDN source

### EXAMPLE 2
```powershell
Add-PnPOrgAssetsLibrary -LibraryUrl https://yourtenant.sharepoint.com/sites/branding/logos -ThumbnailUrl https://yourtenant.sharepoint.com/sites/branding/logos/thumbnail.jpg
```

Adds the document library with the url "logos" located in the sitecollection at "https://yourtenant.sharepoint.com/sites/branding" as an organizational asset specifying the thumbnail image "thumbnail.jpg" residing in the same document library for it and enabling the document library as a public Office 365 CDN source

### EXAMPLE 3
```powershell
Add-PnPOrgAssetsLibrary -LibraryUrl https://yourtenant.sharepoint.com/sites/branding/logos -CdnType Private
```

Adds the document library with the url "logos" located in the sitecollection at "https://yourtenant.sharepoint.com/sites/branding" as an organizational asset not specifying a thumbnail image for it and enabling the document library as a private Office 365 CDN source

## PARAMETERS

### -CdnType
Indicates what type of Office 365 CDN source the document library will be added to

```yaml
Type: SPOTenantCdnType
Parameter Sets: (All)
Aliases:
Accepted values: Public, Private

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
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LibraryUrl
The full url of the document library to be marked as one of organization's assets sources

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ThumbnailUrl
The full url to an image that should be used as a thumbnail for showing this source. The image must reside in the same site as the document library you specify.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)