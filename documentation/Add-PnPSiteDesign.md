---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPSiteDesign.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPSiteDesign
---
  
# Add-PnPSiteDesign

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Creates a new Site Design on the current tenant

## SYNTAX

### By SiteScript Instance (Default)

```powershell
Add-PnPSiteDesign -Title <String> -SiteScript <TenantSiteScriptPipeBind> [-Description <String>] [-IsDefault]
 [-PreviewImageAltText <String>] [-PreviewImageUrl <String>] [-WebTemplate <SiteWebTemplate>]
 [-ThumbnailUrl <String>] [-DesignPackageId <Guid>] [-Connection <PnPConnection>]
```

### By SiteScript Ids

```powershell
Add-PnPSiteDesign -Title <String> -SiteScriptIds <Guid[]> [-Description <String>] [-IsDefault]
 [-PreviewImageAltText <String>] [-PreviewImageUrl <String>] [-WebTemplate <SiteWebTemplate>]
 [-ThumbnailUrl <String>] [-DesignPackageId <Guid>] [-Connection <PnPConnection>]
```

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
Parameter Sets: (All)
Aliases: cf

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

### -IsDefault
Specifies that the site design is a default site design

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

### -SiteScript
An instance, id or title of a site script to use for the site design

```yaml
Type: TenantSiteScriptPipeBind
Parameter Sets: By SiteScript Instance

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -SiteScriptIds
An array of guids of site scripts to use for the site design

```yaml
Type: Guid[]
Parameter Sets: By SiteScript Ids

Required: True
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

Required: True
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

Required: True
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
Type: Guid
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
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

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)