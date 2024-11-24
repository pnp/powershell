---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPWebPartXml.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPWebPartXml
---

# Get-PnPWebPartXml

## SYNOPSIS

Returns the web part XML of a web part registered on a site

## SYNTAX

### Default (Default)

```
Get-PnPWebPartXml -ServerRelativePageUrl <String> -Identity <WebPartPipeBind>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve web part XML defintion.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPWebPartXml -ServerRelativePageUrl "/sites/demo/sitepages/home.aspx" -Identity a2875399-d6ff-43a0-96da-be6ae5875f82
```

Returns the web part XML for a given web part on a page.

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

Id or title of the web part. Use Get-PnPWebPart to retrieve all web part Ids

```yaml
Type: WebPartPipeBind
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

### -ServerRelativePageUrl

Full server relative url of the web part page, e.g. /sites/mysite/sitepages/home.aspx

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
