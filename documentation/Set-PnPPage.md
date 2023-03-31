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
 [-Publish] [-HeaderType <PageHeaderType>] [-HeaderLayoutType <PageHeaderLayoutType>] [-ScheduledPublishDate <DateTime>] 
 [-RemoveScheduledPublish] [-ContentType <ContentTypePipeBind>] [-ThumbnailUrl <String>] 
 [-Translate][-TranslationLanguageCodes <Int[][]>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

Sets parameters of a page. All pages must be located inside the Site Pages library.

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
Set-PnPPage -Identity "hr/MyPage" -HeaderType Default
```

Sets the header of the page called MyPage located in the folder hr inside the Site Pages library to the default header

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

### EXAMPLE 7
```powershell
Set-PnPPage -Identity "MyPage" -ScheduledPublishDate (Get-Date).AddHours(1)
```

Schedules the page "MyPage" to be published in one hour from now

### EXAMPLE 8
```powershell
Set-PnPPage -Name "NewPage" -Translate
```

Creates the necessary translated pages for all the supported languages in the site collection.

### EXAMPLE 9
```powershell
Set-PnPPage -Name "NewPage" -Translate -TranslationLanguageCodes 1043
```

Creates the necessary translated page for the specified language in the site collection. In this case, it will create the translated page for Dutch language. If the Dutch language is not enabled, it will enable the language and then create the translated page.

### EXAMPLE 10
```powershell
Set-PnPPage -Name "NewPage" -Translate -TranslationLanguageCodes 1043,1035
```

Creates the necessary translated page for the specified languages in the site collection. In this case, it will create the translated pages for Dutch and Finnish languages. If these languages are not enabled, it will enable these languages and then create the translated pages for the specified languages.

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
Accepted values: ColorBlock, CutInShape, FullWidthImage, NoImage

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The name/identity of the page. This can be a page instance or the filename of the page. I.e. if the page is called MyPage.aspx and is located in the root of the Site Pages library, provide "MyPage" or "MyPage.aspx". If the page is called MyOtherPage.aspx and is located inside a subfolder called HR located in the root of the Site Pages library, provide "HR/MyOtherPage" or "HR/MyOtherPage.aspx".

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
Accepted values: Article, Home, SingleWebPartAppPage,  RepostPage, HeaderlessSearchResults, Spaces, Topic

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

### -ScheduledPublishDate
If provided, the page will be scheduled to be published on the provided date and time. It will enable page scheduling on the Site Pages library if not already enabled. If not provided, the publishing of the page will not be scheduled.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveScheduledPublish
If provided, the page publish schedule will be removed, if it has been set.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Translate
Creates multilingual pages for all the languages specified in the site collection

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TranslationLanguageCodes
Creates multilingual pages for specified languages.

```yaml
Type: Integer array
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)