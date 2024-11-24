---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Enable-PnPSiteClassification.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Enable-PnPSiteClassification
---

# Enable-PnPSiteClassification

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Directory.ReadWrite.All

Enables Site Classifications for the tenant

## SYNTAX

### Default (Default)

```
Enable-PnPSiteClassification -Classifications <System.Collections.Generic.List`1[System.String]>
 -DefaultClassification <String> [-UsageGuidelinesUrl <String>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to enable site classifications for the tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Enable-PnPSiteClassification -Classifications "HBI","LBI","Top Secret" -DefaultClassification "LBI"
```

Enables Site Classifications for your tenant and provides three classification values. The default value will be set to "LBI"

### EXAMPLE 2

```powershell
Enable-PnPSiteClassification -Classifications "HBI","LBI","Top Secret" -UsageGuidelinesUrl https://aka.ms/m365pnp
```

Enables Site Classifications for your tenant and provides three classification values. The usage guidelines will be set to the specified URL.

## PARAMETERS

### -Classifications



```yaml
Type: System.Collections.Generic.List`1[System.String]
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

### -DefaultClassification



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

### -UsageGuidelinesUrl



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
