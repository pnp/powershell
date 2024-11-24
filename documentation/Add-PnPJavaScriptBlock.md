---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPJavaScriptBlock.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPJavaScriptBlock
---

# Add-PnPJavaScriptBlock

## SYNOPSIS

Adds a link to a JavaScript snippet/block to a web or site collection, valid only for SharePoint classic site experience.

## SYNTAX

### Default (Default)

```
Add-PnPJavaScriptBlock -Name <String> -Script <String> [-Sequence <Int32>]
 [-Scope <CustomActionScope>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Specify a scope as 'Site' to add the custom action to all sites in a site collection.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPJavaScriptBlock -Name myAction -script '<script>Alert("This is my Script block");</script>' -Sequence 9999 -Scope Site
```

Add a JavaScript code block  to all pages within the current site collection under the name myAction and at order 9999

### EXAMPLE 2

```powershell
Add-PnPJavaScriptBlock -Name myAction -script '<script>Alert("This is my Script block");</script>'
```

Add a JavaScript code block  to all pages within the current web under the name myAction

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

The name of the script block. Can be used to identify the script with other cmdlets or coded solutions

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

The scope of the script to add to. Either Web or Site, defaults to Web. 'All' is not valid for this command.

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

### -Script

The javascript block to add to the specified scope

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

### -Sequence

A sequence number that defines the order on the page

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
