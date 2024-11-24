---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPWebHeader.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPWebHeader
---

# Set-PnPWebHeader

## SYNOPSIS

Allows configuration of the "Change the look" Header

## SYNTAX

### Default (Default)

```
Set-PnPWebHeader [-SiteLogoUrl <string>] [-SiteThumbnailUrl <string>]
 [-HeaderLayout <HeaderLayoutType>] [-HeaderEmphasis <SPVariantThemeType>] [-HideTitleInHeader]
 [-HeaderBackgroundImageUrl <string>] [-HeaderBackgroundImageFocalX <double>]
 [-HeaderBackgroundImageFocalY <double>] [-LogoAlignment <LogoAlignment>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Through this cmdlet the various options offered through "Change the look" Header can be configured.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPWebHeader -HeaderBackgroundImageUrl "/sites/hrdepartment/siteassets/background.png" -HeaderLayout Extended
```

Sets the background image of the heading of the site to the provided image

### EXAMPLE 2

```powershell
Set-PnPWebHeader -HeaderEmphasis Strong
```

Sets the site to use a strong colored bar at the top of the site

### EXAMPLE 3

```powershell
Set-PnPWebHeader -LogoAlignment Middle
```

Sets the site title and logo to be displayed in the middle of the screen

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

### -HeaderBackgroundImageFocalX

When having HeaderLayout set to Extended and when providing a background image to show in the header through HeaderBackgroundImageUrl, this property allows for defining the X coordinate of the image how it should be shown.

```yaml
Type: Double
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

### -HeaderBackgroundImageFocalY

When having HeaderLayout set to Extended and when providing a background image to show in the header through HeaderBackgroundImageUrl, this property allows for defining the Y coordinate of the image how it should be shown.

```yaml
Type: Double
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

### -HeaderBackgroundImageUrl

Allows providing a server relative URL to an image that should be used as the background of the header of the site, i.e. /sites/hrdepartment/siteassets/background.png. HeaderLayout must be set to Extended for the image to show up. Provide "" or $null to remove the current background image.

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

### -HeaderEmphasis

Defines the tone of color used for the bar at shown at the top of the site under the site name and logo

```yaml
Type: SPVariantThemeType
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
- None
- Neutral
- Soft
- Strong
HelpMessage: ''
```

### -HeaderLayout

Defines how the header of the site should be layed out

```yaml
Type: HeaderLayoutType
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
- None
- Standard
- Compact
- Minimal
- Extended
HelpMessage: ''
```

### -HideTitleInHeader

Toggle the title visibility in the header.

Set -HideTitleInHeader:$false to show the header

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

### -LogoAlignment

Allows configuring the site title and logo to be shown on the left (default), in the middle or on the right.

```yaml
Type: LogoAlignment
DefaultValue: Left
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
- Left
- Middle
- Right
HelpMessage: ''
```

### -SiteLogoUrl

Sets the logo of the site shown at the top left to the provided server relative url, i.e. /sites/hrdepartment/siteassets/logo.png. Provide "" or $null to remove the logo.

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

### -SiteThumbnailUrl

Sets the thumbnail of the site shown at the top left to the provided server relative url, i.e. /sites/hrdepartment/siteassets/thumbnail.png. Provide "" or $null to remove the thumbnail.

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
