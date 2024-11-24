---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPWikiPage.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPWikiPage
---

# Add-PnPWikiPage

## SYNOPSIS

Adds a wiki page

## SYNTAX

### WithContent

```
Add-PnPWikiPage -ServerRelativePageUrl <String> -Content <String> [-Connection <PnPConnection>]
```

### WithLayout

```
Add-PnPWikiPage -ServerRelativePageUrl <String> -Layout <WikiPageLayout>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add a wiki page.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPWikiPage -PageUrl '/sites/demo1/pages/wikipage.aspx' -Content 'New WikiPage'
```

Creates a new wiki page '/sites/demo1/pages/wikipage.aspx' and sets the content to 'New WikiPage'

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

### -Content



```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: WithContent
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Layout



```yaml
Type: WikiPageLayout
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: WithLayout
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- OneColumn
- OneColumnSideBar
- TwoColumns
- TwoColumnsHeader
- TwoColumnsHeaderFooter
- ThreeColumns
- ThreeColumnsHeader
- ThreeColumnsHeaderFooter
- Custom
HelpMessage: ''
```

### -ServerRelativePageUrl

The server relative page URL

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- PageUrl
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
