---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPWebHeader.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPWebHeader
---

# Get-PnPWebHeader

## SYNOPSIS

Retrieves the current configuration regarding the "Change the look" Header of the current site

## SYNTAX

### Default (Default)

```
Get-PnPWebHeader [-SiteLogoUrl <string>] [-HeaderLayout <HeaderLayoutType>]
 [-HeaderEmphasis <SPVariantThemeType>] [-HideTitleInHeader] [-HeaderBackgroundImageUrl <string>]
 [-HeaderBackgroundImageFocalX <double>] [-HeaderBackgroundImageFocalY <double>]
 [-LogoAlignment <LogoAlignment>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Through this cmdlet the current configuration of the various options offered through "Change the look" Header can be retrieved.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPWebHeader
```

Retrieves all of the available configuration

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
