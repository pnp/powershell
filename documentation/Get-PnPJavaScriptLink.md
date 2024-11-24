---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPJavaScriptLink.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPJavaScriptLink
---

# Get-PnPJavaScriptLink

## SYNOPSIS

Returns all or a specific custom action(s) with location type ScriptLink

## SYNTAX

### Default (Default)

```
Get-PnPJavaScriptLink [[-Name] <String>] [-Scope <CustomActionScope>]
 [-ThrowExceptionIfJavaScriptLinkNotFound] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPJavaScriptLink
```

Returns all web scoped JavaScript links

### EXAMPLE 2
```powershell
Get-PnPJavaScriptLink -Scope All
```

Returns all web and site scoped JavaScript links

### EXAMPLE 3
```powershell
Get-PnPJavaScriptLink -Scope Web
```

Returns all Web scoped JavaScript links

### EXAMPLE 4
```powershell
Get-PnPJavaScriptLink -Scope Site
```

Returns all Site scoped JavaScript links

### EXAMPLE 5
```powershell
Get-PnPJavaScriptLink -Name Test
```

Returns the web scoped JavaScript link named Test

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPJavaScriptLink
```

Returns all web scoped JavaScript links

### EXAMPLE 2

```powershell
Get-PnPJavaScriptLink -Scope All
```

Returns all web and site scoped JavaScript links

### EXAMPLE 3

```powershell
Get-PnPJavaScriptLink -Scope Web
```

Returns all Web scoped JavaScript links

### EXAMPLE 4

```powershell
Get-PnPJavaScriptLink -Scope Site
```

Returns all Site scoped JavaScript links

### EXAMPLE 5

```powershell
Get-PnPJavaScriptLink -Name Test
```

Returns the web scoped JavaScript link named Test

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

Name of the Javascript link. Omit this parameter to retrieve all script links

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Key
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

### -Scope

Scope of the action, either Web, Site or All to return both, defaults to Web

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

### -ThrowExceptionIfJavaScriptLinkNotFound

Switch parameter if an exception should be thrown if the requested JavaScriptLink does not exist (true) or if omitted, nothing will be returned in case the JavaScriptLink does not exist

```yaml
Type: SwitchParameter
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
