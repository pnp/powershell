---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteDesign.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPSiteDesign
---

# Set-PnPSiteDesign

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Updates a site design on the current tenant.

## SYNTAX

### Default (Default)

```
Set-PnPSiteDesign -Identity <TenantSiteDesignPipeBind> [-Title <String>]
 [-SiteScriptIds <GuidPipeBind[]>] [-Description <String>] [-IsDefault]
 [-PreviewImageAltText <String>] [-PreviewImageUrl <String>] [-WebTemplate <SiteWebTemplate>]
 [-Version <Int32>] [-ThumbnailUrl <String>] [-DesignPackageId <Guid>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to update a site design on the current tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPSiteDesign -Identity 046e2e76-67ba-46ca-a5f6-8eb418a7821e -Title "My Updated Company Design"
```

Updates an existing site design and sets a new title.

### EXAMPLE 2

```powershell
$design = Get-PnPSiteDesign -Identity 046e2e76-67ba-46ca-a5f6-8eb418a7821e
Set-PnPSiteDesign -Identity $design -Title "My Updated Company Design"
```

Updates an existing site design and sets a new title.

### EXAMPLE 3

```powershell
Set-PnPSiteDesign -Identity 046e2e76-67ba-46ca-a5f6-8eb418a7821e -Title "My Company Design" -Description "My description" -ThumbnailUrl "https://contoso.sharepoint.com/sites/templates/my images/logo.png"
```

Updates an existing site design, providing a new title, description and logo to be shown in the template picker. Notice that when the location for the ThumbnailUrl contains a space, it should be provided URL decoded, so i.e. no %20 for spaces.

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

### -Description

The description of the site design.

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

### -Identity

The guid or an object representing the site design.

```yaml
Type: TenantSiteDesignPipeBind
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

### -IsDefault

Specifies if the site design is a default site design.

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

### -SiteScriptIds

An array of guids of site scripts.

```yaml
Type: Guid[]
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

The title of the site design.

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

### -Version

Specifies the version of the design.

```yaml
Type: Int32
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

### -WebTemplate

Specifies the type of site to which this design applies.

```yaml
Type: SiteWebTemplate
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
