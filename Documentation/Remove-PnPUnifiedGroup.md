---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/remove-pnpunifiedgroup
schema: 2.0.0
title: Remove-PnPUnifiedGroup
---

# Remove-PnPUnifiedGroup

## SYNOPSIS
Removes one Office 365 Group (aka Unified Group). Requires the Azure Active Directory application permission 'Group.ReadWrite.All'.

## SYNTAX

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPUnifiedGroup -Identity $groupId
```

Removes an Office 365 Group based on its ID

### EXAMPLE 2
```powershell
Remove-PnPUnifiedGroup -Identity $group
```

Removes the provided Office 365 Group

### EXAMPLE 3
```powershell
Get-PnPUnifiedGroup | ? Visibility -eq "Public" | Remove-PnPUnifiedGroup
```

Removes all the public Office 365 Groups

## PARAMETERS

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)