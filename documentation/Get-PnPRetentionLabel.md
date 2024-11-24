---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPRetentionLabel.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPRetentionLabel
---

# Get-PnPRetentionLabel

## SYNOPSIS

Gets the Microsoft Purview retention labels that are available within the tenant

## SYNTAX

### Default (Default)

```
Get-PnPRetentionLabel [-Identity <Guid>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows retrieval of the available Microsoft Purview retention labels in the currently connected tenant. You can retrieve all the labels or a specific label.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPRetentionLabel
```

Returns all the Microsoft Purview retention labels that exist on the tenant

### EXAMPLE 3

```powershell
Get-PnPRetentionLabel -Identity 58f77809-9738-5080-90f1-gh7afeba2995
```

Returns a specific Microsoft Purview retention label by its id

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

### -Identity

The Id of the Microsoft Purview retention label to retrieve

```yaml
Type: Guid
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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/informationprotectionpolicy-list-labels)
