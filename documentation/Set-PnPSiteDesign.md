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
Sets the text for the preview image

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
Sets the url to the preview image

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
The URL of a thumbnail image. If none is specified, SharePoint uses a generic image. Recommended size is 400 x 300 pixels.

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

