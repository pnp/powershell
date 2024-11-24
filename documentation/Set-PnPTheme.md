---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPTheme.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPTheme
---

# Set-PnPTheme

## SYNOPSIS

Sets the theme of the current web.

## SYNTAX

### Default (Default)

```
Set-PnPTheme [-ColorPaletteUrl <String>] [-FontSchemeUrl <String>] [-BackgroundImageUrl <String>]
 [-ResetSubwebsToInherit] [-UpdateRootWebOnly] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Sets the theme of the current web. If any of the attributes is not set, that value will be set to null.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPTheme
```

Removes the current theme and resets it to the default.

### EXAMPLE 2

```powershell
Set-PnPTheme -ColorPaletteUrl _catalogs/theme/15/company.spcolor
```

### EXAMPLE 3

```powershell
Set-PnPTheme -ColorPaletteUrl _catalogs/theme/15/company.spcolor -BackgroundImageUrl 'style library/background.png'
```

### EXAMPLE 4

```powershell
Set-PnPTheme -ColorPaletteUrl _catalogs/theme/15/company.spcolor -BackgroundImageUrl 'style library/background.png' -ResetSubwebsToInherit
```

Sets the theme to the web, and updates all subwebs to inherit the theme from this web.

## PARAMETERS

### -BackgroundImageUrl

Specifies the Background Image Url based on the site or server relative url.

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

### -ColorPaletteUrl

Specifies the Color Palette Url based on the site or server relative url.

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

### -FontSchemeUrl

Specifies the Font Scheme Url based on the site or server relative url.

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

### -ResetSubwebsToInherit

Resets subwebs to inherit the theme from the rootweb.

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

### -UpdateRootWebOnly

Updates only the rootweb, even if subwebs are set to inherit the theme.

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
