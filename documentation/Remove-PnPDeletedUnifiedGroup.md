---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-help.xml
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/remove-pnpdeletedunifiedgroup
schema: 2.0.0
title: Remove-PnPDeletedUnifiedGroup
---

# Remove-PnPDeletedUnifiedGroup

## SYNOPSIS
Permanently removes one deleted Office 365 Group (aka Unified Group)

## SYNTAX

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPDeletedUnifiedGroup -Identity 38b32e13-e900-4d95-b860-fb52bc07ca7f
```

Permanently removes a deleted Office 365 Group based on its ID

### EXAMPLE 2
```powershell
$group = Get-PnPDeletedUnifiedGroup -Identity 38b32e13-e900-4d95-b860-fb52bc07ca7f
Remove-PnPDeletedUnifiedGroup -Identity $group
```

Permanently removes the provided deleted Office 365 Group

## PARAMETERS

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)