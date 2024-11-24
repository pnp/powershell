---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPWebPartToWebPartPage.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPWebPartToWebPartPage
---

# Add-PnPWebPartToWebPartPage

## SYNOPSIS

Adds a web part to a web part page in a specified zone

## SYNTAX

### XML

```
Add-PnPWebPartToWebPartPage -ServerRelativePageUrl <String> -Xml <String> -ZoneId <String>
 -ZoneIndex <Int32> [-Connection <PnPConnection>]
```

### FILE

```
Add-PnPWebPartToWebPartPage -ServerRelativePageUrl <String> -Path <String> -ZoneId <String>
 -ZoneIndex <Int32> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add a web part to a web part page. Use the `ZoneIndex` option to specify the zone.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPWebPartToWebPartPage -ServerRelativePageUrl "/sites/demo/sitepages/home.aspx" -Path "c:\myfiles\listview.webpart" -ZoneId "Header" -ZoneIndex 1
```

This will add the web part as defined by the XML in the listview.webpart file to the specified page in the specified zone and with the order index of 1

### EXAMPLE 2

```powershell
Add-PnPWebPartToWebPartPage -ServerRelativePageUrl "/sites/demo/sitepages/home.aspx" -XML $webpart -ZoneId "Header" -ZoneIndex 1
```

This will add the web part as defined by the XML in the $webpart variable to the specified page in the specified zone and with the order index of 1

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

### -Path

A path to a web part file on a the file system.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: FILE
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ServerRelativePageUrl

Server Relative Url of the page to add the web part to.

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

### -Xml

A string containing the XML for the web part.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: XML
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ZoneId

The Zone Id where the web part must be placed

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

### -ZoneIndex

The Zone Index where the web part must be placed

```yaml
Type: Int32
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
