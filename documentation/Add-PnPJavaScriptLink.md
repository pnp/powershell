---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPJavaScriptLink.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPJavaScriptLink
---

# Add-PnPJavaScriptLink

## SYNOPSIS

Adds a link to a JavaScript file to a web or sitecollection, valid only for SharePoint classic site experience.

## SYNTAX

### Default (Default)

```
Add-PnPJavaScriptLink -Name <String> -Url <String[]> [-Sequence <Int32>]
 [-Scope <CustomActionScope>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Creates a custom action that refers to a JavaScript file

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPJavaScriptLink -Name jQuery -Url https://code.jquery.com/jquery.min.js -Sequence 9999 -Scope Site
```

Injects a reference to the latest v1 series jQuery library to all pages within the current site collection under the name jQuery and at order 9999

### EXAMPLE 2

```powershell
Add-PnPJavaScriptLink -Name jQuery -Url https://code.jquery.com/jquery.min.js
```

Injects a reference to the latest v1 series jQuery library to all pages within the current web under the name jQuery

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

### -Name

Name under which to register the JavaScriptLink

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Key
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

### -Scope

Defines if this JavaScript file will be injected to every page within the current site collection or web. All is not allowed in for this command. Default is web.

```yaml
Type: CustomActionScope
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
- Web
- Site
- All
HelpMessage: ''
```

### -Sequence

Sequence of this JavaScript being injected. Use when you have a specific sequence with which to have JavaScript files being added to the page. I.e. jQuery library first and then jQueryUI.

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

### -Url

URL to the JavaScript file to inject

```yaml
Type: String[]
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
