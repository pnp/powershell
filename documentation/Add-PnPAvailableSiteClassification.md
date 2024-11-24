---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPAvailableSiteClassification.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPAvailableSiteClassification
---

# Add-PnPAvailableSiteClassification

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Directory.ReadWrite.All

Adds one or more classic site classification values to the list of possible values.

## SYNTAX

### Default (Default)

```
Add-PnPAvailableSiteClassification
 -Classifications <System.Collections.Generic.List`1[System.String]> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add classic site classification values.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPAvailableSiteClassification -Classifications "Top Secret"
```

Adds the "Top Secret" classification to the already existing classification values.

### EXAMPLE 2

```powershell
Add-PnPAvailableSiteClassification -Classifications "Top Secret","HBI"
```

Adds the "Top Secret" and the "HBI" classifications to the already existing classification values.

## PARAMETERS

### -Classifications

Classic classifications values to add.

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
