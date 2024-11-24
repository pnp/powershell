---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPProperty.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPProperty
---

# Get-PnPProperty

## SYNOPSIS

Returns a previously not loaded property of a ClientObject

## SYNTAX

### Default (Default)

```
Get-PnPProperty [-ClientObject] <ClientObject> [-Property] <String[]> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Will populate properties of an object and optionally, if needed, load the value from the server. If one property is specified its value will be returned to the output.

## EXAMPLES

### EXAMPLE 1

```powershell
$web = Get-PnPWeb
Get-PnPProperty -ClientObject $web -Property Id, Lists
$web.Lists
```

Will load both the Id and Lists properties of the specified Web object.

### EXAMPLE 2

```powershell
$list = Get-PnPList -Identity 'Site Assets'
Get-PnPProperty -ClientObject $list -Property Views
```

Will load the views object of the specified list object and return its value to the output.

## PARAMETERS

### -ClientObject

Specifies the object where the properties of should be retrieved

```yaml
Type: ClientObject
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
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

### -Property

The properties to load. If one property is specified its value will be returned to the output.

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 1
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
