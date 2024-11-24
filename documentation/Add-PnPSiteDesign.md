---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPSiteDesign.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPSiteDesign
---

# Add-PnPSiteDesign

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Creates a new Site Design on the current tenant

## SYNTAX

### By SiteScript Instance (Default)

```
Add-PnPSiteDesign -Title <String> -SiteScript <TenantSiteScriptPipeBind> [-Description <String>]
 [-IsDefault] [-PreviewImageAltText <String>] [-PreviewImageUrl <String>]
 [-WebTemplate <SiteWebTemplate>] [-ThumbnailUrl <String>] [-DesignPackageId <Guid>]
 [-Connection <PnPConnection>]
```

### By SiteScript Ids

```
Add-PnPSiteDesign -Title <String> -SiteScriptIds <Guid[]> [-Description <String>] [-IsDefault]
 [-PreviewImageAltText <String>] [-PreviewImageUrl <String>] [-WebTemplate <SiteWebTemplate>]
 [-ThumbnailUrl <String>] [-DesignPackageId <Guid>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add a new Site Design on the current tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPSiteDesign -Title "My Company Design" -SiteScriptIds "e84dcb46-3ab9-4456-a136-66fc6ae3d3c5","6def687f-0e08-4f1e-999c-791f3af9a600" -Description "My description" -WebTemplate TeamSite
```

Adds a new Site Design, with the specified title and description. When applied it will run the scripts as referenced by the IDs. Use Get-PnPSiteScript to receive Site Scripts. The WebTemplate parameter specifies that this design applies to modern Team Sites.

### EXAMPLE 2

```powershell
Add-PnPSiteDesign -Title "My Company Design" -SiteScriptIds "e84dcb46-3ab9-4456-a136-66fc6ae3d3c5","6def687f-0e08-4f1e-999c-791f3af9a600" -Description "My description" -WebTemplate TeamSite -ThumbnailUrl https://contoso.sharepoint.com/sites/templates/siteassets/logo.png
```

Adds a new Site Design, with the specified title, description and logo to be shown in the template picker. When applied it will run the scripts as referenced by the IDs. Use Get-PnPSiteScript to receive Site Scripts. The WebTemplate parameter specifies that this design applies to modern Team Sites.

### EXAMPLE 3

```powershell
Add-PnPSiteDesign -Title "My Company Design" -SiteScriptIds "e84dcb46-3ab9-4456-a136-66fc6ae3d3c5","6def687f-0e08-4f1e-999c-791f3af9a600" -Description "My description" -WebTemplate TeamSite -ThumbnailUrl "https://contoso.sharepoint.com/sites/templates/my images/logo.png"
```

Adds a new Site Design, with the specified title, description and logo to be shown in the template picker. When applied it will run the scripts as referenced by the IDs. Use Get-PnPSiteScript to receive Site Scripts. The WebTemplate parameter specifies that this design applies to modern Team Sites. Notice that when the location for the ThumbnailUrl contains a space, it should be provided URL decoded, so i.e. no %20 for spaces.

### EXAMPLE 4

```powershell
Get-PnPSiteScriptFromWeb -IncludeAll | Add-PnPSiteScript -Title "My Site Script" | Add-PnPSiteDesign -Title "My Site Design" -WebTemplate TeamSite
```

Adds a new Site Design based on the currently connected to site, with the specified title to be shown in the template picker.

## PARAMETERS

### -Confirm

Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- cf
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

### -Description

The description of the site design

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

### -DesignPackageId

Sets the design package Id of this site design.

```yaml
Type: Guid
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

### -IsDefault

Specifies that the site design is a default site design

```yaml
Type: SwitchParameter
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

### -PreviewImageAltText

Sets the text for the preview image. This was used in the old site designs approach and currently has no function anymore.

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

### -PreviewImageUrl

Sets the url to the preview image. This was used in the old site designs approach and currently has no function anymore. Use ThumbnailUrl instead.

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

### -SiteScript

An instance, id or title of a site script to use for the site design

```yaml
Type: TenantSiteScriptPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By SiteScript Instance
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SiteScriptIds

An array of guids of site scripts to use for the site design

```yaml
Type: Guid[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By SiteScript Ids
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ThumbnailUrl

The full URL of a thumbnail image, i.e. https://contoso.sharepoint/siteassets/image.png. If none is specified, SharePoint uses a generic image. Recommended size is 400 x 300 pixels. This is the image that will be shown when selecting a template through "Apply a site template" or "Browse templates" shown in "Start designing your site" shown when creating a new site. If there are spaces in the URL, do not URL encode them, so i.e. do not use %20 where there is a space, but instead just provide the link with the space inside.

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

### -Title

The title of the site design

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

### -WebTemplate

Specifies the type of site to which this design applies

```yaml
Type: SiteWebTemplate
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
AcceptedValues:
- TeamSite
- CommunicationSite
- GrouplessTeamSite
- ChannelSite
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
