---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPAvailableSiteClassification.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPAvailableSiteClassification
---

# Get-PnPAvailableSiteClassification

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All

Returns the available classic Site Classifications for the tenant

## SYNTAX

### Default (Default)

```
Get-PnPAvailableSiteClassification [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows for retrieving the configuration of the classic site classifications configured within the tenant. For the new Microsoft Purview sensitivity labels, use [Get-PnPAvailableSensitivityLabel](Get-PnPAvailableSensitivityLabel.md) instead.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPAvailableSiteClassification
```

Returns the currently set site classifications for the tenant.

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
