---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPSiteDesignFromWeb.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPSiteDesignFromWeb
---
  
# Add-PnPSiteDesignFromWeb

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Creates a new Site Design on the current tenant based on the site provided through -Url or the currently connected to site if -Url is omitted

## SYNTAX

```powershell
Add-PnPSiteDesignFromWeb -Title <String> [-Description <String>] [-IsDefault]
 [-PreviewImageAltText <String>] [-PreviewImageUrl <String>] [-WebTemplate <SiteWebTemplate>]
 [-ThumbnailUrl <String>] [-DesignPackageId <Guid>] [-Lists <String[]>] [-IncludeBranding] [-IncludeLinksToExportedItems]
 [-IncludeRegionalSettings] [-IncludeSiteExternalSharingCapability] [-IncludeTheme]
 [-Connection <PnPConnection>]
```

## DESCRIPTION

Creates a new Site Design on the current tenant based on the site provided through -Url or the currently connected to site if -Url is omitted. It combines the steps of `Get-PnPSiteScriptFromWeb`, `Add-PnPSiteScript` and `Add-PnPSiteDesign` into one cmdlet. The information returned from running the cmdlet is the information of the Site Design that has been created.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPSiteDesignFromWeb -Title "My Company Design" -Description "My description" -WebTemplate TeamSite -IncludeAll
```

Generates a site script based on all the components of the currently connected to site, excluding its lists and libraries and based on the generated script it will create a site script and a site design with the specified title and description for modern team sites.

### EXAMPLE 2
```powershell
Add-PnPSiteDesignFromWeb -Title "My Company Design" -Description "My description" -WebTemplate TeamSite -IncludeAll -Lists ("/lists/Issue list", "Shared Documents)
```

Generates a site script based on all the components of the currently connected to site, including the list "Issue list" and the default document library "Shared Documents" and based on the generated script it will create a site script and a site design with the specified title and description for modern team sites.

### EXAMPLE 3
```powershell
Add-PnPSiteDesignFromWeb -Title "My Company Design" -Description "My description" -WebTemplate TeamSite -Lists "/lists/Issue list" -ThumbnailUrl https://contoso.sharepoint.com/SiteAssets/logo.png
```

Generates a site script based on the list "Issue list" in the current site and based on the generated script it will create a site script and a site design with the specified title, description and logo for modern team sites.

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
The full URL of a thumbnail image, i.e. https://contoso.sharepoint/siteassets/image.png. If none is specified, SharePoint uses a generic image. Recommended size is 400 x 300 pixels. This is the image that will be shown when selecting a template through "Apply a site template" or "Browse templates" shown in "Start designing your site" shown when creating a new site.

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
Sets the design package Id of this site design

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

### -IncludeAll
If specified will include all supported components into the Site Script except for the lists and document libraries, these need to be explicitly be specified through -Lists

```yaml
Type: SwitchParameter
Parameter Sets: All components

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeBranding
If specified will include the branding of the site into the Site Script

```yaml
Type: SwitchParameter
Parameter Sets: Specific components

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeLinksToExportedItems
If specified will include navigation links into the Site Script

```yaml
Type: SwitchParameter
Parameter Sets: Specific components

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeRegionalSettings
If specified will include the regional settings into the Site Script

```yaml
Type: SwitchParameter
Parameter Sets: Specific components

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeSiteExternalSharingCapability
If specified will include the external sharing configuration into the Site Script

```yaml
Type: SwitchParameter
Parameter Sets: Specific components

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeTheme
If specified will include the branding of the site into the Site Script

```yaml
Type: SwitchParameter
Parameter Sets: Specific components

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Lists
Allows specifying one or more site relative URLs of lists that should be included into the Site Script, i.e. "Shared Documents","List\MyList"

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)