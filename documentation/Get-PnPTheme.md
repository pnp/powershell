---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTheme.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTheme
---

# Get-PnPTheme

## SYNOPSIS

Returns the current theme/composed look of the current web.

## SYNTAX

### Default (Default)

```
Get-PnPTheme [-DetectCurrentComposedLook] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve current theme/composed look of the current web.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTheme
```

Returns the current composed look of the current web.

### EXAMPLE 2

```powershell
Get-PnPTheme -DetectCurrentComposedLook
```

Returns the current composed look of the current web, and will try to detect the currently applied composed look based upon the actual site. Without this switch the cmdlet will first check for the presence of a property bag variable called _PnP_SiteTemplateComposedLookInfo that contains composed look information when applied through the provisioning engine or the Set-PnPTheme cmdlet.

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

### -DetectCurrentComposedLook

Specify this switch to not use the PnP Provisioning engine based composed look information but try to detect the current composed look as is.

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
