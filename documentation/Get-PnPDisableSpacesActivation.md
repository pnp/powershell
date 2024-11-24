---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPDisableSpacesActivation.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPDisableSpacesActivation
---

# Get-PnPDisableSpacesActivation

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Retrieves if SharePoint Spaces is disabled on the entire tenant

## SYNTAX

### Default (Default)

```
Get-PnPDisableSpacesActivation [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Retrieves if SharePoint Spaces is disabled on the entire tenant. At this point there is no API yet for retrieving the setting for a specific site, although you can set it for a specific site.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPDisableSpacesActivation
```

Returns if SharePoint Spaces is disabled on the tenant

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
