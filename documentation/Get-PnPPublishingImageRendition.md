---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPPublishingImageRendition.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPPublishingImageRendition
---

# Get-PnPPublishingImageRendition

## SYNOPSIS

Returns all image renditions or if Identity is specified a specific one

## SYNTAX

### Default (Default)

```
Get-PnPPublishingImageRendition [[-Identity] <ImageRenditionPipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve all image renditions or a specific one when `Identity` option is used.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPPublishingImageRendition
```

Returns all Image Renditions

### EXAMPLE 2

```powershell
Get-PnPPublishingImageRendition -Identity "Test"
```

Returns the image rendition named "Test"

### EXAMPLE 3

```powershell
Get-PnPPublishingImageRendition -Identity 2
```

Returns the image rendition where its id equals 2

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

Id or name of an existing image rendition

```yaml
Type: ImageRenditionPipeBind
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
