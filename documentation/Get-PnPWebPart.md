---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPWebPart.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPWebPart
---

# Get-PnPWebPart

## SYNOPSIS

Returns a web part definition object

## SYNTAX

### Default (Default)

```
Get-PnPWebPart -ServerRelativePageUrl <String> [-Identity <WebPartPipeBind>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows retrieval of the definition of a webpart on a classic SharePoint Online page.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPWebPart -ServerRelativePageUrl "/sites/demo/sitepages/home.aspx"
```

Returns all webparts defined on the given classic page.

### EXAMPLE 2

```powershell
Get-PnPWebPart -ServerRelativePageUrl "/sites/demo/sitepages/home.aspx" -Identity a2875399-d6ff-43a0-96da-be6ae5875f82
```

Returns a specific web part defined on the given classic page.

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

The identity of the web part, this can be the web part guid or a web part object

```yaml
Type: WebPartPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ServerRelativePageUrl

Full server relative URL of the web part page, e.g. /sites/mysite/sitepages/home.aspx

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
