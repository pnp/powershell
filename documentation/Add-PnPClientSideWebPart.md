---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/add-pnpclientsidewebpart
schema: 2.0.0
title: Add-PnPClientSideWebPart
---

# Add-PnPClientSideWebPart

## SYNOPSIS
Adds a Client-Side Web Part to a client-side page

## SYNTAX

### Default with built-in web part
```powershell
Add-PnPClientSideWebPart [-Page] <ClientSidePagePipeBind> -DefaultWebPartType <DefaultClientSideWebParts>
 [-WebPartProperties <PropertyBagPipeBind>] [-Order <Int32>] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### Default with 3rd party web part
```powershell
Add-PnPClientSideWebPart [-Page] <ClientSidePagePipeBind> -Component <ClientSideComponentPipeBind>
 [-WebPartProperties <PropertyBagPipeBind>] [-Order <Int32>] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

### Positioned with built-in web part
```powershell
Add-PnPClientSideWebPart [-Page] <ClientSidePagePipeBind> -DefaultWebPartType <DefaultClientSideWebParts>
 [-WebPartProperties <PropertyBagPipeBind>] [-Order <Int32>] -Section <Int32> -Column <Int32>
 [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

### Positioned with 3rd party web part
```powershell
Add-PnPClientSideWebPart [-Page] <ClientSidePagePipeBind> -Component <ClientSideComponentPipeBind>
 [-WebPartProperties <PropertyBagPipeBind>] [-Order <Int32>] -Section <Int32> -Column <Int32>
 [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Adds a client-side web part to an existing client-side page.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPClientSideWebPart -Page "MyPage" -DefaultWebPartType BingMap
```

Adds a built-in Client-Side component 'BingMap' to the page called 'MyPage'

### EXAMPLE 2
```powershell
Add-PnPClientSideWebPart -Page "MyPage" -Component "HelloWorld"
```

Adds a Client-Side component 'HelloWorld' to the page called 'MyPage'

### EXAMPLE 3
```powershell
Add-PnPClientSideWebPart  -Page "MyPage" -Component "HelloWorld" -Section 1 -Column 2
```

Adds a Client-Side component 'HelloWorld' to the page called 'MyPage' in section 1 and column 2

## PARAMETERS

### -Column
Sets the column where to insert the web part control.

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: Int32
Parameter Sets: Positioned with built-in web part, Positioned with 3rd party web part
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Component
Specifies the component instance or Id to add.

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: ClientSideComponentPipeBind
Parameter Sets: Default with 3rd party web part, Positioned with 3rd party web part
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

Only applicable to: SharePoint Online, SharePoint Server 2019

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

### -DefaultWebPartType
Defines a default web part type to insert.

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: DefaultClientSideWebParts
Parameter Sets: Default with built-in web part, Positioned with built-in web part
Aliases:
Accepted values: ThirdParty, ContentRollup, BingMap, ContentEmbed, DocumentEmbed, Image, ImageGallery, LinkPreview, NewsFeed, NewsReel, News, PowerBIReportEmbed, QuickChart, SiteActivity, VideoEmbed, YammerEmbed, Events, GroupCalendar, Hero, List, PageTitle, People, QuickLinks, CustomMessageRegion, Divider, MicrosoftForms, Spacer, ClientWebPart, PowerApps, CodeSnippet, PageFields, Weather, YouTube, MyDocuments, YammerFullFeed, CountDown, ListProperties, MarkDown, Planner, Sites, CallToAction, Button

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Order
Sets the order of the web part control. (Default = 1)

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Page
The name of the page.

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: ClientSidePagePipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Section
Sets the section where to insert the web part control.

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: Int32
Parameter Sets: Positioned with built-in web part, Positioned with 3rd party web part
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

Only applicable to: SharePoint Online, SharePoint Server 2019

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

### -WebPartProperties
The properties of the web part

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: PropertyBagPipeBind
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