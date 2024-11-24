---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Reset-PnPRetentionLabel.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Reset-PnPRetentionLabel
---

# Reset-PnPRetentionLabel

## SYNOPSIS

Resets a retention label on the specified list or library to None, or resets a retention label on specified list items in a list or a library

## SYNTAX

### Reset on a list

```
Reset-PnPRetentionLabel [-List] <ListPipeBind> [-SyncToItems <Boolean>]
 [-Connection <PnPConnection>]
```

### Reset on items in bulk

```
Reset-PnPRetentionLabel [-List] <ListPipeBind> -ItemIds <List<Int32>> [-BatchSize <Int32>]
 [-Connection <PnPConnection>] [-Verbose]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Removes the retention label on a list or library and its items, or removes the retention label from specified items in a list or a library. Does not work for sensitivity labels.
When resetting retention label on specified items, cmdlet allows passing of unlimited number of items - items will be split and processed in batches (CSOM method SetComplianceTagOnBulkItems has a hard count limit on number of processed items in one go). If needed, batch size may be adjusted with BatchSize parameter.

## EXAMPLES

### EXAMPLE 1

```powershell
Reset-PnPRetentionLabel -List "Demo List"
```

This resets an O365 label on the specified list or library to None

### EXAMPLE 2

```powershell
Reset-PnPRetentionLabel -List "Demo List" -SyncToItems $true
```

This resets an O365 label on the specified list or library to None and resets the label on all the items in the list and library except Folders and where the label has been manually or previously automatically assigned

### EXAMPLE 3

```powershell
Set-PnPRetentionLabel -List "Demo List" -ItemIds @(1,2,3)
```

This clears a retention label from items with ids 1, 2 and 3 on a list "Demo List"

## PARAMETERS

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

