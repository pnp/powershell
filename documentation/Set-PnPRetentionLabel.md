---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPRetentionLabel.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPRetentionLabel
---

# Set-PnPRetentionLabel

## SYNOPSIS

Sets a retention label on the specified list or library, or on specified items within a list or library. Use Reset-PnPRetentionLabel to remove the label again.

## SYNTAX

### Set on a list

```
Set-PnPRetentionLabel [-List] <ListPipeBind> -Label <String> [-SyncToItems <Boolean>]
 [-BlockDeletion <Boolean>] [-BlockEdit <Boolean>] [-Connection <PnPConnection>]
```

### Set on items in bulk

```
Set-PnPRetentionLabel [-List] <ListPipeBind> -Label <String> -ItemIds <List<Int32>>
 [-BatchSize <Int32>] [-Connection <PnPConnection>] [-Verbose]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows setting a retention label on a list or library and its items, or sets the retention label for specified items in a list or a library. Does not work for sensitivity labels.
When setting retention label to specified items, cmdlet allows passing of unlimited number of items - items will be split and processed in batches (CSOM method SetComplianceTagOnBulkItems has a hard count limit on number of processed items in one go). If needed, batch size may be adjusted with BatchSize parameter.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPRetentionLabel -List "Demo List" -Label "Project Documentation"
```

This sets an O365 label on the specified list or library.

### EXAMPLE 2

```powershell
Set-PnPRetentionLabel -List "Demo List" -Label "Project Documentation" -SyncToItems $true
```

This sets an O365 label on the specified list or library and sets the label to all the items in the list and library as well.

### EXAMPLE 3

```powershell
Set-PnPRetentionLabel -List "Demo List" -ItemIds @(1,2,3) -Label "My demo label"
```

Sets "My demo label" retention label for items with ids 1, 2 and 3 on a list "Demo List".

## PARAMETERS

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

