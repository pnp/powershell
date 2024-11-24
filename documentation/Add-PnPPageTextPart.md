---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPPageTextPart.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPPageTextPart
---

# Add-PnPPageTextPart

## SYNOPSIS

Adds a text element to a client-side page.

## SYNTAX

### Default

```
Add-PnPPageTextPart -Page <PagePipeBind> -Text <String> [-Order <Int32>] [-ImageUrl <String>]
 [-PageImageAlignment <PageImageAlignment>] [-ImageWidth <Int32>] [-ImageHeight <Int32>]
 [-Connection <PnPConnection>]
```

### Positioned

```
Add-PnPPageTextPart -Page <PagePipeBind> -Text <String> -Section <Int32> -Column <Int32>
 [-Order <Int32>] [-ImageUrl <String>] [-PageImageAlignment <PageImageAlignment>]
 [-ImageWidth <Int32>] [-ImageHeight <Int32>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Adds a new text element to a section on a client-side page.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPPageTextPart -Page "MyPage" -Text "Hello World!"
```

Adds the text 'Hello World!' to the Page 'MyPage'

### EXAMPLE 2

```powershell
Add-PnPPageTextPart -Page "MyPage" -Text "Hello World!" -ImageUrl "/sites/contoso/SiteAssets/test.png"
```

Adds the text 'Hello World!' to the Page 'MyPage' with specified image as inline image.

### EXAMPLE 3

```powershell
Add-PnPPageTextPart -Page "MyPage" -Text "Hello World!" -ImageUrl "/sites/contoso/SiteAssets/test.png" -TextBeforeImage "Text before" -TextAfterImage "Text after"
```

Adds the text 'Hello World!' to the Page 'MyPage' with specified image as inline image with text specified before and after the inline image.

## PARAMETERS

### -Column

Sets the column where to insert the text control.

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Positioned
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

### -ImageHeight

Specifies the height of the inline image.

```yaml
Type: Int32
DefaultValue: 150
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

### -ImageUrl

Specifies the inline image to be added. Image will be added after the text content.

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

### -ImageWidth

Specifies the width of the inline image.

```yaml
Type: Int32
DefaultValue: 150
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

Sets the order of the text control. (Default = 1)

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

### -PageImageAlignment

Specifies the inline image's alignment. Available values are Center, Left and Right.

```yaml
Type: PageImageAlignment
DefaultValue: Center
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

### -Section

Sets the section where to insert the text control.

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Positioned
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Text

Specifies the text to display in the text area.

```yaml
Type: String
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
AcceptedValues: []
HelpMessage: ''
```

### -TextAfterImage

Specifies the text to display after the inline image.

```yaml
Type: String
DefaultValue: 150
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

### -TextBeforeImage

Specifies the text to display before the inline image.

```yaml
Type: String
DefaultValue: 150
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
