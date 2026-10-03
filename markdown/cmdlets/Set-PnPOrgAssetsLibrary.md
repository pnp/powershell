---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPOrgAssetsLibrary.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPOrgAssetsLibrary
---
  
# Set-PnPOrgAssetsLibrary

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Updates a document library which was already defined as an organizational asset source

## SYNTAX

```powershell
Set-PnPOrgAssetsLibrary -LibraryUrl <String> [-ThumbnailUrl <String>] [-OrgAssetType <OrgAssetType>] [-IsCopilotSearchable <bool>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Updates a document library which was already set as an organizational asset source in your SharePoint Online Tenant.

It may take some time before this change will be reflected in the web interface.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPOrgAssetsLibrary -LibraryUrl "https://yourtenant.sharepoint.com/sites/branding/logos" -ThumbnailUrl "https://yourtenant.sharepoint.com/sites/branding/logos/thumbnail.jpg"
```

Updates the tumbnail for the document library with the url "logos" located in the sitecollection at "https://yourtenant.sharepoint.com/sites/branding"

### EXAMPLE 2
```powershell
Set-PnPOrgAssetsLibrary -LibraryUrl "https://yourtenant.sharepoint.com/sites/branding/logos" -IsCopilotSearchable:$true
```

Enables Microsoft 365 Copilot to use the organizational assets library with the url "logos" located in the sitecollection at "https://yourtenant.sharepoint.com/sites/branding" for searching corporate images

## PARAMETERS

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

### -IsCopilotSearchable
Indicates that the organizational assets library should be searchable in the CoPilot search experience in Office applications to locate corporate images. Only works when the OrgAssetType is set to ImageDocumentLibrary.

```yaml
Type: Boolean
Parameter Sets: (All)

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

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OrgAssetType
Indicates the type of content in this library. Currently supported values are "ImageDocumentLibrary" and "OfficeTemplateLibrary".

ImageDocumentLibrary is the default OrgAssetType and is best used for images. You can access the contents of this library from any site or page in the SharePoint filepicker. OfficeTemplateLibrary is the suggested type for Office files and will show up in the UI of all Office desktop apps and Office online in the templates section.

```yaml
Type: OrgAssetType
Parameter Sets: (All)
Accepted values: ImageDocumentLibrary, OfficeTemplateLibrary

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