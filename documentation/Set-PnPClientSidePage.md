---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/set-pnpclientsidepage
schema: 2.0.0
title: Set-PnPClientSidePage
---

# Set-PnPClientSidePage

## SYNOPSIS
Sets parameters of a Client-Side Page

## SYNTAX

```powershell
Set-PnPClientSidePage [-Identity] <ClientSidePagePipeBind> [-Name <String>] [-Title <String>]
 [-LayoutType <ClientSidePageLayoutType>] [-PromoteAs <ClientSidePagePromoteType>] [-CommentsEnabled]
 [-Publish] [-HeaderType <ClientSidePageHeaderType>] [-ContentType <ContentTypePipeBind>]
 [-ThumbnailUrl <String>] [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPClientSidePage -Identity "MyPage" -LayoutType Home -Title "My Page"
```

Updates the properties of the Client-Side page named 'MyPage'

### EXAMPLE 2
```powershell
Set-PnPClientSidePage -Identity "MyPage" -CommentsEnabled
```

Enables the comments on the Client-Side page named 'MyPage'

### EXAMPLE 3
```powershell
Set-PnPClientSidePage -Identity "MyPage" -CommentsEnabled:$false
```

Disables the comments on the Client-Side page named 'MyPage'

### EXAMPLE 4
```powershell
Set-PnPClientSidePage -Identity "MyPage" -HeaderType Default
```

Sets the header of the page to the default header

### EXAMPLE 5
```powershell
Set-PnPClientSidePage -Identity "MyPage" -HeaderType None
```

Removes the header of the page

### EXAMPLE 6
```powershell
Set-PnPClientSidePage -Identity "MyPage" -HeaderType Custom -ServerRelativeImageUrl "/sites/demo1/assets/myimage.png" -TranslateX 10.5 -TranslateY 11.0
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
Type: ClientSidePageHeaderType
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
Type: ClientSidePagePipeBind
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
Type: ClientSidePageLayoutType
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
Type: ClientSidePagePromoteType
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