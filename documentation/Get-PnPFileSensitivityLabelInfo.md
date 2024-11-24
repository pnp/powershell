---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPFileSensitivityLabelInfo.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPFileSensitivityLabelInfo
---

# Get-PnPFileSensitivityLabelInfo

## SYNOPSIS

Retrieves the sensitivity label information for a file in SharePoint.

## SYNTAX

### Default (Default)

```
Get-PnPFileSensitivityLabelInfo -Url <String>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

The Get-PnPFileSensitivityLabelInfo cmdlet retrieves the sensitivity label information for a file in SharePoint. It takes a URL as input, decodes it, and specifically encodes the '+' character if it is part of the filename.

## EXAMPLES

### Example 1

This example retrieves the sensitivity label information for the file at the specified URL.

```powershell
Get-PnPFileSensitivityLabelInfo -Url "https://contoso.sharepoint.com/sites/Marketing/Shared Documents/Report.pdf"
```

This example retrieves the sensitivity label information for the file at the specified URL.

## PARAMETERS

### -Url

Specifies the URL of the file for which to retrieve the sensitivity label information.

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
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
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
