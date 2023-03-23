---
Module Name: PnP.PowerShell
title: Set-PnPSiteDesign
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteDesign.html
---
 
# Set-PnPSiteDesign

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Updates a Site Design on the current tenant.

## SYNTAX

```powershell
Set-PnPSiteDesign -Identity <TenantSiteDesignPipeBind> [-Title <String>] [-SiteScriptIds <GuidPipeBind[]>]
 [-Description <String>] [-IsDefault] [-PreviewImageAltText <String>] [-PreviewImageUrl <String>]
 [-WebTemplate <SiteWebTemplate>] [-Version <Int32>] [-ThumbnailUrl <String>] [-DesignPackageId <Guid>] [-Connection <PnPConnection>]  
 [<CommonParameters>]
```

## DESCRIPTION

Allows to update a Site Design on the current tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSiteDesign -Identity 046e2e76-67ba-46ca-a5f6-8eb418a7821e -Title "My Updated Company Design"
```

Updates an existing Site Design and sets a new title.

### EXAMPLE 2
```powershell
$design = Get-PnPSiteDesign -Identity 046e2e76-67ba-46ca-a5f6-8eb418a7821e
Set-PnPSiteDesign -Identity $design -Title "My Updated Company Design"
```

Updates an existing Site Design and sets a new title.

### EXAMPLE 3
```powershell
Set-PnPSiteDesign -Identity 046e2e76-67ba-46ca-a5f6-8eb418a7821e -Title "My Company Design" -Description "My description" -ThumbnailUrl "https://contoso.sharepoint.com/sites/templates/my images/logo.png"
```

Updates an existing Site Design, providing a new title, description and logo to be shown in the template picker. Notice that when the location for the ThumbnailUrl contains a space, it should be provided URL decoded, so i.e. no %20 for spaces.

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

### -Description
The description of the site design

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The guid or an object representing the site design

```yaml
Type: TenantSiteDesignPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsDefault
Specifies if the site design is a default site design

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PreviewImageAltText
Sets the text for the preview image. This was used in the old site designs approach and currently has no function anymore.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PreviewImageUrl
Sets the url to the preview image. This was used in the old site designs approach and currently has no function anymore. Use ThumbnailUrl instead.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteScriptIds
An array of guids of site scripts

```yaml
Type: Guid[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The title of the site design

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Version
Specifies the version of the design

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WebTemplate
Specifies the type of site to which this design applies

```yaml
Type: SiteWebTemplate
Parameter Sets: (All)
Accepted values: TeamSite, CommunicationSite, GrouplessTeamSite, ChannelSite

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ThumbnailUrl
The full URL of a thumbnail image, i.e. https://contoso.sharepoint/siteassets/image.png. If none is specified, SharePoint uses a generic image. Recommended size is 400 x 300 pixels. This is the image that will be shown when selecting a template through "Apply a site template" or "Browse templates" shown in "Start designing your site" shown when creating a new site. If there are spaces in the URL, do not URL encode them, so i.e. do not use %20 where there is a space, but instead just provide the link with the space inside.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DesignPackageId
Sets the design package Id of this site design.

```yaml
Type: SiteWebTemplate
Parameter Sets: (All)
Accepted values: TeamSite, CommunicationSite

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

