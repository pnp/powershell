---
Module Name: PnP.PowerShell
title: Set-PnPPage
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPPage.html
---
 
# Set-PnPPage

## SYNOPSIS
Sets parameters of a page

## SYNTAX

```powershell
Set-PnPPage [-Identity] <PagePipeBind> [-Name <String>] [-Title <String>]
 [-LayoutType <PageLayoutType>] [-PromoteAs <PagePromoteType>] [-CommentsEnabled]
 [-Publish] [-HeaderType <PageHeaderType>] [-HeaderLayoutType <PageHeaderLayoutType>] [-ContentType <ContentTypePipeBind>]
 [-ThumbnailUrl <String>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPPage -Identity "MyPage" -LayoutType Home -Title "My Page"
```

Updates the properties of the page named 'MyPage'

### EXAMPLE 2
```powershell
Set-PnPPage -Identity "MyPage" -CommentsEnabled
```

Enables the comments on the page named 'MyPage'

### EXAMPLE 3
```powershell
Set-PnPPage -Identity "MyPage" -CommentsEnabled:$false
```

Disables the comments on the page named 'MyPage'

### EXAMPLE 4
```powershell
Set-PnPPage -Identity "MyPage" -HeaderType Default
```

Sets the header of the page to the default header

### EXAMPLE 5
```powershell
Set-PnPPage -Identity "MyPage" -HeaderType None
```

Removes the header of the page

### EXAMPLE 6
```powershell
Set-PnPPage -Identity "MyPage" -HeaderType Custom -ServerRelativeImageUrl "/sites/demo1/assets/myimage.png" -TranslateX 10.5 -TranslateY 11.0
```

Sets the header of the page to custom header, using the specified image and translates the location of the image in the header given the values specified

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

### -HeaderType
Sets the page header type

```yaml
Type: PageHeaderType
Parameter Sets: (All)
Accepted values: None, Default, Custom

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HeaderLayoutType
Sets the page header layout type

```yaml
Type: PageHeaderLayoutType
Parameter Sets: (All)
Accepted values: None, Default, Custom

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The name/identity of the page

```yaml
Type: PagePipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -LayoutType
Sets the layout type of the page. (Default = Article)

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
Sets the name of the page.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PromoteAs
Allows to promote the page for a specific purpose (None | HomePage | NewsArticle | Template)

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
Publishes the page once it is saved.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ThumbnailUrl
Thumbnail Url

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
Sets the title of the page.

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

