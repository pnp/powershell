---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPServiceHealthIssue.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPServiceHealthIssue
---

# Get-PnPServiceHealthIssue

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : ServiceHealth.Read.All

Gets service health issues of the Office 365 Services from the Microsoft Graph API

## SYNTAX

### Default (Default)

```
Get-PnPServiceHealthIssue [-Identity <Id>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve current service health issues of the Office 365 Services from the Microsoft Graph API.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPServiceHealthIssue
```

Retrieves all the available service health issues

### EXAMPLE 2

```powershell
Get-PnPServiceHealthIssue -Identity "EX123456"
```

Retrieves the details of the service health issue with the Id EX123456

## PARAMETERS

### -Identity



```yaml
Type: Identity
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
