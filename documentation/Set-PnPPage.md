---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPPage.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPPage
---

# Set-PnPPage

## SYNOPSIS

Sets parameters of a page.

## SYNTAX

### Default (Default)

```
Set-PnPPage -Identity <PagePipeBind> [-Name <String>] [-Title <String>]
 [-LayoutType <PageLayoutType>] [-PromoteAs <PagePromoteType>] [-CommentsEnabled] [-Publish]
 [-HeaderType <PageHeaderType>] [-HeaderLayoutType <PageHeaderLayoutType>]
 [-ScheduledPublishDate <DateTime>] [-RemoveScheduledPublish] [-ContentType <ContentTypePipeBind>]
 [-ThumbnailUrl <String>] [-ShowPublishDate <Boolean>]
 [-Translate][-TranslationLanguageCodes <Int[][]>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Sets parameters of a page. All pages must be located inside the Site Pages library.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPPage -Identity "MyPage" -LayoutType Home -Title "My Page"
```

Updates the properties of the page named 'MyPage'.

### EXAMPLE 2

```powershell
Set-PnPPage -Identity "MyPage" -CommentsEnabled
```

Enables the comments on the page named 'MyPage'.

### EXAMPLE 3

```powershell
Set-PnPPage -Identity "MyPage" -CommentsEnabled:$false
```

Disables the comments on the page named 'MyPage'.

### EXAMPLE 4

```powershell
Set-PnPPage -Identity "hr/MyPage" -HeaderType Default
```

Sets the header of the page called MyPage located in the folder hr inside the Site Pages library to the default header.

### EXAMPLE 5

```powershell
Set-PnPPage -Identity "MyPage" -HeaderType None
```

Removes the header of the page.

### EXAMPLE 6

```powershell
Set-PnPPage -Identity "MyPage" -HeaderType Custom -ServerRelativeImageUrl "/sites/demo1/assets/myimage.png" -TranslateX 10.5 -TranslateY 11.0
```

Sets the header of the page to custom header, using the specified image and translates the location of the image in the header given the values specified.

### EXAMPLE 7

```powershell
Set-PnPPage -Identity "MyPage" -ScheduledPublishDate (Get-Date).AddHours(1)
```

Schedules the page "MyPage" to be published in one hour from now.

### EXAMPLE 8

```powershell
Set-PnPPage -Identity "MyPage" -Translate
```

Creates the necessary translated pages for all the supported languages in the site collection.

### EXAMPLE 9

```powershell
Set-PnPPage -Identity "MyPage" -Translate -TranslationLanguageCodes 1043
```

Creates the necessary translated page for the specified language in the site collection. In this case, it will create the translated page for Dutch language. If the Dutch language is not enabled, it will enable the language and then create the translated page.

### EXAMPLE 10

```powershell
Set-PnPPage -Identity "MyPage" -Translate -TranslationLanguageCodes 1043,1035
```

Creates the necessary translated page for the specified languages in the site collection. In this case, it will create the translated pages for Dutch and Finnish languages. If these languages are not enabled, it will enable these languages and then create the translated pages for the specified languages.

### EXAMPLE 11

```powershell
Set-PnPPage -Identity "MyPage" -ShowPublishDate $true -Publish
```
Display the date when the page was published in the header section of the page.

### EXAMPLE 12

```powershell
Set-PnPPage -Identity "MyPage.aspx" -Like
```
Likes the page.

### EXAMPLE 11

```powershell
Set-PnPPage -Identity "MyPage.aspx" -Like:$false
```
Unlikes the page.

## PARAMETERS

### -CommentsEnabled

Enables or disables the comments on the page.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ContentType

Specify either the name, ID or an actual content type.

```yaml
Type: ContentTypePipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DemoteNewsArticle

Demotes an existing news post to a regular page.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -HeaderLayoutType

Sets the page header layout type.

```yaml
Type: PageHeaderLayoutType
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- ColorBlock
- CutInShape
- FullWidthImage
- NoImage
HelpMessage: ''
```

### -HeaderType

Sets the page header type.

```yaml
Type: PageHeaderType
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- None
- Default
- Custom
HelpMessage: ''
```

### -Identity

The name/identity of the page. This can be a page instance or the filename of the page. I.e. if the page is called MyPage.aspx and is located in the root of the Site Pages library, provide "MyPage" or "MyPage.aspx". If the page is called MyOtherPage.aspx and is located inside a subfolder called HR located in the root of the Site Pages library, provide "HR/MyOtherPage" or "HR/MyOtherPage.aspx".

```yaml
Type: PagePipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -LayoutType

Sets the layout type of the page.

```yaml
Type: PageLayoutType
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Article
- Home
- SingleWebPartAppPage
- RepostPage
- HeaderlessSearchResults
- Spaces
- Topic
HelpMessage: ''
```

### -Like

Likes the page, if parameter is set to false then it Unlikes the page.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Name

Sets the name of the page.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -PromoteAs

Allows to promote the page for a specific purpose (None | HomePage | NewsArticle | Template).

```yaml
Type: PagePromoteType
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- None
- HomePage
- NewsArticle
- Template
HelpMessage: ''
```

### -Publish

Publishes the page once it is saved.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -RemoveScheduledPublish

If provided, the page publish schedule will be removed, if it has been set.

```yaml
Type: DateTime
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ScheduledPublishDate

If provided, the page will be scheduled to be published on the provided date and time. It will enable page scheduling on the Site Pages library if not already enabled. If not provided, the publishing of the page will not be scheduled.

```yaml
Type: DateTime
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ShowPublishDate

Shows Published Date in the header.

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ThumbnailUrl

Specifies the URL of a thumbnail image.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Title

Sets the title of the page.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Translate

Creates multilingual pages for all the languages specified in the site collection.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TranslationLanguageCodes

Creates multilingual pages for specified languages.

```yaml
Type: Integer array
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
