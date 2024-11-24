---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Update-PnPAvailableSiteClassification.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Update-PnPAvailableSiteClassification
---

# Update-PnPAvailableSiteClassification

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Directory.ReadWrite.All

Updates available classic Site Classifications for the tenant

## SYNTAX

### Settings

```
Update-PnPAvailableSiteClassification -Settings <SiteClassificationsSettings>
```

### Specific

```
Update-PnPAvailableSiteClassification
 [-Classifications <System.Collections.Generic.List`1[System.String]>]
 [-DefaultClassification <String>] [-UsageGuidelinesUrl <String>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows for updating the configuration of the classic site classifications configured within the tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Update-PnPAvailableSiteClassification -Classifications "HBI","Top Secret"
```

Replaces the existing values of the site classification settings

### EXAMPLE 2

```powershell
Update-PnPAvailableSiteClassification -DefaultClassification "LBI"
```

Sets the default classification value to "LBI". This value needs to be present in the list of classification values.

### EXAMPLE 3

```powershell
Update-PnPAvailableSiteClassification -UsageGuidelinesUrl https://aka.ms/m365pnp
```

sets the usage guidelines URL to the specified URL

## PARAMETERS

### -Classifications

A list of classifications, separated by commas. E.g. "HBI","LBI","Top Secret"

```yaml
Type: System.Collections.Generic.List`1[System.String]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Specific
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DefaultClassification

The default classification to be used. The value needs to be present in the list of possible classifications

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Specific
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Settings

A settings object retrieved by Get-PnPSiteClassification

```yaml
Type: SiteClassificationsSettings
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Settings
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -UsageGuidelinesUrl

The UsageGuidelinesUrl. Set to "" to clear.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Specific
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
