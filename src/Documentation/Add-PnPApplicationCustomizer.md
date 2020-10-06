---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/add-pnpapplicationcustomizer
schema: 2.0.0
title: Add-PnPApplicationCustomizer
---

# Add-PnPApplicationCustomizer

## SYNOPSIS
Adds a SharePoint Framework client side extension application customizer

## SYNTAX

```
Add-PnPApplicationCustomizer [-Title <String>] [-Description <String>] [-Sequence <Int32>]
 [-Scope <CustomActionScope>] -ClientSideComponentId <GuidPipeBind> [-ClientSideComponentProperties <String>]
 [-ClientSideHostProperties <String>] [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Adds a SharePoint Framework client side extension application customizer by registering a user custom action to a web or sitecollection

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPApplicationCustomizer -Title "CollabFooter" -ClientSideComponentId c0ab3b94-8609-40cf-861e-2a1759170b43 -ClientSideComponentProperties "{`"sourceTermSet`":`"PnP-CollabFooter-SharedLinks`",`"personalItemsStorageProperty`":`"PnP-CollabFooter-MyLinks`"}
```

Adds a new application customizer to the current web. This requires that a SharePoint Framework solution has been deployed containing the application customizer specified in its manifest. Be sure to run Install-PnPApp before trying this cmdlet on a site.

## PARAMETERS

### -ClientSideComponentId
The Client Side Component Id of the SharePoint Framework client side extension application customizer found in the manifest

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: GuidPipeBind
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientSideComponentProperties
The Client Side Component Properties of the application customizer. Specify values as a json string : "{Property1 : 'Value1', Property2: 'Value2'}"

Only applicable to: SharePoint Online, SharePoint Server 2019

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

### -ClientSideHostProperties
The Client Side Host Properties of the application customizer. Specify values as a json string : "{'preAllocatedApplicationCustomizerTopHeight': '50', 'preAllocatedApplicationCustomizerBottomHeight': '50'}"

Only applicable to: SharePoint Online

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

### -Description
The description of the application customizer

Only applicable to: SharePoint Online, SharePoint Server 2019

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

### -Scope
The scope of the CustomAction to add to. Either Web or Site; defaults to Web. 'All' is not valid for this command.

Only applicable to: SharePoint Online, SharePoint Server 2019

```yaml
Type: CustomActionScope
Parameter Sets: (All)
Aliases:
Accepted values: Web, Site, All

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Sequence
Sequence of this application customizer being injected. Use when you have a specific sequence with which to have multiple application customizers being added to the page.

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

### -Title
The title of the application customizer

Only applicable to: SharePoint Online, SharePoint Server 2019

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

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)