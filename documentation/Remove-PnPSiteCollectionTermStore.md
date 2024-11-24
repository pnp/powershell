---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPSiteCollectionTermStore.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPSiteCollectionTermStore
---

# Remove-PnPSiteCollectionTermStore

## SYNOPSIS

Removes the site collection term store if it exists or else nothing will happen.

## SYNTAX

### Default (Default)

```
Remove-PnPSiteCollectionTermStore [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Removes the site collection scoped term store for the currently connected site collection. If it does not exist yet, it will not do anything.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPSiteCollectionTermStore
```

Removes the site collection term store.

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
