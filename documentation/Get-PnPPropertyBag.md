---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPPropertyBag.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPPropertyBag
---

# Get-PnPPropertyBag

## SYNOPSIS

Returns the property bag values.

## SYNTAX

### Default (Default)

```
Get-PnPPropertyBag [[-Key] <String>] [-Folder <String>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve all property bag values. It is possible to get property bag values for a folder using `Folder` option or a specific property bag value using `Key` option.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPPropertyBag
```

This will return all web property bag values

### EXAMPLE 2

```powershell
Get-PnPPropertyBag -Key MyKey
```

This will return the value of the key MyKey from the web property bag

### EXAMPLE 3

```powershell
Get-PnPPropertyBag -Folder /MyFolder
```

This will return all property bag values for the folder MyFolder which is located in the root of the current web

### EXAMPLE 4

```powershell
Get-PnPPropertyBag -Folder /MyFolder -Key vti_mykey
```

This will return the value of the key vti_mykey from the folder MyFolder which is located in the root of the current web

### EXAMPLE 5

```powershell
Get-PnPPropertyBag -Folder / -Key vti_mykey
```

This will return the value of the key vti_mykey from the root folder of the current web

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

### -Folder

Site relative url of the folder. See examples for use.

```yaml
Type: String
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

### -Key

Key that should be looked up

```yaml
Type: String
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
