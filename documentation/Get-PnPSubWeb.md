---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSubWeb.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSubWeb
---

# Get-PnPSubWeb

## SYNOPSIS

Returns the subwebs of the current web

## SYNTAX

### Default (Default)

```
Get-PnPSubWeb [[-Identity] <WebPipeBind>] [-Recurse] [-Connection <PnPConnection>]
 [-Includes <String[]>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve subwebs of the current web.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSubWeb
```

Retrieves all subsites of the current context returning the Id, Url, Title and ServerRelativeUrl of each subsite in the output

### EXAMPLE 2

```powershell
Get-PnPSubWeb -Recurse
```

Retrieves all subsites of the current context and all of their nested child subsites returning the Id, Url, Title and ServerRelativeUrl of each subsite in the output

### EXAMPLE 3

```powershell
Get-PnPSubWeb -Recurse -Includes "WebTemplate","Description" | Select ServerRelativeUrl, WebTemplate, Description
```

Retrieves all subsites of the current context and shows the ServerRelativeUrl, WebTemplate and Description properties in the resulting output

### EXAMPLE 4

```powershell
Get-PnPSubWeb -Identity Team1 -Recurse
```

Retrieves all subsites of the subsite Team1 and all of its nested child subsites

### EXAMPLE 5

```powershell
Get-PnPSubWeb -Identity Team1 -Recurse -IncludeRootWeb
```

Retrieves the root web, all subsites of the subsite Team1 and all of its nested child subsites

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

If provided, only the subsite with the provided Id, GUID or the Web instance will be returned

```yaml
Type: WebPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IncludeRootWeb

If provided, the results will also contain the root web

```yaml
Type: SwitchParameter
DefaultValue: False
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

### -Includes

Optionally allows properties to be retrieved for the returned sub web which are not included in the response by default

```yaml
Type: String[]
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

### -Recurse

If provided, recursion through all subsites and their children will take place to return them as well

```yaml
Type: SwitchParameter
DefaultValue: False
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
