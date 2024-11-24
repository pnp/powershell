---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPPageWebPart.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPPageWebPart
---

# Add-PnPPageWebPart

## SYNOPSIS

Adds a web part to a page

## SYNTAX

### Default with built-in web part

```
Add-PnPPageWebPart [-Page] <PagePipeBind> -DefaultWebPartType <DefaultClientSideWebParts>
 [-WebPartProperties <PropertyBagPipeBind>] [-Order <Int32>] [-Connection <PnPConnection>]
```

### Default with 3rd party web part

```
Add-PnPPageWebPart [-Page] <PagePipeBind> -Component <PageComponentPipeBind>
 [-WebPartProperties <PropertyBagPipeBind>] [-Order <Int32>] [-Connection <PnPConnection>]
```

### Positioned with built-in web part

```
Add-PnPPageWebPart [-Page] <PagePipeBind> -DefaultWebPartType <DefaultClientSideWebParts>
 -Section <Int32> -Column <Int32> [-WebPartProperties <PropertyBagPipeBind>] [-Order <Int32>]
 [-Connection <PnPConnection>]
```

### Positioned with 3rd party web part

```
Add-PnPPageWebPart [-Page] <PagePipeBind> -Component <PageComponentPipeBind> -Section <Int32>
 -Column <Int32> [-WebPartProperties <PropertyBagPipeBind>] [-Order <Int32>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

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
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Positioned with built-in web part
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Positioned with 3rd party web part
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Component

Specifies the component instance or Id to add.

```yaml
Type: PageComponentPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Default with 3rd party web part
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Positioned with 3rd party web part
  Position: Named
  IsRequired: true
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

### -DefaultWebPartType

Defines a default web part type to insert.

```yaml
Type: DefaultClientSideWebParts
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Default with built-in web part
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Positioned with built-in web part
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- ThirdParty
- ContentRollup
- BingMap
- ContentEmbed
- DocumentEmbed
- Image
- ImageGallery
- LinkPreview
- NewsFeed
- NewsReel
- News
- PowerBIReportEmbed
- QuickChart
- SiteActivity
- VideoEmbed
- YammerEmbed
- Events
- GroupCalendar
- Hero
- List
- PageTitle
- People
- QuickLinks
- CustomMessageRegion
- Divider
- MicrosoftForms
- Spacer
- ClientWebPart
- PowerApps
- CodeSnippet
- PageFields
- Weather
- YouTube
- MyDocuments
- YammerFullFeed
- CountDown
- ListProperties
- MarkDown
- Planner
- Sites
- CallToAction
- Button
HelpMessage: ''
```

### -Order

Sets the order of the web part control. (Default = 1)

```yaml
Type: Int32
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

### -Page

The name of the page.

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

### -Section

Sets the section where to insert the web part control.

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Positioned with built-in web part
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
- Name: Positioned with 3rd party web part
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -WebPartProperties

The properties of the web part

```yaml
Type: PropertyBagPipeBind
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
