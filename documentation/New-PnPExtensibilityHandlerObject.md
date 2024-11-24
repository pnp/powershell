---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPExtensibilityHandlerObject.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPExtensibilityHandlerObject
---

# New-PnPExtensibilityHandlerObject

## SYNOPSIS

Creates an ExtensibilityHandler Object, to be used by the Get-PnPSiteTemplate cmdlet

## SYNTAX

### Default (Default)

```
New-PnPExtensibilityHandlerObject [-Assembly] <String> -Type <String> [-Configuration <String>]
 [-Disabled]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to create an ExtensibilityHandler.

## EXAMPLES

### EXAMPLE 1

```powershell
$handler = New-PnPExtensibilityHandlerObject -Assembly Contoso.Core.Handlers -Type Contoso.Core.Handlers.MyExtensibilityHandler
Get-PnPSiteTemplate -Out NewTemplate.xml -ExtensibilityHandlers $handler
```

This will create a new ExtensibilityHandler object that is run during extraction of the template

## PARAMETERS

### -Assembly

The full assembly name of the handler

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Configuration

Any configuration data you want to send to the handler

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

### -Disabled

If set, the handler will be disabled

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

### -Type

The type of the handler

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
