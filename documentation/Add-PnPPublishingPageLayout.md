---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/add-pnppublishingpagelayout
schema: 2.0.0
title: Add-PnPPublishingPageLayout
---

# Add-PnPPublishingPageLayout

## SYNOPSIS
Adds a publishing page layout

## SYNTAX

```powershell
Add-PnPPublishingPageLayout -SourceFilePath <String> -Title <String> -Description <String>
 -AssociatedContentTypeID <String> [-DestinationFolderHierarchy <String>] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPPublishingPageLayout -Title 'Our custom page layout' -SourceFilePath 'customlayout.aspx' -Description 'A custom page layout' -AssociatedContentTypeID 0x01010901
```

Uploads the pagelayout 'customlayout.aspx' to the current site as a 'web part page' pagelayout

## PARAMETERS

### -AssociatedContentTypeID
Associated content type ID

```yaml
Type: String
Parameter Sets: (All)

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

### -Description
Description for the page layout

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DestinationFolderHierarchy
Folder hierarchy where the html page layouts will be deployed

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SourceFilePath
Path to the file which will be uploaded

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
Title for the page layout

```yaml
Type: String
Parameter Sets: (All)

Required: True
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

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)