---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpunifiedgroupowners
schema: 2.0.0
title: Get-PnPUnifiedGroupOwners
---

# Get-PnPUnifiedGroupOwners

## SYNOPSIS
Gets owners of a particular Office 365 Group (aka Unified Group). Requires the Azure Active Directory application permissions 'Group.Read.All' and 'User.Read.All'.

## SYNTAX

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPUnifiedGroupOwners -Identity $groupId
```

Retrieves all the owners of a specific Office 365 Group based on its ID

### EXAMPLE 2
```powershell
Get-PnPUnifiedGroupOwners -Identity $group
```

Retrieves all the owners of a specific Office 365 Group based on the group's object instance

## PARAMETERS

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)