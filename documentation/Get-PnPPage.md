---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPPage.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPPage
---

# Get-PnPPage

## SYNOPSIS

Returns a modern page

## SYNTAX

### Default (Default)

```
Get-PnPPage -Identity <PagePipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command allows the retrieval of a modern sitepage along with its properties and contents on it. Note that for a newly created modern site, the Columns and Sections of the Home.aspx page will not be filled according to the actual site page contents. This is because the underlying CanvasContent1 will not be populated until the homepage has been edited and published. The reason for this behavior is to allow for the default homepage to be able to be updated by Microsoft as long as it hasn't been modified. For any other site page or after editing and publishing the homepage, this command will return the correct columns and sections as they are positioned on the site page.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPPage -Identity "MyPage.aspx"
```

Gets the page named 'MyPage.aspx' in the current SharePoint site

### EXAMPLE 2

```powershell
Get-PnPPage "MyPage"
```

Gets the page named 'MyPage.aspx' in the current SharePoint site

### EXAMPLE 3

```powershell
Get-PnPPage "Templates/MyPageTemplate"
```

Gets the page named 'MyPageTemplate.aspx' from the templates folder of the Page Library in the current SharePoint site

### EXAMPLE 4

```powershell
Get-PnPPage -Identity "MyPage.aspx" -Web (Get-PnPWeb -Identity "Subsite1")
```

Gets the page named 'MyPage.aspx' from the subsite named 'Subsite1'

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

### -Identity

The name of the page

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
