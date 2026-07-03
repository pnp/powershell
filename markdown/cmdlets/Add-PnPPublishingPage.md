---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPPublishingPage.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPPublishingPage
---
  
# Add-PnPPublishingPage

## SYNOPSIS
Adds a publishing page

## SYNTAX

```powershell
Add-PnPPublishingPage -PageName <String> [-FolderPath <String>] -PageTemplateName <String> [-Title <String>]
 [-Publish] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add a publishing page.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPPublishingPage -PageName 'OurNewPage' -Title 'Our new page' -PageTemplateName 'ArticleLeft'
```

Creates a new page based on the pagelayout 'ArticleLeft'

### EXAMPLE 2
```powershell
Add-PnPPublishingPage -PageName 'OurNewPage' -Title 'Our new page' -PageTemplateName 'ArticleLeft' -Folder '/Pages/folder'
```

Creates a new page based on the pagelayout 'ArticleLeft' with a site relative folder path

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

### -FolderPath
The site relative folder path of the page to be added

```yaml
Type: String
Parameter Sets: (All)
Aliases: Folder

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PageName
The name of the page to be added as an aspx file

```yaml
Type: String
Parameter Sets: (All)
Aliases: Name

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PageTemplateName
The name of the page layout you want to use. Specify without the .aspx extension. So 'ArticleLeft' or 'BlankWebPartPage'

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Publish
Publishes the page. Also Approves it if moderation is enabled on the Pages library.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The title of the page

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


