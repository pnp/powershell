---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPPage.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPPage
---
  
# Add-PnPPage

## SYNOPSIS
Adds a Page

## SYNTAX

```powershell
Add-PnPPage [-Name] <String> [-LayoutType <PageLayoutType>]
 [-PromoteAs <PagePromoteType>] [-ContentType <ContentTypePipeBind>] [-CommentsEnabled] [-Publish]
 [-HeaderLayoutType <PageHeaderLayoutType>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPPage -Name "NewPage"
```

Creates a new page named 'NewPage'

### EXAMPLE 2
```powershell
Add-PnPPage -Name "NewPage" -ContentType "MyPageContentType"
```

Creates a new page named 'NewPage' and sets the content type to the content type specified

### EXAMPLE 3
```powershell
Add-PnPPage -Name "NewPageTemplate" -PromoteAs Template
```

Creates a new page named 'NewPage' and saves as a template to the site.

### EXAMPLE 4
```powershell
Add-PnPPage -Name "Folder/NewPage"
```

Creates a new page named 'NewPage' under 'Folder' folder and saves as a template to the site.

### EXAMPLE 5
```powershell
Add-PnPPage -Name "NewPage" -HeaderLayoutType ColorBlock
```

Creates a new page named 'NewPage' using the ColorBlock header layout

## PARAMETERS

### -CommentsEnabled
Enables or Disables the comments on the page

```yaml
Type: SwitchParameter
Parameter Sets: (All)

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

### -ContentType
Specify either the name, ID or an actual content type.

```yaml
Type: ContentTypePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HeaderLayoutType
Type of layout used for the header

```yaml
Type: PageHeaderLayoutType
Parameter Sets: (All)
Accepted values: FullWidthImage, NoImage, ColorBlock, CutInShape

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LayoutType
Specifies the layout type of the page.

```yaml
Type: PageLayoutType
Parameter Sets: (All)
Accepted values: Article, Home, SingleWebPartAppPage, RepostPage, HeaderlessSearchResults, Spaces, Topic

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Specifies the name of the page.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PromoteAs
Allows to promote the page for a specific purpose (HomePage | NewsPage)

```yaml
Type: PagePromoteType
Parameter Sets: (All)
Accepted values: None, HomePage, NewsArticle, Template

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Publish
Publishes the page once it is saved. Applicable to libraries set to create major and minor versions.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


