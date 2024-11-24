---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPFooter.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPFooter
---

# Set-PnPFooter

## SYNOPSIS

Configures the footer of the current web.

## SYNTAX

### Default (Default)

```
Set-PnPFooter [-Enabled] [-Layout <FooterLayoutType>] [-BackgroundTheme <FooterVariantThemeType>]
 [-Title <String>] [-LogoUrl <String>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows the footer to be enabled or disabled and fine tuned in the current web. For modifying the navigation links shown in the footer, use Add-PnPNavigationNode -Location Footer.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPFooter -Enabled:$true
```

Enables the footer to be shown on the current web.

### EXAMPLE 2

```powershell
Set-PnPFooter -Enabled:$true -Layout Extended -BackgroundTheme Neutral
```

Enables the footer to be shown on the current web with the extended layout using a neutral background.

### EXAMPLE 3

```powershell
Set-PnPFooter -Title "Contoso Inc." -LogoUrl "/sites/communication/Shared Documents/logo.png"
```

Sets the title and logo shown in the footer.

### EXAMPLE 4

```powershell
Set-PnPFooter -LogoUrl ""
```

Removes the current logo shown in the footer.

## PARAMETERS

### -BackgroundTheme

Defines the background emphasis of the content in the footer.

```yaml
Type: FooterVariantThemeType
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
- Strong
- Neutral
- Soft
- None
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

### -Enabled

Indicates if the footer should be shown on the current web ($true) or if it should be hidden ($false).

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

### -Layout

Defines how the footer should look like.

```yaml
Type: FooterLayoutType
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
- Simple
- Extended
- Stacked
HelpMessage: ''
```

### -LogoUrl

Defines the server relative URL to the logo to be displayed in the footer. Provide an empty string to remove the current logo.

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

### -Title

Defines the title displayed in the footer.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
