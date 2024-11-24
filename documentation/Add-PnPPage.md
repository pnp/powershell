---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPPage.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPPage
---

# Add-PnPPage

## SYNOPSIS

Creates a new page

## SYNTAX

### Default (Default)

```
Add-PnPPage [-Name] <String> [-LayoutType <PageLayoutType>] [-PromoteAs <PagePromoteType>]
 [-ContentType <ContentTypePipeBind>] [-CommentsEnabled] [-Publish]
 [-HeaderLayoutType <PageHeaderLayoutType>] [-ScheduledPublishDate <DateTime>]
 [-Translate][-TranslationLanguageCodes <Int[][]>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Creates a new page. The page will be located inside the Site Pages library of the site currently connected to. If the library does not exist yet, tries to ensure it is created.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPPage -Name "NewPage"
```

Creates a new page named 'NewPage'. The resulting file will accordingly be named 'NewPage.aspx'. If a file with that name already exists, executing the commandlet will fail.

### EXAMPLE 2

```powershell
Add-PnPPage -Name "NewPage" -Title "Welcome to my page"
```

Creates a new page NewPage.aspx with the title as provided.

### EXAMPLE 3

```powershell
Add-PnPPage -Name "NewPage" -ContentType "MyPageContentType"
```

Creates a new page named 'NewPage' and sets the content type to the content type specified.

### EXAMPLE 4

```powershell
Add-PnPPage -Name "NewPageTemplate" -PromoteAs Template
```

Creates a new page named 'NewPage' and saves as a template to the site.

### EXAMPLE 5

```powershell
Add-PnPPage -Name "Folder/NewPage"
```

Creates a new page named 'NewPage' under 'Folder' folder and saves as a template to the site.

### EXAMPLE 6

```powershell
Add-PnPPage -Name "NewPage" -HeaderLayoutType ColorBlock
```

Creates a new page named 'NewPage' using the ColorBlock header layout.

### EXAMPLE 7

```powershell
Add-PnPPage -Name "NewPage" Article -ScheduledPublishDate (Get-Date).AddHours(1)
```

Creates a new page named 'NewPage' using the article layout and schedule it to be published in 1 hour from now.

### EXAMPLE 8

```powershell
Add-PnPPage -Name "NewPage" -Translate
```

Creates a new page named 'NewPage' and also creates the necessary translated page for the supported languages in the site collection.

### EXAMPLE 9

```powershell
Add-PnPPage -Name "NewPage" -Translate -TranslationLanguageCodes 1043
```

Creates a new page named 'NewPage' and also creates the necessary translated page for the specified language in the site collection. In this case, it will create the translated page for Dutch language. If the Dutch language is not enabled, it will enable the language and then create the translated page.

### EXAMPLE 10

```powershell
Add-PnPPage -Name "NewPage" -Translate -TranslationLanguageCodes 1043,1035
```

Creates a new page named 'NewPage' and also creates the necessary translated page for the specified languages in the site collection. In this case, it will create the translated pages for Dutch and Finnish languages. If these languages are not enabled, it will enable these languages and then create the translated pages for the specified languages.

## PARAMETERS

### -CommentsEnabled

Enables or Disables the comments on the page.

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

Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

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

### -HeaderLayoutType

Type of layout used for the header.

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
- FullWidthImage
- NoImage
- ColorBlock
- CutInShape
HelpMessage: ''
```

### -LayoutType

Specifies the layout type of the page.

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

### -Name

Specifies the name of the page. It will also be used to define the filename. I.e. if you provide MyPage, it will create a page MyPage.aspx inside the Site Pages library.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -PromoteAs

Allows to promote the page for a specific purpose (HomePage | NewsPage).

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
HelpMessage: ''
```

### -Publish

Publishes the page once it is saved. Applicable to libraries set to create major and minor versions.

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
