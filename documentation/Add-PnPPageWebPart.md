---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPPageWebPart.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPPageWebPart
---
  
# Add-PnPPageWebPart

## SYNOPSIS
Adds a web part to a page

## SYNTAX

### Default with built-in web part
```powershell
Add-PnPPageWebPart [-Page] <PagePipeBind> -DefaultWebPartType <DefaultClientSideWebParts>
 [-WebPartProperties <PropertyBagPipeBind>] [-Order <Int32>] [-Connection <PnPConnection>]
 
```

### Default with 3rd party web part
```powershell
Add-PnPPageWebPart [-Page] <PagePipeBind> -Component <PageComponentPipeBind>
 [-WebPartProperties <PropertyBagPipeBind>] [-Order <Int32>] [-Connection <PnPConnection>]
 
```

### Positioned with built-in web part
```powershell
Add-PnPPageWebPart [-Page] <PagePipeBind> -DefaultWebPartType <DefaultClientSideWebParts>
 [-WebPartProperties <PropertyBagPipeBind>] [-Order <Int32>] -Section <Int32> -Column <Int32>
 [-Connection <PnPConnection>] 
```

### Positioned with 3rd party web part
```powershell
Add-PnPPageWebPart [-Page] <PagePipeBind> -Component <PageComponentPipeBind>
 [-WebPartProperties <PropertyBagPipeBind>] [-Order <Int32>] -Section <Int32> -Column <Int32>
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Adds a client-side web part to an existing client-side page.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPPageWebPart -Page "MyPage" -DefaultWebPartType BingMap
```

Adds a built-in component 'BingMap' to the page called 'MyPage'

### EXAMPLE 2
```powershell
Add-PnPPageWebPart -Page "MyPage" -Component "HelloWorld"
```

Adds a component 'HelloWorld' to the page called 'MyPage'

### EXAMPLE 3
```powershell
Add-PnPPageWebPart -Page "MyPage" -Component "HelloWorld" -Section 1 -Column 2
```

Adds a component 'HelloWorld' to the page called 'MyPage' in section 1 and column 2

## PARAMETERS

### -Column
Sets the column where to insert the web part control.

```yaml
Type: Int32
Parameter Sets: Positioned with built-in web part, Positioned with 3rd party web part

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Component
Specifies the component instance or Id to add.

```yaml
Type: PageComponentPipeBind
Parameter Sets: Default with 3rd party web part, Positioned with 3rd party web part

Required: True
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

### -DefaultWebPartType
Defines a default web part type to insert.

```yaml
Type: DefaultClientSideWebParts
Parameter Sets: Default with built-in web part, Positioned with built-in web part
Accepted values: ThirdParty, ContentRollup, BingMap, ContentEmbed, DocumentEmbed, Image, ImageGallery, LinkPreview, NewsFeed, NewsReel, News, PowerBIReportEmbed, QuickChart, SiteActivity, VideoEmbed, YammerEmbed, Events, GroupCalendar, Hero, List, PageTitle, People, QuickLinks, CustomMessageRegion, Divider, MicrosoftForms, Spacer, ClientWebPart, PowerApps, CodeSnippet, PageFields, Weather, YouTube, MyDocuments, YammerFullFeed, CountDown, ListProperties, MarkDown, Planner, Sites, CallToAction, Button

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Order
Sets the order of the web part control. (Default = 1)

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Page
The name of the page.

```yaml
Type: PagePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Section
Sets the section where to insert the web part control.

```yaml
Type: Int32
Parameter Sets: Positioned with built-in web part, Positioned with 3rd party web part

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



### -WebPartProperties
The properties of the web part

```yaml
Type: PropertyBagPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


