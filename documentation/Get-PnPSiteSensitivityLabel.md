---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteSensitivityLabel.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSiteSensitivityLabel
---

# Get-PnPSiteSensitivityLabel

## SYNOPSIS

Gets the Microsoft Purview sensitivity label that is set on the connected to site

## SYNTAX

### Default (Default)

```
Get-PnPSiteSensitivityLabel [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows retrieval of the currently assigned Microsoft Purview sensitivity label to the currently connected to site.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSiteSensitivityLabel
```

Returns the Microsoft Purview sensitivity label set on the currently connected to site

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
- [Microsoft Docs documentation](https://learn.microsoft.com/sharepoint/dev/solution-guidance/modern-experience-site-classification#programmatically-read-the-classification-of-a-site)
