---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPOrgAssetsLibrary.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPOrgAssetsLibrary
---

# Set-PnPOrgAssetsLibrary

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Updates a document library which was already defined as an organizational asset source

## SYNTAX

### Default (Default)

```
Set-PnPOrgAssetsLibrary -LibraryUrl <String> [-ThumbnailUrl <String>] [-OrgAssetType <OrgAssetType>]
 [-IsCopilotSearchable <bool>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

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
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IsCopilotSearchable

Indicates that the organizational assets library should be searchable in the CoPilot search experience in Office applications to locate corporate images. Only works when the OrgAssetType is set to ImageDocumentLibrary.

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -LibraryUrl

The full url of the document library to be marked as one of organization's assets sources

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -OrgAssetType

Indicates the type of content in this library. Currently supported values are "ImageDocumentLibrary" and "OfficeTemplateLibrary".

ImageDocumentLibrary is the default OrgAssetType and is best used for images. You can access the contents of this library from any site or page in the SharePoint filepicker. OfficeTemplateLibrary is the suggested type for Office files and will show up in the UI of all Office desktop apps and Office online in the templates section.

```yaml
Type: OrgAssetType
DefaultValue: ImageDocumentLibrary
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- ImageDocumentLibrary
- OfficeTemplateLibrary
HelpMessage: ''
```

### -ThumbnailUrl

The full url to an image that should be used as a thumbnail for showing this source. The image must reside in the same site as the document library you specify.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
