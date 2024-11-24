---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPSiteCollectionTermStore.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPSiteCollectionTermStore
---

# New-PnPSiteCollectionTermStore

## SYNOPSIS

Creates the site collection term store if it doesn't exist yet or if it does it will return the already existing site collection term store

## SYNTAX

### Default (Default)

```
New-PnPSiteCollectionTermStore [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

The site collection scoped term store will be created if it does not exist yet. If it does already exist for the currently connected to site collection, it will return the existing instance.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPSiteCollectionTermStore
```

Returns the site collection term store by creating it if it doesn't exist yet or returning the existing instance if it does

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
