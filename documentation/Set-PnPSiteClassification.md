---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteClassification.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPSiteClassification
---

# Set-PnPSiteClassification

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.Read.All, Directory.ReadWrite.All (see description below)

Allows placing a classic site classification on the current site.

## SYNTAX

### Default (Default)

```
Set-PnPSiteClassification -Identity <String> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows for setting a classic site classification on the currently connected to site. If the site has a Microsoft 365 Group behind it, the classification will be placed on the Microsoft 365 Group and will require either Directory.Read.All or Directory.ReadWrite.All application permissions on Microsoft Graph. If it does not have a Microsoft 365 Group behind it, it will set the site classification on the SharePoint Online site and will not require Microsoft Graph permissions. Use [Get-PnPAvailableSiteClassification](Get-PnPAvailableSiteClassification.md) to get an overview of the available site classifications on the tenant. For the new Microsoft Purview sensitivity labels, use [Set-PnPSiteSensitivityLabel](Set-PnPSiteSensitivityLabel.md) instead.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPSiteClassification -Identity "LBI"
```

Sets the "LBI" site classification on the current site.

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

Specifies the name of the classification tag.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
