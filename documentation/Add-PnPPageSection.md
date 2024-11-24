---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPPageSection.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPPageSection
---

# Add-PnPPageSection

## SYNOPSIS

Adds a new section to a page.

## SYNTAX

### Default (Default)

```
Add-PnPPageSection [-Page] <PagePipeBind> -SectionTemplate <CanvasSectionTemplate> [-Order <Int32>]
 [-ZoneEmphasis <Int32>] [-VerticalZoneEmphasis <Int32>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add a new section to a page.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPPageSection -Page "MyPage" -SectionTemplate OneColumn
```

Adds a new one-column section to the page 'MyPage'

### EXAMPLE 2

```powershell
Add-PnPPageSection -Page "MyPage" -SectionTemplate ThreeColumn -Order 10
```

Adds a new Three columns section to the page 'MyPage' with an order index of 10.

### EXAMPLE 3

```powershell
$page = Add-PnPPage -Name "MyPage"
Add-PnPPageSection -Page $page -SectionTemplate OneColumn
```

Adds a new one column section to the page 'MyPage'.

### EXAMPLE 4

```powershell
$page = Add-PnPPage -Name "MyPage"
Add-PnPPageSection -Page $page -SectionTemplate OneColumn -ZoneEmphasis 2
```

Adds a new one column section to the page 'MyPage' and sets the background to 2 (0 is no background, 3 is highest emphasis).

### EXAMPLE 5

```powershell
$page = Add-PnPPage -Name "MyPage"
Add-PnPPageSection -Page $page -SectionTemplate OneColumnVerticalSection -Order 1 -ZoneEmphasis 2 -VerticalZoneEmphasis 3
```

Adds a new one column with one vertical section to the page 'MyPage' and sets the zone emphasis to 2 for one column and vertical zone emphasis to 3 for the vertical column.

## PARAMETERS

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

### -Order

Sets the order of the section. (Default = 1)

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

The name of the page or the page object.

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

### -SectionTemplate

Specifies the columns template to use for the section.

```yaml
Type: CanvasSectionTemplate
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- OneColumn
- OneColumnFullWidth
- TwoColumn
- ThreeColumn
- TwoColumnLeft
- TwoColumnRight
- OneColumnVerticalSection
- TwoColumnVerticalSection
- ThreeColumnVerticalSection
- TwoColumnLeftVerticalSection
- TwoColumnRightVerticalSection
HelpMessage: ''
```

### -VerticalZoneEmphasis

Sets the background of the vertical section (default = 0).
Works only for vertical column layouts, will be ignored for other layouts.

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

### -ZoneEmphasis

Sets the background of the section (default = 0).

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
