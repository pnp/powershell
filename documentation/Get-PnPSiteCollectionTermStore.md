---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteCollectionTermStore.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSiteCollectionTermStore
---

# Get-PnPSiteCollectionTermStore

## SYNOPSIS

Returns the site collection term store if it exists or else it will not return anything

## SYNTAX

### Default (Default)

```
Get-PnPSiteCollectionTermStore [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Returns the site collection scoped term store for the currently connected to site collection. If it does not exist yet, it will return a null value.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSiteCollectionTermStore
```

Returns the site collection term store.

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
