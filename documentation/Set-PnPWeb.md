---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/set-pnpweb
schema: 2.0.0
title: Set-PnPWeb
---

# Set-PnPWeb

## SYNOPSIS
Sets properties on a web

## SYNTAX

```
Set-PnPWeb [-SiteLogoUrl <String>] [-AlternateCssUrl <String>] [-Title <String>] [-Description <String>]
 [-MasterUrl <String>] [-CustomMasterUrl <String>] [-QuickLaunchEnabled] [-MembersCanShare] [-NoCrawl]
 [-HeaderLayout <HeaderLayoutType>] [-HeaderEmphasis <SPVariantThemeType>] [-NavAudienceTargetingEnabled]
 [-MegaMenuEnabled] [-DisablePowerAutomate] [-CommentsOnSitePagesDisabled] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Allows setting various properties on a web

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPWeb -CommentsOnSitePagesDisabled:$true
```

Disables the page comments to be shown below each page in the current web by default

### EXAMPLE 2
```powershell
Set-PnPWeb -QuickLaunchEnabled:$false
```

Hides the quick launch from being shown in the current web

### EXAMPLE 3
```powershell
Set-PnPWeb -NoCrawl:$true
```

Prevents the current web from being returned in search results

## PARAMETERS

### -AlternateCssUrl
Sets the AlternateCssUrl of the web. Only works for classic pages.

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

### -CommentsOnSitePagesDisabled
Defines if comments on modern site pages should be enabled by default ($false) or they should be hidden ($true)

Only applicable to: SharePoint Online

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

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

### -CustomMasterUrl
Sets the CustomMasterUrl of the web. Only works for classic pages.

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

### -Description
Sets the description of the web

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

### -DisablePowerAutomate
Defines if Power Automate should be available on lists and document libraries ($false) or if the option should be hidden ($true)

Only applicable to: SharePoint Online

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HeaderEmphasis

Only applicable to: SharePoint Online

```yaml
Type: SPVariantThemeType
Parameter Sets: (All)
Aliases:
Accepted values: None, Neutral, Soft, Strong

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HeaderLayout

Only applicable to: SharePoint Online

```yaml
Type: HeaderLayoutType
Parameter Sets: (All)
Aliases:
Accepted values: None, Standard, Compact, Minimal, Extended

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MasterUrl
Sets the MasterUrl of the web. Only works for classic pages.

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

### -MegaMenuEnabled
Defines if the navigation menu should be shown as the mega menu ($true) or the smaller sized menu ($false)

Only applicable to: SharePoint Online

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MembersCanShare
Indicates if members of this site can share the site and individual sites with others ($true) or only owners can do this ($false)

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NavAudienceTargetingEnabled
Defines if the navigation menu on a modern site should be enabled for modern audience targeting ($true) or not ($false)

Only applicable to: SharePoint Online

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoCrawl
Indicates if this site should not be returned in search results ($true) or if it should be ($false)

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -QuickLaunchEnabled
Defines if the quick launch menu on the left side of modern Team Sites should be shown ($true) or hidden ($false)

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteLogoUrl
Sets the logo of the web to the current url. If you want to set the logo to a modern team site, use Set-PnPSite -LogoFilePath.

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

### -Title
Sets the title of the web

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

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
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