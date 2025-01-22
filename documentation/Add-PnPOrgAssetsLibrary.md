---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPOrgAssetsLibrary.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPOrgAssetsLibrary
---
  
# Add-PnPOrgAssetsLibrary

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Adds a given document library as an organizational asset source

## SYNTAX

```powershell
Add-PnPOrgAssetsLibrary -LibraryUrl <String> [-ThumbnailUrl <String>] [-CdnType <SPOTenantCdnType>] [-OrgAssetType <OrgAssetType>] [-DefaultOriginAdded <bool>] [-IsCopilotSearchable <bool>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Adds a given document library as an organizational asset source in your SharePoint Online Tenant. You can specify multiple libraries, but all organizational asset sources you add must reside in the same site collection.

Document libraries specified as organizational asset must be enabled as an Office 365 CDN source, either as private or public.

The libraries must also have read rights for 'Everyone except external users' enabled on them. Either on library or on the entire site level.

Only entire libraries can be configured as an organizational asset, folders cannot.

It may take some time before this change will be reflected in the web interface.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPOrgAssetsLibrary -LibraryUrl "https://yourtenant.sharepoint.com/sites/branding/logos"
```

Adds the document library with the url "logos" located in the sitecollection at "https://yourtenant.sharepoint.com/sites/branding" as an organizational asset not specifying a thumbnail image for it and enabling the document library as a public Office 365 CDN source

### EXAMPLE 2
```powershell
Add-PnPOrgAssetsLibrary -LibraryUrl "https://yourtenant.sharepoint.com/sites/branding/logos" -ThumbnailUrl "https://yourtenant.sharepoint.com/sites/branding/logos/thumbnail.jpg"
```

Adds the document library with the url "logos" located in the sitecollection at "https://yourtenant.sharepoint.com/sites/branding" as an organizational asset specifying the thumbnail image "thumbnail.jpg" residing in the same document library for it and enabling the document library as a public Office 365 CDN source

### EXAMPLE 3
```powershell
Add-PnPOrgAssetsLibrary -LibraryUrl "https://yourtenant.sharepoint.com/sites/branding/logos" -CdnType Private
```

Adds the document library with the url "logos" located in the sitecollection at "https://yourtenant.sharepoint.com/sites/branding" as an organizational asset not specifying a thumbnail image for it and enabling the document library as a private Office 365 CDN source

## PARAMETERS

### -CdnType
Indicates what type of Office 365 CDN source the document library will be added to

```yaml
Type: SPOTenantCdnType
Parameter Sets: (All)
Accepted values: Public, Private

Required: False
Position: Named
Default value: Public
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

### -DefaultOriginAdded
Indicates that if the OFfice 365 CDN would not be enabled yet for the tenant, that it should be enabled and [the default origins](https://learn.microsoft.com/microsoft-365/enterprise/use-microsoft-365-cdn-with-spo?view=o365-worldwide#default-cdn-origins) should be added to the tenant. This is only applicable when the CDN has not been enabled yet on the tenant.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: True
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsCopilotSearchable
Indicates that the organizational assets library should be searchable in the CoPilot search experience in Office applications to locate corporate images. Only works when the OrgAssetType is set to ImageDocumentLibrary.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: True
Accept pipeline input: False
Accept wildcard characters: False
```

### -LibraryUrl
The full url of the document library to be marked as one of organization's assets sources

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OrgAssetType
Indicates the type of content in this library.

ImageDocumentLibrary is the default OrgAssetType and is best used for images. You can access the contents of this library from any site or page in the SharePoint filepicker. OfficeTemplateLibrary is the suggested type for Office files and will show up in the UI of all Office desktop apps and Office online in the templates section.

```yaml
Type: OrgAssetType
Parameter Sets: (All)
Accepted values: ImageDocumentLibrary, OfficeTemplateLibrary, BrandColorsList, BrandFontsLibrary, BrandKitLibrary, OfficeFontLibrary, Undefined

Required: False
Position: Named
Default value: ImageDocumentLibrary
Accept pipeline input: False
Accept wildcard characters: False
```

### -ThumbnailUrl
The full url to an image that should be used as a thumbnail for showing this source. The image must reside in the same site as the document library you specify.

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
